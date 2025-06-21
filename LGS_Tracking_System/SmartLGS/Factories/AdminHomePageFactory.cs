using SmartLGS.Core.Interfaces.Factory;
using SmartLGS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartLGS.admin;

namespace SmartLGS.Factories
{
    class AdminHomePageFactory : IAdminHomePageFactory
    {
        private readonly IAddingService _addingService;
        private readonly ICustomMethodHelper _customMethodHelper;
        private readonly IUserRepository _userRepository;
        private readonly IMessageHelper _messageHelper;
        private readonly ILoadDataService _loadDataService;
        private readonly IDeletionService _deletionService;
        private readonly IExamDetailsFormFactory _examDetailsFormFactory;
        private readonly IExamRepository _examRepository;
        private readonly IUpdateStudentFormFactory _updateStudentFormFactory;
        public AdminHomePageFactory(
            IAddingService addingService,
            ICustomMethodHelper customMethodHelper,
            IUserRepository userRepository,
            IMessageHelper messageHelper,
            ILoadDataService loadDataService,
            IDeletionService deletionService,
            IExamRepository examRepository,
            IExamDetailsFormFactory examDetailsFormFactory,
            IUpdateStudentFormFactory updateStudentFormFactory)
        {
            _examRepository = examRepository;
            _addingService = addingService;
            _customMethodHelper = customMethodHelper;
            _userRepository = userRepository;
            _messageHelper = messageHelper;
            _loadDataService = loadDataService;
            _deletionService = deletionService;
            _examDetailsFormFactory = examDetailsFormFactory;
            _updateStudentFormFactory = updateStudentFormFactory;
        }

        public AdminHomePage Create(int userId)
        {
            return new AdminHomePage(
                _addingService,
                _customMethodHelper,
                _userRepository,
                _messageHelper,
                _loadDataService,
                _deletionService,
                _examRepository,
                _examDetailsFormFactory,
                _updateStudentFormFactory,
                userId
            );
        }
    }
}
