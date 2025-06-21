using SmartLGS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using SmartLGS.Core.Models;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using iText.StyledXmlParser.Jsoup.Helper;

namespace SmartLGS.Services
{
    class AddingService : IAddingService
    {
        private readonly IMessageHelper _messageHelper;
        private readonly ICustomMethodHelper _customMethodHelper;
        private readonly IExamRepository _examRepository;
        private readonly IUserRepository _userRepository;

        public AddingService(   IMessageHelper messageHelper, 
                                ICustomMethodHelper customMethodHelper, 
                                IExamRepository examRepository,
                                IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _messageHelper = messageHelper;
            _customMethodHelper = customMethodHelper;
            _examRepository = examRepository;
        }

        public bool AddExamResult(string examName, string examDate, int userId, string[] correctInputs, string[] wrongInputs, string[] emptyInputs, out string errorMessage)
        {
            errorMessage = string.Empty;
            var subjects = Enum.GetValues(typeof(SubjectType))
                .Cast<SubjectType>()
                .Select(st => new Subject
                {
                    SubjectId = (int)st,
                    SubjectName = st.ToString(),
                    TotalQuestions = _customMethodHelper.GetTotalQuestions(st)
                })
                .ToList();

            if (correctInputs.Length != subjects.Count || wrongInputs.Length != subjects.Count || emptyInputs.Length != subjects.Count)
            {
                errorMessage = "Giriş alanlarının sayısı, ders sayısı ile uyuşmuyor.";
                return false;
            }

            var examDetails = new List<ExamResult>();
            bool isValid = true;
            List<string> errorMessages = new List<string>();

            for (int i = 0; i < subjects.Count; i++)
            {
                int correct = int.TryParse(correctInputs[i], out int c) ? c : 0;
                int wrong = int.TryParse(wrongInputs[i], out int w) ? w : 0;
                int empty = int.TryParse(emptyInputs[i], out int emp) ? emp : 0;

                var result = _customMethodHelper.ValidateAndAdjustExamResults(correct, wrong, empty, subjects[i].TotalQuestions, subjects[i].SubjectName, errorMessages);

                if (result.correct == 0 && result.wrong == 0 && result.empty == 0)
                {
                    isValid = false;
                    continue;
                }

                examDetails.Add(new ExamResult
                {
                    SubjectId = subjects[i].SubjectId,
                    SubjectName = subjects[i].SubjectName,
                    Correct = result.correct,
                    Wrong = result.wrong,
                    Empty = result.empty
                });
            }

            if (isValid)
            {
                bool withResult = true;
                AddExam(examDetails, examName, examDate, userId, withResult);
                return true;
            }
            else
            {
                errorMessage = errorMessages.Any()
                    ? string.Join("\n", errorMessages)
                    : "Lütfen sınav sonuçlarını kontrol edip tekrar deneyin.";
                return false;
            }
        }
        
        public void AddExam(List<ExamResult> examDetails, string examName, string examDate, int userId, bool withResult)
        {
            try
            {
                int examId = _examRepository.AddExamTable(examName, examDate, userId);
                int studentExamId = 0;
                if (examId > 0)
                {
                    studentExamId = _examRepository.AddStudentExamsTable(userId, examId);
                }
                else
                {
                    _messageHelper.ShowError("_examRepository.AddExamTable() hatası");
                }
                if (examId > 0 && studentExamId > 0)
                {
                    for (int i = 0; i < examDetails.Count; i++)
                    {
                        var examResult = examDetails[i];
                        int subjectId = examResult.SubjectId;
                        int correct = examResult.Correct;
                        int wrong = examResult.Wrong;
                        int empty = examResult.Empty;
                        _examRepository.AddExamSubjectDetails(studentExamId, subjectId, correct, wrong, empty);
                    }
                    _messageHelper.ShowSuccess("Sınav başarıyla eklendi.");
                }
                else
                {
                    _messageHelper.ShowError("AddStudentExamsTable patladı");
                }
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError($"An error occurred: {ex.Message}");
            }
        }

        public void AddExamFromPdf(int id)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PDF Dosyaları (*.pdf)|*.pdf";
                openFileDialog.Title = "PDF Raporunu Seçin";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string pdfPath = openFileDialog.FileName;
                    string examName = Path.GetFileNameWithoutExtension(pdfPath);

                    try
                    {
                        var pdfParser = Program.ServiceProvider.GetRequiredService<IPdfParserHelper>();
                        var examData = pdfParser.ParseExamPdf(pdfPath);

                        if (examData == null || !examData.IsValid)
                        {
                            MessageBox.Show("PDF geçerli bir sınav raporu içermiyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string examDate = examData.ExamDate;
                        string[] correct = examData.CorrectAnswers;
                        string[] wrong = examData.WrongAnswers;
                        string[] empty = examData.EmptyAnswers;

                        AddExamResult(examName, examDate, id, correct, wrong, empty, out string errorMessage);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }
    }
}