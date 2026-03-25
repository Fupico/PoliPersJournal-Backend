using Microsoft.Data.SqlClient;
using Npgsql;
using System.Data;

LoadDotEnv();

var sourceConnectionString = Environment.GetEnvironmentVariable("MSSQL_SOURCE_CONNECTION_STRING")
    ?? "Server=localhost;Database=PoliPersJournal;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False";
var sourceSchemaOverride = Environment.GetEnvironmentVariable("MSSQL_SOURCE_SCHEMA");

var targetConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
    ?? throw new InvalidOperationException("ConnectionStrings__DefaultConnection is not configured.");

await using var sourceConnection = new SqlConnection(sourceConnectionString);
await using var targetConnection = new NpgsqlConnection(targetConnectionString);

await sourceConnection.OpenAsync();
await targetConnection.OpenAsync();

var sourceTables = await GetSourceTablesAsync(sourceConnection, sourceSchemaOverride);
var targetTables = await GetTargetTablesAsync(targetConnection);
var dependencyOrder = await GetDependencyOrderAsync(targetConnection, targetTables);
var tableMappings = GetCommonTables(sourceTables, dependencyOrder);

if (tableMappings.Count == 0)
{
    Console.WriteLine("No common tables were found between MSSQL and PostgreSQL.");
    return;
}

Console.WriteLine($"Found {tableMappings.Count} tables to migrate.");

await TruncateTargetTablesAsync(targetConnection, tableMappings.Select(mapping => mapping.TargetTable).ToList());

foreach (var tableMapping in tableMappings)
{
    await CopyTableAsync(sourceConnection, targetConnection, tableMapping);
    await SyncIdentitySequenceAsync(targetConnection, tableMapping.TargetTable);
}

Console.WriteLine("MSSQL -> PostgreSQL migration completed.");

static void LoadDotEnv()
{
    foreach (var candidate in GetEnvCandidates())
    {
        if (!File.Exists(candidate))
        {
            continue;
        }

        foreach (var rawLine in File.ReadAllLines(candidate))
        {
            var line = rawLine.Trim();
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
            {
                continue;
            }

            var separatorIndex = line.IndexOf('=');
            if (separatorIndex <= 0)
            {
                continue;
            }

            var key = line[..separatorIndex].Trim();
            var value = line[(separatorIndex + 1)..].Trim().Trim('"');

            if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(key)))
            {
                Environment.SetEnvironmentVariable(key, value);
            }
        }

        break;
    }
}

static IEnumerable<string> GetEnvCandidates()
{
    var currentDirectory = Directory.GetCurrentDirectory();
    yield return Path.Combine(currentDirectory, ".env");

    var parent = Directory.GetParent(currentDirectory);
    while (parent is not null)
    {
        yield return Path.Combine(parent.FullName, ".env");
        parent = parent.Parent;
    }
}

static async Task<Dictionary<string, string>> GetSourceTablesAsync(SqlConnection sourceConnection, string? sourceSchemaOverride)
{
    const string sourceTablesSql = """
        SELECT TABLE_SCHEMA, TABLE_NAME
        FROM INFORMATION_SCHEMA.TABLES
        WHERE TABLE_TYPE = 'BASE TABLE'
          AND TABLE_SCHEMA NOT IN ('sys', 'INFORMATION_SCHEMA')
          AND (@schemaOverride IS NULL OR TABLE_SCHEMA = @schemaOverride)
        ORDER BY TABLE_SCHEMA, TABLE_NAME;
        """;

    var sourceTables = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    await using var sourceCommand = new SqlCommand(sourceTablesSql, sourceConnection);
    sourceCommand.Parameters.AddWithValue("@schemaOverride", (object?)sourceSchemaOverride ?? DBNull.Value);

    await using var reader = await sourceCommand.ExecuteReaderAsync();
    while (await reader.ReadAsync())
    {
        var schemaName = reader.GetString(0);
        var tableName = reader.GetString(1);

        if (!sourceTables.ContainsKey(tableName))
        {
            sourceTables[tableName] = schemaName;
        }
    }

    return sourceTables;
}

static async Task<List<string>> GetTargetTablesAsync(NpgsqlConnection targetConnection)
{
    const string targetTablesSql = """
        SELECT table_name
        FROM information_schema.tables
        WHERE table_schema = 'public'
          AND table_type = 'BASE TABLE'
          AND table_name <> '__EFMigrationsHistory'
        ORDER BY table_name;
        """;

    var targetTables = new List<string>();
    await using var targetCommand = new NpgsqlCommand(targetTablesSql, targetConnection);
    await using var reader = await targetCommand.ExecuteReaderAsync();

    while (await reader.ReadAsync())
    {
        targetTables.Add(reader.GetString(0));
    }

    return targetTables;
}

static List<TableMapping> GetCommonTables(IReadOnlyDictionary<string, string> sourceTables, IReadOnlyCollection<string> targetTables)
{
    var mappings = new List<TableMapping>();

    foreach (var targetTable in targetTables)
    {
        if (sourceTables.TryGetValue(targetTable, out var sourceSchema))
        {
            mappings.Add(new TableMapping(sourceSchema, targetTable, targetTable));
        }
    }

    return mappings;
}

static async Task<List<string>> GetDependencyOrderAsync(NpgsqlConnection targetConnection, IReadOnlyCollection<string> targetTables)
{
    const string dependencySql = """
        SELECT tc.table_name AS child_table, ccu.table_name AS parent_table
        FROM information_schema.table_constraints tc
        INNER JOIN information_schema.constraint_column_usage ccu
            ON ccu.constraint_name = tc.constraint_name
           AND ccu.constraint_schema = tc.constraint_schema
        WHERE tc.table_schema = 'public'
          AND tc.constraint_type = 'FOREIGN KEY';
        """;

    var targetTableSet = new HashSet<string>(targetTables, StringComparer.OrdinalIgnoreCase);
    var dependencies = targetTables.ToDictionary(
        table => table,
        _ => new HashSet<string>(StringComparer.OrdinalIgnoreCase),
        StringComparer.OrdinalIgnoreCase);

    await using var dependencyCommand = new NpgsqlCommand(dependencySql, targetConnection);
    await using var reader = await dependencyCommand.ExecuteReaderAsync();

    while (await reader.ReadAsync())
    {
        var childTable = reader.GetString(0);
        var parentTable = reader.GetString(1);

        if (!targetTableSet.Contains(childTable) || !targetTableSet.Contains(parentTable))
        {
            continue;
        }

        if (string.Equals(childTable, parentTable, StringComparison.OrdinalIgnoreCase))
        {
            continue;
        }

        dependencies[childTable].Add(parentTable);
    }

    var orderedTables = new List<string>();
    var queue = new Queue<string>(dependencies.Where(item => item.Value.Count == 0).Select(item => item.Key).Order(StringComparer.OrdinalIgnoreCase));

    while (queue.Count > 0)
    {
        var table = queue.Dequeue();
        orderedTables.Add(table);

        foreach (var dependency in dependencies)
        {
            if (!dependency.Value.Remove(table) || dependency.Value.Count != 0)
            {
                continue;
            }

            if (!orderedTables.Contains(dependency.Key, StringComparer.OrdinalIgnoreCase) &&
                !queue.Contains(dependency.Key, StringComparer.OrdinalIgnoreCase))
            {
                queue.Enqueue(dependency.Key);
            }
        }
    }

    foreach (var table in targetTables)
    {
        if (!orderedTables.Contains(table, StringComparer.OrdinalIgnoreCase))
        {
            orderedTables.Add(table);
        }
    }

    return orderedTables;
}

static async Task TruncateTargetTablesAsync(NpgsqlConnection targetConnection, IReadOnlyCollection<string> tableNames)
{
    var quotedTables = string.Join(", ", tableNames.Select(tableName => $"public.\"{tableName}\""));
    var truncateSql = $"TRUNCATE TABLE {quotedTables} RESTART IDENTITY CASCADE;";

    await using var truncateCommand = new NpgsqlCommand(truncateSql, targetConnection)
    {
        CommandTimeout = 0
    };

    await truncateCommand.ExecuteNonQueryAsync();
}

static async Task CopyTableAsync(SqlConnection sourceConnection, NpgsqlConnection targetConnection, TableMapping tableMapping)
{
    var targetColumns = await GetTargetColumnsAsync(targetConnection, tableMapping.TargetTable);
    if (targetColumns.Count == 0)
    {
        Console.WriteLine($"Skipping {tableMapping.TargetTable}: no target columns found.");
        return;
    }

    var sourceSql = BuildSourceSelectSql(tableMapping);
    await using var sourceCommand = new SqlCommand(sourceSql, sourceConnection)
    {
        CommandTimeout = 0
    };

    await using var reader = await sourceCommand.ExecuteReaderAsync(CommandBehavior.SequentialAccess);
    var sourceSchema = reader.GetColumnSchema();

    var columns = sourceSchema
        .Where(column => column.ColumnName is not null && targetColumns.ContainsKey(column.ColumnName))
        .Select(column => new
        {
            SourceName = column.ColumnName!,
            TargetType = targetColumns[column.ColumnName!]
        })
        .ToList();

    if (columns.Count == 0)
    {
        Console.WriteLine($"Skipping {tableMapping.TargetTable}: no common columns found.");
        return;
    }

    var columnList = string.Join(", ", columns.Select(column => $"\"{column.SourceName}\""));
    var parameterList = string.Join(", ", columns.Select((_, index) => $"@p{index}"));
    var insertSql = $"INSERT INTO public.\"{tableMapping.TargetTable}\" ({columnList}) VALUES ({parameterList});";

    await using var transaction = await targetConnection.BeginTransactionAsync();
    var rowCount = 0;

    while (await reader.ReadAsync())
    {
        await using var insertCommand = new NpgsqlCommand(insertSql, targetConnection, transaction)
        {
            CommandTimeout = 0
        };

        for (var index = 0; index < columns.Count; index++)
        {
            var value = reader[columns[index].SourceName];
            insertCommand.Parameters.AddWithValue($"p{index}", NormalizeValue(value, columns[index].TargetType));
        }

        await insertCommand.ExecuteNonQueryAsync();
        rowCount++;
    }

    await transaction.CommitAsync();
    Console.WriteLine($"{tableMapping.TargetTable}: {rowCount} rows copied.");
}

static string BuildSourceSelectSql(TableMapping tableMapping)
{
    if (string.Equals(tableMapping.SourceTable, "Comments", StringComparison.OrdinalIgnoreCase))
    {
        return $"SELECT * FROM [{tableMapping.SourceSchema}].[{tableMapping.SourceTable}] ORDER BY CASE WHEN [ParentCommentId] IS NULL THEN 0 ELSE 1 END, [ParentCommentId], [Id]";
    }

    return $"SELECT * FROM [{tableMapping.SourceSchema}].[{tableMapping.SourceTable}]";
}

static object NormalizeValue(object value, string targetType)
{
    if (value == DBNull.Value)
    {
        return DBNull.Value;
    }

    if (value is byte byteValue && targetType is "smallint" or "integer" or "bigint")
    {
        return Convert.ToInt16(byteValue);
    }

    if (value is DateTime dateTime && targetType == "timestamp with time zone")
    {
        return dateTime.Kind == DateTimeKind.Utc
            ? dateTime
            : DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
    }

    return value;
}

static async Task<Dictionary<string, string>> GetTargetColumnsAsync(NpgsqlConnection targetConnection, string tableName)
{
    const string sql = """
        SELECT column_name, data_type
        FROM information_schema.columns
        WHERE table_schema = 'public' AND table_name = @tableName
        ORDER BY ordinal_position;
        """;

    var columns = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    await using var command = new NpgsqlCommand(sql, targetConnection);
    command.Parameters.AddWithValue("tableName", tableName);

    await using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync())
    {
        columns[reader.GetString(0)] = reader.GetString(1);
    }

    return columns;
}

static async Task SyncIdentitySequenceAsync(NpgsqlConnection targetConnection, string tableName)
{
    const string identitySql = """
        SELECT column_name
        FROM information_schema.columns
        WHERE table_schema = 'public'
          AND table_name = @tableName
          AND is_identity = 'YES';
        """;

    string? identityColumn = null;

    await using (var identityCommand = new NpgsqlCommand(identitySql, targetConnection))
    {
        identityCommand.Parameters.AddWithValue("tableName", tableName);
        identityColumn = (string?)await identityCommand.ExecuteScalarAsync();
    }

    if (string.IsNullOrWhiteSpace(identityColumn))
    {
        return;
    }

    var syncSql = $"""
        SELECT setval(
            pg_get_serial_sequence('public."{tableName}"', '{identityColumn}'),
            COALESCE((SELECT MAX("{identityColumn}") FROM public."{tableName}"), 1),
            (SELECT COUNT(*) > 0 FROM public."{tableName}")
        );
        """;

    await using var syncCommand = new NpgsqlCommand(syncSql, targetConnection);
    await syncCommand.ExecuteNonQueryAsync();
}

internal sealed record TableMapping(string SourceSchema, string SourceTable, string TargetTable);
