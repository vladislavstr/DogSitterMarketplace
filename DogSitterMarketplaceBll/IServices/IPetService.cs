using DogSitterMarketplaceBll.Models.Pets.Response;

namespace DogSitterMarketplaceBll.IServices
{
    public interface IPetService
    {
        public List<PetResponse> GetAllPets();
    }
}
