using DogSitterMarketplaceDal.Models.Appeals;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IAppealRepository
    {
        public IEnumerable<AppealEntity> GetAllAppeals();

        public AppealEntity GetAppealById(int id);

        public IEnumerable<AppealStatusEntity> GetAllAppealStatuses();

        public IEnumerable<AppealTypeEntity> GetAllAppealTypes();

        public AppealEntity GetAppealByUserIdToWhom(int id);
        
        public AppealEntity GetAppealByUserIdFromWhom(int id);

        public AppealEntity AddAppeal(AppealEntity appeal);

        public AppealStatusEntity AddAppealStatus(AppealStatusEntity appealStatus);

        public AppealTypeEntity AddAppealType(AppealTypeEntity appealType);

        public void UpdateAppealStatusById(int AppealId, int StatusId);

        public AppealEntity DoResponseTextByAppeal(AppealEntity appeal);
    }
}
