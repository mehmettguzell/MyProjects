using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartLGS.Core.Models
{
    public class LastNExam
    {
        public int ExamId { get; set; }
        public DateTime ExamDate { get; set; }
        public string ExamName { get; set; }

        public decimal TurkceNet { get; set; }
        public decimal InkilapNet { get; set; }
        public decimal DinNet { get; set; }
        public decimal IngilizceNet { get; set; }
        public decimal MatematikNet { get; set; }
        public decimal FenNet { get; set; }
    }
}