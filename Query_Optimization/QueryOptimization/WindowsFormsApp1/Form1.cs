
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private const string ConnectionString = "Data Source=YAKISIKLI;Initial Catalog=AdventureWorks2012;Integrated Security=True;Encrypt=False";

        private readonly Dictionary<string, string> Queries = new Dictionary<string, string>
        {
            { "Query1", @"SELECT SOH.OrderDate, PROV.Name AS StateProvinceName, ADDR.City,
                SUM(SOD.OrderQty) AS TotalOrderQty, SUM(SOD.LineTotal) AS TotalLineTotal
                FROM Sales.SalesOrderDetail SOD
                INNER JOIN Sales.SalesOrderHeader SOH ON SOH.SalesOrderID = SOD.SalesOrderID
                INNER JOIN Person.Address ADDR ON ADDR.AddressID = SOH.ShipToAddressID
                INNER JOIN Person.StateProvince PROV ON PROV.StateProvinceID = ADDR.StateProvinceID
                WHERE SOH.OrderDate BETWEEN '20130101' AND '20131231' AND SOH.OnlineOrderFlag = 1
                GROUP BY SOH.OrderDate, PROV.Name, ADDR.City
                ORDER BY SOH.OrderDate, PROV.Name, ADDR.City" },

            { "Query2", @"SELECT SOH.OrderDate, CAT.Name as CategoryName,
                SUM(SOD.OrderQty) AS TotalOrderQty, SUM(SOD.LineTotal) AS TotalLineTotal
                FROM Sales.SalesOrderDetail SOD
                INNER JOIN Sales.SalesOrderHeader SOH ON SOH.SalesOrderID = SOD.SalesOrderID
                INNER JOIN Production.Product P ON P.ProductID = SOD.ProductID
                INNER JOIN Production.ProductSubcategory SUBCAT ON SUBCAT.ProductCategoryID = P.ProductSubcategoryID
                INNER JOIN Production.ProductCategory CAT ON CAT.ProductCategoryID = SUBCAT.ProductSubcategoryID
                WHERE SOH.OrderDate BETWEEN '20130101' AND '20131231' AND SOH.OnlineOrderFlag = 1
                AND (P.MakeFlag = 1 OR P.FinishedGoodsFlag = 1) AND P.Color IN ('Black', 'Yellow')
                GROUP BY SOH.OrderDate, CAT.Name
                ORDER BY SOH.OrderDate, CAT.Name" },

            { "Query3", @"SELECT STOR.Name as StoreName, CAT.Name as CategoryName,
                SUM(SOD.OrderQty) AS TotalOrderQty, SUM(SOD.LineTotal) AS TotalLineTotal
                FROM Sales.SalesOrderDetail SOD
                INNER JOIN Sales.SalesOrderHeader SOH ON SOH.SalesOrderID = SOD.SalesOrderID
                INNER JOIN Production.Product P ON P.ProductID = SOD.ProductID
                INNER JOIN Production.ProductSubcategory SUBCAT ON SUBCAT.ProductCategoryID = P.ProductSubcategoryID
                INNER JOIN Production.ProductCategory CAT ON CAT.ProductCategoryID = SUBCAT.ProductSubcategoryID
                INNER JOIN Sales.Customer CUST ON CUST.CustomerID = SOH.CustomerID
                INNER JOIN Sales.Store STOR ON STOR.BusinessEntityID = CUST.StoreID
                WHERE SOH.OrderDate BETWEEN '20130101' AND '20131231' AND SOH.OnlineOrderFlag = 0
                AND (P.MakeFlag = 1 OR P.FinishedGoodsFlag = 1) AND P.Color IN ('Black', 'Yellow')
                GROUP BY STOR.Name, CAT.Name
                ORDER BY STOR.Name, CAT.Name" }
        };

        public Form1()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            cmbQueries.Items.AddRange(new[] { "Query 1", "Query 2", "Query 3" });
            cmbQueries.SelectedIndex = 0;

            chartPerformance.Series.Clear();
            var series = new Series("Execution Time (ms)")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.DodgerBlue
            };
            chartPerformance.Series.Add(series);

            dgvResults.AutoGenerateColumns = true;
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            string selectedQuery = cmbQueries.SelectedItem.ToString().Replace(" ", "");
            string sql = Queries[selectedQuery];

            txtResults.Clear();
            chartPerformance.Series[0].Points.Clear();
            dgvResults.DataSource = null;

            var results = RunPerformanceTest(selectedQuery, sql, 100);

            DisplayResults(results, selectedQuery);
        }

        private List<long> RunPerformanceTest(string queryName, string sql, int iterations)
        {
            var executionTimes = new List<long>();
            DataTable resultData = null;

            for (int i = 0; i < iterations; i++)
            {
                ClearSqlCache();

                var stopwatch = Stopwatch.StartNew();

                using (var connection = new SqlConnection(ConnectionString))
                using (var command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();

                        if (i == 0)
                        {
                            using (var adapter = new SqlDataAdapter(command))
                            {
                                resultData = new DataTable();
                                adapter.Fill(resultData);
                            }
                        }
                        else
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error executing query: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }

                stopwatch.Stop();
                executionTimes.Add(stopwatch.ElapsedMilliseconds);

                progressBar.Value = (int)((i + 1) * 100.0 / iterations);
                Application.DoEvents();
            }

            if (resultData != null)
            {
                BeginInvoke(new Action(() =>
                {
                    dgvResults.DataSource = resultData;
                    lblRowCount.Text = $"Row Count: {resultData.Rows.Count}";
                }));
            }

            return executionTimes;
        }

        private void ClearSqlCache()
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand("DBCC FREEPROCCACHE; DBCC DROPCLEANBUFFERS;", connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void DisplayResults(List<long> executionTimes, string queryName)
        {
            if (executionTimes == null || executionTimes.Count == 0) return;

            long totalTime = 0;
            long minTime = long.MaxValue;
            long maxTime = long.MinValue;

            foreach (var time in executionTimes)
            {
                totalTime += time;
                if (time < minTime) minTime = time;
                if (time > maxTime) maxTime = time;

                chartPerformance.Series[0].Points.AddY(time);
            }

            double avgTime = totalTime / (double)executionTimes.Count;

            txtResults.AppendText($"=== {queryName} Performance Results ===\r\n");
            txtResults.AppendText($"Total Executions: {executionTimes.Count}\r\n");
            txtResults.AppendText($"Total Time: {totalTime} ms\r\n");
            txtResults.AppendText($"Average Time: {avgTime:F2} ms\r\n");
            txtResults.AppendText($"Minimum Time: {minTime} ms\r\n");
            txtResults.AppendText($"Maximum Time: {maxTime} ms\r\n");
            txtResults.AppendText("\r\n");

            txtResults.AppendText("First 10 Execution Times (ms):\r\n");
            for (int i = 0; i < Math.Min(10, executionTimes.Count); i++)
            {
                txtResults.AppendText($"{i + 1}: {executionTimes[i]} ms\r\n");
            }
        }
        private void btnShowTop100_Click(object sender, EventArgs e)
        {
            if (cmbQueries.SelectedItem != null)
            {
                string selectedQuery = cmbQueries.SelectedItem.ToString().Replace(" ", "");
                string sql = Queries[selectedQuery];

                var times = RunPerformanceTest(selectedQuery, sql, 100);
                if (times != null)
                {
                    var form2 = new Form2(times);
                    form2.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir sorgu seçin.");
            }
        }

        private void btnOptimize_Click(object sender, EventArgs e)
        {
            string selectedQuery = cmbQueries.SelectedItem.ToString().Replace(" ", "");
            string optimizationInfo = GetOptimizationInfo(selectedQuery);

            MessageBox.Show(optimizationInfo, "Optimization Suggestions",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string GetOptimizationInfo(string queryName)
        {
            string rawText;

            switch (queryName)
            {
                case "Query1":
                    rawText = @"Optimization Suggestions for Query 1:

1. For filtering by order date, online order flag, and shipping address.
--CREATING AN INDEX:CREATE NONCLUSTERED INDEX IX1_SalesOrderHeader_OrderDate_OnlineFlag_Address
--ON Sales.SalesOrderHeader (OrderDate, OnlineOrderFlag, ShipToAddressID);
--DELETE INDEX:DROP INDEX IX1_SalesOrderHeader_OrderDate_OnlineFlag_Address ON Sales.SalesOrderHeader;
2. For join and aggregation in the SalesOrderDetail table.
--CREATING AN INDEX:CREATE NONCLUSTERED INDEX IX2_SalesOrderDetail_OrderID_Qty_Total
--ON Sales.SalesOrderDetail (SalesOrderID)
--INCLUDE (OrderQty, LineTotal);
--DELETE INDEX:DROP INDEX IX2_SalesOrderDetail_OrderID_Qty_Total ON Sales.SalesOrderDetail;
3. For join operations in the Address table.
--CREATING AN INDEX:CREATE NONCLUSTERED INDEX IX3_Address_AddressID_City_State
--ON Person.Address (AddressID)
--INCLUDE (City, StateProvinceID);
--DELETE INDEX:DROP INDEX IX3_Address_AddressID_City_State ON Person.Address;";
                    break;

                case "Query2":
                    rawText = @"Optimization Suggestions for Query 2:
1. Index for filtering on MakeFlag, FinishedGoodsFlag, and Color columns.
--CREATING AN INDEX:CREATE NONCLUSTERED INDEX IX2_Product_Make_Finish_Color
--ON Production.Product (MakeFlag, FinishedGoodsFlag, Color);
--DELETE INDEX:DROP INDEX IX2_Product_Make_Finish_Color ON Production.Product;
2. Index to speed up JOIN operations between ProductSubcategory and ProductCategory on ProductCategoryID.
--CREATING AN INDEX:CREATE NONCLUSTERED INDEX IX3_Subcategory_ProductCategoryID
--ON Production.ProductSubcategory (ProductCategoryID);
--DELETE INDEX:DROP INDEX IX3_Subcategory_ProductCategoryID ON Production.ProductSubcategory;";
                    break;

                case "Query3":
                    rawText = @"Optimization Suggestions for Query 3:
1. This index covers the SOH.OrderDate and SOH.OnlineOrderFlag columns, speeding up the filtering based on the date range.
--CREATING AN INDEX:CREATE INDEX IDX_SO_OrderDate_OnlineOrderFlag ON Sales.SalesOrderHeader(OrderDate, OnlineOrderFlag);
--DELETE INDEX:DROP INDEX IDX_SO_OrderDate_OnlineOrderFlag ON Sales.SalesOrderHeader;
2. This index speeds up the query by indexing the SalesOrderID and ProductID columns in the SalesOrderDetail table.
--CREATING AN INDEX:CREATE INDEX IDX_SalesOrderDetail_SalesOrderID_ProductID ON Sales.SalesOrderDetail(SalesOrderID, ProductID);
--DELETE INDEX:DROP INDEX IDX_SalesOrderDetail_SalesOrderID_ProductID ON Sales.SalesOrderDetail;
3. This index optimizes filtering of physical store orders within a specific date range
--CREATING AN INDEX:CREATE INDEX IDX_SOH_Date_Online_Store 
--ON Sales.SalesOrderHeader(OrderDate, OnlineOrderFlag)
--INCLUDE (SalesOrderID, CustomerID)
--WHERE OnlineOrderFlag = 0;
--DELETE INDEX:DROP INDEX IDX_SOH_Date_Online_Store ON Sales.SalesOrderHeader;";
                    break;

                default:
                    return "No optimization suggestions available for this query.";
            }

            string filtered = string.Join("\n",
                rawText.Split('\n')
                       .Where(line => !line.TrimStart().StartsWith("--"))
            );

            return filtered;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}