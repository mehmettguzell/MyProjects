using SmartLGS.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLGS.Core.Interfaces
{
    public interface IUserRepository
    {
        (string role, int id) GetUserRoleId(string username, string password);
        int AddNewStudent(string name, string surname, string username, string password, string gender, string address, string phone, string email, string city);
        string GetUserName(int id);
        string GetUserRole(int id);
        List<int> GetUserId(string stuName, string stuSurname);
        void DeleteStudent(int userId);
        SmartLGS.Core.Models.Student GetStudentTable(int userId);
        void UpdateStudentTable(int userId, string name, string surname, string gender, string address, string phone, string email, string city);
        (string Name, string Surname) GetStuNameSurname(int stuId);
        bool isEmailExist(string Email);
        bool UpdateUsernameControl(string username, int userId);
        bool isUsernameExist(string username);
        bool UpdateEmailControl(string email, int userId);
    }
}