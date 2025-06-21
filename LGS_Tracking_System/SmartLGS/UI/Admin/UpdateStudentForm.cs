using SmartLGS.Core.Interfaces;
using System.Windows.Forms;
using System;

namespace SmartLGS.UI.Admin
{
    public partial class UpdateStudentForm : Form
    {
        private readonly IMessageHelper _messageHelper;
        private readonly IUserRepository _userRepository;
        private readonly IValidationHelper _validationHelper;
        private int _userId;

        public UpdateStudentForm( IMessageHelper messageHelper, 
                                  IUserRepository userRepository,
                                  IValidationHelper validationHelper,
                                  int userId  )
        {
            _userId = userId;
            _userRepository = userRepository;
            _messageHelper = messageHelper;
            _validationHelper = validationHelper;
            InitializeComponent();
            LoadStudentData();
        }
        private void LoadStudentData()
        {
            try
            {
                SmartLGS.Core.Models.Student student = _userRepository.GetStudentTable(_userId);

                if (student != null)
                {
                    txtName.Text = student.Name ?? string.Empty;
                    txtSurname.Text = student.Surname ?? string.Empty;
                    txtUsername.Text = student.Username ?? string.Empty;
                    txtAddress.Text = student.Address ?? string.Empty;
                    txtPhone.Text = student.Phone ?? string.Empty;
                    txtEmail.Text = student.Email ?? string.Empty;
                    txtCity.Text = student.City ?? string.Empty;

                    if (!string.IsNullOrEmpty(student.Gender))
                    {
                        cmbGender.SelectedItem = student.Gender;
                    }
                    else
                    {
                        cmbGender.SelectedIndex = -1;
                    }
                }
                else
                {
                    _messageHelper.ShowError("Öğrenci bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError($"Bir hata oluştu: {ex.Message}");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool isEmailExist = _validationHelper.IsEmailAvailableForUpdate(txtEmail.Text, _userId);
                if (isEmailExist)
                {
                    _messageHelper.ShowError("Bu email adresi zaten var. Lütfen farklı bir mail adresi girin.");
                    return;
                }
                bool isUsernameExist = _validationHelper.IsUsernameAvailableForUpdate(txtUsername.Text, _userId);
                if (isUsernameExist)
                {
                    _messageHelper.ShowError("Bu kullanıcı adı zaten var. Lütfen farklı bir kullanıcı adı girin.");
                    return;
                }
                _userRepository.UpdateStudentTable(_userId, txtName.Text, txtSurname.Text,
                    cmbGender.SelectedItem?.ToString(), txtAddress.Text, txtPhone.Text,
                    txtEmail.Text, txtCity.Text);
                _messageHelper.ShowSuccess("Öğrenci bilgileri başarıyla güncellendi.");
                this.Close();
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError($"Hata: {ex.Message}");
            }
        }
    }
}