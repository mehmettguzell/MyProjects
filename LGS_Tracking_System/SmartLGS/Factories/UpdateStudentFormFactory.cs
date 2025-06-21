using SmartLGS.Core.Interfaces;
using SmartLGS.Core.Interfaces.Factory;
using SmartLGS.UI.Admin;

namespace SmartLGS.Factories
{
    class UpdateStudentFormFactory : IUpdateStudentFormFactory
    {
        private readonly IMessageHelper _messageHelper;
        private readonly IUserRepository _userRepository;
        private readonly IValidationHelper _validationHelper;

        public UpdateStudentFormFactory(IMessageHelper messageHelper, IUserRepository userRepository, IValidationHelper validationHelper)
        {
            _messageHelper = messageHelper;
            _userRepository = userRepository;
            _validationHelper = validationHelper;
        }

        public UpdateStudentForm Create(int userId)
        {
            return new UpdateStudentForm(
                _messageHelper,
                _userRepository,
                _validationHelper,
                userId);
        }
    }
}
