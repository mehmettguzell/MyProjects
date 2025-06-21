using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Windows.Forms;
using SmartLGS.Core.Helpers;
using SmartLGS.Core.Interfaces;
using SmartLGS.Data.Repositories;
using SmartLGS.Services;
using SmartLGS.admin;
using SmartLGS.student;
using SmartLGS.UI.Student;
using SmartLGS.UI.Admin;
using SmartLGS.Core.Interfaces.Factory;
using SmartLGS.Factories;

namespace SmartLGS
{
    static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ConfigureServices();

            using (var scope = ServiceProvider.CreateScope())
            {
                var loginForm = scope.ServiceProvider.GetRequiredService<Login>();
                Application.Run(loginForm);
            }
        }

        static void ConfigureServices()
        {
            var services = new ServiceCollection();

            // Configuration
            string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            // Repositories
            services.AddScoped<IUserRepository>(provider =>new UserRepository(connectionString, provider.GetRequiredService<IMessageHelper>()));
            services.AddScoped<IExamRepository>(provider =>
                new ExamRepository(
                    connectionString,
                    provider.GetRequiredService<IMessageHelper>(),
                    provider.GetRequiredService<IValidationHelper>()
                )
            );

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAddingService, AddingService>();
            services.AddScoped<ILoadDataService, LoadDataService>();
            services.AddScoped<IDeletionService, DeletionService>();
            services.AddScoped<IPdfExportService, PdfExportService>();

            // Helpers
            services.AddScoped<IPdfParserHelper, PdfParserHelper>();
            services.AddSingleton<IMessageHelper, MessageHelper>();
            services.AddSingleton<IValidationHelper, ValidationHelper>();
            services.AddSingleton<ICustomMethodHelper, CustomMethodHelper>();

            //Factories
            services.AddScoped<IExamDetailsFormFactory, ExamDetailsFormFactory>();
            services.AddScoped<IAdminHomePageFactory, AdminHomePageFactory>();
            services.AddScoped<IStudentHomePageFactory, StudentHomePageFactory>();
            services.AddScoped<IUpdateStudentFormFactory, UpdateStudentFormFactory>();

            // Forms
            services.AddTransient<Login>();
            services.AddTransient<AdminHomePage>();
            services.AddTransient<StudentHomePage>();
            services.AddTransient<StudentRegister>();
            services.AddTransient<ExamDetailsForm>();
            services.AddScoped<UpdateStudentForm>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}