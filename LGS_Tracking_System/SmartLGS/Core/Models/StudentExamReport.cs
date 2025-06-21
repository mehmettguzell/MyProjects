using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLGS.Core.Models
{
    public class AllStudentsExamReport
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentSurName { get; set; }
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public DateTime ExamDate { get; set; }
        public int CorrectCount { get; set; }
        public int WrongCount { get; set; }
        public int BlankCount { get; set; }
        public int TotalQuestions { get; set; }
        public decimal SuccessRate { get; set; }

        public List<SubjectPerformance> SubjectPerformances { get; set; } = new List<SubjectPerformance>();
    }

    public class StudentExamReport
    {
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public DateTime ExamDate { get; set; }
        public int CorrectCount { get; set; }
        public int WrongCount { get; set; }
        public int BlankCount { get; set; }
        public int TotalQuestions { get; set; }
        public decimal SuccessRate { get; set; }

        public List<SubjectPerformance> SubjectPerformances { get; set; } = new List<SubjectPerformance>();
    }
    public class SubjectPerformance
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public int BlankAnswers { get; set; }
    }
}
