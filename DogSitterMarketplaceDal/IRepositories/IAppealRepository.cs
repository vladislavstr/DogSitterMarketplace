using DogSitterMarketplaceDal.Models.Appeals;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IAppealRepository
    {
        public IEnumerable<AppealEntity> GetAllAppeals();

        public AppealEntity GetAppealById(int id);

        public AppealEntity GetAppealByUserId(int id);

        public AppealEntity AddAppeal(AppealEntity appeal);

        public void DeleteAppealById(int id);
    }
}
