using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class firsts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
    table: "Companies",
    columns: new[] { "Id", "Name", "LogoUrl" },
    values: new object[,]
    {
        { 1, "Poli Pers Journal", "https://polipersjournal.com/logoes.png" }
    });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[]
                {
        "Id", "CompanyId", "RequireEmailConfirmation", "RequirePhoneConfirmation",
        "RequireAdminApproval", "RequireTCApproval", "AllowUserRegistration",
        "AllowedLoginMethods", "CreatedAt", "UpdatedAt"
                },
                values: new object[,]
                {
        { 1, 1, true, true, true, true, true, (byte)1, DateTime.Parse("2025-03-05 15:29:26.3478912"), null }
                });


            migrationBuilder.InsertData(
    table: "AspNetUsers",
    columns: new[]
    {
        "Id", "Name", "Surname", "DateOfBirth", "ProfilePictureUrl", "Address",
        "TC", "CompanyId", "CityId", "DistrictId", "CreatedAt", "UpdatedAt",
        "Invalidated", "UserName", "NormalizedUserName", "Email",
        "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp",
        "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed",
        "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount"
    },
    values: new object[,]
    {
        {
            "792e3777-a5f2-4c7e-bb19-cbcec996ed80",
            "Devrim Mehmet",
            "Pattabanoğlu",
            null, // DateOfBirth
            null, // ProfilePictureUrl
            null, // Address
            "481a5ae21f4a3797a0a30945862870ae59394fec3b1722e687e7800c76362694",
            1, // CompanyId
            null, // CityId
            null, // DistrictId
            DateTime.Parse("2025-03-15 19:50:17.0850530"), // CreatedAt
            DateTime.Parse("2025-03-15 19:50:17.0850546"), // UpdatedAt
            (byte)1, // Invalidated
            "devrimmehmet",
            "DEVRIMMEHMET",
            "devrimmehmet@gmail.com",
            "DEVRIMMEHMET@GMAIL.COM",
            true, // EmailConfirmed
            "AQAAAAIAAYagAAAAEKXnEcamtaYm+3XWvyjAWsxYw0mEUUePDpN2JXthvlrRvG3P/8zrNH63LDmgHFskzA==",  //12345Poli*
            "R3V5WEDJMWYQNAL7W3UMIQRV7F2STDHZ",
            "9c4b1d9b-e66e-4c8e-a626-f18bc45a9843",
            "905438194976",
            true, // PhoneNumberConfirmed
            false, // TwoFactorEnabled
            null, // LockoutEnd
            true, // LockoutEnabled
            0 // AccessFailedCount
        }


    });

            migrationBuilder.InsertData(
    table: "AspNetRoles",
    columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
    values: new object[,]
    {
        {
            "1a1a1a1a-a1a1-4a1a-a1a1-a1a1a1a1a1a1", // Admin Rolü için ID (GUID gibi benzersiz bir ID ver)
            "Admin",
            "ADMIN",
            "b3f1de1a-815e-42b8-9b56-5b4d4e283163" // Farklı bir GUID veya rastgele string kullanılabilir
        },
        {
            "2b2b2b2b-b2b2-4b2b-b2b2-b2b2b2b2b2b2", // User Rolü için ID (GUID gibi benzersiz bir ID ver)
            "User",
            "USER",
            "c2e2e6f2-f8d9-49b8-a76e-0b7a3b7dc654" // Farklı bir GUID veya rastgele string kullanılabilir
        }
    });

            migrationBuilder.InsertData(
    table: "AspNetUserRoles",
    columns: new[] { "UserId", "RoleId" },
    values: new object[,]
    {
        {
            "792e3777-a5f2-4c7e-bb19-cbcec996ed80", // Kullanıcı ID (Daha önce eklediğin Admin kullanıcısının ID'si)
            "1a1a1a1a-a1a1-4a1a-a1a1-a1a1a1a1a1a1"  // Admin Rolü ID'si
        }
    });


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
