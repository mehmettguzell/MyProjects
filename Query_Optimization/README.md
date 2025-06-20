# Gelişmiş SQL Sorgu Optimizasyon Projesi

**AdventureWorks 2012 Veritabanında Stratejik İndeksleme ile Performans İyileştirme**

## Proje Genel Bakış

Bu proje, `AdventureWorks 2012` veritabanında **sorgu mantığı ve veritabanı yapısı değiştirilmeden** performans iyileştirmeleri gerçekleştirmeyi amaçlamaktadır. Temel hedefler:

* **Ana Amaç**: Sorgu çalışma sürelerini **%16-33** oranında azaltmak
* **Kritik Kısıtlamalar**:

  * ❌ Sorgu değişikliği yasak
  * ❌ Şema değişikliği yasak
  * ❌ Veri manipülasyonu yasak
  * ✅ Yeni indeks oluşturma serbest
* **Metodoloji**:

  * Her sorgu 100+ kez çalıştırılarak istatistiksel geçerlilik sağlandı
  * Önbellek etkisini ortadan kaldırmak için her çalıştırmadan önce `DBCC FREEPROCCACHE` ve `DBCC DROPCLEANBUFFERS` komutları uygulandı
  * Optimizasyonlar kademeli olarak uygulanıp her adımın etkisi ölçüldü
  * Projenin arayüzünden örnek bir fotoğraf aşağıda bulunmaktadır. Geri kalan arayüz ssleri `/ScreenShots/UISS`klasöründe bulunmaktadır.
    ![]()

## Kullanılan Sorgular

Bu projede, performans iyileştirme amacıyla üç temel sorgu kullanılmıştır. Bu sorgular doğrudan gerçek senaryoları yansıtmakta ve sorgu mantığı **değiştirilmeksizin** optimize edilmiştir.

### Sorgu 1 – Çevrimiçi Siparişler (2013)

2013 yılına ait çevrimiçi siparişleri, sipariş tarihi, eyalet adı ve şehir adı bilgileriyle birlikte; toplam sipariş miktarı ve toplam tutar bazında gruplar.

```sql
SELECT SOH.OrderDate,
       PROV.Name AS StateProvinceName,
       ADDR.City,
       SUM(SOD.OrderQty) AS TotalOrderQty,
       SUM(SOD.LineTotal) AS TotalLineTotal
  FROM Sales.SalesOrderDetail SOD
 INNER JOIN Sales.SalesOrderHeader SOH
    ON SOH.SalesOrderID = SOD.SalesOrderID
 INNER JOIN Person.Address ADDR
    ON ADDR.AddressID = SOH.ShipToAddressID
 INNER JOIN Person.StateProvince PROV
    ON PROV.StateProvinceID = ADDR.StateProvinceID
 WHERE SOH.OrderDate BETWEEN '20130101' AND '20131231'
   AND SOH.OnlineOrderFlag = 1
 GROUP BY SOH.OrderDate, PROV.Name, ADDR.City
 ORDER BY SOH.OrderDate, PROV.Name, ADDR.City;
```

---

### Sorgu 2 – Belirli Ürünler İçin Çevrimiçi Siparişler (2013)

Rengi **Siyah** veya **Sarı** olan ve `MakeFlag = 1` veya `FinishedGoodsFlag = 1` şartını sağlayan ürünlerin 2013 yılına ait çevrimiçi siparişleri. Sonuçlar, sipariş tarihi ve ürün kategorisi adına göre gruplanır.

```sql
SELECT SOH.OrderDate,
       CAT.Name as CategoryName,
       SUM(SOD.OrderQty) AS TotalOrderQty,
       SUM(SOD.LineTotal) AS TotalLineTotal
  FROM Sales.SalesOrderDetail SOD
 INNER JOIN Sales.SalesOrderHeader SOH
    ON SOH.SalesOrderID = SOD.SalesOrderID
 INNER JOIN Production.Product P
    ON P.ProductID = SOD.ProductID
 INNER JOIN Production.ProductSubcategory SUBCAT
    ON SUBCAT.ProductCategoryID = P.ProductSubcategoryID
 INNER JOIN Production.ProductCategory CAT
    ON CAT.ProductCategoryID = SUBCAT.ProductSubcategoryID
 WHERE SOH.OrderDate BETWEEN '20130101' AND '20131231'
   AND SOH.OnlineOrderFlag = 1
   AND (P.MakeFlag = 1 OR P.FinishedGoodsFlag = 1)
   AND P.Color IN ('Black', 'Yellow')
 GROUP BY SOH.OrderDate, CAT.Name
 ORDER BY SOH.OrderDate, CAT.Name;
```

---

### Sorgu 3 – Fiziksel Mağaza Siparişleri (2013)

Rengi **Siyah** veya **Sarı** olan ve `MakeFlag = 1` veya `FinishedGoodsFlag = 1` olan ürünlerin **fiziksel mağazalardan verilen siparişleri**. Sonuçlar mağaza adı ve ürün kategorisine göre gruplanır.

```sql
SELECT STOR.Name as StoreName,
       CAT.Name as CategoryName,
       SUM(SOD.OrderQty) AS TotalOrderQty,
       SUM(SOD.LineTotal) AS TotalLineTotal
  FROM Sales.SalesOrderDetail SOD
 INNER JOIN Sales.SalesOrderHeader SOH
    ON SOH.SalesOrderID = SOD.SalesOrderID
 INNER JOIN Production.Product P
    ON P.ProductID = SOD.ProductID
 INNER JOIN Production.ProductSubcategory SUBCAT
    ON SUBCAT.ProductCategoryID = P.ProductSubcategoryID
 INNER JOIN Production.ProductCategory CAT
    ON CAT.ProductCategoryID = SUBCAT.ProductSubcategoryID
 INNER JOIN Sales.Customer CUST
    ON CUST.CustomerID = SOH.CustomerID
 INNER JOIN Sales.Store STOR
    ON STOR.BusinessEntityID = CUST.StoreID
 WHERE SOH.OrderDate BETWEEN '20130101' AND '20131231'
   AND SOH.OnlineOrderFlag = 0
   AND (P.MakeFlag = 1 OR P.FinishedGoodsFlag = 1)
   AND P.Color IN ('Black', 'Yellow')
 GROUP BY STOR.Name, CAT.Name
 ORDER BY STOR.Name, CAT.Name;
```

## Temel Kazanımlar ve Dersler  
### Kritik Başarı Faktörleri  
- Hangi sütunlara indeks eklenmesi gerektiğini (WHERE, JOIN, ORDER BY) ve bileşik indekslerin nasıl tasarlanacağını öğrendim.
- "Her indeks her zaman iyileştirmez" prensibini öğrendim.
- Fazla indeksin yazma performansını nasıl düşürdüğünü gözlemledim.
- DBCC komutlarıyla önbellek temizlemenin test sonuçlarını nasıl etkilediğini öğrendim.
- Gerçek hayatta cache'in performansa etkisini kavradım.

### Karşılaşılan Zorluklar  
- **Önbellek Yönetimi**: DBCC komutlarının tutarlı uygulanmasının sağlanması  
- **Ölçüm Tutarlılığı**: Donanım kaynaklı dalgalanmaların giderilmesi  
- **İndeks Etkileşimi**: Yeni indekslerin diğer sorguları etkileme riski  

## Sorgu Analizleri ve Optimizasyon Stratejileri  

### Sorgu 1: Çevrimiçi Siparişler (2013)  
**Orijinal Sorgu**:  
- 10,899 satır döndürür  
- 4 tablo birleşimi (SalesOrderDetail, SalesOrderHeader, Address, StateProvince)  
- **Ortalama Süre**: 761.19 ms  

**Optimizasyonlar**:  
1. **Bileşik İndeks**:  
   ```sql
   CREATE NONCLUSTERED INDEX IX1_SalesOrderHeader 
   ON Sales.SalesOrderHeader (OrderDate, OnlineOrderFlag, ShipToAddressID)
   ```
   - **Etki**: WHERE koşullarında ve birleştirmelerde indeks taraması sağladı  
   - **Sonuç**: %16.12 iyileşme (638.51 ms)  

2. **Kapsayan İndeks**:  
   ```sql
   CREATE INDEX IX2_SalesOrderDetail 
   ON Sales.SalesOrderDetail (SalesOrderID) 
   INCLUDE (OrderQty, LineTotal)
   ```
   - **Etki**: Toplama işlemleri için ek tablo erişimi ortadan kaldırıldı  
   - **Sonuç**: %33.36 iyileşme (507.15 ms)  

3. **Birleştirme Optimizasyonu**:  
   ```sql
   CREATE INDEX IX3_Address 
   ON Person.Address (AddressID) 
   INCLUDE (City, StateProvinceID)
   ```
   - **Etki**: Adres bilgilerine doğrudan indeks üzerinden erişim  
   - **Sonuç**: %6.60 ek iyileşme  

---

### Sorgu 2: Belirli Ürünler için Çevrimiçi Siparişler  
**Orijinal Sorgu**:  
- 1,360 satır döndürür  
- 5 tablo birleşimi  
- **Ortalama Süre**: 833.74 ms  

**Optimizasyonlar**:  
1. **Filtreleme İndeksi**:  
   ```sql
   CREATE INDEX IX2_Product 
   ON Production.Product (MakeFlag, FinishedGoodsFlag, Color)
   ```
   - **Etki**: Ürün filtreleme işlemlerinde tam tablo taraması önlendi  
   - **Sonuç**: %2.16 iyileşme (815.76 ms)  

2. **Birleştirme Optimizasyonu**:  
   ```sql
   CREATE INDEX IX3_Subcategory 
   ON Production.ProductSubcategory (ProductCategoryID)
   ```
   - **Etki**: Kategori hiyerarşisinde daha hızlı gezinme  
   - **Sonuç**: %2.78 toplam iyileşme (810.54 ms)  

---

### Sorgu 3: Fiziksel Mağaza Siparişleri  
**Orijinal Sorgu**:  
- 656 satır döndürür  
- 6 tablo birleşimi  
- **Ortalama Süre**: 840.24 ms  

**Optimizasyonlar**:  
1. **Filtrelenmiş İndeks**:  
   ```sql
   CREATE INDEX IDX_SOH_Filtered 
   ON Sales.SalesOrderHeader (OrderDate, OnlineOrderFlag)
   WHERE OnlineOrderFlag = 0
   ```
   - **Etki**: Sadece fiziksel siparişleri içeren küçültülmüş indeks  
   - **Sonuç**: %10.55 iyileşme (751.56 ms)  

2. **Birleştirme İndeksi**:  
   ```sql
   CREATE INDEX IDX_SOD_Join 
   ON Sales.SalesOrderDetail (SalesOrderID, ProductID)
   ```
   - **Etki**: Satış detaylarında hızlı eşleştirme  
   - **Sonuç**: %1.33 ek iyileşme  

3. **Kapsamlı Optimizasyon**:  
   ```sql
   CREATE INDEX IDX_SOH_Enhanced 
   ON Sales.SalesOrderHeader (OrderDate, OnlineOrderFlag)
   INCLUDE (SalesOrderID, CustomerID)
   WHERE OnlineOrderFlag = 0
   ```
   - **Etki**: Filtreleme ve birleştirmeler için tek indeks  
   - **Sonuç**: %11.92 toplam iyileşme (740.06 ms)  

---



## Performans Sonuçları Özeti  
| Sorgu | Optimizasyon Öncesi | En İyi Sonuç | İyileşme | Ana Optimizasyon Tekniği |  
|-------|---------------------|--------------|----------|--------------------------|  
| Sorgu 1 | 761.19 ms | 507.15 ms | **%33.36** | Bileşik + Kapsayan İndeks |  
| Sorgu 2 | 833.74 ms | 810.54 ms | **%2.78** | Filtreleme İndeksi |  
| Sorgu 3 | 840.24 ms | 740.06 ms | **%11.92** | Filtrelenmiş İndeks |  


## Teknik Yaklaşım ve Otomasyon  
### Optimizasyon Stratejileri  
1. **İndeks Seçimi**: WHERE, JOIN ve ORDER BY cümleciklerinde kullanılan sütunlar analiz edildi  
2. **İndeks Türü Belirleme**:  
   - Bileşik indeksler (sıralı erişim için)  
   - Kapsayan indeksler (tablo erişimini önlemek için)  
   - Filtrelenmiş indeksler (özel koşullar için)  
3. **Performans Ölçümü**:  
   - Her optimizasyon adımı sonrası 100+ çalıştırma  
   - Min, max ve ortalama sürelerin kaydedilmesi  

### Otomasyon Yazılımı  
- **Sorgu Seçimi**: 3 farklı sorgu için seçim imkanı  
- **Otomatik Test**: 100 çalıştırma ve önbellek temizleme otomasyonu  
- **Görsel Raporlama**: Çalışma sürelerinin grafiksel gösterimi  
- **Optimizasyon Önerileri**: Sorguya özel indeks yapıları önerisi  

---

İstediğiniz düzeltmeyle birlikte "Nasıl Çalıştırılır?" bölümünü aşağıdaki gibi güncelleyebilirsiniz:

---

## Nasıl Çalıştırılır?
1. **Veritabanı Kurulumu**:

   ```sql
   RESTORE DATABASE AdventureWorks2012 
   FROM DISK = 'C:\Backup\AdventureWorks2012.bak'
   WITH MOVE 'AdventureWorks2012_Data' TO 'C:\Data\AdventureWorks2012.mdf',
        MOVE 'AdventureWorks2012_Log' TO 'C:\Data\AdventureWorks2012.ldf';
   ```

2. **İndeks Optimizasyonlarını Uygula**:
   Her sorguya karşılık gelen indeksleri aşağıdaki gibi sırayla uygulayabilirsiniz:

   ```sql
   -- Örnek: Sorgu 1 için
   CREATE NONCLUSTERED INDEX IX1_SalesOrderHeader 
   ON Sales.SalesOrderHeader (OrderDate, OnlineOrderFlag, ShipToAddressID);

   CREATE INDEX IX2_SalesOrderDetail 
   ON Sales.SalesOrderDetail (SalesOrderID) 
   INCLUDE (OrderQty, LineTotal);

   CREATE INDEX IX3_Address 
   ON Person.Address (AddressID) 
   INCLUDE (City, StateProvinceID);
   ```

3. **Uygulama Arayüzünü Kullan**:

   * `Form1.cs` dosyasındaki `connectionString` alanını kendi veritabanı ortamınıza göre güncelleyin
   * Arayüz üzerinden sorgu seçin, kullanıcı sayısını belirtin
   * "Analiz Et" butonuna tıklayarak sorgu testi ve performans ölçümünü başlatın


## Projenin Öne Çıkan Yönleri  
* Sorguların ve veritabanı şemasının değiştirilmeden yalnızca stratejik indeksleme ile performans iyileştirmesi sağlandı.
* Her optimizasyon adımı, önbellek temizlenerek 100+ tekrar ile ölçümlenerek istatistiksel güvenilirlik elde edildi.
* İyileştirmeler sadece sorgu performansına değil, indeksleme stratejilerine dair pratik bilgi birikimi sağladı.
* Otomasyon arayüzü ile indeks testleri ve süre analizleri kullanıcı dostu bir biçimde yönetildi.
* Gerçek dünyadaki sistem davranışına yakın sonuçlar elde etmek için cache etkisi kontrollü biçimde ele alındı.

## Lisans  
Bu proje akademik amaçlarla hazırlanmıştır. Kaynak gösterilerek kullanılabilir.