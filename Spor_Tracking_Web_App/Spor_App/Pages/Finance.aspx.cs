using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Activities.Expressions;
using System.Activities.Statements;

public partial class Pages_Finance : System.Web.UI.Page
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
    }

    protected void FilterButton_Click(object sender, EventArgs e)
    {
        string startDate = StartDateText.Text;
        string endDate = EndDateText.Text;

        DateTime? startDateTime = null;
        DateTime? endDateTime = null;

        if (DateTime.TryParse(startDate, out DateTime parsedStartDate))
        {
            startDateTime = parsedStartDate;
        }

        if (DateTime.TryParse(endDate, out DateTime parsedEndDate))
        {
            endDateTime = parsedEndDate;
        }

        if (startDateTime.HasValue)
        {
            deneme.Text = startDateTime.Value.ToString("yyyy-MM-dd");
        }
        else
        {
            deneme.Text = "Invalid start date";
        }
        string kar = GetRevenue(startDateTime, endDateTime);
        TotalRevenueLabel.Text = kar;

        string zarar = GetExpence(startDateTime, endDateTime);
        TotalExpensesLabel.Text = "-" + zarar;

        decimal totalMoney = Convert.ToDecimal(kar) - Convert.ToDecimal(zarar);
        MoneyTotal.Text = totalMoney.ToString("C");

        string script = $"updateCharts({kar}, {zarar});";
        ClientScript.RegisterStartupScript(this.GetType(), "updateCharts", script, true);

        GetNewRegistrations(startDateTime, endDateTime);
        GetDroupOut(startDateTime, endDateTime);
        getTotal(startDateTime, endDateTime);
    }

    private void GetDroupOut(DateTime? startDateTime, DateTime? endDateTime)
    {
        string query = "SELECT Count(*) FROM Student WHERE SeparationDate between @StartDate AND @EndDate";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@StartDate", startDateTime.Value);
                cmd.Parameters.AddWithValue("@EndDate", endDateTime.Value);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        int registrationsCount = Convert.ToInt32(result);
                        TotalDropoutsLabel.Text = registrationsCount.ToString();
                    }
                    else
                    {
                        TotalDropoutsLabel.Text = "No registrations found for the selected date range.";
                    }
                }
                catch (Exception ex)
                {
                    TotalDropoutsLabel.Text = "An error occurred while fetching data.";
                }
            }
        }
    }

    private void GetNewRegistrations(DateTime? startDateTime, DateTime? endDateTime)
    {
        string query = "SELECT COUNT(*) FROM Student WHERE RegistrationDate BETWEEN @StartDate AND @EndDate";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@StartDate", startDateTime.Value);
                cmd.Parameters.AddWithValue("@EndDate", endDateTime.Value);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        int registrationsCount = Convert.ToInt32(result);
                        NewRegistrationsLabel.Text = registrationsCount.ToString();
                    }
                    else
                    {
                        NewRegistrationsLabel.Text = "No registrations found for the selected date range.";
                    }
                }
                catch (Exception ex)
                {
                    NewRegistrationsLabel.Text = "An error occurred while fetching data.";
                }
            }
        }
    }

    private void getTotal(DateTime? startDateTime, DateTime? endDateTime)
    {
        string query = "SELECT COUNT(*) FROM Student";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        int registrationsCount = Convert.ToInt32(result);
                        TotalRegistrationsLabel.Text = registrationsCount.ToString();
                    }
                    else
                    {
                        TotalRegistrationsLabel.Text = "No registrations found for the selected date range.";
                    }
                }
                catch (Exception ex)
                {
                    TotalRegistrationsLabel.Text = "An error occurred while fetching data.";
                }
            }
        }
    }

    private string GetExpence(DateTime? startDateTime, DateTime? endDateTime)
    {
        if (!startDateTime.HasValue || !endDateTime.HasValue)
        {
            return "Invalid date range.";
        }

        string query = "select Amount from Expense where Date BETWEEN @StartDate AND @EndDate";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@StartDate", startDateTime.Value);
                cmd.Parameters.AddWithValue("@EndDate", endDateTime.Value);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader["Amount"].ToString();
                    }
                    else
                    {
                        return "No revenue found for the selected date range.";
                    }
                }
                catch (Exception ex)
                {
                    return "An error occurred while fetching the data: " + ex.Message;
                }
            }
        }
    }

    protected string GetRevenue(DateTime? startDateTime, DateTime? endDateTime)
    {
        if (!startDateTime.HasValue || !endDateTime.HasValue)
        {
            return "Invalid date range.";
        }

        string query = "SELECT RevenueAmount FROM Revenue WHERE Date BETWEEN @StartDate AND @EndDate";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@StartDate", startDateTime.Value);
                cmd.Parameters.AddWithValue("@EndDate", endDateTime.Value);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader["RevenueAmount"].ToString();
                    }
                    else
                    {
                        return "No revenue found for the selected date range.";
                    }
                }
                catch (Exception ex)
                {
                    return "An error occurred while fetching the data: " + ex.Message;
                }
            }
        }
    }
}
