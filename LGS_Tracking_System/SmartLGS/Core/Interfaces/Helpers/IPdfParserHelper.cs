using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartLGS.Core.Models;

namespace SmartLGS.Core.Interfaces
{
    public interface IPdfParserHelper
    {
        ParsedExamData ParseExamPdf(string pdfPath);
    }
}
