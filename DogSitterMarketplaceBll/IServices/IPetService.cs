using DogSitterMarketplaceBll.Models.Pets.Response;

namespace DogSitterMarketplaceBll.IServices
{
    public interface IPetService
    {
        public List<PetResponse> GetAllPets();

        public PetResponse GetPetById(int id);

        public void DeletePetById(int id);
    }
}
