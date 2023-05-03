using AutoMapper;

using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Appeals.Request;
using DogSitterMarketplaceBll.Models.Appeals.Response;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Appeals;


namespace DogSitterMarketplaceBll.Services
{
    public class AppealService : IAppealService
    {
        private readonly IMapper _mapper;
        private readonly IAppealRepository _appealRepository;

        public AppealService(IAppealRepository appealRepository, IMapper mapper)
        {
            _mapper = mapper;
            _appealRepository = appealRepository;
        }

        public IEnumerable<AppealResponse> GetAllAppeals()
        {
            var allappealsEntitys = _appealRepository.GetAllAppeals();
            var appealResponse = _mapper.Map<IEnumerable<AppealResponse>>(allappealsEntitys);

            return appealResponse;
        }

        public IEnumerable<AppealResponse> GetAllNotAnsweredAppeals()
        {
            var allappealsEntitys = _appealRepository.GetAllAppeals();
            var appealsEntitys = allappealsEntitys
                           .Where(u => u.StatusId == 1);
            var appealResponse = _mapper.Map<IEnumerable<AppealResponse>>(appealsEntitys);

            return appealResponse;
        }

        public AppealResponse GetAppealById(int id)
        {
            var appealEntitys = _appealRepository.GetAppealById(id);
            var appealResponse = _mapper.Map<AppealResponse>(appealEntitys);

            return appealResponse;
        }

        public AppealResponse GetAppealByUserIdToWhom(int id)
        {
            var appealEntitys = _appealRepository.GetAppealByUserIdToWhom(id);
            var appealResponse = _mapper.Map<AppealResponse>(appealEntitys);

            return appealResponse;
        }

        public AppealResponse GetAppealByUserIdFromWhom(int id)
        {
            var appealEntitys = _appealRepository.GetAppealByUserIdFromWhom(id);
            var appealResponse = _mapper.Map<AppealResponse>(appealEntitys);

            return appealResponse;
        }

        public AppealResponse AddAppeal(AppealRequest appeal)
        {
            appeal.ResponseText = null;
            appeal.DateOfCreate = DateTime.UtcNow;
            appeal.DateOfResponse = null;
            appeal.StatusId = 1;
            var appealEntity = _mapper.Map<AppealEntity>(appeal);
            var addAppealEntity = _appealRepository.AddAppeal(appealEntity);
            var addAppealResponse = _mapper.Map<AppealResponse>(addAppealEntity);

            return addAppealResponse;
        }

        public AppealStatusResponse AddAppealStatus(AppealStatusRequest appealStatus)
        {
            var appealStatusEntity = _mapper.Map<AppealStatusEntity>(appealStatus);
            var addAppealStatusEntity = _appealRepository.AddAppealStatus(appealStatusEntity);
            var addAppealStatusResponse = _mapper.Map<AppealStatusResponse>(addAppealStatusEntity);

            return addAppealStatusResponse;
        }

        public AppealTypeResponse AddAppealType(AppealTypeRequest appealType)
        {
            var appealTypeEntity = _mapper.Map<AppealTypeEntity>(appealType);
            var addAppealTypeEntity = _appealRepository.AddAppealType(appealTypeEntity);
            var addAppealTypeResponse = _mapper.Map<AppealTypeResponse>(addAppealTypeEntity);

            return addAppealTypeResponse;
        }

        public void UpdateAppealStatusById(int AppealId, int StatusId)
        {
            _appealRepository.UpdateAppealStatusById(AppealId, StatusId);
        }

        public AppealResponse DoResponseTextByAppeal(int id, string text, int statusId)
        {
            var appeal = _appealRepository.GetAppealById(id);
            int stsusId = appeal.StatusId;
            if (stsusId == 1)
            {

                appeal.DateOfResponse = DateTime.Now;
                appeal.ResponseText = text;
                appeal.StatusId = statusId;
                var appealEntity = _mapper.Map<AppealEntity>(appeal);
                var addAppealEntity = _appealRepository.DoResponseTextByAppeal(appealEntity);
                var addAppealResponse = _mapper.Map<AppealResponse>(addAppealEntity);

                return addAppealResponse;
            }
            else
            {
                throw new ArgumentException($"Appeal with stsrus id {stsusId} was answered");
            }
        }
    }
}
