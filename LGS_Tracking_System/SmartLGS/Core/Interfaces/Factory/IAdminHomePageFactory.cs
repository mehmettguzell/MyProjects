using SmartLGS.admin;
using SmartLGS.Factories;
using SmartLGS.UI.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLGS.Core.Interfaces.Factory
{
    public interface IAdminHomePageFactory
    {
        AdminHomePage Create(int userId);
    }
}
