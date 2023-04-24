using DogSitterMarketplaceBll.Models.Appeals.Request;
using DogSitterMarketplaceBll.Models.Appeals.Response;
using DogSitterMarketplaceDal.Models.Appeals;

namespace DogSitterMarketplaceBll.IServices
{
    public interface IAppealService
    {
        public IEnumerable<AppealResponse> GetAllAppeals();
        
        public IEnumerable<AppealResponse> GetAllNotDeletedAppeals();

        public AppealResponse GetAppealById(int id);

        public AppealResponse GetAppealByUserIdToWhom(int id);

        public AppealResponse GetAppealByUserIdFromWhom(int id);

        public AppealResponse AddAppeal(AppealRequest appeal);

        public AppealStatusResponse AddAppealStatus(AppealStatusRequest appealStatus);

        public AppealTypeResponse AddAppealType(AppealTypeRequest appealType);

        public void DeleteAppealById(int id);

        public void UpdateAppealStatusById(int AppealId, int StatusId);
    }
}
