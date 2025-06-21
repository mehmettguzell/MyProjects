using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartLGS.Core.Interfaces
{
    public interface IPdfExportService
    {
        void ExportDataGridViewToPdf(DataGridView dgv,
                                                  string filePath,
                                                  string username,
                                                  int userId,
                                                  int examId,
                                                  string examName,
                                                  List<(int examId, int net)> results,
                                                  string DateOfExam,
                                                  string StuName,
                                                  string StuSurname);
    }
}
