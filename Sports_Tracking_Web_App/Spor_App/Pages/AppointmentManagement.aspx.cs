using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IdentityModel.Protocols.WSTrust;

public partial class Pages_Appointment_Management : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] != null)
        {
            string username = Session["Username"].ToString();
            userInfo.Text = "Hoşgeldin " + username;
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        if (!IsPostBack)
        {
            LoadBranches();
        }
    }
    protected void BranchDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadStudents();
    }


    private void LoadBranches()
    {
        string query = "select BranchID, BranchName from Branch ";
        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(query, conn);
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        BranchDropDownList.Items.Add(new ListItem("Select Branch", ""));
        while (reader.Read())
        {
            string branchId = reader["BranchID"].ToString();
            string branchName = reader["BranchName"].ToString();
            BranchDropDownList.Items.Add(new ListItem(branchName, branchId));
        }
        conn.Close();
    }

    private void LoadStudents()
    {
        string secilenBranchId = BranchDropDownList.SelectedValue;

        if (string.IsNullOrEmpty(secilenBranchId))
        {
            return;
        }
        string query = "SELECT NAME,Surname,StudentID FROM Student where BranchID = @secilenBranchId ";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@secilenBranchId", secilenBranchId);

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        StudentDropDownList.Items.Clear();
        StudentDropDownList.Items.Add(new ListItem("Select Student", ""));
        while (reader.Read())
        {
            string StudentID = reader["StudentID"].ToString();
            string studentName = reader["NAME"].ToString();
            string studentSurName = reader["Surname"].ToString();
            StudentDropDownList.Items.Add(new ListItem(studentName + " " + studentSurName, StudentID));
        }
        conn.Close();
    }
   
    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        setBranchStatus();
        string StudentID = StudentDropDownList.SelectedValue;
        if (string.IsNullOrEmpty(StudentID))
        {
            uykusuz.Text = "No student selected.";
            return;
        }
        string AppointmentID = null;

        if (RadioButton1.Checked)
        {
            AppointmentID = "1";
            uykusuz.Text = RadioButton1.Text;
        }
        else if (RadioButton2.Checked)
        {
            AppointmentID = "2";
            uykusuz.Text = RadioButton2.Text;
        }
        else if (RadioButton3.Checked)
        {
            AppointmentID = "3";
            uykusuz.Text = RadioButton3.Text;
        }
        else if (RadioButton4.Checked)
        {
            AppointmentID = "4";
            uykusuz.Text = RadioButton4.Text;
        }

        if (AppointmentID == null)
        {
            uykusuz.Text = "No time slot selected.";
            return;
        }

        try
        {
            string teacherQuery = "UPDATE Student SET AppointmentID = @AppointmentID WHERE StudentID = @StudentID";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(teacherQuery, conn);
                cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                cmd.Parameters.AddWithValue("@StudentID", StudentID);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            string teacherName = string.Empty;
            string query1 = "select distinct t.Name\r\nfrom Appointment as a\r\ninner join Teacher as t on t.TeacherID = a.TeacherID\r\nwhere a.AppointmentID=@AppointmentID\r\n";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query1, conn);
                cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                conn.Open();
                teacherName = (string)cmd.ExecuteScalar();
                conn.Close();
            }
            Teacher.Text = "The teacher of this course: " + teacherName;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void setBranchStatus()
    {
        string BranchName = BranchDropDownList.SelectedItem.Text;
        string BranchID = BranchDropDownList.SelectedValue;
        string Status = "no info";

        if (RadioButton5.Checked)
        {
            Status = "Available";
        }
        else if (RadioButton6.Checked)
        {
            Status = "Busy";
        }
        else if (RadioButton7.Checked)
        {
            Status = "Closed";
        }
        else
        {
            throw new InvalidOperationException("Please select a valid status");
        }

        string Query = "UPDATE BRANCH SET BranchStatus = @Status WHERE BranchID = @BranchID;";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@BranchID", BranchID);
            cmd.Parameters.AddWithValue("@Status", Status);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        Branch.Text = BranchName + " Branch status successfully changed to " + Status;
    }

}