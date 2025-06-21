using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLGS.Core.Models
{
    public class ExamResult
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int Correct { get; set; }
        public int Wrong { get; set; }
        public int Empty { get; set; }
    }
}
