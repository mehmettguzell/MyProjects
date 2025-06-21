using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Home : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] != null)
        {
            string username = Session["Username"].ToString();
            userInfo.Text = "Hoşgeldin " + username;
            getStaredStudent();
            getUpcomingPayments();
            getBranchStatus();
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
    private void getBranchStatus()
    {
        string query = "select BranchStatus from Branch";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int branchIndex = 1;

                while (reader.Read() && branchIndex <= 3)
                {
                    string branchStatus = reader["BranchStatus"].ToString();

                    switch (branchIndex)
                    {
                        case 1:
                            maltepe.Text = branchStatus;
                            break;
                        case 2:
                            kartal.Text = branchStatus;
                            break;
                        case 3:
                            besiktas.Text = branchStatus;
                            break;
                    }
                    branchIndex++;
                }
            }
        }
    }
    private void getUpcomingPayments()
    {
        string query = "SELECT TOP 5 Student.Name, Student.Surname, PaymentPlan.PaymentDate FROM Student INNER JOIN PaymentPlan ON PaymentPlan.StudentID = STUDENT.StudentID WHERE PaymentPlan.PaymentDate BETWEEN GETDATE() AND DATEADD(DAY, 15, GETDATE())";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int studentIndex = 1;

                while (reader.Read() && studentIndex <= 5)
                {
                    string studentName = reader["Name"].ToString();
                    string studentSurname = reader["Surname"].ToString();
                    string paymentPlan = Convert.ToDateTime(reader["PaymentDate"]).ToString("dd.MM.yyyy");
                    string fullName = studentName + " " + studentSurname + " : " + paymentPlan;

                    switch (studentIndex)
                    {
                        case 1:
                            payment1.Text = fullName;
                            break;
                        case 2:
                            payment2.Text = fullName;
                            break;
                        case 3:
                            payment3.Text = fullName;
                            break;
                        case 4:
                            payment4.Text = fullName;
                            break;
                        case 5:
                            payment5.Text = fullName;
                            break;
                    }
                    studentIndex++;
                }
            }
        }
    }
    private void getStaredStudent()
    {
        string query = "select distinct TOP 5 Student.Name, Student.Surname FROM Student\r\nINNER JOIN Comments ON Comments.StudentID = STUDENT.StudentID\r\nWHERE Comments.CommentStar = 1";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int studentIndex = 1;

                while (reader.Read() && studentIndex <= 5)
                {
                    string studentName = reader["Name"].ToString();
                    string studentSurname = reader["Surname"].ToString();
                    string fullName = studentName + " " + studentSurname;

                    switch (studentIndex)
                    {
                        case 1:
                            student1.Text = fullName;
                            break;
                        case 2:
                            student2.Text = fullName;
                            break;
                        case 3:
                            student3.Text = fullName;
                            break;
                        case 4:
                            student4.Text = fullName;
                            break;
                        case 5:
                            student5.Text = fullName;
                            break;
                    }
                    studentIndex++;
                }
            }
        }
    }

    
}