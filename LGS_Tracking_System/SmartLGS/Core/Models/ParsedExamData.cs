using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLGS.Core.Models
{
    public class ParsedExamData
    {
        public string ExamName { get; set; }
        public string ExamDate { get; set; }
        public string[] CorrectAnswers { get; set; }
        public string[] WrongAnswers { get; set; }
        public string[] EmptyAnswers { get; set; }
        public bool IsValid { get; set; } 

    }
}
