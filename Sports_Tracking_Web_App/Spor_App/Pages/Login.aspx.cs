using System;
using System.CodeDom;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;

public partial class Pages_Login : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblTrueMessage.Text = " ";
        lblErrorMessage.Text = " ";
    }
    protected void SubmitClick(object sender, EventArgs e)
    {
        //1 admin1
        //2 test
        //3 deneme
        string IDinput = IDBox.Text.Trim();
        int Input_ID = Convert.ToInt32(IDinput);
        string PASSWORD = PassBox.Text.Trim();

        string DB_PASS = Get_Passwd(Input_ID);
        string DB_SALT = Get_Salt(Input_ID);
        string USERNAME = Get_Username(Input_ID);

        string hashedPassword = HashPassword(PASSWORD, DB_SALT);

        if (hashedPassword == DB_PASS)
        {
            lblTrueMessage.Text = ("Hosgeldiniz.");
            Session.Add("Username", USERNAME);
            Response.Redirect("Home.aspx");
        }
        else
        {
            lblErrorMessage.Text = ("Hatalı kullanıcı adı veya şifre.");
        }
    }

    private string Get_Username(int Input_ID)
    {
        string username = string.Empty;
        string query = "SELECT USERNAME FROM PERSONEL WHERE PERSONEL_ID = @id";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", Input_ID);

            try
            {
                conn.Open();
                object result = cmd.ExecuteScalar();
                username = result?.ToString();
                return username;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    private string Get_Passwd(int Input_ID)
    {
        string password = string.Empty;
        string query = "SELECT PASSWORD FROM Personel WHERE PERSONEL_ID = @ID";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", Input_ID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    password = result.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
        }

        return password;
    }

    private string Get_Salt(int Input_ID)
    {
        string salt = string.Empty;
        string query = "SELECT Salt FROM Personel WHERE PERSONEL_ID = @ID";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", Input_ID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    salt = result.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
        }

        return salt;
    }

    protected static string GenerateSalt()
    {
        byte[] saltBytes = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }

    protected static string HashPassword(string PASSWORD, string Salt)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            string saltedPasswd = PASSWORD + Salt;
            byte[] passwdBytes = Encoding.UTF8.GetBytes(saltedPasswd);
            byte[] hashBytes = sha256.ComputeHash(passwdBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
