
Fupitech
│── 📂 Solution Items               									--> 
│	├── Readme.md        												--> Proje İle ilgili Notlar genel Readme.md dosyası
│── 📂 API               												--> Web API katmanı
│	├── Dependencies       												--> Bağımlılıklar (NuGet paketleri)
│		├── Packages       												--> Kütüphaneler
│			├── FluentValidation.AspNetCore	(11.3.0)					--> 
│			├── Microsoft.AspNetCore.Authentication.JwtBearer (9.0.2)	--> 
│			├── Microsoft.AspNetCore.Diagnostics (2.3.0)				--> 
│			├── Microsoft.AspNetCore.Mvc.Core (2.3.0)					--> 
│			├── Microsoft.AspNetCore.OpenApi (9.0.2)					--> 
│			├── Microsoft.EntityFrameworkCore (9.0.2)					--> 
│			├── Microsoft.EntityFrameworkCore.Design (9.0.2)			--> 
│			├── Microsoft.EntityFrameworkCore.SqlServer (9.0.2)			--> 
│			├── Microsoft.Extensions.Configuration (9.0.2)				--> 
│			├── Microsoft.Extensions.DependencyInjection (9.0.2)		--> 
│			├── Swashbuckle.AspNetCore (7.3.1)							--> 
│		├── Projects       												--> 
│			├── Application       										--> 
│			├── Infrastructure       									--> 
│			├── Shared       											--> 
│	├── Properties       												--> 
│		├── launchSettings.json      									--> 
│	├── Controllers      												--> API uç noktaları
│		├── StudentController.cs										--> 
│	├── Filters          												--> Validation ve Authorization filtreleri
│	├── Middlewares      												--> Hata yönetimi ve diğer middleware'ler
│		├── ExceptionMiddleware.cs										--> 
│	├── API.http         												--> 
│	├── appsettings.json 												--> Uygulama ayarları
│		├── appsettings.Development.json								--> 
│	├── Program.cs       												--> API'nin giriş noktası
│	├── Readme.md        												--> Api KATMANI İLE İLGİLİ NOTLAR
│
│── 📂 Application       												--> İş mantığını içeren katman
│	├── Dependencies       												--> Bağımlılıklar (NuGet paketleri)
│		├── Packages       												--> Kütüphaneler
│			├── FluentValidation	(11.11.0)							--> 
│			├── Microsoft.Extensions.DependencyInjection (9.0.2)		--> 
│		├── Projects       												--> 
│			├── Domain       											--> 
│			├── Infrastructure       									--> 
│			├── Shared       											--> 
│	├── DTOs            												--> Veri transfer objeleri
│		├── Student            											--> 
│			├── StudentDto.cs											--> 
│	├── Extensions      												--> Application için Dependency Injection helper'ları
│		├── ApplicationServiceCollectionExtensions.cs					--> 
│	├── Interfaces      												--> Servis arayüzleri (Service Layer)
│		├── IStudentService.cs											--> 
│	├── Services        												--> İş mantığını yöneten servisler
│		├── StudentService.cs											--> 
│	├── Validators      												--> FluentValidation sınıfları
│	├── Readme.md        												--> Application KATMANI İLE İLGİLİ NOTLAR
│
│── 📂 Domain           												--> Çekirdek domain katmanı
│	├── Dependencies       												--> Bağımlılıklar (NuGet paketleri)
│		├── Packages       												--> Kütüphaneler
│			├── Microsoft.Extensions.Options (9.0.2)					--> 
│		├── Projects       												--> 
│			├── Shared       											--> 
│   ├── Aggregates      												--> Aggregate root'lar
│   ├── Entities        												--> Veritabanı modelleri
│		├── Student.cs        											-->
│   ├── Enums           												--> Enum tanımları
│   ├── Events          												--> Domain eventleri
│   ├── Interfaces      												--> Repository arayüzleri
│		├── IStudentRepository.cs      									--> Repository arayüzleri
│   ├── ValueObjects    												--> Değer nesneleri
│	├── Readme.md        												--> Domain KATMANI İLE İLGİLİ NOTLAR
│
│── 📂 Infrastructure   												--> Veri erişimi ve dış servis entegrasyonu
│	├── Dependencies       												--> Bağımlılıklar (NuGet paketleri)
│		├── Packages       												--> Kütüphaneler
│			├── Microsoft.EntityFrameworkCore (9.0.2)					--> 
│			├── Microsoft.EntityFrameworkCore.Design (9.0.2)			--> 
│			├── Microsoft.EntityFrameworkCore.InMemory (9.0.2)			--> 
│			├── Microsoft.EntityFrameworkCore.SqlServer (9.0.2)			--> 
│			├── Microsoft.Extensions.DependencyInjection (9.0.2)		--> 
│			├── Microsoft.Extensions.Logging (9.0.2)					--> 
│		├── Projects       												--> 
│			├── Domain       											--> 
│			├── Shared       											--> 
│   ├── Caching															--> Önbellekleme mekanizması (MemoryCache)
│   ├── Extensions														--> Dependency Injection helper'ları (Infrastructure için)
│		├── InfrastructureServiceCollectionExtensions.cs				--> 
│   ├── Identity														--> Kimlik doğrulama mekanizması
│   ├── Logging															--> Log yönetimi
│   ├── Persistence														--> EF Core ve veritabanı yönetimi
│		├── AppDbContext.cs												-->
│   ├── Repositories													--> Veritabanı işlemleri
│		├── StudentRepository.cs										-->
│   ├── Security														--> Güvenlik servisleri
│	├── Readme.md        												--> Infrastructure KATMANI İLE İLGİLİ NOTLAR
│
│── 📂 Shared															--> Ortak kullanılan bileşenler
│	├── Dependencies       												--> Bağımlılıklar (NuGet paketleri)
│		├── Packages       												--> Kütüphaneler
│			├── Microsoft.Extensions.DependencyInjection (9.0.2)		--> 
│			├── Microsoft.Extensions.Configuration (9.0.2)				--> 
│   ├── Constants														--> Sabit değişkenler
│   ├── Exceptions														--> Global hata yönetimi
│		├── NotFoundException.cs										--> 
│   ├── Extensions														--> Yardımcı genişletmeler
│   ├── Responses														--> Standart API yanıtları
│		├── ApiResponse.cs												--> 
│   ├── Utilities														--> Yardımcı metotlar
│	├── Readme.md        												--> Shared KATMANI İLE İLGİLİ NOTLAR