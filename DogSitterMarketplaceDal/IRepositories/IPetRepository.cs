using DogSitterMarketplaceDal.Models.Pets;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IPetRepository
    {
        public List<PetEntity> GetAllPets();
    }
}
