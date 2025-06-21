using SmartLGS.Core.Interfaces;
using System;

namespace SmartLGS.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMessageHelper _messageHelper;
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository, IMessageHelper messageHelper)
        {
            _userRepository = userRepository;
            _messageHelper = messageHelper;
        }

        public (bool success, string role, int id) Authenticate(string username, string password)
        {
            try
            {
                var result = _userRepository.GetUserRoleId(username, password);
                return (result.role != null, result.role, result.id);
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError($"An error occurred: {ex.Message}");
                return (false, null,-1);
            }
        }
        
        public (bool success, string id) Register(string name, string surname, string username, string password, string gender, string address, string phone, string email, string city)
        {
            Console.WriteLine(gender);
            try
            {
                int userId = _userRepository.AddNewStudent(name, surname, username, password, gender, address, phone, email, city);
                return (userId > 0, userId.ToString());
            }
            catch(Exception ex)
            {
                _messageHelper.ShowError($"An error occurred: {ex.Message}");
                return (false, null);
            }
        }
    
    }
}
