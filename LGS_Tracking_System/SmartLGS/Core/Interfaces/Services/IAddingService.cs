using SmartLGS.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLGS.Core.Interfaces
{
    public interface IAddingService
    {
        void AddExam(List<ExamResult> examDetails, string examName, string examDat, int userId, bool withResult);

        bool AddExamResult(string examName, string examDate, int userId, string[] correctInputs, string[] wrongInputs, string[] emptyInputs, out string errorMessage);

        void AddExamFromPdf(int id);
    }
}
