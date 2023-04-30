using DogSitterMarketplaceDal.Models.Pets;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IPetRepository
    {
        public Task<List<PetEntity>> GetAllPets();

        public Task<PetEntity> GetPetById(int id);

        public Task DeletePetById(int id);

        public Task<PetEntity> AddPet(PetEntity addPet);

        public Task<PetEntity> UpdatePet(PetEntity updatePet);

        public Task<List<PetEntity>> GetPetsInOrderEntities(List<int> pets);

        public Task<AnimalTypeEntity> GetAnimalTypeById(int id);
    }
}
