using SmartLGS.Core.Interfaces;
using System.Windows.Forms;

namespace SmartLGS.Core.Helpers
{
    class MessageHelper : IMessageHelper
    {    
        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public bool ShowConfirmation(string message, string caption)
        {
            DialogResult result = MessageBox.Show(
                message,
                caption,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            return result == DialogResult.Yes;
        }
    }
}
