using System;
using System.Data.SqlClient;

namespace DatabaseSimulation
{
    public class TestDbConn
    {
        string connectionString = "Server=YAKISIKLI;Database=AdventureWorks2022;Trusted_Connection=True;";

        public void SimpleQueryForDbConnTest()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @" SELECT * FROM SALES.Currency
                                    WHERE CurrencyCode = 'TRL'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("-----------------------------------");

                            while (reader.Read())
                            {
                                String currencyCode = reader.GetString(0);
                                String Name = reader.GetString(1);
                                DateTime ModifiedDate = reader.GetDateTime(2);

                                Console.WriteLine($"{currencyCode,-12} | {Name} | {ModifiedDate,-18:yyyy-MM-dd HH:mm:ss}");
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"❌ SQL Hatası: {ex.Message} (Hata Kodu: {ex.Number})");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Genel Hata: {ex.Message}");
                }
            }
        }
    }
}