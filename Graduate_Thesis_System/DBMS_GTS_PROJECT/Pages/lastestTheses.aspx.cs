using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_sonEklenen : System.Web.UI.Page
{
    string connectionString = "Data Source=YAKıŞıKLı\\MSSQLSERVER01;Initial Catalog=DBMS_PROJECT;Integrated Security=True;Encrypt=False";

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        string gunDegeri = gun.Text;
        int gunInt;
        if (!int.TryParse(gunDegeri, out gunInt))
        {
            return;
        }
        gunInt *= -1;
        try
        {
            string query = "SELECT T.T_TITLE AS ThesisTitle, A.AUTHOR_NAME AS Author ,T.D_DATE AS DateAdded FROM THESIS T " +
                           "INNER JOIN AUTHOR AS A ON A.AUTHOR_ID = T.AUTHOR_ID " +
                           "WHERE T.D_DATE BETWEEN DATEADD(DAY, @GunDegeri, GETDATE()) AND GETDATE()";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@GunDegeri", gunInt);

                    conn.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ThesisGrid.DataSource = dt;
                        ThesisGrid.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}