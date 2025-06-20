# Advanced SQL Query Optimization Project

**Performance Improvement through Strategic Indexing in the AdventureWorks 2012 Database**

## Project Overview

This project aims to achieve performance improvements in the `AdventureWorks 2012` database **without altering query logic or the database schema**. The main objectives:

* **Primary Goal**: Reduce query execution times by **16–33%**
* **Critical Constraints**:

  * ❌ Query modification is not allowed
  * ❌ Schema modification is not allowed
  * ❌ Data manipulation is not allowed
  * ✅ Creating new indexes is allowed
* **Methodology**:

  * Each query was executed 100+ times to ensure statistical validity
  * Cache effects were eliminated by running `DBCC FREEPROCCACHE` and `DBCC DROPCLEANBUFFERS` before each execution
  * Optimizations were applied incrementally and their impact was measured step by step
  * A sample screenshot of the project interface is shown below. Additional interface screenshots can be found in the `/ScreenShots/UISS` directory.
    ![]()

## Queries Used

This project focuses on performance improvements of three core queries that reflect real-world scenarios. Query logic remained **unchanged** during optimization.

### Query 1 – Online Orders (2013)

Displays online orders from the year 2013, grouped by order date, state name, and city, along with total quantity and total revenue.

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

### Query 2 – Online Orders for Specific Products (2013)

Shows online orders in 2013 for products that are **black** or **yellow** and satisfy `MakeFlag = 1` or `FinishedGoodsFlag = 1`. Results are grouped by order date and category.

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

### Query 3 – Physical Store Orders (2013)

Displays physical store orders in 2013 for products that are **black** or **yellow** and meet the `MakeFlag = 1` or `FinishedGoodsFlag = 1` condition. Results are grouped by store name and product category.

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

## Key Learnings and Lessons

### Critical Success Factors

* Learned how to determine which columns should be indexed (WHERE, JOIN, ORDER BY)
* Understood that "not every index leads to improvement"
* Observed how excessive indexing can negatively impact write performance
* Learned how `DBCC` commands affect test outcomes
* Gained insights into real-world caching effects on performance

### Challenges Encountered

* **Cache Management**: Ensuring consistent use of `DBCC` commands
* **Measurement Consistency**: Eliminating hardware-related timing fluctuations
* **Index Interaction**: Avoiding unintended effects on other queries

## Query Analysis and Optimization Strategies

### Query 1: Online Orders (2013)

**Original Query**:

* Returns 10,899 rows
* 4-table join
* **Average Execution Time**: 761.19 ms

**Optimizations**:

1. **Composite Index**

   ```sql
   CREATE NONCLUSTERED INDEX IX1_SalesOrderHeader 
   ON Sales.SalesOrderHeader (OrderDate, OnlineOrderFlag, ShipToAddressID)
   ```

   * Enabled index scans on WHERE and JOIN clauses
   * **Improvement**: 16.12% (638.51 ms)

2. **Covering Index**

   ```sql
   CREATE INDEX IX2_SalesOrderDetail 
   ON Sales.SalesOrderDetail (SalesOrderID) 
   INCLUDE (OrderQty, LineTotal)
   ```

   * Eliminated need for extra table access during aggregation
   * **Improvement**: 33.36% (507.15 ms)

3. **Join Optimization**

   ```sql
   CREATE INDEX IX3_Address 
   ON Person.Address (AddressID) 
   INCLUDE (City, StateProvinceID)
   ```

   * Enabled direct access to address details via index
   * **Additional Improvement**: 6.60%

---

### Query 2: Online Orders for Specific Products

**Original Query**:

* Returns 1,360 rows
* 5-table join
* **Average Execution Time**: 833.74 ms

**Optimizations**:

1. **Filtering Index**

   ```sql
   CREATE INDEX IX2_Product 
   ON Production.Product (MakeFlag, FinishedGoodsFlag, Color)
   ```

   * Prevented full table scan on product filters
   * **Improvement**: 2.16% (815.76 ms)

2. **Join Optimization**

   ```sql
   CREATE INDEX IX3_Subcategory 
   ON Production.ProductSubcategory (ProductCategoryID)
   ```

   * Faster navigation through category hierarchy
   * **Total Improvement**: 2.78% (810.54 ms)

---

### Query 3: Physical Store Orders

**Original Query**:

* Returns 656 rows
* 6-table join
* **Average Execution Time**: 840.24 ms

**Optimizations**:

1. **Filtered Index**

   ```sql
   CREATE INDEX IDX_SOH_Filtered 
   ON Sales.SalesOrderHeader (OrderDate, OnlineOrderFlag)
   WHERE OnlineOrderFlag = 0
   ```

   * Smaller index covering only physical orders
   * **Improvement**: 10.55% (751.56 ms)

2. **Join Index**

   ```sql
   CREATE INDEX IDX_SOD_Join 
   ON Sales.SalesOrderDetail (SalesOrderID, ProductID)
   ```

   * Faster match in sales details
   * **Additional Improvement**: 1.33%

3. **Comprehensive Optimization**

   ```sql
   CREATE INDEX IDX_SOH_Enhanced 
   ON Sales.SalesOrderHeader (OrderDate, OnlineOrderFlag)
   INCLUDE (SalesOrderID, CustomerID)
   WHERE OnlineOrderFlag = 0
   ```

   * A single index for both filtering and joining
   * **Total Improvement**: 11.92% (740.06 ms)

---

## Performance Summary

| Query   | Before Optimization | Best Result | Improvement | Key Technique              |
| ------- | ------------------- | ----------- | ----------- | -------------------------- |
| Query 1 | 761.19 ms           | 507.15 ms   | **33.36%**  | Composite + Covering Index |
| Query 2 | 833.74 ms           | 810.54 ms   | **2.78%**   | Filtering Index            |
| Query 3 | 840.24 ms           | 740.06 ms   | **11.92%**  | Filtered Index             |

## Technical Approach & Automation

### Optimization Strategy

1. **Index Selection**: Analyzed columns used in WHERE, JOIN, and ORDER BY clauses
2. **Index Type Decisions**:

   * Composite indexes (for sequential access)
   * Covering indexes (to avoid base table access)
   * Filtered indexes (for selective conditions)
3. **Performance Measurement**:

   * Over 100 executions per optimization step
   * Recorded min, max, and average durations

### Automation Software

* **Query Selection**: Users can choose from 3 different queries
* **Auto Testing**: Automates 100 executions and cache clearing
* **Visual Reporting**: Graphical visualization of execution times
* **Optimization Suggestions**: Index recommendations tailored to each query

---

## How to Run

1. **Database Setup**:

   ```sql
   RESTORE DATABASE AdventureWorks2012 
   FROM DISK = 'C:\Backup\AdventureWorks2012.bak'
   WITH MOVE 'AdventureWorks2012_Data' TO 'C:\Data\AdventureWorks2012.mdf',
        MOVE 'AdventureWorks2012_Log' TO 'C:\Data\AdventureWorks2012.ldf';
   ```

2. **Apply Index Optimizations**:
   Execute indexes relevant to the selected query:

   ```sql
   -- Example: For Query 1
   CREATE NONCLUSTERED INDEX IX1_SalesOrderHeader 
   ON Sales.SalesOrderHeader (OrderDate, OnlineOrderFlag, ShipToAddressID);

   CREATE INDEX IX2_SalesOrderDetail 
   ON Sales.SalesOrderDetail (SalesOrderID) 
   INCLUDE (OrderQty, LineTotal);

   CREATE INDEX IX3_Address 
   ON Person.Address (AddressID) 
   INCLUDE (City, StateProvinceID);
   ```

3. **Use the Application Interface**:

   * Update the `connectionString` in `Form1.cs` to your own database settings
   * Select a query and specify user count through the interface
   * Click the “Analyze” button to run performance tests and view results

---

## Project Highlights

* Achieved performance improvement **without modifying queries or database schema**, only through strategic indexing.
* Every optimization step was validated with over 100 iterations and memory cleared, ensuring statistical reliability.
* Provided hands-on experience in indexing strategies and performance tuning.
* The user-friendly automation interface facilitated testing and analysis.
* Simulated real-world system behavior by carefully managing caching effects.

## License

This project was developed for academic purposes and is free to use with attribution.
