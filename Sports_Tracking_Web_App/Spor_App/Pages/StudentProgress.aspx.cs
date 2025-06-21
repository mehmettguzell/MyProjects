using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Student_development : System.Web.UI.Page
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
            getStudentInfo();
        }
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        commentAuthor.Text = string.Empty;
        comnment.Text = string.Empty;
        StarPlaceholder.Controls.Clear();

        int selectedStudentId;
        int selectedMonth;

        if (!int.TryParse(SelectStudentDropDownList.SelectedValue, out selectedStudentId) || !int.TryParse(MonthDropDownList.SelectedValue, out selectedMonth))
        {
            lblError.Text = "Invalid student or month selection.";
            return;
        }

        string query = @"
        SELECT Height, Weight, MuscleMass, Jump FROM Performance WHERE StudentID = @StudentID AND Month = @SelectedMonth";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentID", selectedStudentId);
                    command.Parameters.AddWithValue("@SelectedMonth", selectedMonth);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            HeightLiteral.Text = reader["Height"].ToString();
                            WeightLiteral.Text = reader["Weight"].ToString();
                            MuscleMassLiteral.Text = reader["MuscleMass"].ToString();
                            JumpLiteral.Text = reader["Jump"].ToString();
                        }
                        else
                        {
                            lblError.Text = "No data found for the selected student and month.";
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                lblError.Text = "Database error: " + sqlEx.Message;
            }
            catch (Exception ex)
            {
                lblError.Text = "An unexpected error occurred: " + ex.Message;
            }
        }

        string query2 = @"
        SELECT T.Name, CommentText, CommentStar 
        FROM Comments 
        INNER JOIN Teacher AS T ON T.TeacherID = Comments.TeacherID 
        WHERE StudentID = @selectedStudentId";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                using (SqlCommand com = new SqlCommand(query2, conn))
                {
                    com.Parameters.AddWithValue("@selectedStudentId", selectedStudentId);
                    conn.Open();
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string author = reader["Name"].ToString();
                            string comment = reader["CommentText"].ToString();
                            string star = reader["CommentStar"].ToString();

                            commentAuthor.Text = author;
                            comnment.Text = comment;

                            if (star == "1")
                            {
                                LiteralControl starHtml = new LiteralControl(
                                    "<span class='comment-rating'>" +
                                    "    <svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' width='16' height='16'>" +
                                    "        <path d='M12 17.27L18.18 21l-1.64-7.03L22 9.24l-7.19-.61L12 2 9.19 8.63 2 9.24l5.46 4.73L5.82 21z'></path>" +
                                    "    </svg>" +
                                    "</span>"
                                );

                                StarPlaceholder.Controls.Add(starHtml);
                            }
                        }
                        else
                        {
                            lblError.Text = "No teacher comments found for the selected student.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "An error occurred while fetching teacher comments: " + ex.Message;
            }
        }
    }

    protected void getStudentInfo()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT StudentID, Name, Surname FROM Student";
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SelectStudentDropDownList.Items.Clear();
                SelectStudentDropDownList.Items.Add(new ListItem("Select Student", ""));

                while (reader.Read())
                {
                    string StudentID = reader["StudentID"].ToString();
                    string Name = reader["Name"].ToString();
                    string Surname = reader["Surname"].ToString();
                    SelectStudentDropDownList.Items.Add(new ListItem(Name + " " + Surname, StudentID));
                }
            }
            catch (SqlException sqlEx)
            {
                lblError.Text = "Database error: " + sqlEx.Message;
            }
            catch (Exception ex)
            {
                lblError.Text = "An unexpected error occurred: " + ex.Message;
            }
        }
    }
}
