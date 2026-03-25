# 📂 Infrastructure (Veri Erişim Katmanı)

Bu katman, **veritabanı işlemleri, kimlik doğrulama, caching ve güvenlik mekanizmalarını** yönetir.  
`Application` ve `Domain` katmanlarıyla etkileşime geçerek **veri erişimini ve dış bağımlılıkları** yönetir.

---

## 📌 Klasör Yapısı

```plaintext
📂 Infrastructure
├── 📂 Dependencies         # Infrastructure bağımlılıkları
│   ├── Microsoft.EntityFrameworkCore (10.0.0)
│   ├── Microsoft.EntityFrameworkCore.Design (10.0.0)
│   ├── Npgsql.EntityFrameworkCore.PostgreSQL (10.0.0)
│   ├── System.IdentityModel.Tokens.Jwt (8.0.1)
├── 📂 Caching              # Önbellekleme mekanizması (MemoryCache)
├── 📂 Extensions           # Dependency Injection kayıtları
│   ├── InfrastructureServiceCollectionExtensions.cs
├── 📂 Identity             # Kimlik doğrulama yönetimi
├── 📂 Logging              # Log yönetimi
├── 📂 Persistence          # EF Core ve veritabanı yönetimi
│   ├── AppDbContext.cs
├── 📂 Repositories         # Veritabanı işlemleri
│   ├── StudentRepository.cs
├── 📂 Security             # Güvenlik servisleri
└── Readme.md               # Bu katmanın dökümantasyonu
```

---

## 📌 Bağımlılıklar

| **Paket Adı**                              | **Açıklama**                                      |
| ------------------------------------------ | ------------------------------------------------- |
| `Microsoft.EntityFrameworkCore`            | ORM (Object Relational Mapping) işlemleri için.   |
| `Microsoft.EntityFrameworkCore.Design`     | EF Core için migration desteği sağlar.            |
| `Npgsql.EntityFrameworkCore.PostgreSQL`    | PostgreSQL bağlantısı ve migration'lar icin kullanilir. |
| `System.IdentityModel.Tokens.Jwt`          | JWT olusturma ve token islemleri icin kullanilir. |

---

## 📌 Genel Kurallar

✔️ **Bu katmanda yalnızca veritabanı erişimi, kimlik doğrulama, güvenlik ve caching işlemleri bulunur.**  
✔️ **İş mantığı `Application` katmanında tanımlanır, burada iş mantığı bulunmaz.**  
✔️ **Tüm repository işlemleri `Repositories` klasöründe gerçekleştirilir.**  
✔️ **EF Core veritabanı işlemleri `Persistence` klasöründe yönetilir.**  
✔️ **Kimlik doğrulama ve yetkilendirme `Identity` klasöründe yapılır.**  
✔️ **Önbellekleme işlemleri `Caching` klasöründe yönetilir.**  
✔️ **Dependency Injection yönetimi `Extensions` klasöründeki `InfrastructureServiceCollectionExtensions.cs` içinde yapılır.**

🚀 **Infrastructure katmanı, uygulamanın dış bağımlılıklarını yöneterek modülerliği ve sürdürülebilirliği artırır!**
