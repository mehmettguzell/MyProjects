using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AddThesis : System.Web.UI.Page
{
    string connectionString = "Data Source=YAKıŞıKLı\\MSSQLSERVER01;Initial Catalog=DBMS_PROJECT;Integrated Security=True;Encrypt=False;MultipleActiveResultSets=True;";

    protected void Page_Load(object sender, EventArgs e)
    {
        getType();
        getLanguage();
        getDate();
        getIns();
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        string T_TITLE = ThesisTitle.Text.ToString();
        string T_ABSTRACT = ThesisAbstract.Text.ToString();

        string AUTHOR_NAME = AuthorName.Text.ToString().ToUpper();
        string AUTHOR_SURNAME = AuthorSurname.Text.ToString().ToUpper();
        string AUTHOR_AGE = AuthorSurname.Text.ToString();
        string UNIVERSITY_NAME = AuthorUni.Text.ToString().ToUpper();
        string T_INSTITUTE = AuthorIns.SelectedValue;
        string AUTHOR_ID = AuthorId.Text.ToString();

        string D_DATE = ThesisDate.Text.ToString();
        string T_YEAR = ThesisYear.Text.ToString();
        string T_PAGE = ThesisPage.Text.ToString();
        string S_NAME = ThesisSuperVisor.Text.ToString().ToUpper();

        string TYPE_NO = ThesisType.SelectedValue;
        string T_LANGUAGE_NO = ThesisType.SelectedValue;

        string UNI_NO = CreateUniversity(UNIVERSITY_NAME);
        CreateAuthor(AUTHOR_NAME, AUTHOR_ID, AUTHOR_SURNAME, AUTHOR_AGE);
        string THESIS_NUM = GenerateThesisNum();
        CreateThesis(THESIS_NUM, T_TITLE, T_ABSTRACT, AUTHOR_ID, D_DATE, T_YEAR, T_PAGE, S_NAME, TYPE_NO, T_LANGUAGE_NO, T_INSTITUTE, UNI_NO);
        string S_ID = createSuperVisor(S_NAME, THESIS_NUM);
    }

    private string createSuperVisor(string S_NAME, string THESIS_NUM)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            string query = "SELECT S_ID FROM SUPERVISOR WHERE S_NAME = @S_NAME";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@S_NAME", S_NAME);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string existingS_ID = reader["S_ID"].ToString();

                string query3 = "INSERT INTO ADVISING (S_ID, THESIS_NUM) VALUES (@S_ID, @THESIS_NUM)";
                SqlCommand cmd3 = new SqlCommand(query3, conn);
                cmd3.Parameters.AddWithValue("@S_ID", existingS_ID);
                cmd3.Parameters.AddWithValue("@THESIS_NUM", THESIS_NUM);
                cmd3.ExecuteNonQuery();

                reader.Close();
                return existingS_ID;
            }

            reader.Close();

            string query1 = "SELECT TOP 1 S_ID FROM SUPERVISOR ORDER BY S_ID DESC";
            SqlCommand cmd1 = new SqlCommand(query1, conn);

            SqlDataReader reader1 = cmd1.ExecuteReader();

            string newS_ID = "";
            if (reader1.Read())
            {
                string lastS_ID = reader1["S_ID"].ToString();
                int lastIDNumber = int.Parse(lastS_ID.Substring(1));
                int newIDNumber = lastIDNumber + 1;
                newS_ID = "S" + newIDNumber.ToString("D3");
            }

            reader1.Close();

            string query2 = "INSERT INTO SUPERVISOR (S_ID, S_NAME) VALUES (@S_ID, @S_NAME)";
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            cmd2.Parameters.AddWithValue("@S_ID", newS_ID);
            cmd2.Parameters.AddWithValue("@S_NAME", S_NAME);
            cmd2.ExecuteNonQuery();

            string query3_1 = "INSERT INTO ADVISING (S_ID, THESIS_NUM) VALUES (@S_ID, @THESIS_NUM)";
            SqlCommand cmd3_1 = new SqlCommand(query3_1, conn);
            cmd3_1.Parameters.AddWithValue("@S_ID", newS_ID);
            cmd3_1.Parameters.AddWithValue("@THESIS_NUM", THESIS_NUM);
            cmd3_1.ExecuteNonQuery();

            return newS_ID;
        }
    }
    private string CreateUniversity(string UNI_NAME)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT UNI_NAME, UNI_NO FROM UNIVERSITY";
            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            bool exists = false;
            string UNI_NO = string.Empty;

            while (reader.Read())
            {
                string NAME = reader["UNI_NAME"].ToString();
                if (NAME == UNI_NAME)
                {
                    exists = true;
                    UNI_NO = reader["UNI_NO"].ToString();
                    break;
                }
            }

            reader.Close();
            conn.Close();

            if (!exists)
            {
                string uniPrefix = UNI_NAME.Length >= 2 ? UNI_NAME.Substring(0, 2).ToUpper() : UNI_NAME.ToUpper();
                UNI_NO = uniPrefix + "U";

                string query1 = "INSERT INTO UNIVERSITY (UNI_NAME, UNI_NO) VALUES(@UNI_NAME, @UNI_NO)";
                SqlCommand cmd1 = new SqlCommand(query1, conn);
                cmd1.Parameters.AddWithValue("@UNI_NAME", UNI_NAME);
                cmd1.Parameters.AddWithValue("@UNI_NO", UNI_NO);

                conn.Open();
                cmd1.ExecuteNonQuery();
                conn.Close();

            }

            return UNI_NO;
        }
    }
    private void CreateAuthor(string AUTHOR_NAME, string AUTHOR_ID, string AUTHOR_SURNAME, string AUTHOR_AGE)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query1 = "SELECT AUTHOR_ID FROM AUTHOR WHERE AUTHOR_ID = @AUTHOR_ID";
            string query = "INSERT INTO AUTHOR (AUTHOR_ID, AUTHOR_NAME, AUTHOR_SURNAME, AUTHOR_AGE) " +
                           "VALUES (@AUTHOR_ID, @AUTHOR_NAME, @AUTHOR_SURNAME, @AUTHOR_AGE)";

            using(SqlCommand cmd1 = new SqlCommand(query1, conn))
            {
                cmd1.Parameters.AddWithValue("@AUTHOR_ID", AUTHOR_ID);
                conn.Open();
                object result = cmd1.ExecuteScalar();
                conn.Close();
                if (result != null)
                {
                    Console.WriteLine("AUTHOR_ID already exists.");
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AUTHOR_ID", AUTHOR_ID);
                        cmd.Parameters.AddWithValue("@AUTHOR_NAME", AUTHOR_NAME);
                        cmd.Parameters.AddWithValue("@AUTHOR_SURNAME", AUTHOR_SURNAME);
                        cmd.Parameters.AddWithValue("@AUTHOR_AGE", AUTHOR_AGE);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();

            }
        }
    }
    private string GenerateThesisNum()
    {
        using(SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT TOP 1 THESIS_NUM FROM THESIS ORDER BY THESIS_NUM DESC";
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    string lastNumber = result.ToString();
                    int numberPart = int.Parse(lastNumber.Substring(1));
                    return $"T{numberPart + 1}";
                }
                else
                {
                    return $"";
                }
            }
        }
    }
    private void CreateThesis(string THESIS_NUM, string T_TITLE, string T_ABSTRACT, string AUTHOR_ID, string D_DATE, string T_YEAR, string T_PAGE, string S_NAME, string TYPE_NO, string T_LANGUAGE_NO, string T_INSTITUTE, string UNI_NO)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO THESIS(THESIS_NUM, T_TITLE, T_ABSTRACT, D_DATE, T_YEAR, TYPE_NO, T_PAGE, T_LANGUAGE_NO, UNI_NO, AUTHOR_ID, T_INSTITUTE) VALUES(@THESIS_NUM, @T_TITLE, @T_ABSTRACT, @D_DATE, @T_YEAR, @TYPE_NO, @T_PAGE, @T_LANGUAGE_NO, @UNI_NO, @AUTHOR_ID, @T_INSTITUTE)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@THESIS_NUM", THESIS_NUM);
                cmd.Parameters.AddWithValue("@T_TITLE", T_TITLE);
                cmd.Parameters.AddWithValue("@T_ABSTRACT", T_ABSTRACT);
                cmd.Parameters.AddWithValue("@D_DATE", D_DATE);
                cmd.Parameters.AddWithValue("@T_YEAR", T_YEAR);
                cmd.Parameters.AddWithValue("@TYPE_NO", TYPE_NO);
                cmd.Parameters.AddWithValue("@T_PAGE", T_PAGE);
                cmd.Parameters.AddWithValue("@T_LANGUAGE_NO", T_LANGUAGE_NO);
                cmd.Parameters.AddWithValue("@UNI_NO", UNI_NO);
                cmd.Parameters.AddWithValue("@AUTHOR_ID", AUTHOR_ID);
                cmd.Parameters.AddWithValue("@T_INSTITUTE", T_INSTITUTE);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
    private void getIns()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT T_INSTITUTE, T_INS_NAME FROM INSTITUTE";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            AuthorIns.Items.Add(new ListItem("Select Institute", ""));
            while (reader.Read())
            {
                string insId = reader["T_INSTITUTE"].ToString();
                string insName = reader["T_INS_NAME"].ToString();
                AuthorIns.Items.Add(new ListItem(insName, insId));
            }
            reader.Close();
        }
    }
    private void getDate()
    {
        ThesisDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
    }
    private void getType()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT TYPE_NO, TYPE_NAME FROM TYPE";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                ThesisType.Items.Add(new ListItem("Select Type", ""));

                while (reader.Read())
                {
                    string typeNo = reader["TYPE_NO"].ToString();
                    string typeName = reader["TYPE_NAME"].ToString();
                    ThesisType.Items.Add(new ListItem(typeName, typeNo));
                }
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void getLanguage()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT T_LANGUAGE, T_LANGUAGE_NO FROM LANGUAGE";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                ThesisLanguage.Items.Clear();
                ThesisLanguage.Items.Add(new ListItem("Select Language", ""));

                while (reader.Read())
                {
                    string lang = reader["T_LANGUAGE"].ToString();
                    string no = reader["T_LANGUAGE_NO"].ToString();
                    ThesisLanguage.Items.Add(new ListItem(lang, no));
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}