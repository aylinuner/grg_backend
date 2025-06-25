## Backend Özellikleri

- **Platform:** ASP.NET Core Web API (.NET 6 veya üstü)
- **Programlama Dili:** C#
- **API Tasarımı:** RESTful, `api/[controller]` routing kullanılmıştır.
- **Veri Yönetimi:** Projede şimdilik bellekte (static liste) tutulan veri ile CRUD işlemleri yapılmaktadır.
- **CRUD İşlemleri:**  
  - Listeleme (GET)  
  - Arama (GET, query param ile)  
  - Ekleme (POST)  

- **Endpoint Örnekleri:**  
  - `GET /api/[controller]/GetMaterials` → Tüm kayıtları listeler  
  - `GET /api/[controller]/Search?query=...` → İsme göre arama yapar  
  - `POST /api/[controller]/AddMaterial` → Yeni kayıt ekler (JSON gövdesi ile)

- **Özellikler:**  
  - Case-insensitive arama  
  - Yeni eklenen kayıtlara otomatik ID atama  
  - Basit ve anlaşılır yapı, kolayca genişletilebilir

---

## API Kullanımı Örneği (Fetch ile)

```javascript
// Veri çekme
fetch('http://localhost:5265/api/Material/GetMaterials')
  .then(res => res.json())
  .then(data => console.log(data));

// Arama yapma
fetch('http://localhost:5265/api/Material/Search?query=örnek')
  .then(res => res.json())
  .then(data => console.log(data));

// Yeni kayıt ekleme
fetch('http://localhost:5265/api/Material/AddMaterial', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({ name: 'Yeni Kayıt' })
})
.then(res => res.json())
.then(data => console.log(data));
