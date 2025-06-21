using Microsoft.Extensions.DependencyInjection;
using SmartLGS.Core.Interfaces;
using System;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using SmartLGS.Core.Interfaces.Factory;
using System.Collections.Generic;


namespace SmartLGS.student
{
    public partial class StudentHomePage : Form
    {
        private int _userId;
        private readonly IAddingService _addingHomeService;
        private readonly ICustomMethodHelper _customMethodHelper;
        private readonly ILoadDataService _loadDataService;
        private readonly IExamDetailsFormFactory _examDetailsFormFactory;
        private List<(int examId, int net)> _results;


        public StudentHomePage( IExamDetailsFormFactory examDetailsFormFactory,
                                IAddingService studentHomeService, 
                                ICustomMethodHelper customMethodHelper, 
                                ILoadDataService loadDataService,
                                int userId)
        {
            _examDetailsFormFactory = examDetailsFormFactory;
            _addingHomeService = studentHomeService;
            _customMethodHelper = customMethodHelper;
            _loadDataService = loadDataService;
            _userId = userId;
            InitializeComponent();
            dgvRecentExams.CellDoubleClick += dgvRecentExams_CellDoubleClick;
            _customMethodHelper.CustomizeDataGrid(dgvRecentExams);
        }

        private void StudentHomePage_Load(object sender, EventArgs e)
        {
            LoadStudentData();
        }
        private void LoadStudentData()
        {
            string userName = _loadDataService.LoadUserName(_userId);
            lblWelcome.Text = $"Hoş geldin, {userName} - Kullanıcı ID: {_userId}";

            string role = _loadDataService.LoadUserRole(_userId);
            _customMethodHelper.InitializeExamDetailsControls(gbExamDetails, out txtCorrect, out txtWrong, out txtEmpty);
            var examReports = _loadDataService.GetStudentExamReports(_userId);

            if (examReports?.Count > 0)
            {
                dgvRecentExams.DataSource = examReports;

                _results = _loadDataService.LoadExamData(dgvRecentExams);
                _loadDataService.LoadChartFromData(chartPerformance, _results);
                _customMethodHelper.ConfigureGridByRole(dgvRecentExams, role);
            }
            else
            {
                MessageBox.Show("Bu kullanıcıya ait sınav bulunamadı.");
            }
        }
        private void dgvRecentExams_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int examId = -1;
                examId = Convert.ToInt32(dgvRecentExams.Rows[e.RowIndex].Cells["ExamId"].Value);
                string examName = dgvRecentExams.Rows[e.RowIndex].Cells["ExamName"].Value?.ToString();
                string examDate = dgvRecentExams.Rows[e.RowIndex].Cells["ExamDate"].Value?.ToString();
                if (examId < 0)
                {
                    return;
                }
                using (var scope = Program.ServiceProvider.CreateScope())
                {
                    var examDetailsForm = _examDetailsFormFactory.Create(examId, _userId, examName, _results, examDate);
                    examDetailsForm.ShowDialog();
                }
                this.Show();
            }
        }
        private void RefreshExamTableAndChart()
        {
            var examReports = _loadDataService.GetStudentExamReports(_userId);
            if (examReports?.Count > 0)
            {
                dgvRecentExams.DataSource = examReports;

                var results = _loadDataService.LoadExamData(dgvRecentExams);
                _loadDataService.LoadChartFromData(chartPerformance, results);

                string role = _loadDataService.LoadUserRole(_userId);
                _customMethodHelper.ConfigureGridByRole(dgvRecentExams, role);
            }
            else
            {
                dgvRecentExams.DataSource = null;
                chartPerformance.Series.Clear();
            }
        }

        private void btnSubmitExam_Click(object sender, EventArgs e)
        {
            string examName = txtExamName.Text;
            string examDate = dtpExamDate.Value.ToString("yyyy-MM-dd");
            string[] correctInputs = new string[txtCorrect.Length];
            string[] wrongInputs = new string[txtWrong.Length];
            string[] emptyInputs = new string[txtEmpty.Length];
            for (int i = 0; i < txtCorrect.Length; i++)
            {
                correctInputs[i] = txtCorrect[i].Text;
                wrongInputs[i] = txtWrong[i].Text;
                emptyInputs[i] = txtEmpty[i].Text;
            }
            bool isAdded = _addingHomeService.AddExamResult(examName, examDate, _userId, correctInputs, wrongInputs, emptyInputs, out string errorMessage);
            if (isAdded)
            {
                txtExamName.Text = "";
                dtpExamDate.Value = DateTime.Today;

                for (int i = 0; i < txtCorrect.Length; i++)
                {
                    txtCorrect[i].Text = "";
                    txtWrong[i].Text = "";
                    txtEmpty[i].Text = "";
                }
                RefreshExamTableAndChart();
            }
        }
        
        private void btnImportReport_Click(object sender, EventArgs e)
        {
            _addingHomeService.AddExamFromPdf(_userId);
            RefreshExamTableAndChart();
        }
    }
}