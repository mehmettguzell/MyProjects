using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_StudentPaymentPlan : System.Web.UI.Page
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
                LoadStudents();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
    //string studentId = studentSelect.SelectedValue;
    private void LoadStudents()
    {
        string query = "SELECT NAME, Surname, StudentID FROM Student";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                studentSelect.Items.Clear();
                studentSelect.Items.Add(new ListItem("Select Student", ""));

                while (reader.Read())
                {
                    string StudentID = reader["StudentID"].ToString();
                    string studentName = reader["NAME"].ToString();
                    string studentSurName = reader["Surname"].ToString();
                    studentSelect.Items.Add(new ListItem($"{studentName} {studentSurName}", StudentID));
                }
            }
        }
    }

    protected void studentSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        string studentId = studentSelect.SelectedValue;
        getStudentInfo(studentId);
        getPaymentInfo(studentId);

    }
    private void getPaymentInfo(string studentId)
    {
        if (string.IsNullOrEmpty(studentId))
        {
            PaymentMethodValue.Text = "-";
            PaymentTypeValue.Text = "-";
            PaymentDateValue.Text = "-";
            ParentInfoValue.Text = "-";
            return;
        }
        string query = "select PaymentMethod,PaymentType,PaymentDate, Parent.Name ,Parent.Phone FROM Student Inner Join PaymentPlan on PaymentPlan.StudentID = Student.StudentID Inner JOIN Parent on Parent.ParentID = Student.ParentID where Student.StudentID = @StudentID";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    PaymentMethodValue.Text = reader["PaymentMethod"].ToString();
                    PaymentTypeValue.Text = reader["PaymentType"].ToString();
                    PaymentDateValue.Text = Convert.ToDateTime(reader["PaymentDate"]).ToString("dd.MM.yyyy");
                    ParentInfoValue.Text = reader["Name"].ToString() + "<br>";
                    ParentInfoValue.Text += reader["Phone"].ToString();
                }
                else
                {
                    PaymentMethodValue.Text = "-";
                    PaymentTypeValue.Text = "-";
                    PaymentDateValue.Text = "-";
                    ParentInfoValue.Text = "-";
                }
            }
            
        }

    }
    protected void getStudentInfo(string studentId)
    {
        if (string.IsNullOrEmpty(studentId))
        {
            NameValue.Text = "Öğrenci seçilmedi!";
            SurnameValue.Text = "-";
            AthleteNumberValue.Text = "-";
            AgeValue.Text = "-";
            return;
        }

        string query = "SELECT StudentID, Name, Surname, Age FROM Student WHERE StudentID = @studentId";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@studentId", studentId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    NameValue.Text = reader["Name"]?.ToString() ?? "-";
                    SurnameValue.Text = reader["Surname"]?.ToString() ?? "-";
                    AthleteNumberValue.Text = reader["StudentID"]?.ToString() ?? "-";
                    AgeValue.Text = reader["Age"]?.ToString() ?? "-";
                }
                else
                {
                    NameValue.Text = "Bilgi bulunamadı!";
                    SurnameValue.Text = "-";
                    AthleteNumberValue.Text = "-";
                    AgeValue.Text = "-";
                }
            }
        }
    }

    
}
