using DogSitterMarketplaceDal.Models.Pets;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IPetRepository
    {
        public List<PetEntity> GetAllPets();

        public PetEntity GetPetById(int id);

        public void DeletePetById(int id);

        public PetEntity AddPet(PetEntity addPet);

        public int UpdatePet(PetEntity updatePet);

    }
}
