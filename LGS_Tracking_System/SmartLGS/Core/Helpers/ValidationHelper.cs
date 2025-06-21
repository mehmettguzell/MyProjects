using SmartLGS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

public class ValidationHelper : IValidationHelper
{
    private readonly IUserRepository _userRepository;
    public ValidationHelper(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public decimal CalculateSuccessRate(int correct, int wrong)
    {
        return correct - (wrong / 3);
    }
    
    public bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public bool IsValidDate(DateTime date)
    {
        return date <= DateTime.Now;
    }

    public bool ValidateLoginCredentials(string username, string password)
    {
        return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
    }

    public bool CheckNumbersOnly(string str)
    {
        return Regex.IsMatch(str, @"^[0-9]+$");
    }

    public bool CheckLettersOnly(string str)
    {
        return Regex.IsMatch(str, @"^[a-zA-Z]+$");
    }

    public bool IsStrongPassword(string password)
    {
        return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");
    }

    public bool IsValidPhoneNumber(string phone)
    {
        return !string.IsNullOrWhiteSpace(phone) &&
               phone.Trim().Length == 13 &&
               phone.StartsWith("+90") &&
               phone.Substring(3).All(char.IsDigit) &&
               phone[3] == '5';
    }
    
    public string FormatPhoneNumber(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone)) return phone;

        phone = phone.Trim();

        if (phone.Length == 10 && !phone.StartsWith("+90"))
            return "+90" + phone;

        if (phone.Length == 11 && phone.StartsWith("0"))
            return "+90" + phone.Substring(1);

        return phone;
    }
    
    public bool ValidateRegisterCredentials(
        string name, string surname, string username, string password, string confirmPassWord,
        string gender, string address, string phone, string email, string city, out List<string> errorMessages)
    {
        errorMessages = new List<string>();

        if (string.IsNullOrWhiteSpace(name))
            errorMessages.Add("İsim alanı zorunludur.");
        else if (!CheckLettersOnly(name))
            errorMessages.Add("İsim sadece harflerden oluşmalıdır.");

        if (string.IsNullOrWhiteSpace(surname))
            errorMessages.Add("Soyisim alanı zorunludur.");
        else if (!CheckLettersOnly(surname))
            errorMessages.Add("Soyisim sadece harflerden oluşmalıdır.");

        if (string.IsNullOrWhiteSpace(username))
            errorMessages.Add("Kullanıcı adı zorunludur.");
        else if (username.Length < 3 || username.Length > 20)
            errorMessages.Add("Kullanıcı adı 3 ile 20 karakter arasında olmalıdır.");

        if (string.IsNullOrWhiteSpace(password))
            errorMessages.Add("Şifre zorunludur.");
        else if (!IsStrongPassword(password))
            errorMessages.Add("Şifre en az 8 karakter uzunluğunda, küçük harf, büyük harf, özel karakter ve rakam içermelidir.");

        if (string.IsNullOrWhiteSpace(confirmPassWord))
            errorMessages.Add("Şifre tekrarı zorunludur.");
        else if (password != confirmPassWord)
            errorMessages.Add("Şifreler uyuşmuyor.");

        if (string.IsNullOrWhiteSpace(gender))
            errorMessages.Add("Cinsiyet alanı zorunludur.");

        if (string.IsNullOrWhiteSpace(email))
            errorMessages.Add("Email alanı zorunludur.");
        else if (!IsValidEmail(email))
            errorMessages.Add("Geçersiz email formatı.");

        if (string.IsNullOrWhiteSpace(phone))
            errorMessages.Add("Telefon numarası zorunludur.");
        else if (!IsValidPhoneNumber(phone))
            errorMessages.Add("Geçersiz telefon numarası formatı.");

        if (string.IsNullOrWhiteSpace(address))
            errorMessages.Add("Adres alanı zorunludur.");

        if (string.IsNullOrWhiteSpace(city))
            errorMessages.Add("Şehir alanı zorunludur.");
        else if (!CheckLettersOnly(city))
            errorMessages.Add("Şehir sadece harflerden oluşmalıdır.");

        return errorMessages.Count == 0;
    }


    public bool isEmailExist(string email)
    {
        return _userRepository.isEmailExist(email);
    }
    public bool isUsernameExist(string username)
    {
        return _userRepository.isUsernameExist(username);
    }
    public bool IsUsernameAvailableForUpdate(string username, int userId)
    {
        return _userRepository.UpdateUsernameControl(username, userId);
    }
    public bool IsEmailAvailableForUpdate(string email, int userId)
    {
        return _userRepository.UpdateEmailControl(email, userId);
    }
}