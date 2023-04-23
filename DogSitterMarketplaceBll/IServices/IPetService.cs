using DogSitterMarketplaceBll.Models.Pets.Request;
using DogSitterMarketplaceBll.Models.Pets.Response;

namespace DogSitterMarketplaceBll.IServices
{
    public interface IPetService
    {
        public List<PetResponse> GetAllNotDeletedPets();

        public PetResponse GetNotDeletedPetById(int id);

        public void DeletePetById(int id);

        public PetResponse AddPet(PetRequest addPet);

        public PetResponse UpdatePet(PetUpdate petUpdate);

    }
}
