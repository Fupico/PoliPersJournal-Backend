# Fupitech Backend Yapısı

Bu proje **.NET 9 Web API** mimarisiyle geliştirilmiş olup **DDD (Domain-Driven Design) + Clean Architecture** prensiplerini temel alır.  
Her katman tek bir sorumluluğa sahiptir ve birbirleriyle **bağımsız** çalışacak şekilde tasarlanmıştır.

## 📂 Proje Yapısı

Aşağıda her katman için detaylı açıklamalara ulaşabilirsiniz:

### 📌 [API (Web API Katmanı)](API/Readme.md)

📌 **Kullanıcıların HTTP istekleriyle etkileşime geçtiği katmandır.**

### 📌 [Application (İş Mantığı Katmanı)](Application/Readme.md)

📌 **Servisler, DTO'lar ve uygulama iş mantığını içeren katmandır.**

### 📌 [Domain (Çekirdek Katman)](Domain/Readme.md)

📌 **Veritabanı modelleri, iş kuralları ve repository arayüzleri bulunur.**

### 📌 [Infrastructure (Veri Erişim Katmanı)](Infrastructure/Readme.md)

📌 **Veritabanı işlemleri, kimlik doğrulama ve caching mekanizmaları bulunur.**

### 📌 [Shared (Ortak Bileşenler)](Shared/Readme.md)

📌 **Genel yardımcı metotlar, hata yönetimi ve API yanıtlarını içerir.**

---

📌 **Bu dokümantasyon sürekli güncellenecektir. Eğer yeni bir yapı eklenirse ilgili katmanın `README.md` dosyası güncellenmelidir.** 🚀
