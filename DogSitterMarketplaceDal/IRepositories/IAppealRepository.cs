using DogSitterMarketplaceDal.Models.Appeals;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IAppealRepository
    {
        public IEnumerable<AppealEntity> GetAllAppeals();

        public AppealEntity GetAppealById(int id);

        public AppealEntity GetAppealByUserId(int id);
        
        public AppealEntity GetAppealToUserId(int id);

        public AppealEntity AddAppeal(AppealEntity appeal);

        public AppealStatusEntity AddAppealStatus(AppealStatusEntity appealStatus);

        public AppealTypeEntity AddAppealType(AppealTypeEntity appealType);

        public void DeleteAppealById(int id);
    }
}
