using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Linq;

namespace DatabaseSimulation
{
    public partial class Form1 : Form
    {
        private readonly string connectionString = "Data Source= **ServerName** ;Initial Catalog=AdventureWorks2021;Integrated Security=True;Encrypt=False";
        private Random random = new Random();
        private List<SimulationResult> results = new List<SimulationResult>();

        public Form1()
        {
            InitializeComponent();
            InitializeIsolationLevels();
        }

        private void InitializeIsolationLevels()
        {
            comboBoxIsolationLevel.SelectedIndex = 0;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            results.Clear();
            textBoxResults.Clear();
            progressBar.Value = 0;
            buttonStart.Enabled = false;
            results.Clear();
            textBoxResults.Clear();

            int typeAUsers = (int)numericUpDownTypeA.Value;
            int typeBUsers = (int)numericUpDownTypeB.Value;
            string isolationLevel = comboBoxIsolationLevel.SelectedItem.ToString();

            progressBar.Maximum = typeAUsers + typeBUsers;
            progressBar.Value = 0;

            try
            {
                textBoxResults.AppendText($"Simulation is starting\r\nCount of the typeA user:{typeAUsers}\r\nCount of the typeB user:{typeBUsers}\r\n\r\n");
                RunSimulation(typeAUsers, typeBUsers, isolationLevel);
            }
            catch (Exception ex)
            {
                textBoxResults.AppendText($"Error: {ex.Message}\n");
            }
            finally
            {
                buttonStart.Enabled = true;
                progressBar.Value = 0;
            }
        }
        
        private void RunSimulation(int typeAUsers, int typeBUsers, string isolationLevel)
        {
            List<Thread> allThreads = new List<Thread>();

            for (int i = 0; i < typeAUsers; i++)
            {
                int userId = i + 1;
                Thread thread = new Thread(() => RunTypeAUser(userId, isolationLevel));
                allThreads.Add(thread);
                thread.Start();
            }

            for (int i = 0; i < typeBUsers; i++)
            {
                int userId = i + 1;
                Thread thread = new Thread(() => RunTypeBUser(userId, isolationLevel));
                allThreads.Add(thread);
                thread.Start();
            }

            Thread mainThread = new Thread(() =>
            {
                foreach (var thread in allThreads)
                {
                    thread.Join();
                }

                CalculateAndDisplayResults();
            });

            mainThread.Start();
        }

        private void RunTypeAUser(int userId, string isolationLevel)
        {
            SimulationResult result = new SimulationResult { UserType = "A", UserId = userId };
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            List<string[]> dates = new List<string[]>
    {
        new[] {"20110101", "20111231"},
        new[] {"20120101", "20121231"},
        new[] {"20130101", "20131231"},
        new[] {"20140101", "20141231"},
        new[] {"20150101", "20151231"}
    };

            int deadlockCount = 0;

            for (int i = 0; i < 100; i++)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Set the isolation level before starting the transaction
                        SqlCommand cmd = connection.CreateCommand();
                        cmd.CommandText = $"SET TRANSACTION ISOLATION LEVEL {isolationLevel}; BEGIN TRANSACTION;";
                        cmd.ExecuteNonQuery();

                        foreach (var range in dates)
                        {
                            if (random.NextDouble() < 0.5)
                            {
                                string begin = range[0];
                                string end = range[1];

                                SqlCommand cmd2 = connection.CreateCommand();
                                cmd2.CommandText = @"
                            UPDATE Sales.SalesOrderDetail 
                               SET UnitPrice = UnitPrice * 10.0 / 10.0 
                             WHERE UnitPrice > 100 
                               AND EXISTS (
                                    SELECT * FROM Sales.SalesOrderHeader 
                                     WHERE SalesOrderHeader.SalesOrderID = SalesOrderDetail.SalesOrderID 
                                       AND SalesOrderHeader.OrderDate BETWEEN @Begin AND @End 
                                       AND SalesOrderHeader.OnlineOrderFlag = 1
                               )";
                                cmd2.Parameters.AddWithValue("@Begin", begin);
                                cmd2.Parameters.AddWithValue("@End", end);
                                cmd2.ExecuteNonQuery();
                            }
                        }

                        // Commit the transaction after all operations
                        SqlCommand cmd3 = connection.CreateCommand();
                        cmd3.CommandText = "COMMIT;";
                        cmd3.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    // Handle deadlocks explicitly
                    if (ex.Number == 1205)
                    {
                        deadlockCount++;
                    }
                    else
                    {
                        Console.WriteLine($"SQL Error: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            stopwatch.Stop();
            result.Duration = stopwatch.ElapsedMilliseconds;
            result.DeadlockCount = deadlockCount;

            lock (results)
            {
                results.Add(result);
            }

            BeginInvoke(new Action(() => progressBar.Value++));

            // Update the results in the UI thread
            BeginInvoke(new Action(() =>
            {
                textBoxResults.AppendText($"User A{userId} finished.\r\n");
                textBoxResults.AppendText($"Duration: {result.Duration} ms, Deadlocks: {result.DeadlockCount}\r\n");
            }));
        }

        private void RunTypeBUser(int userId, string isolationLevel)
        {
            SimulationResult result = new SimulationResult { UserType = "B", UserId = userId };
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            List<string[]> dates = new List<string[]>
    {
        new[] {"20110101", "20111231"},
        new[] {"20120101", "20121231"},
        new[] {"20130101", "20131231"},
        new[] {"20140101", "20141231"},
        new[] {"20150101", "20151231"}
    };

            int deadlockCount = 0;

            for (int i = 0; i < 100; i++)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Set the isolation level before starting the transaction
                        SqlCommand cmd = connection.CreateCommand();
                        cmd.CommandText = $"SET TRANSACTION ISOLATION LEVEL {isolationLevel}; BEGIN TRANSACTION;";
                        cmd.ExecuteNonQuery();

                        foreach (var range in dates)
                        {
                            if (random.NextDouble() < 0.5)
                            {
                                string begin = range[0];
                                string end = range[1];

                                SqlCommand cmd2 = connection.CreateCommand();
                                cmd2.CommandText = @"
                            SELECT SUM(Sales.SalesOrderDetail.OrderQty)
                              FROM Sales.SalesOrderDetail 
                             WHERE UnitPrice > 100 
                               AND EXISTS (
                                    SELECT * FROM Sales.SalesOrderHeader 
                                     WHERE SalesOrderHeader.SalesOrderID = SalesOrderDetail.SalesOrderID 
                                       AND SalesOrderHeader.OrderDate BETWEEN @Begin AND @End 
                                       AND SalesOrderHeader.OnlineOrderFlag = 1
                                )";
                                cmd2.Parameters.AddWithValue("@Begin", begin);
                                cmd2.Parameters.AddWithValue("@End", end);
                                cmd2.ExecuteScalar();
                            }
                        }

                        // Commit the transaction after all operations
                        SqlCommand cmd3 = connection.CreateCommand();
                        cmd3.CommandText = "COMMIT;";
                        cmd3.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    // Handle deadlocks explicitly
                    if (ex.Number == 1205)
                    {
                        deadlockCount++;
                    }
                    else
                    {
                        Console.WriteLine($"SQL Error: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            stopwatch.Stop();
            result.Duration = stopwatch.ElapsedMilliseconds;
            result.DeadlockCount = deadlockCount;

            lock (results)
            {
                results.Add(result);
            }

            BeginInvoke(new Action(() => progressBar.Value++));

            // Update the results in the UI thread
            BeginInvoke(new Action(() =>
            {
                textBoxResults.AppendText($"User B{userId} finished.\r\n");
                textBoxResults.AppendText($"Duration: {result.Duration} ms, Deadlocks: {result.DeadlockCount}\r\n");
            }));
        }
        
        private void CalculateAndDisplayResults()
        {
            double totalTypeADuration = 0, totalTypeBDuration = 0;
            int totalTypeADeadlocks = 0, totalTypeBDeadlocks = 0;
            int typeACount = 0, typeBCount = 0;

            foreach (var result in results)
            {
                if (result.UserType == "A")
                {
                    totalTypeADuration += result.Duration;
                    totalTypeADeadlocks += result.DeadlockCount;
                    typeACount++;
                }
                else if (result.UserType == "B")
                {
                    totalTypeBDuration += result.Duration;
                    totalTypeBDeadlocks += result.DeadlockCount;
                    typeBCount++;
                }
            }

            double averageA = typeACount > 0 ? totalTypeADuration / typeACount : 0;
            double averageB = typeBCount > 0 ? totalTypeBDuration / typeBCount : 0;

            BeginInvoke((MethodInvoker)delegate
            {
                textBoxResults.AppendText($"------------------------------------------------------------------------------------------------\r\n");
                textBoxResults.AppendText($"[Sonuç] A Tipi ortalama süre: {averageA:F2} ms, Toplam Deadlock: {totalTypeADeadlocks}\r\n");
                textBoxResults.AppendText($"[Sonuç] B Tipi ortalama süre: {averageB:F2} ms, Toplam Deadlock: {totalTypeBDeadlocks}\r\n");
                textBoxResults.AppendText($"------------------------------------------------------------------------------------------------\r\n");

                buttonStart.Enabled = true;
                progressBar.Value = 0;

                try
                {
                    Console.Beep(800, 1000);
                }
                catch (Exception ex)
                {
                    textBoxResults.AppendText($"Beep Error: {ex.Message}\r\n");
                }
            });
        }
    }

    public class SimulationResult
    {
        public string UserType { get; set; }
        public int UserId { get; set; }
        public long Duration { get; set; }
        public int DeadlockCount { get; set; }
    }
}