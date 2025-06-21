using SmartLGS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SmartLGS.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly IMessageHelper _messageHelper;

        public UserRepository(string connectionString, IMessageHelper messageHelper)
        {
            _connectionString = connectionString;
            _messageHelper = messageHelper;
        }
        
        public (string role, int id) GetUserRoleId(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT UserId, Role FROM Users WHERE Username = @Username AND Password = @Password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    conn.Open();
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string role = reader["Role"] != DBNull.Value ? reader["Role"].ToString() : null;
                            int id = reader["UserID"] != DBNull.Value ? Convert.ToInt32(reader["UserID"]) : -1;
                            return (role, id);
                        }
                        else
                        {
                            return (null, -1);
                        }
                    }
                }
            }
        }

        public string GetUserRole(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "select Role from users where UserId = @UserId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : null;
                }
            }
        }

        public int AddNewStudent(string name, string surname, string username, string password,string gender, string address, string phone, string email, string city)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int userId = InsertUserTable(conn, transaction, username, password);

                        InsertStudentTable(conn, transaction, userId, name, surname, gender,
                                         address, phone, email, city);

                        transaction.Commit();
                        return userId;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Öğrenci kaydı oluşturulurken hata oluştu", ex);
                    }
                }
            }
        }

        private int InsertUserTable(SqlConnection conn, SqlTransaction transaction,string username, string password)
        {
            string query = @"INSERT INTO Users (Username, Password, Role) 
                    VALUES (@username, @password, @role);
                    SELECT SCOPE_IDENTITY();";

            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@role", "Student");

                object result = cmd.ExecuteScalar();
                return (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
            }
        }

        public void InsertStudentTable(SqlConnection conn, SqlTransaction transaction, int userId, string name, string surname, string gender, string address, string phone, string email, string city)
        {
            string query = @"INSERT INTO Students (UserID, Name, Surname, Gender, 
                    Address, Phone, Email, City)
                    VALUES (@UserID, @name, @surname, @gender, @address, 
                    @phone, @email, @city)";

            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@name", name ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@surname", surname ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@gender", gender ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@address", address ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@phone", phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@email", email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@city", city ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        public string GetUserName(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT Username FROM Users WHERE UserId = @UserId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", id);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : null;
                }
            }
        }

        public (string Name, string Surname) GetStuNameSurname(int stuId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT Name, Surname FROM Students WHERE UserId = @StuId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StuId", stuId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string stuName = reader["Name"].ToString();
                                string stuSurname = reader["Surname"].ToString();
                                return (stuName, stuSurname);
                            }
                            else
                            {
                                _messageHelper.ShowError("Öğrenci bulunamadı.");
                                return ("", "");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError($"UserRepository.GetStuNameSurname() hatası: {ex.Message}");
                return ("", "");
            }
        }

        public List<int> GetUserId(string stuName, string stuSurname)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        List<int> userIds = new List<int>();
                        string query = "SELECT UserId FROM Students WHERE Name LIKE @name AND Surname LIKE @surname";
                        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@name", $"%{stuName}%");
                            cmd.Parameters.AddWithValue("@surname", $"%{stuSurname}%");
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    userIds.Add(reader.GetInt32(reader.GetOrdinal("UserId")));
                                }
                                if (userIds.Count == 0)
                                {
                                    _messageHelper.ShowError($"{stuName}, {stuSurname} adlı öğrenci bulunamadı");
                                    return null;
                                }
                            }
                            return userIds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError($"GetUserId patladı: {ex.Message}");
                return null;
            }
        }
        
        public void DeleteStudent(int userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string query = "DELETE FROM Students WHERE UserId = @userId";
                        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@userId", userId);
                            cmd.ExecuteNonQuery();
                        }
                        string query2= "DELETE FROM Users WHERE UserId = @userId";
                        using (SqlCommand cmd = new SqlCommand(query2, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@userId", userId);
                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _messageHelper.ShowError($"DeleteStudent patladı: {ex.Message}");
                    }
                }
            }
        }
        
        public SmartLGS.Core.Models.Student GetStudentTable(int userId)
        {
            SmartLGS.Core.Models.Student student = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Students WHERE UserId = @UserId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            student = new SmartLGS.Core.Models.Student
                            {
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name")),
                                Surname = reader.IsDBNull(reader.GetOrdinal("Surname")) ? null : reader.GetString(reader.GetOrdinal("Surname")),
                                Gender = reader.IsDBNull(reader.GetOrdinal("Gender")) ? null : reader.GetString(reader.GetOrdinal("Gender")),
                                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                                Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                                City = reader.IsDBNull(reader.GetOrdinal("City")) ? null : reader.GetString(reader.GetOrdinal("City"))
                            };
                        }
                    }
                }

                if (student != null)
                {
                    string query2 = "SELECT Username FROM Users WHERE UserId = @UserId";
                    using (SqlCommand cmd2 = new SqlCommand(query2, conn))
                    {
                        cmd2.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = cmd2.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                student.Username = reader.IsDBNull(reader.GetOrdinal("Username")) ? null : reader.GetString(reader.GetOrdinal("Username"));
                            }
                        }
                    }
                }
            }

            return student;
        }

        public void UpdateStudentTable(int userId, string name, string surname, string gender, string address, string phone, string email, string city)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string query = @"UPDATE Students 
                                        SET Name = @name, Surname = @surname, Gender = @gender, 
                                            Address = @address, Phone = @phone, Email = @email, City = @city
                                        WHERE UserID = @UserID";

                        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            cmd.Parameters.AddWithValue("@name", name ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@surname", surname ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@gender", gender ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@address", address ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@phone", phone ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@email", email ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@city", city ?? (object)DBNull.Value);

                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Öğrenci güncelleme hatası: {ex.Message}", ex);
                    }
                }
            }
        }

        public bool isEmailExist(string Email)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "Select COUNT(*) From Students Where Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", Email);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        
        public bool isUsernameExist(string username)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public bool UpdateUsernameControl(string username, int userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @username AND UserId != @userId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public bool UpdateEmailControl(string email, int userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Students WHERE Email = @email AND UserId != @userId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}
