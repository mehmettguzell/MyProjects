# Sporcu Takip Web Uygulaması

## Proje Hakkında

**Sporcu Takip Web Uygulaması**, spor okulları ve futbol kursları için geliştirilmiş, öğrenci yönetimi, ödeme takibi, sporcu gelişim izleme ve finansal raporlama süreçlerini kolaylaştıran bir web tabanlı yönetim sistemidir.

Projenin temel amacı, spor okullarının günlük operasyonlarını dijitalleştirerek verimliliği artırmak ve aynı zamanda yazılım geliştirme süreçlerinde pratik deneyim kazanmaktır. Uygulama, ASP.NET, Microsoft SQL Server, HTML, CSS ve JavaScript teknolojileri kullanılarak geliştirilmiştir.

## Proje Amaçları

Proje, aşağıdaki temel hedeflere ulaşmayı amaçlamaktadır:

1. Merkezi Yönetim: Spor okullarının öğrenci kayıtları, seans planlamaları ve finansal işlemlerini tek platformda birleştirmek.

2. Veri Güvenliği: Kullanıcı verilerini tuzlama ve karma yöntemleriyle korumak.

3. Kullanıcı Dostu Arayüz: Sezgisel ve hızlı bir arayüzle operasyonel süreçleri kolaylaştırmak.

4. Sporcu Gelişimi: Öğrencilerin fiziksel ve performans verilerini kaydedip analiz ederek gelişimlerini takip etmek.

5. Finansal Şeffaflık: Gelir, gider ve istatistikleri raporlayarak stratejik kararları desteklemek.

6. Esnek Planlama: Haftalık seansları sabah/öğleden sonra seçenekleriyle planlayarak öğrenci ve aile ihtiyaçlarına uyum sağlamak.

## Proje Özellikleri

Uygulama, spor okullarının ihtiyaçlarına yönelik aşağıdaki modülleri ve sayfaları içermektedir:

### 1. Giriş Sayfası

- **Amaç:** Yetkili personelin (örneğin, admin1, test, deneme hesapları) güvenli bir şekilde sisteme erişmesini sağlamak.
- **Özellikler:**
  - Kullanıcı adı ve şifre ile kimlik doğrulama.
  - Şifre güvenliği için tuzlama ve karma yöntemleri kullanılarak veri güvenliği sağlanmıştır.
  - Yetkisiz erişim engellenir; kullanıcı, giriş yapmadan diğer sayfalara yönlendirilirse otomatik olarak giriş sayfasına geri döner.
  - Giriş yapan kullanıcıya kişiselleştirilmiş bir karşılama mesajı gösterilir: *"Hoş geldiniz, \[Kullanıcı Adı\] (\[Personel Unvanı\])!"*
  - Veritabanı bağlantısı: *Personel* tablosu.

### 2. Kayıt Sayfası

- **Amaç:** Yeni öğrencilerin ve velilerinin bilgilerini sisteme kaydetmek.
- **Özellikler:**
  - **Öğrenci Bilgileri:** Ad, soyad, yaş, boy, kilo, kas kütlesi, dikey sıçrama.
  - **Veli Bilgileri:** Ad, soyad, telefon numarası, e-posta.
  - **Ödeme Bilgileri:** Ödeme planı, kart bilgileri, üyelik türü.
  - Veriler, *Öğrenci*, *Veli* ve *Ödeme Planı* tablolarına kaydedilir.
  - Form doğrulaması ile veri bütünlüğü sağlanır.
  - Veritabanı bağlantısı: *Öğrenci*, *Veli*, *Ödeme Planı* tabloları.

### 3. Ana Sayfa

- **Amaç:** Yöneticilere spor okulunun genel durumunu özetleyen bir kontrol paneli sunmak.
- **Özellikler:**
  - Yaklaşan ödemeler, seanslar, yıldız öğrenciler ve şube durumları (açık, kapalı, bakımda) gerçek zamanlı olarak görüntülenir.
  - Dinamik veri güncellemeleri için veritabanı ile entegre çalışır.
  - Standart başlık (header) ve sayfa içi yönlendirme menüsü tüm sayfalarda kullanılır.
  - Veritabanı bağlantısı: *Yorumlar* (yıldızlı yorumlar), *Şube* (durumlar), *Ödeme Planı* (yaklaşan ödemeler) tabloları.

### 4. Öğrenci Gelişim Sayfası

- **Amaç:** Öğrencilerin aylık performans verilerini ve öğretmen yorumlarını takip etmek.
- **Özellikler:**
  - Aylık bazda kaydedilen veriler: Boy, kilo, kas kütlesi, dikey sıçrama vb.
  - Öğretmen yorumları ve yıldız değerlendirmeleri (*true*/*false* ile işaretlenir).
  - Yıldızlı yorumlar (*true* olarak işaretlenenler) ana sayfada öne çıkar.
  - Aylar, düğmelerle seçilebilir; ilgili veriler ve yorumlar listelenir.
  - Veritabanı bağlantısı: *Performans* ve *Yorumlar* tabloları.

### 5. Randevu Yönetim Sayfası

- **Amaç:** Seans planlamasını ve öğrenci atamalarını kolaylaştırmak.
- **Özellikler:**
  - Seanslar haftada iki gün (Pazartesi-Çarşamba veya Salı-Perşembe) sabah ve öğleden sonra olarak planlanır.
  - Şube, öğrenci, öğretmen ve gün seçilerek uygun saatler atanır.
  - Her seansın öğrenci sayısı görüntülenir.
  - Hafta sonları aile zamanına ayrılması için seanslar hafta içine planlanmıştır.
  - Veritabanı bağlantısı: *Randevu*, *Öğrenci*, *Öğretmen*, *Şube* tabloları.

### 6. Ödeme Planı Sayfası

- **Amaç:** Öğrencilerin ödeme planlarını ve ilgili bilgileri detaylı bir şekilde görüntülemek.
- **Özellikler:**
  - Öğrencinin fotoğrafı, kart bilgileri, ödeme planı ve veli bilgileri gösterilir.
  - Yaklaşan ödemeler, ana sayfadan seçilerek bu sayfada detaylı incelenebilir.
  - Ödeme tarihi, yöntemi, türü ve miktarı gibi bilgiler veritabanından çekilir.
  - Veritabanı bağlantısı: *Ödeme Planı*, *Öğrenci*, *Veli* tabloları.

### 7. Finans Sayfası

- **Amaç:** Belirli bir tarih aralığında finansal verileri ve öğrenci istatistiklerini raporlamak.
- **Özellikler:**
  - Toplam gelir, gider, net kâr, kayıt olan ve ayrılan öğrenci sayıları tablo formatında sunulur.
  - Veriler, *Gelir*, *Gider* ve *Kayıt* tablolarından dinamik olarak alınır.
  - Spor okulunun performans analizi ve stratejik kararlar için değerli bilgiler sağlar.
  - Veritabanı bağlantısı: *Gelir*, *Gider*, *Öğrenci*, *Kayıt* tabloları.

## Teknik Altyapı
### Veritabanı Tasarımı

Veritabanın, Diagramı, Ana tabloları ve veri tipleri aşağıdaki görsellerde bulunmaktadır:


## Geliştirme Süreci

### 1. Aşama 

- **Planlama ve Araştırma:** Spor okullarının operasyonel süreçleri ve ihtiyaçları analiz edildi.
- **Tasarım:** Wireframe.cc kullanılarak yedi sayfanın wireframe'leri oluşturuldu (Giriş, Kayıt, Ana Sayfa, Öğrenci Gelişim, Randevu Yönetim, Ödeme Planı, Finans). Bu taslaklar, HTML, CSS ve JavaScript ile gerçek sayfalara dönüştürüldü.
- **Veritabanı Tasarımı:** Lucidchart ile ERD çizildi. MSSQL Server ile tablolar oluşturuldu, veri tipleri ve PK/FK ilişkileri tanımlandı.

> wireframe, Database ve frontend ssleri ScreenShots klasöründe bulunmaktadır.
### 2. Aşama 

- **Backend Geliştirme:** ASP.NET ile sunucu tarafı kodları yazıldı. Kullanıcı girişi, veri kaydı, ödeme takibi ve raporlama işlevleri implemente edildi.
- **Veri Güvenliği:** Şifreler için tuzlama ve karma yöntemleri uygulandı.
- **Test ve Hata Ayıklama:** Uygulama farklı senaryolarda test edildi, hatalar giderildi.
- **Dokümantasyon:** Proje süreçleri detaylı bir şekilde belgelendi, sunum hazırlandı.
- **Teslim Edilenler:** Çalışan uygulama, veritabanı yedeği, kaynak kodlar, güncellenmiş diyagramlar ve final raporu.

## Kazanımlar
Bu proje, yazılım geliştirme sürecinin her aşamasında bana değerli beceriler kazandırdı:

- **Teknik Beceriler:**
  - ASP.NET, C# ile full-stack web geliştirme.
  - MSSQL Server ile veritabanı tasarımı, ERD oluşturma ve karmaşık SQL sorguları yazma.
  - HTML, CSS ve JavaScript ile kullanıcı dostu arayüz tasarımı.
  - Parola güvenliği için salting, hashleme ve şifreleme yöntemleri uygulama.
- **Proje Yönetimi:**
  - Gereksinim analizi, proje planlama ve zaman yönetimi.

## Gelecekteki İyileştirmeler

- **Mimariyi Geliştirme:** Sistem mimarisini daha ölçeklenebilir, modüler ve güvenli hale getirmek için yeniden yapılandırma ve modern yaklaşımların benimsenmesi.

- **Mobil Uyumluluk:** Responsive tasarım ile mobil cihazlarda sorunsuz kullanım.
- **Otomatik Bildirimler:** Ödeme hatırlatmaları için e-posta veya SMS entegrasyonu.
- **Gelişmiş Görselleştirme:** Öğrenci gelişim verileri için interaktif grafikler.
- **Çoklu Dil Desteği:** Uluslararası kullanım için dil seçenekleri.
- **Performans Optimizasyonu:** Veritabanı sorgularını ve sayfa yükleme sürelerini iyileştirme.