using System;
using System.Activities.Statements;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Pages_DetailedSearch : System.Web.UI.Page
{
    public string connectionString = "Data Source=YAKıŞıKLı\\MSSQLSERVER01;Initial Catalog=DBMS_PROJECT;Integrated Security=True;Encrypt=False";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getUni();
            getIns();
            getTopic();
            getLanguage();
            getTitle();
            getAdvisor();
            getType();
            getAuthor();
        }
    }
    private void getAuthor()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT AUTHOR_ID, AUTHOR_NAME, AUTHOR_SURNAME FROM AUTHOR";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                DropDownList7.Items.Add(new ListItem("Select Author", ""));

                while (reader.Read())
                {
                    string AUTHOR_ID = reader["AUTHOR_ID"].ToString();
                    string AUTHOR_NAME = reader["AUTHOR_NAME"].ToString();
                    string AUTHOR_SURNAME = reader["AUTHOR_SURNAME"].ToString();
                    DropDownList7.Items.Add(new ListItem(AUTHOR_NAME + " " + AUTHOR_SURNAME, AUTHOR_ID));
                }
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
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
                DropDownList3.Items.Add(new ListItem("Select Thesis Type", ""));

                while (reader.Read())
                {
                    string typeNo = reader["TYPE_NO"].ToString();
                    string typeName = reader["TYPE_NAME"].ToString();
                    DropDownList3.Items.Add(new ListItem(typeName, typeNo));
                }
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void getAdvisor()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT SV.S_ID, SV.S_NAME FROM THESIS AS T " +
                    "INNER JOIN ADVISING AS A ON A.THESIS_NUM = T.THESIS_NUM " +
                    "INNER JOIN SUPERVISOR AS SV ON SV.S_ID = A.S_ID " +
                    "GROUP BY SV.S_NAME, SV.S_ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                DropDownList5.Items.Add(new ListItem("Select Advisor", ""));

                while (reader.Read())
                {
                    string thesisNo = reader["S_ID"].ToString();
                    string title = reader["S_NAME"].ToString();
                    DropDownList5.Items.Add(new ListItem(title, thesisNo));
                }
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void getTitle()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Select THESIS_NUM, T_TITLE From THESIS";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                LAN.Items.Add(new ListItem("Select Title", ""));

                while (reader.Read())
                {
                    string thesisNo= reader["THESIS_NUM"].ToString();
                    string title = reader["T_TITLE"].ToString();
                    LAN.Items.Add(new ListItem(title, thesisNo));
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
                string query = "SELECT T_LANGUAGE_NO, T_LANGUAGE FROM LANGUAGE";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                DropDownList4.Items.Add(new ListItem ("Select Language",""));
                
                while (reader.Read())
                {
                    string lanNo = reader["T_LANGUAGE_NO"].ToString();
                    string lanName = reader["T_LANGUAGE"].ToString();
                    DropDownList4.Items.Add(new ListItem(lanName, lanNo));
                }
                conn.Close();
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    private void getTopic()
    {
        using(SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                string query = "SELECT TL.TOPIC_ID, TL.TOPIC_ELEMENT FROM THESIS AS T" +
                    " INNER JOIN LIST AS L ON L.THESIS_NUM = T.THESIS_NUM " +
                    "INNER JOIN TOPIC_LIST AS TL ON TL.TOPIC_ID = L.TOPIC_ID " +
                    "WHERE EXISTS (SELECT TLD.TOPIC_ELEMENT FROM TOPIC_LIST AS TLD)";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                DropDownList6.Items.Add(new ListItem("Select Topic",""));

                while (reader.Read())
                {
                    string id = reader["TOPIC_ID"].ToString();
                    string element = reader["TOPIC_ELEMENT"].ToString();
                    DropDownList6.Items.Add(new ListItem(element, id));
                }
                conn.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
    private void getUni()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                string uniQuery = "SELECT UNI_NAME, UNI_NO FROM UNIVERSITY";
                SqlCommand cmd = new SqlCommand(uniQuery, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                DropDownList1.Items.Add(new ListItem("Select University", ""));

                while (reader.Read())
                {
                    string universityId = reader["UNI_NO"].ToString();
                    string universityName = reader["UNI_NAME"].ToString();
                    DropDownList1.Items.Add(new ListItem(universityName, universityId));
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    private void getIns()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT T_INSTITUTE, T_INS_NAME FROM INSTITUTE";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                DropDownList2.Items.Add(new ListItem("Select Institute", ""));
                while (reader.Read())
                {
                    string insId = reader["T_INSTITUTE"].ToString();
                    string insName = reader["T_INS_NAME"].ToString();
                    DropDownList2.Items.Add(new ListItem(insName, insId));
                }
                reader.Close();
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        try
        {
            string UNI_NO = DropDownList1.SelectedValue; //MAU
            string INSTITUTE = DropDownList2.SelectedValue;
            int T_INSTITUTE = string.IsNullOrEmpty(INSTITUTE) ? -1 : Convert.ToInt32(INSTITUTE);//2
            string TYPE = DropDownList3.SelectedValue;
            int TYPE_NO = string.IsNullOrEmpty(TYPE) ? -1 : Convert.ToInt32(TYPE);//2
            string LANGUAGE = DropDownList4.SelectedValue;
            int T_LANGUAGE_NO = string.IsNullOrEmpty(LANGUAGE) ? -1 : Convert.ToInt32(LANGUAGE);//2
            string TOPIC_ID = DropDownList6.SelectedValue; //TOPIC4
            string abs = TextBox1.Text; // %abs% abstract
            string startDateInput = StartDateText.Text; // 2024-34-34
            string endDateInput = EndDateText.Text; // 2024-34-34
            string THESIS_NUM = TextBox2.Text.Trim(); //T2
            string THESIS_NUM_TITLE = LAN.SelectedValue; //T2
            string keyword = TextBox3.Text.Trim(); //titleda ara, KELİME
            string author = DropDownList7.SelectedValue;
            int AUTHOR_ID = string.IsNullOrEmpty(author) ? -1 : Convert.ToInt32(author); // 210706030
            
            string S_ID = DropDownList5.SelectedValue; //S101


            DateTime startDate = string.IsNullOrEmpty(startDateInput) ? DateTime.MinValue : DateTime.ParseExact(startDateInput, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime endDate = string.IsNullOrEmpty(endDateInput) ? DateTime.MinValue : DateTime.ParseExact(endDateInput, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

            bool hasWhereClause = false;

            string query = @"SELECT THESIS.T_TITLE AS Title, AUTHOR.AUTHOR_NAME AS Author, LANGUAGE.T_LANGUAGE AS LANGUAGE, THESIS.D_DATE AS DateAdded, TYPE.TYPE_NAME AS TYPE, THESIS.T_PAGE AS PAGE, UNIVERSITY.UNI_NAME AS UNIVERSITY, INSTITUTE.T_INS_NAME AS INSTITUE, TOPIC_LIST.TOPIC_ELEMENT AS TOPIC, SUPERVISOR.S_NAME AS SUPERVISOR, CO_SUPERVISOR.CS_NAME AS CO_SUPERVISOR FROM THESIS INNER JOIN AUTHOR ON AUTHOR.AUTHOR_ID = THESIS.AUTHOR_ID INNER JOIN LIST ON LIST.THESIS_NUM = THESIS.THESIS_NUM INNER JOIN TOPIC_LIST ON TOPIC_LIST.TOPIC_ID = LIST.TOPIC_ID INNER JOIN ADVISING ON ADVISING.THESIS_NUM = THESIS.THESIS_NUM INNER JOIN SUPERVISOR ON SUPERVISOR.S_ID = ADVISING.S_ID INNER JOIN UNIVERSITY ON UNIVERSITY.UNI_NO = THESIS.UNI_NO INNER JOIN INSTITUTE ON INSTITUTE.T_INSTITUTE = THESIS.T_INSTITUTE INNER JOIN LANGUAGE ON LANGUAGE.T_LANGUAGE_NO = THESIS.T_LANGUAGE_NO LEFT OUTER JOIN CO_SUPERVISOR ON CO_SUPERVISOR.CS_ID = THESIS.CS_ID INNER JOIN TYPE ON TYPE.TYPE_NO = THESIS.TYPE_NO";


            if (!string.IsNullOrEmpty(UNI_NO))
            {
                query += " WHERE UNIVERSITY.UNI_NO = @UNI_NO";
                hasWhereClause = true;
            }

            if (T_INSTITUTE != -1)
            {
                if(hasWhereClause)
                {
                    query += " AND T_INSTITUTE = @T_INSTITUTE";
                }
                else
                {
                    query += " WHERE T_INSTITUTE = @T_INSTITUTE";
                    hasWhereClause = true;
                }
            }

            if (TYPE_NO != -1)
            {
                if (hasWhereClause)
                {
                    query += " AND TYPE.TYPE_NO = @TYPE_NO";
                }
                else
                {
                    query += " WHERE TYPE.TYPE_NO = @TYPE_NO";
                    hasWhereClause = true;
                }
            }

            if (T_LANGUAGE_NO != -1)
            {
                if (hasWhereClause)
                {
                    query += " AND LANGUAGE.T_LANGUAGE_NO = @T_LANGUAGE_NO";
                }
                else
                {
                    query += " WHERE LANGUAGE.T_LANGUAGE_NO = @T_LANGUAGE_NO";
                    hasWhereClause = true;
                }
            }

            if (!string.IsNullOrEmpty(TOPIC_ID))
            {
                if (hasWhereClause)
                {
                    query += " AND TOPIC_LIST.TOPIC_ID = @TOPIC_ID";
                }
                else
                {
                    query += " WHERE TOPIC_LIST.TOPIC_ID = @TOPIC_ID";
                    hasWhereClause = true;
                }
            }

            if (!string.IsNullOrEmpty(abs))
            {
                if (hasWhereClause)
                {
                    query += " AND THESIS.T_ABSTRACT LIKE '%' + @abs + '%'";
                }
                else
                {
                    query += " WHERE THESIS.T_ABSTRACT LIKE '%' + @abs + '%'";
                    hasWhereClause = true;
                }
            }

            if (!string.IsNullOrEmpty(startDateInput) && !string.IsNullOrEmpty(endDateInput))
            {
                if (hasWhereClause)
                {
                    query += " AND THESIS.D_DATE BETWEEN @startDate AND @endDate";
                }
                else
                {
                    query += " WHERE THESIS.D_DATE BETWEEN @startDate AND @endDate";
                    hasWhereClause = true;
                }
            }

            if (!string.IsNullOrEmpty(THESIS_NUM))
            {
                if (hasWhereClause)
                {
                    query += " AND THESIS.THESIS_NUM = @THESIS_NUM";
                }
                else
                {
                    query += " WHERE THESIS.THESIS_NUM = @THESIS_NUM";
                    hasWhereClause = true;
                }
            }
            
            if (!string.IsNullOrEmpty(THESIS_NUM_TITLE))
            {
                if (hasWhereClause)
                {
                    query += " AND THESIS.THESIS_NUM = @THESIS_NUM_TITLE";
                }
                else
                {
                    query += " WHERE THESIS.THESIS_NUM = @THESIS_NUM_TITLE";
                    hasWhereClause = true;
                }
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                if (hasWhereClause)
                {
                    query += " AND THESIS.T_TITLE LIKE '%' + @keyword + '%'";
                }
                else
                {
                    query += " WHERE THESIS.T_TITLE LIKE '%' + @keyword + '%'";
                    hasWhereClause = true;
                }
            }

            if (AUTHOR_ID != -1)
            {
                if (hasWhereClause)
                {
                    query += " AND THESIS.AUTHOR_ID = @AUTHOR_ID";
                }
                else
                {
                    query += " WHERE  THESIS.AUTHOR_ID = @AUTHOR_ID";
                    hasWhereClause = true;
                }
            }

            if (!string.IsNullOrEmpty(S_ID))
            {
                if (hasWhereClause)
                {
                    query += " AND SUPERVISOR.S_ID = @S_ID";
                }
                else
                {
                    query += " WHERE SUPERVISOR.S_ID = @S_ID";
                    hasWhereClause = true;
                }
            }
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(UNI_NO))
                    {
                        cmd.Parameters.AddWithValue("@UNI_NO", UNI_NO);
                    }
                    if (T_INSTITUTE != -1)
                    {
                        cmd.Parameters.AddWithValue("@T_INSTITUTE", T_INSTITUTE);
                    }
                    if (TYPE_NO != -1)
                    {
                        cmd.Parameters.AddWithValue("@TYPE_NO", TYPE_NO);
                    }
                    if (T_LANGUAGE_NO != -1)
                    {
                        cmd.Parameters.AddWithValue("@T_LANGUAGE_NO", T_LANGUAGE_NO);
                    }
                    if (!string.IsNullOrEmpty(TOPIC_ID))
                    {
                        cmd.Parameters.AddWithValue("@TOPIC_ID", TOPIC_ID);
                    }
                    if (!string.IsNullOrEmpty(abs))
                    {
                        cmd.Parameters.AddWithValue("@abs", abs);
                    }
                    if (!string.IsNullOrEmpty(startDateInput) && !string.IsNullOrEmpty(endDateInput))
                    {
                        cmd.Parameters.AddWithValue("@startDate", startDate);
                        cmd.Parameters.AddWithValue("@endDate", endDate);
                    }
                    if (!string.IsNullOrEmpty(THESIS_NUM))
                    {
                        cmd.Parameters.AddWithValue("@THESIS_NUM", THESIS_NUM);
                    }
                    if (!string.IsNullOrEmpty(THESIS_NUM_TITLE))
                    {
                        cmd.Parameters.AddWithValue("@THESIS_NUM_TITLE", THESIS_NUM_TITLE);
                    }
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        cmd.Parameters.AddWithValue("@keyword", keyword);
                    }
                    if (AUTHOR_ID != -1)
                    {
                        cmd.Parameters.AddWithValue("@AUTHOR_ID", AUTHOR_ID);
                    }
                    if (!string.IsNullOrEmpty(S_ID))
                    {
                        cmd.Parameters.AddWithValue("@S_ID", S_ID);
                    }
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
        catch (FormatException)
        {
            Console.WriteLine("Invalid date format. Please enter dates in the correct format (yyyy-MM-dd).");
        }
    }
    protected void ClearButton_Click(object sender, EventArgs e)
    {
        DropDownList1.SelectedIndex = -1;
        DropDownList2.SelectedIndex = -1;
        DropDownList3.SelectedIndex = -1;
        DropDownList4.SelectedIndex = -1;
        DropDownList6.SelectedIndex = -1;
        DropDownList7.SelectedIndex = -1;
        DropDownList5.SelectedIndex = -1;

        TextBox1.Text = string.Empty;
        TextBox2.Text = string.Empty;
        TextBox3.Text = string.Empty;

        StartDateText.Text = string.Empty;
        EndDateText.Text = string.Empty;

        LAN.SelectedIndex = -1;
    }

}
