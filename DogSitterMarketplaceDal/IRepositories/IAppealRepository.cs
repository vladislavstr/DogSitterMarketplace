using DogSitterMarketplaceDal.Models.Appeals;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IAppealRepository
    {
        public ICollection<AppealEntity> GetAllAppeals();

        public AppealEntity AddAppeal(AppealEntity appeal);
    }
}
