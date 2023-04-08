using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Pets.Request;
using DogSitterMarketplaceBll.Models.Pets.Response;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Pets;
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

        public PetResponse GetPetById(int id)
        {
            var petEntity = _petRepository.GetPetById(id);
            var petResponse = _mapper.Map<PetResponse>(petEntity);

            return petResponse;
        }

        public void DeletePetById(int id) 
        {
            _petRepository.DeletePetById(id);
        }

        public PetResponse AddPet(PetRequest addPet)
        {
            var petEntity = _mapper.Map<PetEntity>(addPet);
            var addPetEntity = _petRepository.AddPet(petEntity);
            var addPetResponse = _mapper.Map<PetResponse>(addPetEntity);

            return addPetResponse;
        }

        public int UpdatePet(PetUpdate petUpdate)
        {
            var petEntity = _mapper.Map<PetEntity>(petUpdate);
            var id = _petRepository.UpdatePet(petEntity);

            return id;
        }
    }
}
