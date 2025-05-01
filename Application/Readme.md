# 📂 Application (İş Mantığı Katmanı)

Bu katman, **servislerin, DTO'ların ve iş mantığının** bulunduğu bölümdür.  
API katmanından gelen istekler burada **işlenir** ve **Domain veya Infrastructure katmanına yönlendirilir**.  
**Veritabanı erişimi doğrudan burada yapılmaz**, tüm işlemler `Infrastructure` katmanına delege edilir.

---

## 📌 Klasör Yapısı

```plaintext
📂 Application
├── 📂 Dependencies         # Application bağımlılıkları
│   ├── FluentValidation (11.11.0)  # DTO validation işlemleri
├── 📂 Projects             # Application katmanının bağımlı olduğu projeler
│   ├── Domain
│   ├── Infrastructure
│   ├── Shared
├── 📂 DTOs                 # Veri transfer objeleri
│   ├── Student
│   │   ├── StudentDto.cs
├── 📂 Extensions           # Dependency Injection kayıtları
│   ├── ApplicationServiceCollectionExtensions.cs
├── 📂 Interfaces           # Servis arayüzleri
│   ├── IStudentService.cs
├── 📂 Services             # Uygulama iş mantığı
│   ├── StudentService.cs
├── 📂 Validators           # FluentValidation doğrulayıcıları
└── Readme.md               # Bu katmanın dökümantasyonu
```

---

## 📌 Bağımlılıklar

| **Paket Adı**                              | **Açıklama**                                       |
| ------------------------------------------ | -------------------------------------------------- |
| `FluentValidation`                         | DTO validasyonları için kullanılır.                |

---

## 📌 Genel Kurallar

✔️ **Bu katmanda yalnızca iş mantığı ile ilgili servisler bulunur.**  
✔️ **Doğrudan veritabanına erişim yapılmaz, tüm veritabanı işlemleri `Infrastructure` katmanına delege edilir.**  
✔️ **API katmanı yalnızca bu katmandaki servislerle iletişim kurar.**  
✔️ **Tüm validasyon işlemleri `Validators` klasöründeki FluentValidation sınıfları ile yapılır.**  
✔️ **Bağımlılık yönetimi `Extensions` klasöründeki `ApplicationServiceCollectionExtensions.cs` ile yönetilir.**

🚀 **Application katmanı, iş mantığını merkezi bir yerde tutar ve bağımlılıkları en aza indirir!**
