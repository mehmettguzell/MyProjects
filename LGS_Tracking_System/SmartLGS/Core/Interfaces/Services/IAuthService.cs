using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLGS.Core.Interfaces
{
    public interface IAuthService
    {
        (bool success, string role, int id) Authenticate(string username, string password);
        (bool success, string id) Register(string name, string surname, string username, string password, string gender, string address, string phone, string email, string city);
    }
}
