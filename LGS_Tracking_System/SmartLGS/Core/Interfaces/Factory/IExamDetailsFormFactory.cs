using SmartLGS.UI.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLGS.Core.Interfaces.Factory
{
    public interface IExamDetailsFormFactory
    {
        ExamDetailsForm Create(int examId, int userId, string examName, List<(int examId, int net)> results, string examDate);
    }
}
