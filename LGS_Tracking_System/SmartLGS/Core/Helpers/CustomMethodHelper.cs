using SmartLGS.Core.Interfaces;
using SmartLGS.Core.Models;
using SmartLGS.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SmartLGS.Core.Helpers
{
    class CustomMethodHelper : ICustomMethodHelper
    {
        private readonly IMessageHelper _messageHelper;
        private readonly IUserRepository _userRepository;

        public CustomMethodHelper(IMessageHelper messageHelper, IUserRepository userRepository)
        {
            _messageHelper = messageHelper;
            _userRepository = userRepository;
        }

        public void CustomizeDataGrid(DataGridView dgv)
        {
            if (dgv == null) return;

            dgv.BackgroundColor = Color.FromArgb(248, 249, 250);
            dgv.BorderStyle = BorderStyle.None;

            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false; 
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false; 
            dgv.AllowUserToResizeColumns = false;
            dgv.EditMode = DataGridViewEditMode.EditProgrammatically; 

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 37, 41);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Roboto", 10F, FontStyle.Bold);

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.Font = new Font("Roboto", 10F);
            dgv.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.MultiSelect = false;

            dgv.CellBeginEdit += (sender, e) => e.Cancel = true;
        }
        public  int GetTotalQuestions(SubjectType subject)
        {
            var field = subject.GetType().GetField(subject.ToString());
            var attribute = field.GetCustomAttribute<TotalQuestionsAttribute>();
            return attribute?.TotalQuestions ?? 0;
        }
        public (int correct, int wrong, int empty) ValidateAndAdjustExamResults(int correct, int wrong, int empty, int total, string subjectName, List<string> errorMessages)
        {
            if (correct < 0 || wrong < 0 || empty < 0)
            {
                errorMessages.Add($"{subjectName}: Cevap sayıları negatif olamaz.");
                return (0, 0, 0);
            }
            int totalAnswered = correct + wrong + empty;
            if (totalAnswered > total)
            {
                errorMessages.Add($"{subjectName}: Toplam doğru, yanlış ve boş cevap sayısı, toplam soru sayısından fazla olamaz.");
                return (0, 0, 0);
            }
            else if (totalAnswered < total)
            {
                errorMessages.Add($"{subjectName}: Toplam doğru, yanlış ve boş cevap sayısı, toplam soru sayısından az olamaz.");
                return (0, 0, 0);
            }
            return (correct, wrong, empty);
        }
        public  void ConfigureGridByRole(DataGridView dgv, string role)
        {
            SetHeaderIfExists(dgv, "StudentId", "Öğrenci ID");
            if (role == "admin")
            {
                SetHeaderIfExists(dgv, "StudentName", "Öğrenci Adı");
                SetHeaderIfExists(dgv, "StudentSurName", "Öğrenci Soyadı");
            }
            SetHeaderIfExists(dgv, "ExamId", "Sınav ID");
            SetHeaderIfExists(dgv, "ExamName", "Sınav Adı");
            SetHeaderIfExists(dgv, "ExamDate", "Tarih");
            SetHeaderIfExists(dgv, "CorrectCount", "Doğru");
            SetHeaderIfExists(dgv, "WrongCount", "Yanlış");
            SetHeaderIfExists(dgv, "BlankCount", "Boş");
            SetHeaderIfExists(dgv, "TotalQuestion", "Toplam Soru");

            if (dgv.Columns.Contains("SuccessRate"))
            {
                dgv.Columns["SuccessRate"].HeaderText = "Toplam Net";
                dgv.Columns["SuccessRate"].DefaultCellStyle.Format = "N2";
            }
        }
        public void SetHeaderIfExists(DataGridView dgv, string columnName, string headerText)
        {
            if (dgv.Columns.Contains(columnName))
            {
                dgv.Columns[columnName].HeaderText = headerText;
            }
        }
        public void InitializeExamDetailsControls(Control container, out TextBox[] txtCorrect, out TextBox[] txtWrong, out TextBox[] txtEmpty)
        {
            var subjects = Enum.GetValues(typeof(SubjectType)).Cast<SubjectType>().Select(st =>
                                new Subject
                                {
                                    SubjectId = (int)st,
                                    SubjectName = st.ToString(),
                                    TotalQuestions = GetTotalQuestions(st)
                                })
                              .ToList();

            txtCorrect = new TextBox[subjects.Count];
            txtWrong = new TextBox[subjects.Count];
            txtEmpty = new TextBox[subjects.Count];

            for (int i = 0; i < subjects.Count; i++)
            {
                var lblSubject = new Label
                {
                    Text = subjects[i].SubjectName + ":",
                    Location = new Point(20, 30 + i * 35),
                    Size = new Size(140, 25)
                };
                container.Controls.Add(lblSubject);

                txtCorrect[i] = new TextBox
                {
                    Size = new Size(50, 25),
                    Location = new Point(180, 30 + i * 35),
                    Tag = subjects[i]
                };
                container.Controls.Add(txtCorrect[i]);

                txtWrong[i] = new TextBox
                {
                    Size = new Size(50, 25),
                    Location = new Point(240, 30 + i * 35)
                };
                container.Controls.Add(txtWrong[i]);

                txtEmpty[i] = new TextBox
                {
                    Size = new Size(50, 25),
                    Location = new Point(300, 30 + i * 35)
                };
                container.Controls.Add(txtEmpty[i]);

                txtCorrect[i].Text = "0";
                txtWrong[i].Text = "0";
                txtEmpty[i].Text = "0";
            }
        }
        public int ShowStudentSelectionDialog(List<(int Id, string Name, string Surname)> students)
        {
            var selectionForm = new Form()
            {
                Text = "Öğrenci Seçimi",
                Width = 400,
                Height = 300,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen
            };

            var label = new Label()
            {
                Text = "Aynı ada sahip birden fazla öğrenci bulundu. Lütfen birini seçin:",
                Dock = DockStyle.Top,
                Padding = new Padding(10)
            };

            var listBox = new ListBox()
            {
                Dock = DockStyle.Fill,
                DisplayMember = "Display",
                ValueMember = "Id"
            };

            listBox.Items.AddRange(students.Select(s => new {
                Display = $"{s.Name} {s.Surname} (ID: {s.Id})",
                Id = s.Id
            }).ToArray());

            var okButton = new Button()
            {
                Text = "Seç",
                Dock = DockStyle.Bottom,
                DialogResult = DialogResult.OK
            };

            selectionForm.Controls.Add(listBox);
            selectionForm.Controls.Add(label);
            selectionForm.Controls.Add(okButton);
            selectionForm.AcceptButton = okButton;

            if (selectionForm.ShowDialog() == DialogResult.OK && listBox.SelectedItem != null)
            {
                dynamic selected = listBox.SelectedItem;
                return selected.Id;
            }

            //MessageBox.Show("Öğrenci seçilmedi.");
            return -1;
        }
        public bool ValidateCorrectUser(int userId)
        {
            var name = _userRepository.GetStuNameSurname(userId);
            bool confiremed =_messageHelper.ShowConfirmation($"{name.Name} {name.Surname} kullanıcı için işlemek yapılacak","Onay");
            return confiremed;
        }
    }
}
