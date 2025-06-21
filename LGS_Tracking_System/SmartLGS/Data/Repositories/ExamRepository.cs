using SmartLGS.Core.Interfaces;
using SmartLGS.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLGS.Data.Repositories
{
    class ExamRepository : IExamRepository
    {
        private readonly string _connectionString;
        private readonly IMessageHelper _messageHelper;
        private readonly IValidationHelper _validationHelper;

        public ExamRepository(string connectionString, IMessageHelper messageHelper, IValidationHelper validationHelper)
        {
            _connectionString = connectionString;
            _messageHelper = messageHelper;
            _validationHelper = validationHelper;
        }
        public int AddExamTable(string examName, string examDate, int userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string query = "INSERT INTO Exams (ExamName, ExamDate, AddedBy) VALUES (@examName, @examDate, @addedBy); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@examName", examName);
                            cmd.Parameters.AddWithValue("@examDate", examDate);
                            cmd.Parameters.AddWithValue("@addedBy", userId);

                            object result = cmd.ExecuteScalar();
                            int examId = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(Convert.ToDecimal(result));

                            transaction.Commit();
                            return examId;
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _messageHelper.ShowError($"An error occurred: {ex.Message}");
                        return -1;

                    }
                }
            }
        }
        
        public int AddStudentExamsTable(int userId, int examId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string query = "INSERT INTO StudentExams (StudentId, ExamId) VALUES (@studentId, @examId); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@studentId", userId);
                            cmd.Parameters.AddWithValue("@examId", examId);
                            object result = cmd.ExecuteScalar();
                            int studentExamId = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);

                            transaction.Commit();
                            return studentExamId;
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _messageHelper.ShowError($"AddStudentExamsTable patladı: {ex.Message}");
                    }
                }
            }
            return -1;
        }
        
        public void AddExamSubjectDetails(int studentExamId, int subjectId, int correct, int wrong, int empty)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string query = "INSERT INTO Results (StudentExamId, SubjectId, TrueAnswers, FalseAnswers, BlankAnswers) VALUES(@StudentExamId, @SubjectId, @TrueAnswers, @FalseAnswers, @BlankAnswers)";
                        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@StudentExamId", studentExamId);
                            cmd.Parameters.AddWithValue("@SubjectId", subjectId);
                            cmd.Parameters.AddWithValue("@TrueAnswers", correct);
                            cmd.Parameters.AddWithValue("@FalseAnswers", wrong);
                            cmd.Parameters.AddWithValue("@BlankAnswers", empty);
                            cmd.ExecuteNonQuery();

                            transaction.Commit();
                        }

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _messageHelper.ShowError($"AddExamSubjectDetails patladı: {ex.Message}");
                    }
                }
            }
        }
        
        public List<AllStudentsExamReport> GetAllStudentsExamReports()
        {
            var reports = new List<AllStudentsExamReport>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
        SELECT 
            Stu.UserId AS StudentId,
            Stu.Name AS StudentName,
            Stu.Surname AS StudentSurName,
            Exam.ExamId,
            Exam.ExamName,
            Exam.ExamDate,
            SUM(Res.TrueAnswers) AS CorrectCount,
            SUM(Res.FalseAnswers) AS WrongCount,
            SUM(Res.BlankAnswers) AS BlankCount,
            SUM(Res.TrueAnswers + Res.FalseAnswers + Res.BlankAnswers) AS TotalQuestions
        FROM Exams AS Exam
        INNER JOIN StudentExams AS StuEx ON StuEx.ExamId = Exam.ExamId
        INNER JOIN Results AS Res ON Res.StudentExamId = StuEx.StudentExamId
        INNER JOIN Students AS Stu ON Stu.UserId = StuEx.StudentId
        GROUP BY Stu.UserId, Stu.Name, Stu.Surname, Exam.ExamId, Exam.ExamName, Exam.ExamDate
        ORDER BY Stu.UserId, Exam.ExamDate DESC;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int correct = Convert.ToInt32(reader["CorrectCount"]);
                            int wrong = Convert.ToInt32(reader["WrongCount"]);

                            reports.Add(new AllStudentsExamReport
                            {
                                StudentId = Convert.ToInt32(reader["StudentId"]),
                                StudentName = reader["StudentName"].ToString(),
                                StudentSurName = reader["StudentSurName"].ToString(),
                                ExamId = Convert.ToInt32(reader["ExamId"]),
                                ExamName = reader["ExamName"].ToString(),
                                ExamDate = Convert.ToDateTime(reader["ExamDate"]),
                                CorrectCount = correct,
                                WrongCount = wrong,
                                BlankCount = Convert.ToInt32(reader["BlankCount"]),
                                TotalQuestions = Convert.ToInt32(reader["TotalQuestions"]),
                                SuccessRate = _validationHelper.CalculateSuccessRate(correct, wrong)
                            });
                        }
                    }
                }
            }

            return reports;
        }

        public List<StudentExamReport> GetUserExams(int userId)
        {
            var reports = new List<StudentExamReport>();

            string query = @"
        SELECT 
            Stu.UserID AS StudentId,
            Exam.ExamId,
            Exam.ExamName,
            Exam.ExamDate,
            SUM(Res.TrueAnswers) AS CorrectCount,
            SUM(Res.FalseAnswers) AS WrongCount,
            SUM(Res.BlankAnswers) AS BlankCount,
            SUM(Res.TrueAnswers + Res.FalseAnswers + Res.BlankAnswers) AS TotalQuestions
        FROM Exams AS Exam
        INNER JOIN StudentExams AS StuEx ON StuEx.ExamId = Exam.ExamId
        INNER JOIN Results AS Res ON Res.StudentExamId = StuEx.StudentExamId
        INNER JOIN Subject AS Sub ON Sub.SubjectId = Res.SubjectId
        INNER JOIN Students AS Stu ON StuEx.StudentId = Stu.UserId
        WHERE Stu.UserId = @UserId
        GROUP BY Stu.UserId, Exam.ExamId, Exam.ExamName, Exam.ExamDate
        ORDER BY Exam.ExamDate DESC;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int correct = Convert.ToInt32(reader["CorrectCount"]);
                        int wrong = Convert.ToInt32(reader["WrongCount"]);
                        int blank = Convert.ToInt32(reader["BlankCount"]);

                        reports.Add(new StudentExamReport
                        {
                            StudentId = Convert.ToInt32(reader["StudentId"]),
                            ExamId = Convert.ToInt32(reader["ExamId"]),
                            ExamName = reader["ExamName"].ToString(),
                            ExamDate = Convert.ToDateTime(reader["ExamDate"]),
                            CorrectCount = correct,
                            WrongCount = wrong,
                            BlankCount = blank,
                            TotalQuestions = Convert.ToInt32(reader["TotalQuestions"]),
                            SuccessRate = _validationHelper.CalculateSuccessRate(correct, wrong)
                        });
                    }
                }
            }
            return reports;
        }

        public List<ExamSubjectDetail> GetExamSubjectDetails(int examId, int userId)
        {
            var details = new List<ExamSubjectDetail>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
            SELECT 
                Sub.SubjectName, 
                Res.TrueAnswers, 
                Res.FalseAnswers, 
                Res.BlankAnswers,
                Sub.TotalQuestions
            FROM Exams AS Exam
                INNER JOIN StudentExams AS StuEx ON StuEx.ExamId = Exam.ExamId
                INNER JOIN Results AS Res ON Res.StudentExamId = StuEx.StudentExamId
                INNER JOIN Subject AS Sub ON Sub.SubjectId = Res.SubjectId 
                INNER JOIN Students AS Stu ON StuEx.StudentId = Stu.UserId
            WHERE Exam.ExamId = @ExamId AND Stu.UserId = @UserId
            ORDER BY Exam.ExamDate DESC;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ExamId", examId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            details.Add(new ExamSubjectDetail
                            {
                                SubjectName = reader["SubjectName"].ToString(),
                                TrueAnswers = Convert.ToInt32(reader["TrueAnswers"]),
                                FalseAnswers = Convert.ToInt32(reader["FalseAnswers"]),
                                BlankAnswers = Convert.ToInt32(reader["BlankAnswers"]),
                                TotalQuestions = Convert.ToInt32(reader["TotalQuestions"])
                            });
                        }
                    }
                }
            }

            return details;
        }

        public int GetExamId(string examName)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT ExamId FROM Exams WHERE ExamName LIKE @ExamName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@ExamName", $"%{examName}%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return -1;
                        }
                        int examId = Convert.ToInt32(reader["ExamId"]);
                        if (reader.Read())
                        {
                            throw new InvalidOperationException($"Multiple exams found matching '{examName}'.");
                        }
                        return examId;
                    }
                }
            }
        }

        public void DeleteStudentAllExams(int userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string query1 = @"
                    DELETE Res 
                    FROM Results Res 
                    INNER JOIN StudentExams Se ON Se.StudentExamId = Res.StudentExamId 
                    WHERE Se.StudentId = @userId;";
                        using (SqlCommand cmd = new SqlCommand(query1, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@userId", userId);
                            cmd.ExecuteNonQuery();
                        }

                        string query2 = @"
                    DELETE FROM StudentExams 
                    WHERE StudentId = @userId 
                    OR ExamId IN (SELECT ExamId FROM Exams WHERE AddedBy = @userId);";
                        using (SqlCommand cmd2 = new SqlCommand(query2, conn, transaction))
                        {
                            cmd2.Parameters.AddWithValue("@userId", userId);
                            cmd2.ExecuteNonQuery();
                        }

                        string query3 = "DELETE FROM Exams WHERE AddedBy = @userId;";
                        using (SqlCommand cmd3 = new SqlCommand(query3, conn, transaction))
                        {
                            cmd3.Parameters.AddWithValue("@userId", userId);
                            cmd3.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _messageHelper.ShowError($"DeleteStudentAllExams patladı: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        public string GetExamName(int examId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT ExamName FROM Exams where ExamId = @examId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@examId", examId);
                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : null;
                }
            }
        }

        public bool IsStudentExam(int examId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM StudentExams WHERE ExamId = @examId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@examId", examId);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public void DeleteExamDirectlyById(int examId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Exams WHERE ExamId = @examId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@examId", examId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        
        public void DeleteExamById(int examId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int resultsDeleted = 0, studentExamsDeleted = 0, examsDeleted = 0;

                        string query1 = @"
                    DELETE Res
                    FROM Results Res
                    INNER JOIN StudentExams Se ON Se.StudentExamId = Res.StudentExamId
                    WHERE Se.ExamId = @examId";
                        using (SqlCommand cmd = new SqlCommand(query1, conn, transaction))
                        {
                            cmd.Parameters.Add("@examId", SqlDbType.Int).Value = examId;
                            resultsDeleted = cmd.ExecuteNonQuery();
                        }

                        string query2 = "DELETE FROM StudentExams WHERE ExamId = @examId";
                        using (SqlCommand cmd = new SqlCommand(query2, conn, transaction))
                        {
                            cmd.Parameters.Add("@examId", SqlDbType.Int).Value = examId;
                            studentExamsDeleted = cmd.ExecuteNonQuery();
                        }

                        string query3 = "DELETE FROM Exams WHERE ExamId = @examId";
                        using (SqlCommand cmd = new SqlCommand(query3, conn, transaction))
                        {
                            cmd.Parameters.Add("@examId", SqlDbType.Int).Value = examId;
                            examsDeleted = cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _messageHelper.ShowError($"Error deleting exam: {ex.Message}");
                    }
                }
            }
        }

        public List<int> GetExamIdsByName(string examName)
        {
            List<int> examIds = new List<int>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT ExamId FROM Exams WHERE ExamName = @examName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@examName", examName);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            examIds.Add(reader.GetInt32(0));
                        }
                    }
                }
            }
            return examIds;
        }

        public List<LastNExam> GetLastExamNetSummaries(int userId, int count)
        {
            var list = new List<LastNExam>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
        SELECT TOP (@N)
            Exam.ExamId,
            Exam.ExamDate,
            Exam.ExamName,
            CAST(SUM(CASE WHEN Res.SubjectId = 6 THEN Res.TrueAnswers - (Res.FalseAnswers / 4.0) END) AS DECIMAL(5,2)) AS [Turkce],
            CAST(SUM(CASE WHEN Res.SubjectId = 4 THEN Res.TrueAnswers - (Res.FalseAnswers / 4.0) END) AS DECIMAL(5,2)) AS [Inkilap],
            CAST(SUM(CASE WHEN Res.SubjectId = 1 THEN Res.TrueAnswers - (Res.FalseAnswers / 4.0) END) AS DECIMAL(5,2)) AS [Din],
            CAST(SUM(CASE WHEN Res.SubjectId = 3 THEN Res.TrueAnswers - (Res.FalseAnswers / 4.0) END) AS DECIMAL(5,2)) AS [Ingilizce],
            CAST(SUM(CASE WHEN Res.SubjectId = 5 THEN Res.TrueAnswers - (Res.FalseAnswers / 4.0) END) AS DECIMAL(5,2)) AS [Matematik],
            CAST(SUM(CASE WHEN Res.SubjectId = 2 THEN Res.TrueAnswers - (Res.FalseAnswers / 4.0) END) AS DECIMAL(5,2)) AS [Fen]
        FROM Exams AS Exam
        INNER JOIN StudentExams AS StuEx ON StuEx.ExamId = Exam.ExamId
        INNER JOIN Results AS Res ON Res.StudentExamId = StuEx.StudentExamId
        INNER JOIN Subject AS Sub ON Sub.SubjectId = Res.SubjectId
        INNER JOIN Students AS Stu ON Stu.UserId = StuEx.StudentId
        WHERE Stu.UserId = @UserId
        GROUP BY Exam.ExamId, Exam.ExamDate, Exam.ExamName
        ORDER BY Exam.ExamDate DESC;";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@N", count);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new LastNExam
                        {
                            ExamId = reader.GetInt32(0),
                            ExamDate = reader.GetDateTime(1),
                            ExamName = reader.GetString(2),
                            TurkceNet = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3),
                            InkilapNet = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4),
                            DinNet = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5),
                            IngilizceNet = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6),
                            MatematikNet = reader.IsDBNull(7) ? 0 : reader.GetDecimal(7),
                            FenNet = reader.IsDBNull(8) ? 0 : reader.GetDecimal(8),
                        });
                    }
                }
            }

            return list;
        }

    }
}
