using Microsoft.Extensions.DependencyInjection;
using SmartLGS.admin;
using SmartLGS.Core.Interfaces;
using SmartLGS.Data.Repositories;
using SmartLGS.student;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SmartLGS
{
    public partial class StudentRegister: Form
    {
        private readonly IAuthService _authService;
        private readonly IMessageHelper _messageHelper;
        private readonly IValidationHelper _validationHelper;

        public StudentRegister(IAuthService authService,
                               IMessageHelper messageHelper,
                               IValidationHelper validationHelper)
        {
            InitializeComponent();
            _authService = authService;
            _messageHelper = messageHelper;
            _validationHelper = validationHelper;
        }
        private void StudentRegister_Load(object sender, EventArgs e)
        {
            InitializePhoneInput(txtPhone);

            var genderOptions = new List<KeyValuePair<int, string>>
                {
                    new KeyValuePair<int, string>(1, "Man"),
                    new KeyValuePair<int, string>(2, "Woman")
                };
            cmbGender.DataSource = genderOptions;
            cmbGender.DisplayMember = "Value";
            cmbGender.ValueMember = "Key";
        }

        private void InitializePhoneInput(TextBox phoneTextBox)
        {
            phoneTextBox.Text = "+90";
            phoneTextBox.Enter += (s, e) => {
                if (phoneTextBox.Text == "+90")
                    phoneTextBox.SelectionStart = 3;
            };

            phoneTextBox.KeyPress += (s, e) => {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                    return;
                }

                if (phoneTextBox.SelectionStart < 3 && phoneTextBox.Text.StartsWith("+90"))
                    e.Handled = true;

                if (phoneTextBox.Text.Length >= 13 && phoneTextBox.SelectionLength == 0)
                    e.Handled = true;
            };
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string formattedPhone = _validationHelper.FormatPhoneNumber(txtPhone.Text.Trim());

                string genderText = cmbGender.Text;

                bool isValid = _validationHelper.ValidateRegisterCredentials(
                    txtName.Text.Trim(), txtSurname.Text.Trim(), txtUsername.Text.Trim(),
                    txtPassword.Text.Trim(), txtConfirmPassword.Text.Trim(), genderText,
                    txtAddress.Text.Trim(), txtPhone.Text.Trim(), txtEmail.Text.Trim(), txtCity.Text.Trim(),
                    out List<string> errorMessages);

                if (!isValid)
                {
                    _messageHelper.ShowError(string.Join("\n", errorMessages));
                    return;
                }
                bool isEmailExist = _validationHelper.isEmailExist(txtEmail.Text);
                if (isEmailExist)
                {
                    _messageHelper.ShowError("Bu email adresi zaten var. Lütfen farklı bir mail adresi girin.");
                    return;
                }
                bool isUsernameExist = _validationHelper.isUsernameExist(txtUsername.Text);
                if (isUsernameExist)
                {
                    _messageHelper.ShowError("Bu kullanıcı adı zaten var. Lütfen farklı bir kullanıcı adı girin.");
                    return;
                }


                var result = _authService.Register(txtName.Text.Trim(), txtSurname.Text.Trim(), txtUsername.Text.Trim(),
                    txtPassword.Text.Trim(), genderText,
                    txtAddress.Text.Trim(), txtPhone.Text.Trim(), txtEmail.Text.Trim(), txtCity.Text.Trim());

                bool success = result.success;
                string studentId = result.id;

                if (success)
                {
                    _messageHelper.ShowSuccess($"Kayıt Başarılı!");
                    this.Hide();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError(ex.ToString());
                btnSave.Enabled = true;
                return;
            }
        }

        private void ClearForm()
        {
            txtName.Text = "";
            txtSurname.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            cmbGender.SelectedIndex = -1;
            txtAddress.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtCity.Text = "";
        }

    }
}
