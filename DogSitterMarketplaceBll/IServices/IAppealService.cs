using DogSitterMarketplaceBll.Models.Appeals.Request;
using DogSitterMarketplaceBll.Models.Appeals.Response;

namespace DogSitterMarketplaceBll.IServices
{
    public interface IAppealService
    {
        public IEnumerable<AppealResponse> GetAllAppeals();
        
        public IEnumerable<AppealResponse> GetAllNotDeletedAppeals();

        public AppealResponse GetAppealById(int id);

        public AppealResponse AddAppeal(AppealRequest appeal);

        public void DeleteAppealById(int id);
    }
}
