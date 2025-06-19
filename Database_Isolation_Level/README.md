# Database Isolation Simulation Project
- Bu proje, **Maltepe Üniversitesi - SE308 Advanced Database Management System** dersi kapsamında yapılan projelerden biri olarak hazırlanmıştır.


## Proje Açıklaması Ve Amacı
- Bu projenin temel amacı, farklı **veritabanı işlem izolasyon seviyelerinin** sistem performansına olan etkilerini **gerçekçi bir simülasyon ortamında** analiz etmektir.
- Kullanıcı sayısı arttıkça **işlem sürelerinin**, **deadlock (kilitlenme)** olaylarının ve **verimliliğin** nasıl değiştiğini gözlemlemek, aynı zamanda **indeks kullanımı** ile **indekssiz yapıların** performanslarını karşılaştırmak hedeflenmiştir.
- Ve bu doğrultuda Microsoft'un **AdventureWorks2022** veritabanını kullanarak farklı konfigürasyonlar altında performans ölçümleri yapan bir simülasyon uygulaması geliştirildi
- Veri tabanı linki : https://github.com/Microsoft/sql-server-samples/releases/

## Neden Bu Projeyi Geliştirdim
- Gerçek dünya uygulamalarında veritabanları, aynı anda **çok sayıda kullanıcıdan gelen işlemleri eşzamanlı olarak** yönetmek zorundadır.
- İşlem **izolasyon seviyeleri**, veri tutarlılığını sağlamak açısından kritik öneme sahiptir; ancak bu durum sistem performansını olumsuz etkileyebilir.
- Bu proje sayesinde, farklı kullanıcı yükleri ve senaryolar altında hangi izolasyon seviyesinin tercih edilmesi gerektiği daha net anlaşılmıştır.
- Aynı zamanda, indeks kullanımının performans üzerindeki doğrudan etkisi detaylı olarak ölçülmüş ve analiz edilmiştir.


## Simülasyon Detayları
- **Tip A Kullanıcılar**: 100 kez `UPDATE` sorgusu çalıştırır (yazma işlemi)
- **Tip B Kullanıcılar**: 100 kez `SELECT + SUM` sorgusu çalıştırır (okuma işlemi)
- Her kullanıcı **ayrı bir thread** içinde çalışır.
- Kullanıcı sayısı, izolasyon seviyesi ve indeks durumu arayüzden seçilebilir.
- Simülasyon sonunda:
  - Ortalama işlem süreleri
  - Oluşan kilitlenme (deadlock) sayıları
  - Arayüzde detaylı olarak raporlanır.


## 🛠️ Kullanılan Teknolojiler
- **C# Windows Forms** (Masaüstü Arayüzü)
- **Microsoft SQL Server** ve **AdventureWorks2022** veritabanı
- **Çoklu Thread Kullanımı** (`System.Threading`)
- **ADO.NET** ile SQL bağlantısı
- **Stopwatch** ile zaman ölçümü
- **İşlem izolasyon seviyeleri**: 
  - READ UNCOMMITTED
  - READ COMMITTED
  - REPEATABLE READ
  - SERIALIZABLE


## 🖼️ Arayüzden Görüntüler
- Kullanıcı sayısı ve izolasyon seviyesi seçimi
- "Simülasyonu Başlat" butonu
- İşlem süreci için ilerleme çubuğu
- Sonuçları gösteren bir metin kutusu

> Arayüz ile ilgili screenshotlar Ekran görüntüleri `screenshots/UIss` klasöründe yer almaktadır.


## 📈 Karşılaştırmalı Performans Testleri
### 🔹 İndeksli Yapı ile:
- Genellikle daha kısa işlem süreleri
- Daha az deadlock
- Daha stabil sonuçlar

### İndekssiz Yapı ile:
- İşlem süreleri ciddi şekilde artmakta
- Deadlock olayları çok daha sık gerçekleşmekte

Tüm test sonuçları `screenshots/ResultSS` klasöründe detaylı tablo olarak sunulmuştur.


## Kurulum ve Çalıştırma
1. Visual Studio ile projeyi açın.
2. `AdventureWorks2022` veritabanını SQL Server’a yükleyin.
  - Veritabanı indirildikten sonra sırasıyla aşağıdaki sslerde bulunan adımlar uygulanarak SSMS'e indirilen database eklenir.
  - 
  - 
3. Form1.cs dosyasında bulunan `connectionString` değişkenine kendi connection string bilginizi giriniz.
4. `connectionString` in doğruluğunu test etmek için  
  - `program.cs` dosyası aşağıdaki gibi düzenlenmeli ve çıktı test edilmeli.
5. Projeyi çalıştırın, arayüzden kullanıcı sayılarını ve izolasyon seviyesini seçip simülasyonu başlatın.


## 📌 Öne Çıkan Özellikler
- Gerçek zamanlı thread yönetimi
- İzolasyon seviyelerine göre işlem mantığı ayrımı
- Kullanıcı sayısına göre otomatik performans değerlendirmesi
- Arayüzde görsel ilerleme ve sonuç raporlama
- Deadlock algılama ve istatistik çıkarımı
- Sonuçlar arasında kıyaslama imkanı


## Edinilen Deneyimler
- İzolasyon seviyeleri teoride basit gibi görünse de performans üzerindeki etkileri çok büyüktür.
- Deadlock problemleri beklenenden daha sık yaşanabilir.
- İndeks kullanımının performansa etkisi doğrudan gözlemlenmiştir.
- Çoklu thread yapılarında senkronizasyon önemlidir.
- Gerçekçi simülasyonlar için sistem kaynaklarının sınırlarını dikkate almak gerekir.

## Lisans
- Bu proje akademik amaçla hazırlanmış olup lisans gerektirmez. Ancak alıntı yapılırken kaynak gösterilmesi rica olunur.

## Sonuç
- Bu proje, **veritabanı işlem izolasyon seviyeleri** ve **indeks kullanımının** performansa olan etkisini anlamak için kapsamlı bir simülasyon ortamı sunar. Gerçek uygulamalarda bu bilgilere dayanarak sistem yapılandırmalarının optimize edilmesi mümkündür.
---

👨‍💻 Mehmet Güzel
