using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Pets.Response;
using DogSitterMarketplaceDal.IRepositories;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace DogSitterMarketplaceBll.Services
{
    public class PetService : IPetService
    {
        private readonly IMapper _mapper;

        private readonly IPetRepository _petRepository;

        public PetService(IPetRepository petRepository, IMapper mapper)
        {
            _petRepository = petRepository;
            _mapper = mapper;
        }

        public List<PetResponse> GetAllPets()
        {
            var petsEntitys = _petRepository.GetAllPets();
            var petsResponse = _mapper.Map<List<PetResponse>>(petsEntitys);

            return petsResponse;
        }
    }
}
