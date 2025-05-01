# 📂 Shared (Ortak Bileşenler Katmanı)

Bu katman, **projede ortak kullanılan yardımcı sınıfları, hata yönetimi, genişletme metotları, sabitler ve API yanıt modellerini** içerir.  
Diğer katmanlar buradaki bileşenleri kullanarak **kod tekrarını önler** ve **ortak işlevleri merkezi bir noktadan yönetir**.

---

## 📌 Klasör Yapısı

```plaintext
📂 Shared
├── 📂 Dependencies         # Shared bağımlılıkları
│   ├── Microsoft.Extensions.Configuration (9.0.2)
├── 📂 Constants            # Sabit değişkenler
├── 📂 Exceptions           # Global hata yönetimi
│   ├── NotFoundException.cs
├── 📂 Extensions           # Yardımcı genişletmeler (Extension Methods)
├── 📂 Responses            # Standart API yanıtları
│   ├── ApiResponse.cs
├── 📂 Utilities            # Yardımcı metotlar
└── Readme.md               # Bu katmanın dökümantasyonu
```

---

## 📌 Bağımlılıklar

| **Paket Adı**                              | **Açıklama**                                     |
| ------------------------------------------ | ------------------------------------------------ |
| `Microsoft.Extensions.Configuration`       | Yapılandırma (`appsettings.json`) yönetimi için. |

---

## 📌 Genel Kurallar

✔️ **Bu katman, projede ortak kullanılan bileşenleri içerir.**  
✔️ **İş mantığı veya veritabanı işlemleri içermez.**  
✔️ **API yanıtları `Responses` klasöründe standart hale getirilmiştir (`ApiResponse.cs`).**  
✔️ **Hata yönetimi `Exceptions` klasöründe merkezi olarak yönetilir (`NotFoundException.cs`).**  
✔️ **Sabit değişkenler `Constants` klasöründe saklanır.**  
✔️ **Yardımcı metotlar ve genişletmeler `Utilities` ve `Extensions` klasörlerinde yer alır.**

🚀 **Shared katmanı, projenin tüm bileşenleri için ortak bir merkez görevi görerek, kod tekrarını önler ve yönetilebilirliği artırır!**
