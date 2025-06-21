using SmartLGS.Core.Interfaces;
using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using SmartLGS.UI.Admin;
using SmartLGS.Core.Interfaces.Factory;
using SmartLGS.Core.Models;
using System.Collections.Generic;
using System.Data;
namespace SmartLGS.admin
{
    public partial class AdminHomePage : Form
    {
        private int _userId;
        private int _selectedStudentId = -1;
        private int _updateDeleteStudentId = -1;
        private List<(int examId, int net)> _results;

        private readonly IAddingService _addingService;
        private readonly ICustomMethodHelper _customMethodHelper;
        private readonly IUserRepository _userRepository;
        private readonly IMessageHelper _messageHelper;
        private readonly ILoadDataService _loadDataService;
        private readonly IDeletionService _deletionService;
        private readonly IExamDetailsFormFactory _examDetailsFormFactory;
        private readonly IUpdateStudentFormFactory _updateStudentFormFactory;

        public AdminHomePage(IAddingService addingService,
                            ICustomMethodHelper customMethodHelper,
                            IUserRepository userRepository,
                            IMessageHelper messageHelper,
                            ILoadDataService loadDataService,
                            IDeletionService deletionService,
                            IExamRepository examRepository,
                            IExamDetailsFormFactory examDetailsFormFactory,
                            IUpdateStudentFormFactory updateStudentFormFactory,
                            int userId)
        {
            _userId = userId;
            InitializeComponent();
            _updateStudentFormFactory = updateStudentFormFactory;
            _examDetailsFormFactory = examDetailsFormFactory;
            _addingService = addingService;
            _customMethodHelper = customMethodHelper;
            _userRepository = userRepository;
            _messageHelper = messageHelper;
            _loadDataService = loadDataService;
            _deletionService = deletionService;
            dgvRecentExams.CellDoubleClick += dgvRecentExams_CellContentClick;
            _customMethodHelper.CustomizeDataGrid(dgvRecentExams);
        }
        private void AdminHomePage_Load(object sender, EventArgs e)
        {
            LoadUserData();
        }

        private void LoadUserData()
        {
            string userName = _loadDataService.LoadUserName(_userId);
            lblWelcome.Text = $"Hoş geldiniz, {userName} - Kullanıcı ID: {_userId}";
            _customMethodHelper.InitializeExamDetailsControls(gbExamDetails, out txtCorrect, out txtWrong, out txtEmpty);
            string role = _loadDataService.LoadUserRole(_userId);
            _customMethodHelper.ConfigureGridByRole(dgvRecentExams, role);
        }

        private void btnStudentInfo_Click(object sender, EventArgs e)
        {
            string studentName = txtStudentName.Text.Trim();
            string studentSurname = txtStudentSurname.Text.Trim();
            bool selectAllStudents = chckBoxSelectAllStudent.Checked;

            if (selectAllStudents)
            {
                var allExamReports = _loadDataService.LoadAllStudentsExamReports();

                if (allExamReports != null && allExamReports.Count > 0)
                {
                    dgvRecentExams.DataSource = allExamReports;
                    _customMethodHelper.ConfigureGridByRole(dgvRecentExams, "admin");

                    _results = _loadDataService.LoadExamData(dgvRecentExams);
                    _loadDataService.LoadChartFromData(chartPerformance, _results);
                }
                else
                {
                    _messageHelper.ShowError("Hiç öğrenci sınav raporu bulunamadı.");
                    return;               
                }
            }
            else
            {
                if (string.IsNullOrEmpty(studentName) && string.IsNullOrEmpty(studentSurname))
                {
                    _messageHelper.ShowError("Lütfen öğrenci adı veya soyadını girin.");
                    return;
                }

                _selectedStudentId  = _loadDataService.LoadUserId(studentName, studentSurname);
                if(_selectedStudentId < 1)
                {
                    return;
                }
                var examReports = _loadDataService.GetStudentExamReports(_selectedStudentId);

                if (examReports != null && examReports.Count > 0)
                {
                    dgvRecentExams.DataSource = examReports;
                    _customMethodHelper.ConfigureGridByRole(dgvRecentExams, "student");

                    _results = _loadDataService.LoadExamData(dgvRecentExams);
                    _loadDataService.LoadChartFromData(chartPerformance, _results);
                }
                else
                {
                    _messageHelper.ShowError("Hiç öğrenci sınav raporu bulunamadı.");
                    return;
                }
            }
        }

        private void btnSubmitExam_Click(object sender, EventArgs e)
        {
            try
            {
                string stuName = txtStudentName2.Text;
                string stuSurname = txtStudentSurname2.Text;
                int addExamToUser = _loadDataService.LoadUserId(stuName, stuSurname);
                if (addExamToUser < 1)
                {
                    return;
                }

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
                bool isAdded = _addingService.AddExamResult(examName, examDate, addExamToUser, correctInputs, wrongInputs, emptyInputs, out string errorMessage);
                if (isAdded)
                {
                    txtStudentName2.Text = "";
                    txtStudentSurname2.Text = "";
                    txtExamName.Text = "";
                    dtpExamDate.Value = DateTime.Today;

                    for (int i = 0; i < txtCorrect.Length; i++)
                    {
                        txtCorrect[i].Text = "";
                        txtWrong[i].Text = "";
                        txtEmpty[i].Text = "";
                    }
                }
            }
            catch(Exception ex)
            {
                _messageHelper.ShowError($"Bir hata oluştu: {ex.Message}");
                return;
            }
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var scope = Program.ServiceProvider.CreateScope())
            {
                var registerForm = scope.ServiceProvider.GetRequiredService<StudentRegister>();
                registerForm.ShowDialog();
            }
            this.Show();
        }

        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            int stuId = -1;
            string name = string.Empty;
            string surname = string.Empty;

            if (_updateDeleteStudentId > 0)
            {
                stuId = _updateDeleteStudentId;
            }
            else
            {
                name = txtStudentName3.Text.Trim();
                surname = txtStudentSurname3.Text.Trim();

                if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(surname))
                {
                    _messageHelper.ShowError("Hem İsim ve hem de soyisim boş bırakılamaz.");
                    return;
                }

                var (success, userId, userName) = _deletionService.DeleteStudent(name, surname);
                if (!success || userId <= 0)
                {
                    return;
                }
                stuId = userId;
            }

            bool confirmed = _customMethodHelper.ValidateCorrectUser(stuId);

            if (confirmed)
            {
                _deletionService.ConfirmDeleteStudent(stuId);
            }
            else
            {
                _messageHelper.ShowError("Öğrenci silme işlemi iptal edildi.");
            }
        }

        private void btnDeleteExam_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtExamName2.Text))
            {
                _messageHelper.ShowError("Lütfen silmek istediğiniz sınav adını girin.");
                return;
            }
            string examName = txtExamName2.Text.Trim();
            _deletionService.DeleteExam(examName);
        }

        private void btnEditStudent_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStudentName3.Text) && (string.IsNullOrEmpty(txtStudentSurname3.Text)))
            {
                _messageHelper.ShowError("Lütfen düzenlemek istediğiniz öğrencinin adını ve soyadını girin.");
                return;
            }
            try
            {
                int id = _loadDataService.LoadUserId(txtStudentName3.Text, txtStudentSurname3.Text);

                if (id == -1)
                {
                    return;
                }
                this.Hide();
                using (var scope = Program.ServiceProvider.CreateScope())
                {
                    var updateStudentForm = _updateStudentFormFactory.Create(id);
                    updateStudentForm.ShowDialog();
                }
                this.Show();
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError($"Bir hata oluştu: {ex.Message}");
            }
        }

        private void dgvRecentExams_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int selectedStudentId = Convert.ToInt32(dgvRecentExams.Rows[e.RowIndex].Cells["StudentId"].Value);
                int examId = Convert.ToInt32(dgvRecentExams.Rows[e.RowIndex].Cells["ExamId"].Value);
                var cellValue = dgvRecentExams.Rows[e.RowIndex].Cells["ExamName"].Value;
                string DateOfExam = dgvRecentExams.Rows[e.RowIndex].Cells["ExamDate"].Value?.ToString();

                string examName = cellValue?.ToString() ?? string.Empty;

                using (var scope = Program.ServiceProvider.CreateScope())
                {
                    var examDetailsForm = _examDetailsFormFactory.Create(examId, selectedStudentId, examName, _results, DateOfExam);
                    examDetailsForm.ShowDialog();
                }
                this.Show();
            }
        }
        
        private void btnImportReport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                string stuName = txtStudentName4.Text.Trim();
                string stuSurname = txtStudentSurname4.Text.Trim();

                if (string.IsNullOrWhiteSpace(stuName) && string.IsNullOrWhiteSpace(stuSurname))
                {
                    _messageHelper.ShowError("Lütfen Mevcut bir öğrenci adı ve soyadını girin.");
                    return;
                }


                int studentId = _loadDataService.LoadUserId(stuName, stuSurname);
                if (studentId <= 0)
                {
                    return;
                }

                string username = _loadDataService.LoadUserName(studentId);
                if (string.IsNullOrWhiteSpace(username))
                {
                    _messageHelper.ShowError("Kullanıcı adı alınamadı. Lütfen geçerli bir kullanıcı seçin.");
                    return;
                }

                bool confirmed = _customMethodHelper.ValidateCorrectUser(studentId);

                if (confirmed)
                {
                    _addingService.AddExamFromPdf(studentId);
                    LoadUserData();
                }
                else
                {
                    _messageHelper.ShowError("Sınav ekleme işlemi iptal edildi.");
                }

            }
        }
    }
}
