# PoliPersJournal - Backend

PoliPersJournal, çok dilli içerik yönetimi yapısına sahip, blog/journal türünde geliştirilen bir platformdur.
Bu repo, projenin **backend (sunucu tarafı)** kodlarını içermektedir.

Proje, **.NET 9 Web API**, **DDD (Domain-Driven Design)** ve **Clean Architecture** mimarisi ile yazılmıştır.
Tamamıyla **özgür yazılım** ve **açık kaynak** felsefesine uygun şekilde geliştirilmiştir.

> Projenin frontend kısmı için: 👉 [PoliPersJournal Frontend Repo](https://github.com/Fupico/PoliPersJournal-frontend)

---

## 🚀 Hızlı Başlangıç

### Gereksinimler:

- .NET 9 SDK
- MSSQL Server

### Projeyi Çalıştırma

```bash
cd PoliPersJournal.Backend

# Bağımlılıkları yükle (opsiyonel)
dotnet restore

# Veritabanını güncelle (migrations uygulanır)
dotnet ef database update

# Projeyi başlat
https://localhost:1923 üzerinden Swagger açılır
dotnet run
```

---

## 📂 Proje Katmanları (DDD + Clean Architecture)

| Katman           | Açıklama                                                 |
| ---------------- | -------------------------------------------------------- |
| `API`            | HTTP uç noktaları ve controller'lar                      |
| `Application`    | DTO, servisler ve validasyon kuralları                   |
| `Domain`         | Varlıklar, interface'ler, iş kuralları                   |
| `Infrastructure` | Repository, kimlik doğrulama, EF Core DbContext          |
| `Shared`         | Ortak yapılar: hata yönetimi, API response modelleri vs. |

Detaylı açıklama için 👉 [PoliPersJournal-Backend-Yapisi.md](PoliPersJournal-Backend-Yapisi.md)

---

## 🔐 Kimlik Doğrulama

JWT tabanlı kimlik doğrulama sistemi entegredir. Swagger üzerinden token alıp test edebilirsiniz.

---

## 🧭 Yol Haritası

- [x] Çok dilli makale yapısı (tr, en, de)
- [x] Kategori ve etiket yönetimi
- [x] PDF içeriği, cover image, summary alanları
- [x] View ve download sayaçları
- [x] PageTranslation ile sayfa başlıkları
- [ ] Admin panel entegrasyonu (yolda)
- [ ] Rol bazlı yetkilendirme
- [ ] Bildirim sistemi (opsiyonel)

---

## 📄 Lisans

Bu proje [GNU General Public License v3.0](https://www.gnu.org/licenses/gpl-3.0.html) lisansı ile lisanslanmıştır.

> Yazılım özgürlüğünü önemseyen herkes için geliştirildi.

- [Özgür yazılım nedir?](ozgur-yazilim-nedir.md)

---

## 👨‍💻 Geliştirici

**Devrim Mehmet Pattabanoğlu**  
✉️ devrimmehmet@gmail.com  
🔗 [LinkedIn](https://www.linkedin.com/in/devrim-mehmet-pattabanoglu/)

---

> "Mükemmeliyetçilik değil, başlamak önemlidir. Yolda geliştiririz."
