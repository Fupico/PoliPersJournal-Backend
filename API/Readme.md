# 📂 API (Web API Katmanı)

Bu katman, **kullanıcıların HTTP istekleriyle etkileşime geçtiği** ve API uç noktalarının tanımlandığı bölümdür.  
İstemciler bu katmandaki **Controller** sınıfları aracılığıyla **Application katmanına** erişir.

---

## 📌 Klasör Yapısı

```plaintext
📂 API
├── 📂 Dependencies         # API bağımlılıkları ve paketler
│   ├── FluentValidation.AspNetCore (11.3.0)
│   ├── Microsoft.AspNetCore.Authentication.JwtBearer (9.0.2)
│   ├── Microsoft.AspNetCore.OpenApi (9.0.2)
│   ├── Microsoft.EntityFrameworkCore.Design (9.0.2)  # Migration için gerekli
│   ├── Microsoft.EntityFrameworkCore.Tools (9.0.2)   # EF Core CLI araçları
│   ├── Microsoft.Extensions.Configuration (9.0.2)
│   ├── Swashbuckle.AspNetCore (7.3.1)  # Swagger UI
├── 📂 Controllers          # API uç noktaları
│   ├── StudentController.cs
├── 📂 Filters              # Validation ve Authorization filtreleri
├── 📂 Middlewares          # Hata yönetimi (Global Exception Handling)
│   ├── ExceptionMiddleware.cs
├── appsettings.json        # API yapılandırmaları
├── Program.cs              # Uygulamanın giriş noktası
├── API.http                # HTTP isteklerini test etmek için
└── Readme.md               # Bu katmanın dökümantasyonu


```

---

## 📌 Bağımlılıklar

| **Paket Adı**                                   | **Açıklama**                                           |
|-----------------------------------------------|------------------------------------------------------|
| `FluentValidation.AspNetCore`                   | DTO validasyonları için kullanılır.                    |
| `Swashbuckle.AspNetCore`                        | API dokümantasyonu için Swagger desteği sağlar.        |
| `Microsoft.AspNetCore.Authentication.JwtBearer` | JWT tabanlı kimlik doğrulama sağlar.                   |
| `Microsoft.AspNetCore.OpenApi`                  | OpenAPI desteği (Swagger için).                        |
| `Microsoft.EntityFrameworkCore.Design`          | EF Core Migration oluşturmak için gerekli.             |
| `Microsoft.EntityFrameworkCore.Tools`           | EF Core komut satırı araçları için gerekli.            |
| `Microsoft.Extensions.Configuration`            | `appsettings.json` dosyalarını okumak için kullanılır. |

---

## 📌 Genel Kurallar

✔️ **Bu katmanda yalnızca `Controllers`, `Middlewares` ve `Filters` bulunur.**  
✔️ **İş mantığı (`Services`) ve veritabanı işlemleri (`Repositories`) bu katmana dahil edilmez.**  
✔️ **Tüm iş mantığı, `Application` katmanına delege edilir.**  
✔️ **API yalnızca `Application` ve `Infrastructure` ile iletişim kurar.**  
✔️ **Migration işlemleri için `Microsoft.EntityFrameworkCore.Design` ve `Microsoft.EntityFrameworkCore.Tools` paketleri eklendi.**  

🚀 **API katmanı yalnızca HTTP isteklerini işler, herhangi bir veri işleme veya iş mantığı içermez!**

