using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2(List<long> executionTimes)
        {
            InitializeComponent();
            ShowExecutionStats(executionTimes);
        }

        private void ShowExecutionStats(List<long> executionTimes)
        {
            var first100 = executionTimes.GetRange(0, Math.Min(100, executionTimes.Count));
            long total = 0;

            foreach (var t in first100)
            {
                total += t;
                lstTimes.Items.Add($"{t} ms");
            }

            double avg = first100.Count > 0 ? total / (double)first100.Count : 0;
            lblTotal.Text = $"Total Duration: {total} ms";
            lblAvg.Text = $"Average Time: {avg:F2} ms";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}