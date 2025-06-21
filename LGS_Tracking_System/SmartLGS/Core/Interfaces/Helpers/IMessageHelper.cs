using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLGS.Core.Interfaces
{
    public interface IMessageHelper
    {
        void ShowError(string message);
        void ShowSuccess(string message);

        bool ShowConfirmation(string message, string caption);
    }
}
