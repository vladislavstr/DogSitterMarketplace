using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Pets.Request;
using DogSitterMarketplaceBll.Models.Pets.Response;
using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
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

        public List<PetResponse> GetAllNotDeletedPets()
        {
            var allpetsEntitys = _petRepository.GetAllPets();
            var petsEntitys = allpetsEntitys
                .Where(p => !p.IsDeleted && !p.User.IsDeleted);
            var petsResponse = _mapper.Map<List<PetResponse>>(petsEntitys);

            return petsResponse;
        }

        public PetResponse GetNotDeletedPetById(int id)
        {
            var petEntity = _petRepository.GetPetById(id);

            if (!petEntity.IsDeleted)
            {
                var petResponse = _mapper.Map<PetResponse>(petEntity);

                return petResponse;
            }
            else
            {
                // что возвращать?  + logger??
                throw new NotFoundException(id, nameof(PetEntity));
            }
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

        public PetResponse UpdatePet(PetUpdate petUpdate)
        {
            var petEntity = _mapper.Map<PetEntity>(petUpdate);
            petUpdate.UserId = _petRepository.GetUserById(petUpdate.UserId).Id;
            var updatePetEntity = _petRepository.UpdatePet(petEntity);
            var updatePetResponse = _mapper.Map<PetResponse>(updatePetEntity);

            return updatePetResponse;
        }
    }
}
