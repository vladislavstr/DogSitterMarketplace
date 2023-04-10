using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Users;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IPetRepository
    {
        public List<PetEntity> GetAllPets();

        public PetEntity GetPetById(int id);

        public void DeletePetById(int id);

        public PetEntity AddPet(PetEntity addPet);

        public int UpdatePet(PetEntity updatePet);

        public List<PetEntity> GetPetsInOrderEntities(List<int> pets);

        public AnimalTypeEntity GetAnimalTypeById(int id);

        public UserEntity GetUserById(int id);
    }
}
