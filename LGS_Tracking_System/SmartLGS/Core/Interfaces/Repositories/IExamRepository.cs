using SmartLGS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLGS.Core.Interfaces
{
    public interface IExamRepository
    {
        List<ExamSubjectDetail> GetExamSubjectDetails(int examId, int userId);
        int AddExamTable(string examName, string examDate, int userId);
        int AddStudentExamsTable(int userId, int examId);
        void AddExamSubjectDetails(int studentExamId, int subjectId, int correct, int wrong, int empty);
        int GetExamId(string examName);
        void DeleteStudentAllExams(int userId);
        void DeleteExamById(int examId);
        string GetExamName(int examId);
        bool IsStudentExam(int examId);
        void DeleteExamDirectlyById(int examId);
        List<int> GetExamIdsByName(string examName);
        List<AllStudentsExamReport> GetAllStudentsExamReports();
        List<StudentExamReport> GetUserExams(int userId);
        List<LastNExam> GetLastExamNetSummaries(int userId, int count);
    }
}
