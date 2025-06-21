using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Register : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] != null)
        {
            string username = Session["Username"].ToString();
            userInfo.Text = "Hoşgeldin " + username;
            if (!IsPostBack)
            {
                getBranch();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void getBranch()
    {
        string query = "select BranchID, BranchName from Branch ";
        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(query, conn);
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        Location.Items.Add(new ListItem("Select Branch", ""));
        while (reader.Read())
        {
            string branchId = reader["BranchID"].ToString();
            string branchName = reader["BranchName"].ToString();
            Location.Items.Add(new ListItem(branchName, branchId));
        }
        conn.Close();
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        int parentID = getParentInformation();
        string StudentID = getStudentID();
        setStudentInformation(parentID, StudentID);
        setPerformance(StudentID);
        setMembershipInformation(StudentID);
    }

    private void setPerformance(string StudentID)
    {
        string studentHeight = StudentHeight.Text.ToString();
        string studentWeight = StudentWeight.Text.ToString();
        string studentMuscleWeight = StudentMuscleWeight.Text.ToString();
        string verticalJump = VerticalJump.Text.ToString();
        string startDateText = StartDate.Text;
        int selectedMonth = -1;

        if (!string.IsNullOrEmpty(startDateText) && DateTime.TryParse(startDateText, out DateTime parsedDate))
        {
            selectedMonth = parsedDate.Month;
            Console.WriteLine($"Seçilen Ay: {selectedMonth}");
        }

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                conn.Open();

                string insertQuery = @"INSERT INTO Performance 
                                (StudentID, Height, Weight, MuscleMass, Jump, Month)
                                VALUES (@StudentID, @Height, @Weight, @MuscleMass, @Jump, @Month)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", Convert.ToInt32(StudentID));
                    cmd.Parameters.AddWithValue("@Height", string.IsNullOrEmpty(studentHeight) ? DBNull.Value : (object)Convert.ToDouble(studentHeight));
                    cmd.Parameters.AddWithValue("@Weight", string.IsNullOrEmpty(studentWeight) ? DBNull.Value : (object)Convert.ToDouble(studentWeight));
                    cmd.Parameters.AddWithValue("@MuscleMass", string.IsNullOrEmpty(studentMuscleWeight) ? DBNull.Value : (object)Convert.ToDouble(studentMuscleWeight));
                    cmd.Parameters.AddWithValue("@Jump", string.IsNullOrEmpty(verticalJump) ? DBNull.Value : (object)Convert.ToDouble(verticalJump));
                    cmd.Parameters.AddWithValue("@Month", selectedMonth);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Performans başarıyla eklendi. Etkilenen satır: {rowsAffected}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");
                throw;
            }
        }
    }

    private void setMembershipInformation(string studentID)
    {
        string paymentDate = StartDate.Text;
        string paymentType = PaymentOptions.SelectedValue.ToString();
        string paymentMethod = PaymentMethod.SelectedValue.ToString();
        string amount = Amount.Text.ToString();

        DateTime parsedPaymentDate;
        if (!DateTime.TryParse(paymentDate, out parsedPaymentDate))
        {
            Console.WriteLine("Geçerli bir ödeme tarihi girilmedi.");
            return;
        }

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                conn.Open();

                string insertQuery = @"INSERT INTO dbo.PaymentPlan 
                                   (StudentID, PaymentType, PaymentMethod, Amount, PaymentDate) 
                                   VALUES (@StudentID, @PaymentType, @PaymentMethod, @Amount, @PaymentDate)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                    cmd.Parameters.AddWithValue("@PaymentType", paymentType);
                    cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    cmd.Parameters.AddWithValue("@Amount", string.IsNullOrEmpty(amount) ? 0 : float.Parse(amount));
                    cmd.Parameters.AddWithValue("@PaymentDate", parsedPaymentDate);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Ödeme bilgisi başarıyla eklendi. Etkilenen satır sayısı: {rowsAffected}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");
                throw;
            }
        }
    }
    
    private int getParentInformation()
    {
        string parentName = ParentName.Text;
        string parentSurname = ParentSurname.Text;
        string parentEmail = ParentEmail.Text;
        string parentPhone = ParentPhone.Text;
        int parentId = 0;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string checkQuery = "SELECT COUNT(*) FROM Parent WHERE Name = @parentName AND Surname = @parentSurname";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@parentName", parentName);
                    checkCmd.Parameters.AddWithValue("@parentSurname", parentSurname);

                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        Console.WriteLine("Bu isim ve soyad zaten mevcut.");
                        return -1;
                    }
                }

                string query = "SELECT TOP 1 ParentID FROM Parent ORDER BY ParentID DESC;";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            parentId = reader.GetInt32(0);
                        }
                    }
                }

                parentId++;

                string query1 = "INSERT INTO Parent (ParentID, Name, Surname, Email, Phone) VALUES (@parentId, @parentName, @parentSurname, @parentEmail, @parentPhone)";
                using (SqlCommand cmd1 = new SqlCommand(query1, conn))
                {
                    cmd1.Parameters.AddWithValue("@parentId", parentId);
                    cmd1.Parameters.AddWithValue("@parentName", parentName);
                    cmd1.Parameters.AddWithValue("@parentSurname", parentSurname);
                    cmd1.Parameters.AddWithValue("@parentEmail", parentEmail);
                    cmd1.Parameters.AddWithValue("@parentPhone", parentPhone);

                    int rowsAffected = cmd1.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} satır eklendi.");

                    return parentId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bir hata oluştu: " + ex.Message);
                return -1;
            }
        }
    }

    private void setStudentInformation(int parentID, string StudentID)
    {
        string studentName = StudentName.Text.ToString();
        string studentSurname = StudentSurname.Text.ToString();
        string studentAge = StudentAge.Text.ToString();
        string registrationDate = !string.IsNullOrEmpty(StartDate.Text) && DateTime.TryParse(StartDate.Text, out DateTime parsedDate)
                                      ? parsedDate.ToString("yyyy-MM-dd")
                                      : string.Empty;

        string studentHeight = StudentHeight.Text.ToString();
        string studentWeight = StudentWeight.Text.ToString();
        string studentMuscleWeight = StudentMuscleWeight.Text.ToString();
        string verticalJump = VerticalJump.Text.ToString();

        string branchIDText = Location.Text.ToString();
        int branchID;

        if (int.TryParse(branchIDText, out branchID))
        {
            Console.WriteLine($"BranchID: {branchID}");
        }
        else
        {
            Console.WriteLine("BranchID geçersiz.");
        }

        string query = "INSERT INTO STUDENT (StudentID, ParentID, BranchID, Name, Surname, Age, RegistrationDate, SeparationDate, AppointmentID, image) " +
               "VALUES (@StudentID, @ParentID, @BranchID, @StudentName, @StudentSurname, @StudentAge, @RegistrationDate, NULL, NULL, NULL)";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", Convert.ToInt32(StudentID));
                    cmd.Parameters.AddWithValue("@ParentID", parentID);
                    cmd.Parameters.AddWithValue("@BranchID", branchID);
                    cmd.Parameters.AddWithValue("@StudentName", studentName);
                    cmd.Parameters.AddWithValue("@StudentSurname", studentSurname);
                    cmd.Parameters.AddWithValue("@StudentAge", Convert.ToInt32(studentAge));

                    cmd.Parameters.AddWithValue("@RegistrationDate", string.IsNullOrEmpty(registrationDate) ? DBNull.Value : (object)registrationDate);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Öğrenci başarıyla kaydedildi.");
                    }
                    else
                    {
                        Console.WriteLine("Öğrenci kaydedilemedi.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bir hata oluştu: " + ex.Message);
            }
        }

    }

    private string getStudentID()
    {
        string studentId = "1";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT TOP 1 StudentId FROM Student ORDER BY StudentId DESC";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            studentId = (reader["StudentId"] != DBNull.Value) ? (Convert.ToInt32(reader["StudentId"]) + 1).ToString() : "1";
                        }
                        else
                        {
                            studentId = "1";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Bir hata oluştu: " + ex.Message);
                    studentId = "1";
                }
            }
        }
        return studentId;
    }
}