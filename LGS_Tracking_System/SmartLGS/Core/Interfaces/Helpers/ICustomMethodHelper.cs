using SmartLGS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartLGS.Core.Interfaces
{
    public interface ICustomMethodHelper
    {
        void CustomizeDataGrid(DataGridView dgv);
        int GetTotalQuestions(SubjectType subject);
        (int correct, int wrong, int empty) ValidateAndAdjustExamResults(int correct, int wrong, int empty, int total, string subjectName, List<string> errorMessages);
        void ConfigureGridByRole(DataGridView dgv, string role);
        void InitializeExamDetailsControls(Control container, out TextBox[] txtCorrect, out TextBox[] txtWrong, out TextBox[] txtEmpty);
        int ShowStudentSelectionDialog(List<(int Id, string Name, string Surname)> students);
        bool ValidateCorrectUser(int userId);
    }
}
