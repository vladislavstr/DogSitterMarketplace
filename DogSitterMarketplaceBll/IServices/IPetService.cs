using DogSitterMarketplaceBll.Models.Pets.Request;
using DogSitterMarketplaceBll.Models.Pets.Response;

namespace DogSitterMarketplaceBll.IServices
{
    public interface IPetService
    {
        public Task<List<PetResponse>> GetAllNotDeletedPets();

        public Task<PetResponse> GetNotDeletedPetById(int id);

        public Task DeletePetById(int id);

        public Task<PetResponse> AddPet(PetRequest addPet);

        public Task<PetResponse> UpdatePet(PetUpdate petUpdate);

    }
}
