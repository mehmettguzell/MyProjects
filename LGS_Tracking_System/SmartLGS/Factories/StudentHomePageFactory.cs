using SmartLGS.admin;
using SmartLGS.Core.Interfaces;
using SmartLGS.Core.Interfaces.Factory;
using SmartLGS.student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLGS.Factories
{
    class StudentHomePageFactory : IStudentHomePageFactory
    {
        private readonly IAddingService _addingHomeService;
        private readonly ICustomMethodHelper _customMethodHelper;
        private readonly ILoadDataService _loadDataService;
        private readonly IExamDetailsFormFactory _examDetailsFormFactory;
        public StudentHomePageFactory(  IExamDetailsFormFactory examDetailsFormFactory,
                                        IAddingService studentHomeService,
                                        ICustomMethodHelper customMethodHelper,
                                        ILoadDataService loadDataService)
        {
            _examDetailsFormFactory = examDetailsFormFactory;
            _addingHomeService = studentHomeService;
            _customMethodHelper = customMethodHelper;
            _loadDataService = loadDataService;
        }
        public StudentHomePage Create(int userId)
        {
            return new StudentHomePage(
                _examDetailsFormFactory,
                _addingHomeService,
                _customMethodHelper,
                _loadDataService,
                userId
            );
        }
    }
}
