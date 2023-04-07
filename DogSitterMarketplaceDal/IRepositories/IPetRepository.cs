using DogSitterMarketplaceDal.Models.Pets;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IPetRepository
    {
        public List<PetEntity> GetAllPets();

        public PetEntity GetPetById(int id);
    }
}
