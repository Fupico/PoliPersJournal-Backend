# 📂 Domain (Çekirdek Katman)

Bu katman, **çekirdek iş kurallarını, veritabanı modellerini ve repository arayüzlerini** içerir.  
**Domain katmanı, bağımsızdır ve diğer katmanlardan etkilenmez.**  
Tüm iş kuralları, modeller ve veri erişim arayüzleri burada tanımlanır.

---

## 📌 Klasör Yapısı

```plaintext
📂 Domain
├── 📂 Projects             # Domain katmanının bağımlı olduğu projeler
│   ├── Shared
├── 📂 Aggregates           # Aggregate root'lar
├── 📂 Entities             # Veritabanı modelleri
│   ├── Student.cs
├── 📂 Enums                # Enum tanımları
├── 📂 Events               # Domain eventleri
├── 📂 Interfaces           # Repository arayüzleri
│   ├── IStudentRepository.cs
├── 📂 ValueObjects         # Değer nesneleri
└── Readme.md               # Bu katmanın dökümantasyonu
```

---

## 📌 Bağımlılıklar

**Domain katmanı tam bağımsızdır.** 

---

## 📌 Genel Kurallar

✔️ **Bu katmanda yalnızca iş kuralları, modeller ve veri erişim arayüzleri bulunur.**  
✔️ **Doğrudan veritabanı erişimi içermez, `Infrastructure` katmanı üzerinden yönetilir.**  
✔️ **İş mantığı ve servisler `Application` katmanında yer alır.**  
✔️ **Repository arayüzleri `Interfaces` klasöründe tanımlanır ve `Infrastructure` katmanında uygulanır.**  
✔️ **Tüm domain olayları (`Events`), enumlar (`Enums`) ve değer nesneleri (`ValueObjects`) burada tanımlanır.**

🚀 **Domain katmanı, tüm projenin iş kurallarını içeren en önemli katmandır ve bağımsız çalışmalıdır!**
