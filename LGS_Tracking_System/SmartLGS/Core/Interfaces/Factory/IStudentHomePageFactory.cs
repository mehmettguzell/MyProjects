using SmartLGS.student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLGS.Core.Interfaces.Factory
{
    public interface IStudentHomePageFactory
    {
        StudentHomePage Create(int userId);
    }
}
