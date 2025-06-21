using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLGS.Core.Interfaces
{
    public interface IValidationHelper
    {
        string FormatPhoneNumber(string phone);
        bool IsValidPhoneNumber(string phone);
        bool IsValidEmail(string email);
        bool IsValidDate(DateTime date);
        bool ValidateLoginCredentials(string username, string password);
        bool ValidateRegisterCredentials(
            string name, string surname, string username, string password, string confirmPassWord,
            string gender, string address, string phone, string email, string city, out List<string> errorMessages);
        decimal CalculateSuccessRate(int correct, int wrong);

        bool CheckNumbersOnly(string str);
        bool CheckLettersOnly(string str);
        bool IsStrongPassword(string password);
        bool isEmailExist(string email);
        bool IsUsernameAvailableForUpdate(string username, int userId);
        bool isUsernameExist(string username);
        bool IsEmailAvailableForUpdate(string email, int userId);
    }
}
