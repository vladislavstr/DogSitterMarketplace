using DogSitterMarketplaceBll.Models.Appeals.Request;
using DogSitterMarketplaceBll.Models.Appeals.Response;
using DogSitterMarketplaceDal.Models.Appeals;

namespace DogSitterMarketplaceBll.IServices
{
    public interface IAppealService
    {
        public IEnumerable<AppealResponse> GetAllAppeals();
        
        public IEnumerable<AppealResponse> GetAllNotDeletedAppeals();

        public AppealResponse GetAppealByUserId(int id);

        public AppealResponse GetAppealToUserId(int id);

        public AppealResponse AddAppeal(AppealRequest appeal);

        public AppealStatusResponse AddAppealStatus(AppealStatusRequest appealStatus);

        public AppealTypeResponse AddAppealType(AppealTypeRequest appealType);

        public void DeleteAppealById(int id);
    }
}
