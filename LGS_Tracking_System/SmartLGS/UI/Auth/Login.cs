using SmartLGS.admin;
using SmartLGS.student;
using System;
using System.Windows.Forms;

using Microsoft.Extensions.DependencyInjection;
using SmartLGS.Core.Interfaces;
using SmartLGS.Core.Models;
using SmartLGS.Core.Interfaces.Factory;
namespace SmartLGS
{
    public partial class Login: Form
    {
        private int _remainingAttempts = 5;
        private readonly IAuthService _authService;
        private readonly IMessageHelper _messageHelper;
        private readonly IValidationHelper _validationHelper;
        private readonly IAdminHomePageFactory _adminHomePageFactory;
        private readonly IStudentHomePageFactory _studentHomePageFactory;

        public Login(IAuthService authService, IMessageHelper messageHelper, IValidationHelper validationHelper, IAdminHomePageFactory adminHomePageFactory, IStudentHomePageFactory studentHomePageFactory)
        {
            _studentHomePageFactory = studentHomePageFactory;
            _adminHomePageFactory = adminHomePageFactory;
            _authService = authService;
            _messageHelper = messageHelper;
            _validationHelper = validationHelper;
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(!_validationHelper.ValidateLoginCredentials(txtUsername.Text, txtPassword.Text))
            {
                _messageHelper.ShowError("Lütfen kullanıcı adı ve şifreyi giriniz.");
                return;
            }

            var result = _authService.Authenticate(txtUsername.Text, txtPassword.Text);
            bool success = result.success;
            string role = result.role;
            int id = result.id;

            if (success)
            {
                HandleSuccessfulLogin(role, id);
            }
            else
            {
                HandleFailedLogin();
            }

        }
        
        private void HandleSuccessfulLogin(string userRole, int id)
        {
            _messageHelper.ShowSuccess($"Giriş başarılı! {userRole} paneline yönlendiriliyorsunuz.");
            this.Hide();

            using (var scope = Program.ServiceProvider.CreateScope())
            {
                if (userRole.Equals("admin", StringComparison.OrdinalIgnoreCase))
                {
                    var adminForm = _adminHomePageFactory.Create(id);
                    adminForm.ShowDialog();
                }
                else if (userRole.Equals("student", StringComparison.OrdinalIgnoreCase))
                {
                    var studentForm = _studentHomePageFactory.Create(id);
                    studentForm.ShowDialog();
                }
            }

            this.Show();
        }

        private void lblRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var scope = Program.ServiceProvider.CreateScope())
            {
                var registerForm = scope.ServiceProvider.GetRequiredService<StudentRegister>();
                registerForm.ShowDialog();
            }
            txtUsername.Clear();
            txtPassword.Clear();
            this.Show();
        }

        private void HandleFailedLogin()
        {
            _remainingAttempts--;

            if (_remainingAttempts > 0)
            {
                _messageHelper.ShowError($"Kullanıcı adı veya şifre hatalı. Kalan deneme hakkınız: {_remainingAttempts}.");
                txtUsername.Clear();
                txtPassword.Clear();
                txtUsername.Focus();
            }
            else
            {
                _messageHelper.ShowError("Maksimum deneme hakkını aştınız. Uygulama şimdi kapanacak.");
                Application.Exit();
            }
        }


    }
}
