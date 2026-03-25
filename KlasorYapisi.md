Fupitech
│── Fupitech.sln                                                     --> Solution dosyasi
│── README.md                                                        --> Genel kurulum ve deploy dokumani
│── PoliPersJournal-Backend-Yapisi.md                                --> Katmanlarin genel ozeti
│── KlasorYapisi.md                                                  --> Repo klasor envanteri
│── Dockerfile                                                       --> Coolify ve container deploy tanimi
│── .dockerignore                                                    --> Docker build ignore listesi
│── .env.example                                                     --> Ornek environment variable dosyasi
│
│── API                                                              --> ASP.NET Core Web API katmani
│   ├── API.csproj                                                   --> net10.0 hedefli API projesi
│   ├── Program.cs                                                   --> Uygulama giris noktasi, DI, auth, swagger, migration, static file root
│   ├── appsettings.json                                             --> Varsayilan PostgreSQL ve JWT ayarlari
│   ├── appsettings.Development.json                                 --> Development ortam ayarlari
│   ├── API.http                                                     --> HTTP test istekleri
│   ├── Controllers                                                  --> API endpoint'leri
│   ├── Middlewares                                                  --> Exception handling ve benzeri middleware'ler
│   ├── Properties
│   │   └── launchSettings.json                                      --> Lokal calisma profilleri
│   └── wwwroot                                                      --> Lokal fallback static file klasoru
│
│── Application                                                      --> Uygulama is mantigi katmani
│   ├── Application.csproj                                           --> net10.0 hedefli application projesi
│   ├── DTOs                                                         --> Request/response ve view model nesneleri
│   ├── Extensions
│   │   └── ApplicationServiceCollectionExtensions.cs                --> Application servis kayitlari
│   ├── Interfaces                                                   --> Servis arayuzleri
│   ├── Services                                                     --> Is mantigi implementasyonlari
│   └── Validators                                                   --> FluentValidation siniflari
│
│── Domain                                                           --> Cekirdek domain katmani
│   ├── Domain.csproj                                                --> net10.0 hedefli domain projesi
│   ├── Entities                                                     --> Veritabani entity'leri
│   ├── Enums                                                        --> Enum tanimlari
│   ├── Interfaces                                                   --> Repository ve servis bagimlilik arayuzleri
│   ├── Aggregates                                                   --> Aggregate root alanlari
│   ├── Events                                                       --> Domain event alanlari
│   └── ValueObjects                                                 --> Value object alanlari
│
│── Infrastructure                                                   --> Veri erisimi ve dis bagimlilik katmani
│   ├── Infrastructure.csproj                                        --> EF Core, Npgsql, Identity ve JWT bagimliliklari
│   ├── Extensions
│   │   └── InfrastructureServiceCollectionExtensions.cs             --> UseNpgsql ve repository DI kayitlari
│   ├── Migrations                                                   --> PostgreSQL EF Core migration dosyalari
│   ├── Persistence
│   │   ├── AppDbContext.cs                                          --> EF Core DbContext
│   │   └── AppDbContextFactory.cs                                   --> Design-time DbContext factory
│   ├── Repositories                                                 --> Repository implementasyonlari
│   ├── Security                                                     --> JWT ve sifreleme servisleri
│   ├── Services                                                     --> Dosya ve benzeri altyapi servisleri
│   ├── Caching                                                      --> Olasi cache bilesenleri icin alan
│   ├── Identity                                                     --> Identity ile ilgili altyapi alanlari
│   └── Logging                                                      --> Logging ile ilgili altyapi alanlari
│
│── Shared                                                           --> Ortak response ve exception yapilari
│   ├── Shared.csproj                                                --> net10.0 hedefli shared projesi
│   ├── Exceptions                                                   --> Ortak exception siniflari
│   ├── Responses                                                    --> Standart API response modelleri
│   ├── Extensions                                                   --> Ortak extension alanlari
│   ├── Constants                                                    --> Sabit tanimlari icin alan
│   └── Utilities                                                    --> Yardimci siniflar icin alan
│
│── scripts                                                          --> Yardimci otomasyon scriptleri
│   └── Sync-MssqlToPostgres.ps1                                     --> MSSQL'den PostgreSQL'e veri tasima scripti
│
│── tools                                                            --> Yardimci arac projeleri
│   └── DatabaseMigrator                                             --> MSSQL -> PostgreSQL veri migrator araci
│
└── .artifacts                                                       --> Lokal publish ciktilari gibi gecici build artefaktlari

## Kullanilan Temel Paketler

### API

- `FluentValidation.AspNetCore`
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.EntityFrameworkCore.Tools`
- `Swashbuckle.AspNetCore`
- `Swashbuckle.AspNetCore.Annotations`

### Application

- `FluentValidation`

### Domain

- `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- `Microsoft.AspNetCore.Http`

### Infrastructure

- `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.EntityFrameworkCore.Tools`
- `Npgsql.EntityFrameworkCore.PostgreSQL`
- `System.IdentityModel.Tokens.Jwt`

### Shared

- `Microsoft.Extensions.Configuration`

## Guncel Teknik Notlar

- Tum projeler `net10.0` hedef framework'u kullanir.
- Veritabani provider'i `PostgreSQL` olup `UseNpgsql` ile baglanir.
- Eski MSSQL migration seti kaldirilmis, PostgreSQL migration seti yeniden uretilmistir.
- Uygulama startup sirasinda bekleyen migration'lari otomatik uygular.
- `.env` dosyasi ile configuration yuklenebilir.
- Kalici static file dizini olarak `/data/polipersjournal` kullanilabilir.
- `scripts/Sync-MssqlToPostgres.ps1` ile local MSSQL verisi PostgreSQL'e tasinabilir.
- Kok dizindeki `Dockerfile` Coolify uzerinden publish almaya uygundur.
