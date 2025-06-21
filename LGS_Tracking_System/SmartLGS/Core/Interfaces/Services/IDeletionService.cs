using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLGS.Core.Interfaces
{
    public interface IDeletionService
    {
        (bool success, int userId, string userName) DeleteStudent(string name, string surname);

        void ConfirmDeleteStudent(int userId);

        void DeleteExam(string examName);
    }
}
