using AutoMapper;

using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Appeals.Request;
using DogSitterMarketplaceBll.Models.Appeals.Response;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Appeals;
using ILogger = NLog.ILogger;
using LogLevel = NLog.LogLevel;


namespace DogSitterMarketplaceBll.Services
{
    public class AppealService : IAppealService
    {
        private readonly IMapper _mapper;
        private readonly IAppealRepository _appealRepository;
        private readonly ILogger _logger;

        public AppealService(IAppealRepository appealRepository, IMapper mapper, ILogger nLogger)
        {
            _mapper = mapper;
            _appealRepository = appealRepository;
            _logger = nLogger;
        }

        public IEnumerable<AppealResponse> GetAllAppeals()
        {
            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} start {nameof(GetAllAppeals)}");

            var allappealsEntitys = _appealRepository.GetAllAppeals();
            var appealResponse = _mapper.Map<IEnumerable<AppealResponse>>(allappealsEntitys);

            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} end {nameof(GetAllAppeals)}");

            return appealResponse;
        }

        public IEnumerable<AppealResponse> GetAllNotAnsweredAppeals()
        {
            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} start {nameof(GetAllNotAnsweredAppeals)}");

            var allappealsEntitys = _appealRepository.GetAllAppeals();
            var appealsEntitys = allappealsEntitys
                           .Where(u => u.StatusId == 1);
            var appealResponse = _mapper.Map<IEnumerable<AppealResponse>>(appealsEntitys);

            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} end {nameof(GetAllNotAnsweredAppeals)}");

            return appealResponse;
        }

        public AppealResponse GetAppealById(int id)
        {
            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} start {nameof(GetAppealById)}");

            var appealEntitys = _appealRepository.GetAppealById(id);
            var appealResponse = _mapper.Map<AppealResponse>(appealEntitys);

            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} end {nameof(GetAppealById)}");

            return appealResponse;
        }

        public IEnumerable<AppealStatusResponse> GetAllAppealStatuses()
        {
            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} start {nameof(GetAllAppealStatuses)}");

            var allappealsStatusesEntitys = _appealRepository.GetAllAppealStatuses();
            var appealStatusesResponse = _mapper.Map<IEnumerable<AppealStatusResponse>>(allappealsStatusesEntitys);

            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} end {nameof(GetAllAppealStatuses)}");

            return appealStatusesResponse;
        }

        public IEnumerable<AppealTypeResponse> GetAllAppealTypes()
        {
            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} start {nameof(GetAllAppealTypes)}");

            var allappealsTypesEntitys = _appealRepository.GetAllAppealTypes();
            var appealTypesResponse = _mapper.Map<IEnumerable<AppealTypeResponse>>(allappealsTypesEntitys);

            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} end {nameof(GetAllAppealTypes)}");

            return appealTypesResponse;
        }

        public AppealResponse GetAppealByUserIdToWhom(int id)
        {
            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} start {nameof(GetAppealByUserIdToWhom)}");

            var appealEntitys = _appealRepository.GetAppealByUserIdToWhom(id);
            var appealResponse = _mapper.Map<AppealResponse>(appealEntitys);

            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} end {nameof(GetAppealByUserIdToWhom)}");

            return appealResponse;
        }

        public AppealResponse GetAppealByUserIdFromWhom(int id)
        {
            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} start {nameof(GetAppealByUserIdFromWhom)}");

            var appealEntitys = _appealRepository.GetAppealByUserIdFromWhom(id);
            var appealResponse = _mapper.Map<AppealResponse>(appealEntitys);

            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} end {nameof(GetAppealByUserIdFromWhom)}");

            return appealResponse;
        }

        public AppealResponse AddAppeal(AppealRequest appeal)
        {
            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} start {nameof(AddAppeal)}");

            var appealEntity = _mapper.Map<AppealEntity>(appeal);
            var addAppealEntity = _appealRepository.AddAppeal(appealEntity);
            var addAppealResponse = _mapper.Map<AppealResponse>(addAppealEntity);

            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} end {nameof(AddAppeal)}");

            return addAppealResponse;
        }

        public AppealStatusResponse AddAppealStatus(AppealStatusRequest appealStatus)
        {
            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} start {nameof(AddAppealStatus)}");

            var appealStatusEntity = _mapper.Map<AppealStatusEntity>(appealStatus);
            var addAppealStatusEntity = _appealRepository.AddAppealStatus(appealStatusEntity);
            var addAppealStatusResponse = _mapper.Map<AppealStatusResponse>(addAppealStatusEntity);

            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} end {nameof(AddAppealStatus)}");

            return addAppealStatusResponse;
        }

        public AppealTypeResponse AddAppealType(AppealTypeRequest appealType)
        {
            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} start {nameof(AddAppealType)}");

            var appealTypeEntity = _mapper.Map<AppealTypeEntity>(appealType);
            var addAppealTypeEntity = _appealRepository.AddAppealType(appealTypeEntity);
            var addAppealTypeResponse = _mapper.Map<AppealTypeResponse>(addAppealTypeEntity);

            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} end {nameof(AddAppealType)}");

            return addAppealTypeResponse;
        }

        public void UpdateAppealStatusById(int AppealId, int StatusId)
        {
            _appealRepository.UpdateAppealStatusById(AppealId, StatusId);
        }

        public AppealResponse DoResponseTextByAppeal(int id, string text, int statusId)
        {
            _logger.Log(LogLevel.Info, $"{nameof(AppealService)} start {nameof(DoResponseTextByAppeal)}");

            var appeal = _appealRepository.GetAppealById(id);
            int stsusId = appeal.StatusId;
            if (stsusId == 1)
            {
                appeal.DateOfResponse = DateTime.UtcNow;
                appeal.ResponseText = text;
                appeal.StatusId = statusId;

                var appealEntity = _mapper.Map<AppealEntity>(appeal);
                var addAppealEntity = _appealRepository.DoResponseTextByAppeal(appealEntity);
                var addAppealResponse = _mapper.Map<AppealResponse>(addAppealEntity);

                _logger.Log(LogLevel.Info, $"{nameof(AppealService)} end {nameof(DoResponseTextByAppeal)}");

                return addAppealResponse;
            }
            else
            {
                _logger.Log(LogLevel.Debug, $"{nameof(OrderService)} {nameof(DoResponseTextByAppeal)} Appeal with stsrus id {stsusId} was answered");

                throw new ArgumentException($"Appeal with stsrus id {stsusId} was answered");
            }
        }
    }
}
