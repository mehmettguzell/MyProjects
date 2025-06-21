using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLGS.Core.Models
{
    public class ExamSubjectDetail
    {
        public string SubjectName { get; set; }
        public int TrueAnswers { get; set; }
        public int FalseAnswers { get; set; }
        public int BlankAnswers { get; set; }
        public int TotalQuestions { get; set; }

        public decimal NetScore
        {
            get
            {
                decimal score = TrueAnswers - (FalseAnswers / 3m);
                return Math.Round(score, 2);
            }
        }
    }
}
