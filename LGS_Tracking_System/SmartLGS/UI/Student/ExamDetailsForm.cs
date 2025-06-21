using iTextSharp.text.pdf;
using SmartLGS.Core.Interfaces;
using SmartLGS.Core.Interfaces.Factory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartLGS.UI.Student
{
    public partial class ExamDetailsForm : Form
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomMethodHelper _customMethodHelper;
        private readonly IExamRepository _examRepository;
        private readonly IPdfExportService _pdfExportService;
        private readonly int _examId;
        private readonly int _userId;
        private readonly string _examName;
        private readonly string _examDate = DateTime.Now.ToString("dd/MM/yyyy");
        List<(int examId, int net)> _results;


        public ExamDetailsForm( IUserRepository userRepository,
                                ICustomMethodHelper customMethodHelper,
                                IExamRepository examRepository,
                                IPdfExportService pdfExportService,
                                int examId,
                                int userId,
                                string examName,
                                List<(int examId, int net)> results,
                                string examDate)
        {
            _userRepository = userRepository;
            _customMethodHelper = customMethodHelper;
            _examRepository = examRepository;
            _pdfExportService = pdfExportService;

            _examId = examId;
            _examName = examName;
            _userId = userId;
            _examDate = examDate;
            _results = results;

            InitializeComponent();
            LoadExamInfo(examName, _userId);
            LoadExamDetails();
            _customMethodHelper.CustomizeDataGrid(dgvExamDetails);
        }

        private void LoadExamInfo(string examName, int userId)
        {
            string _userName = _userRepository.GetUserName(userId);
            lblTitle.Text = _userName + "_"  + examName;
        }
        private void LoadExamDetails()
        {
            var examDetails = _examRepository.GetExamSubjectDetails(_examId, _userId);
            if (examDetails != null && examDetails.Count > 0)
            {   
                DataTable dtBody = new DataTable();
                dtBody.Columns.Add("Ders Adı", typeof(string));
                dtBody.Columns.Add("Doğru Sayısı", typeof(int));
                dtBody.Columns.Add("Yanlış Sayısı", typeof(int));
                dtBody.Columns.Add("Boş Sayısı", typeof(int));
                dtBody.Columns.Add("Toplam Soru", typeof(int));
                dtBody.Columns.Add("Net", typeof(decimal));

                foreach (var detail in examDetails)
                {
                    dtBody.Rows.Add(
                        detail.SubjectName,
                        detail.TrueAnswers,
                        detail.FalseAnswers,
                        detail.BlankAnswers,
                        detail.TotalQuestions,
                        detail.NetScore
                    );
                }

                dgvExamDetails.DataSource = dtBody;
            }
            else
            {
                MessageBox.Show("Bu sınava ait detay bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnDownloadReport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Dosyaları (*.pdf)|*.pdf";
            saveFileDialog.Title = "Sınav Raporunu PDF Olarak Kaydet";
            string username = _userRepository.GetUserName(_userId);
            saveFileDialog.FileName = $"{_examName}_{username}_Rapor.pdf";
            (string name, string surname) = _userRepository.GetStuNameSurname(_userId);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _pdfExportService.ExportDataGridViewToPdf(dgvExamDetails, saveFileDialog.FileName, username, _userId, _examId, _examName, _results, _examDate, name, surname);
                    MessageBox.Show("PDF başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("PDF oluşturulurken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}