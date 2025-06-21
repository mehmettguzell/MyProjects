using SmartLGS.Core.Interfaces;
using SmartLGS.Core.Interfaces.Factory;
using SmartLGS.UI.Student;
using System.Collections.Generic;

namespace SmartLGS.Factories
{
    public class ExamDetailsFormFactory : IExamDetailsFormFactory
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomMethodHelper _customMethodHelper;
        private readonly IExamRepository _examRepository;
        private readonly IPdfExportService _pdfExportService;

        public ExamDetailsFormFactory(
            IAddingService addingService,
            IUserRepository userRepository,
            ICustomMethodHelper customMethodHelper,
            IExamRepository examRepository,
            IPdfExportService pdfExportService)
        {
            _userRepository = userRepository;
            _customMethodHelper = customMethodHelper;
            _examRepository = examRepository;
            _pdfExportService = pdfExportService;
        }

        public ExamDetailsForm Create(int examId, int userId, string examName, List<(int examId, int net)> results, string examDate)
        {
            return new ExamDetailsForm(
                _userRepository,
                _customMethodHelper,
                _examRepository,
                _pdfExportService,
                examId,
                userId,
                examName,
                results,
                examDate);
        }
    }
}