# PoliPersJournal Backend Yapisi

Bu proje `.NET 10 Web API` ile gelistirilmis olup `DDD (Domain-Driven Design)` ve `Clean Architecture` prensiplerini temel alir. Katmanlar birbirinden ayridir ve her katmanin sorumlulugu nettir.

Guncel teknik durum:

- backend `net10.0` uzerinde calisir
- veri tabani olarak `PostgreSQL` kullanilir
- EF Core provider olarak `Npgsql.EntityFrameworkCore.PostgreSQL` kullanilir
- migration'lar startup sirasinda otomatik uygulanir
- `.env` dosyasindan environment variable okunabilir
- static file root kalici mount dizinine yonlenebilir
- kok dizindeki `Dockerfile` ile Coolify uyumlu deploy alinabilir

## Katmanlar

### [API](API/Readme.md)

Dis dunyaya acilan katmandir.

Bu katmanda:

- controller'lar
- middleware'ler
- JWT authentication konfigurasyonu
- CORS ayarlari
- Swagger konfigurasyonu
- uygulama startup akisi

yer alir.

Ek olarak `Program.cs` icinde:

- `Application` ve `Infrastructure` servis kayitlari yapilir
- veritabani migration'i acilista uygulanir
- static file ve pipeline yonetimi yapilir
- `APP_WWWROOT_PATH` ile persistent `wwwroot` yolu belirlenebilir

### [Application](Application/Readme.md)

Uygulamanin is mantigini barindirir.

Bu katmanda:

- servisler
- DTO'lar
- servis arayuzleri
- validation siniflari

yer alir.

`Application` katmani domain kurallarini kullanir ancak veri erisim detaylarini bilmez.

### [Domain](Domain/Readme.md)

Sistemin cekirdek modelidir.

Bu katmanda:

- entity'ler
- enum'lar
- repository arayuzleri
- domain'e ait temel kurallar

yer alir.

Bu katman altyapi detaylarindan bagimsiz tutulur.

### [Infrastructure](Infrastructure/Readme.md)

Dis bagimliliklarin somutlandigi katmandir.

Bu katmanda:

- `AppDbContext`
- EF Core migration'lari
- PostgreSQL baglantisi
- repository implementasyonlari
- JWT ve sifreleme servisleri
- dosya servisi
- design-time `DbContextFactory`
- veri tasima icin yardimci migrator araclari

yer alir.

Bu gecisle birlikte:

- `UseSqlServer` kaldirildi
- `UseNpgsql` eklendi
- MSSQL migration dosyalari temizlendi
- PostgreSQL icin yeni migration seti olusturuldu
- local MSSQL verisini PostgreSQL'e tasimak icin script ve arac eklendi

### [Shared](Shared/Readme.md)

Katmanlar arasinda ortak kullanilan yapilar burada tutulur.

Bu katmanda:

- ortak response modelleri
- exception siniflari
- yardimci yapilar

yer alir.

## Calisma ve Deploy Ozeti

Lokal gelistirme icin:

- `.NET 10 SDK`
- `PostgreSQL`
- SQL Server erisimi

gereklidir.

Deploy tarafinda:

- kok dizindeki `Dockerfile` multi-stage build kullanir
- uygulama container icinde `8080` portundan ayaga kalkar
- static file dizini `/data/polipersjournal` mount noktasina yonlenebilir
- Coolify tarafinda `ConnectionStrings__DefaultConnection` ve `JwtSettings__*` env degiskenleri verilmelidir
- istenirse `.env` anahtarlari ayni isimlerle kullanilabilir

## Dokumantasyon Kurali

Projeye yeni klasor, servis, provider ya da deploy adimi eklendiginde:

- `README.md`
- `KlasorYapisi.md`
- ilgili katmanin kendi `Readme.md` dosyasi

birlikte guncellenmelidir.
