using SmartLGS.Core.Helpers;
using SmartLGS.Core.Models;
using SmartLGS.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SmartLGS.Core.Interfaces
{
    public interface ILoadDataService
    {
        string LoadUserName(int userId);
        string LoadUserRole(int userId);
        List<StudentExamReport> GetStudentExamReports(int userId);
        List<(int examId, int net)> LoadExamData(DataGridView dgv, string examIdCol = "ExamId", string successRateCol = "SuccessRate");
        void LoadChartFromData(Chart chart, List<(int examId, int net)> examResults);
        int LoadUserId(string stuName, string stuSurname);
        int LoadStuNameStuSurname(List<int> userIds);
        List<AllStudentsExamReport> LoadAllStudentsExamReports();
        List<LastNExam> GetLastNExams(int userId, int count);
    }
}
