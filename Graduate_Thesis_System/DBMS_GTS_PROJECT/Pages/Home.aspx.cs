using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Home : System.Web.UI.Page
{
    string connectionString = "Data Source=YAKıŞıKLı\\MSSQLSERVER01;Initial Catalog=DBMS_PROJECT;Integrated Security=True;Encrypt=False";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getLanguage();
            getType();
        }
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
                Language.Items.Clear();
                Language.Items.Add(new ListItem("Select Language", ""));

                while (reader.Read())
                {
                    string lang = reader["T_LANGUAGE"].ToString();
                    string no = reader["T_LANGUAGE_NO"].ToString();
                    Language.Items.Add(new ListItem(lang, no));
                }
            }
        }
        catch (Exception ex)
        {
            kontrol.Text = "Hata: " + ex.Message;
            kontrol.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string info = SearchField.SelectedValue.ToUpper();

        string LANGUAGE = Language.SelectedValue;

        int T_LANGUAGE_NO = string.IsNullOrEmpty(LANGUAGE) ? -1 : Convert.ToInt32(LANGUAGE);//2

        string TYPE = ThesisType.SelectedValue;
        int TYPE_NO = string.IsNullOrEmpty(TYPE) ? -1 : Convert.ToInt32(TYPE);//2

        string sTerm = SearchTerm.Text.Trim();

        if (sTerm.Length < 3)
        {
            kontrol.Text = "Arama terimi en az 3 karakter olmalı.";
            return;
        }

        GetFilteredResults(info, T_LANGUAGE_NO, TYPE_NO, sTerm);
    }

    protected void GetFilteredResults(string info, int T_LANGUAGE_NO, int TYPE_NO, string sTerm)
    {
        try
        {
            string query = "SELECT T.T_TITLE AS ThesisTitle, A.AUTHOR_NAME AS Author, T.D_DATE AS DateAdded FROM THESIS AS T " +
                           "LEFT JOIN AUTHOR AS A ON A.AUTHOR_ID = T.AUTHOR_ID " +
                           "LEFT JOIN TYPE AS TY ON TY.TYPE_NO = T.TYPE_NO ";

            bool hasWhere = false;

            if (!string.IsNullOrEmpty(info) && !string.IsNullOrEmpty(sTerm))
            {
                string condition = "";
                switch (info)
                {
                    case "YAZAR":
                        condition = "A.AUTHOR_NAME LIKE '%' + @InputInfo + '%'";
                        break;
                    case "TEZ ADI":
                        condition = "T.T_TITLE LIKE '%' + @InputInfo + '%'";
                        break;
                    case "KONU":
                    case "ABSTRACT":
                        condition = "T.T_ABSTRACT LIKE '%' + @InputInfo + '%'";
                        break;
                    case "SUPERVISOR":
                        condition = "EXISTS (SELECT 1 FROM ADVISING AS ADV " +
                                    "INNER JOIN SUPERVISOR AS SV ON SV.S_ID = ADV.S_ID " +
                                    "WHERE ADV.THESIS_NUM = T.THESIS_NUM AND SV.S_NAME LIKE '%' + @InputInfo + '%')";
                        break;
                    case "UNIVERSITY":
                        condition = "EXISTS (SELECT 1 FROM UNIVERSITY AS UNI " +
                                    "WHERE UNI.UNI_NO = T.UNI_NO AND UNI.UNI_NAME LIKE '%' + @InputInfo + '%')";
                        break;
                    case "INSTITUTE":
                        condition = "EXISTS (SELECT 1 FROM INSTITUTE AS INS " +
                                    "WHERE INS.T_INSTITUTE = T.T_INSTITUTE AND INS.T_INS_NAME LIKE '%' + @InputInfo + '%')";
                        break;
                }
                query += (hasWhere ? " AND " : " WHERE ") + condition;
                hasWhere = true;
            }

            if (T_LANGUAGE_NO != -1)
            {
                query += (hasWhere ? " AND " : " WHERE ") + "T.T_LANGUAGE_NO = @InputLanguage";
                hasWhere = true;
            }

            if (TYPE_NO != -1)
            {
                query += (hasWhere ? " AND " : " WHERE ") + "TY.TYPE_NO = @InputType";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                if (!string.IsNullOrEmpty(sTerm))
                {
                    command.Parameters.AddWithValue("@InputInfo", sTerm);
                }

                if (T_LANGUAGE_NO != -1)
                {
                    command.Parameters.AddWithValue("@InputLanguage", T_LANGUAGE_NO);
                }

                if (TYPE_NO != -1)
                {
                    command.Parameters.AddWithValue("@InputType", TYPE_NO);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable resultsTable = new DataTable();

                connection.Open();
                adapter.Fill(resultsTable);

                ThesisGrid.DataSource = resultsTable;
                ThesisGrid.DataBind();
            }
        }
        catch (Exception ex)
        {
            kontrol.Text = "Hata: " + ex.Message;
            kontrol.ForeColor = System.Drawing.Color.Red;
        }
    }
}
