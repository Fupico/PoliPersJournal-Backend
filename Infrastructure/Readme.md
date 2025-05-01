# 📂 Infrastructure (Veri Erişim Katmanı)

Bu katman, **veritabanı işlemleri, kimlik doğrulama, caching ve güvenlik mekanizmalarını** yönetir.  
`Application` ve `Domain` katmanlarıyla etkileşime geçerek **veri erişimini ve dış bağımlılıkları** yönetir.

---

## 📌 Klasör Yapısı

```plaintext
📂 Infrastructure
├── 📂 Dependencies         # Infrastructure bağımlılıkları
│   ├── Microsoft.EntityFrameworkCore (9.0.2)
│   ├── Microsoft.EntityFrameworkCore.Design (9.0.2)
│   ├── Microsoft.EntityFrameworkCore.SqlServer (9.0.2)
│   ├── Microsoft.Extensions.Logging (9.0.2)
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
| `Microsoft.EntityFrameworkCore.SqlServer`  | SQL Server bağlantısı için kullanılır.            |
| `Microsoft.Extensions.Logging`             | Uygulama loglarını yönetmek için.                 |

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
