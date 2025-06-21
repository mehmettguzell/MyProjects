using SmartLGS.Core.Interfaces;
using System;
using System.Linq;

namespace SmartLGS.Services
{
    class DeletionService : IDeletionService
    {
        private readonly IMessageHelper _messageHelper;
        private readonly IUserRepository _userRepository;
        private readonly ICustomMethodHelper _customMethodHelper;
        private readonly IExamRepository _examRepository;
        private readonly ILoadDataService _loadDataService;
        public DeletionService(IUserRepository userRepository, 
                                IMessageHelper messageHelper, 
                                ICustomMethodHelper customMethodHelper, 
                                IExamRepository examRepository,
                                ILoadDataService loadDataService) 
        {
            _loadDataService = loadDataService;
            _userRepository = userRepository;
            _messageHelper = messageHelper;
            _customMethodHelper = customMethodHelper;
            _examRepository = examRepository;
        }
        public (bool success, int userId, string userName) DeleteStudent(string name, string surname)
        {
            try
            {
                int userId = _loadDataService.LoadUserId(name, surname);
                if (userId > 0)
                {
                    string userName = _userRepository.GetUserName(userId);
                    return (true, userId, userName ?? $"{name} {surname}");
                }
                else
                {
                    return (false, 0, string.Empty);
                }
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError($"Bir hata oluştu: {ex.Message}");
                return (false, 0, string.Empty);
            }
        }

        public void ConfirmDeleteStudent(int userId)
        {
            try
            {
                var exams = _examRepository.GetUserExams(userId);
                if (exams != null && exams.Any())
                {
                    _examRepository.DeleteStudentAllExams(userId);
                }

                var stillHasExams = _examRepository.GetUserExams(userId);
                if (stillHasExams != null && stillHasExams.Any())
                {
                    _messageHelper.ShowError("Öğrencinin tüm sınavları silinemedi. Öğrenci silinemiyor.");
                    return;
                }

                _userRepository.DeleteStudent(userId);
                _messageHelper.ShowSuccess("Öğrenci başarıyla silindi.");
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError($"Bir hata oluştu: {ex.Message}");
            }
        }


        public void DeleteExam(string examName)
        {
            try
            {
                var examIds = _examRepository.GetExamIdsByName(examName);

                if (examIds.Count == 0)
                {
                    _messageHelper.ShowError("Sınav bulunamadı.");
                    return;
                }

                bool confirm = _messageHelper.ShowConfirmation(
                    $"'{examName}' adlı sınav(lar)ı silmek istediğinize emin misiniz? Toplam {examIds.Count} sınav.",
                    "Sınav Silme Onayı"
                );

                if (!confirm)
                {
                    _messageHelper.ShowError("Sınav silme işlemi iptal edildi.");
                    return;
                }

                foreach (var examId in examIds)
                {
                    bool isExamStudent = _examRepository.IsStudentExam(examId);
                    if (isExamStudent)
                    {
                        _examRepository.DeleteExamById(examId);
                    }
                    else
                    {
                        _examRepository.DeleteExamDirectlyById(examId);
                    }
                }

                _messageHelper.ShowSuccess($"'{examName}' adlı {examIds.Count} sınav başarıyla silindi.");
            }
            catch (Exception ex)
            {
                _messageHelper.ShowError($"Sınav silinirken hata oluştu: {ex.Message}");
            }
        }
    }
}
