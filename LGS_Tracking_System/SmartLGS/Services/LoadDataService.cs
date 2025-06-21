using SmartLGS.Core.Interfaces;
using System;
using System.Collections.Generic;
using SmartLGS.Core.Models;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using System.Drawing;
using System.Data.SqlClient;

namespace SmartLGS.Services
{
    class LoadDataService : ILoadDataService
    {
        private readonly IMessageHelper _messageHelper;
        private readonly IUserRepository _userRepository;
        private readonly ICustomMethodHelper _customMethodHelper;
        private readonly IExamRepository _examRepository;
        private readonly IValidationHelper _validationHelper;   

        public LoadDataService(IUserRepository userRepository, 
                                IMessageHelper messageHelper, 
                                ICustomMethodHelper customMethodHelper, 
                                IExamRepository examRepository,
                                IValidationHelper validationHelper)
        {
            _validationHelper = validationHelper;
            _userRepository = userRepository;
            _messageHelper = messageHelper;
            _customMethodHelper = customMethodHelper;
            _examRepository = examRepository;
        }
        public string LoadUserName(int userId)
        {
            try
            {
                string userName = _userRepository.GetUserName(userId);
                return userName;
            }
            catch (Exception)
            {
                _messageHelper.ShowError("Kullanıcı adı alınırken bir hata oluştu.");
                return string.Empty;
            }
        }

        public int LoadUserId(string stuName, string stuSurname)
        {
            List<int> userIds = new List<int>();
            userIds = _userRepository.GetUserId(stuName, stuSurname);
            if (userIds == null || userIds.Count == 0)
            {
                return -1;
            }
            bool isMultiple = userIds.Count > 1 ? true : false;
            if (isMultiple)
            {
                 return LoadStuNameStuSurname(userIds);
            }
            else
            {
                return  userIds[0];
            }
        }
        
        public int LoadStuNameStuSurname(List<int> userIds)
        {
            var result = new List<(int id, string name, string surname)>();

            foreach (int userId in userIds)
            {
                var (name, surname) = _userRepository.GetStuNameSurname(userId);
                result.Add((userId, name, surname));
            }
            int selectedStuId = _customMethodHelper.ShowStudentSelectionDialog(result);
            if (selectedStuId < 1)
            {
                _messageHelper.ShowError("Öğrenci seçilmedi.");
                return -1;
            }
            return selectedStuId;
        }

        public string LoadUserRole(int userId)
        {
            try
            {
                string role = _userRepository.GetUserRole(userId);
                return role;
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError($"Kullanıcının rolü alınırken hata oluştu: {ex.Message}");
                return string.Empty;
            }
        }

        public List<AllStudentsExamReport> LoadAllStudentsExamReports()
        {
            try
            {
                return _examRepository.GetAllStudentsExamReports();
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError($"Sınav raporları yüklenirken hata oluştu: {ex.Message}");
                return new List<AllStudentsExamReport>();
            }
        }
       
        public List<StudentExamReport> GetStudentExamReports(int userId)
        {
            try
            {
                return _examRepository.GetUserExams(userId);
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError($"Sınav raporları yüklenirken hata oluştu: {ex.Message}");
                return new List<StudentExamReport>();
            }
        }

        public List<(int examId, int net)> LoadExamData(DataGridView dgv, string examIdCol = "ExamId", string successRateCol = "SuccessRate")
        {
            var results = new List<(int examId, int net)>();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                if (int.TryParse(row.Cells[successRateCol]?.Value?.ToString(), out int net) &&
                    int.TryParse(row.Cells[examIdCol]?.Value?.ToString(), out int examId))
                {
                    results.Add((examId, net));
                }
            }

            return results.OrderBy(r => r.examId).ToList();
        }

        public void LoadChartFromData(Chart chart, List<(int examId, int net)> examResults)
        {
            chart.Series.Clear();
            chart.ChartAreas.Clear();

            ChartArea area = new ChartArea("ExamPerformanceArea");
            chart.ChartAreas.Add(area);

            area.AxisX.Interval = 1;
            area.AxisX.MajorGrid.LineColor = Color.LightGray;

            Series series = new Series("Başarı Notları");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.FromArgb(100, 149, 237);

            foreach (var result in examResults)
            {
                series.Points.AddXY(result.examId.ToString(), result.net);
            }

            chart.Series.Add(series);

            chart.Titles.Clear();
            chart.Titles.Add("Sınav Performansı");

            area.AxisX.Title = "Sınav ID";
            area.AxisY.Title = "Net";
        }

        public List<LastNExam> GetLastNExams(int userId, int count)
        {
            try
            {
                var sonuclar = _examRepository.GetLastExamNetSummaries(userId, count);
                return sonuclar;
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError($"Son {count} sınav detayları yüklenirken hata oluştu: {ex.Message}");
                return new List<LastNExam>();
            }
        }
    }
}
