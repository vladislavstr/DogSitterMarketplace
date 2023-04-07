using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.Models.Appeals;


namespace DogSitterMarketplaceDal.Repositories
{
    internal class AppealRepository
    {

        private static AppealContext context;

        public AppealRepository()
        {
            context = new AppealContext();
        }


        public IEnumerable<AppealEntity> GetUser()
        {
            return context.Appeals.Where(t => !t.IsDeleted).ToList();
        }
    }
}