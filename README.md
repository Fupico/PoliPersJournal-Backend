# PoliPersJournal Backend

PoliPersJournal backend, cok dilli icerik yonetimi icin gelistirilmis bir `ASP.NET Core Web API` projesidir. Cozum yapisi `DDD` ve `Clean Architecture` katmanlarina ayrilir.

Bu repo artik:

- `.NET 10` uzerinde calisir
- `PostgreSQL` kullanir
- EF Core migration'larini startup sirasinda otomatik uygular
- `.env` dosyasindan environment variable yukleyebilir
- kalici mount edilen dizini `wwwroot` olarak kullanabilir
- kok dizindeki `Dockerfile` ile Coolify uzerinden publish edilebilir

## Teknoloji Yigini

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core 10
- PostgreSQL
- ASP.NET Core Identity
- JWT Authentication
- Swagger / Swashbuckle

## Proje Katmanlari

| Katman | Aciklama |
| --- | --- |
| `API` | Controller'lar, middleware'ler, uygulama giris noktasi |
| `Application` | DTO'lar, servisler ve uygulama is kurallari |
| `Domain` | Entity'ler, enum'lar ve arayuzler |
| `Infrastructure` | EF Core, repository'ler, guvenlik ve dis bagimliliklar |
| `Shared` | Ortak response ve exception yapilari |

Detayli notlar icin [PoliPersJournal-Backend-Yapisi.md](PoliPersJournal-Backend-Yapisi.md) dosyasina bakabilirsiniz.

## Gereksinimler

- `.NET SDK 10`
- `PostgreSQL 16+` tavsiye edilir
- Lokal veri tasima icin Windows uzerinde erisilebilir `SQL Server`

## Konfigurasyon

Varsayilan ayarlar [API/appsettings.json](/c:/Github/Organizations/Fupico/PoliPersJournal-Backend/API/appsettings.json) icine eklendi.

Onemli ayarlar:

- `ConnectionStrings__DefaultConnection`
- `MSSQL_SOURCE_CONNECTION_STRING`
- `MSSQL_SOURCE_SCHEMA`
- `APP_WWWROOT_PATH`
- `JwtSettings__Secret`
- `JwtSettings__Issuer`
- `JwtSettings__Audience`
- `JwtSettings__TokenExpirationInMinutes`

Repo kokunde `.env` kullanabilirsiniz. Ornek anahtarlar [ .env.example ](/c:/Github/Organizations/Fupico/PoliPersJournal-Backend/.env.example) dosyasindadir.

Varsayilan PostgreSQL connection string formati:

```text
Host=localhost;Port=5432;Database=polipersjournal;Username=postgres;Password=postgres
```

## Lokal Calistirma

1. Bagimliliklari yukleyin:

```bash
dotnet restore
```

2. Gerekirse veritabanini migration ile olusturun:

```bash
dotnet ef database update --project Infrastructure/Infrastructure.csproj --startup-project API/API.csproj
```

3. Uygulamayi calistirin:

```bash
dotnet run --project API/API.csproj
```

Varsayilan development profili ile Swagger su adreste acilir:

```text
https://localhost:1923/swagger
```

Not:

- Uygulama acilisinda `Database.Migrate()` calistigi icin bekleyen migration'lar otomatik uygulanir.
- Production ortaminda dogru PostgreSQL baglanti bilgisini environment variable olarak vermeniz gerekir.
- Static file root varsayilan olarak Linux'ta `/data/polipersjournal`, Windows'ta `data/polipersjournal` altina yonlenir.

## PostgreSQL Gecisi

MSSQL bagimliligi kaldirildi ve EF provider olarak `Npgsql.EntityFrameworkCore.PostgreSQL` kullaniliyor.

Yapilan degisiklikler:

- tum projeler `net10.0` hedef framework'una tasindi
- `UseSqlServer` yerine `UseNpgsql` kullanildi
- SQL Server migration seti silindi
- PostgreSQL icin yeni baslangic migration'i olusturuldu
- `AspNetUsers.TC` kolonu gercek veri boyutuna gore genisletildi
- SQL Server'daki mevcut veriyi PostgreSQL'e tasimak icin migrator araci eklendi

Yeni migration dosyalari [Infrastructure/Migrations](/c:/Github/Organizations/Fupico/PoliPersJournal-Backend/Infrastructure/Migrations) altindadir.

## MSSQL -> PostgreSQL Veri Tasima

Repo icinde iki yardimci bileşen bulunur:

- [scripts/Sync-MssqlToPostgres.ps1](/c:/Github/Organizations/Fupico/PoliPersJournal-Backend/scripts/Sync-MssqlToPostgres.ps1)
- [tools/DatabaseMigrator/Program.cs](/c:/Github/Organizations/Fupico/PoliPersJournal-Backend/tools/DatabaseMigrator/Program.cs)

Veri tasimak icin:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/Sync-MssqlToPostgres.ps1
```

Bu script:

- hedef PostgreSQL'e migration uygular
- local MSSQL `PoliPersJournal` verisini PostgreSQL'e tasir
- repo kokundeki `.env` ayarlarini kullanir

## Docker ve Coolify

Kok dizinde Coolify uyumlu bir [Dockerfile](/c:/Github/Organizations/Fupico/PoliPersJournal-Backend/Dockerfile) bulunuyor.

Container davranisi:

- multi-stage build kullanir
- `API/API.csproj` publish edilir
- uygulama `8080` portundan ayaga kalkar
- `ASPNETCORE_URLS=http://+:8080` ile calisir
- `APP_WWWROOT_PATH=/data/polipersjournal` ile kalici static file root kullanir
- `/data/polipersjournal` volume olarak mount edilmeye uygundur

Ornek `docker build` ve `docker run`:

```bash
docker build -t polipersjournal-backend .

docker run --rm -p 8080:8080 ^
  -e ASPNETCORE_ENVIRONMENT=Production ^
  -e ConnectionStrings__DefaultConnection="Host=host.docker.internal;Port=5432;Database=polipersjournal;Username=postgres;Password=postgres" ^
  -e APP_WWWROOT_PATH="/data/polipersjournal" ^
  -e JwtSettings__Secret="change-this-secret-to-a-long-random-value" ^
  -e JwtSettings__Issuer="PoliPersJournal" ^
  -e JwtSettings__Audience="PoliPersJournal.Client" ^
  -v polipersjournal_data:/data/polipersjournal ^
  polipersjournal-backend
```

Coolify icin:

- build type olarak `Dockerfile` secin
- repository root'u build context olarak kullanin
- container port olarak `8080` tanimlayin
- persistent storage icin `Directory Mount` tanimlayip container path olarak `/data/polipersjournal` verin
- PostgreSQL servisini ayri ekleyip `ConnectionStrings__DefaultConnection` env variable'ini verin
- JWT ayarlarini env variable olarak tanimlayin

## Kimlik Dogrulama

JWT tabanli kimlik dogrulama yapisi aktif. Swagger uzerinden login olup token alip yetkili endpoint'leri test edebilirsiniz.

## Lisans

Bu proje `GNU GPL v3.0` lisansi ile dagitilir.
