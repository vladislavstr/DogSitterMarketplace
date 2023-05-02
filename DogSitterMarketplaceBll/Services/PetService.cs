using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Pets.Request;
using DogSitterMarketplaceBll.Models.Pets.Response;
using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Pets;
using NLog;

namespace DogSitterMarketplaceBll.Services
{
    public class PetService : IPetService
    {
        private readonly IMapper _mapper;

        private readonly IPetRepository _petRepository;

        private readonly IUserRepository _userRepository;

        private readonly ILogger _logger;

        public PetService(IPetRepository petRepository, IUserRepository userRepository, IMapper mapper, ILogger nLogger)
        {
            _petRepository = petRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = nLogger;
        }

        public async Task<List<PetResponse>> GetAllNotDeletedPets()
        {
            _logger.Log(LogLevel.Info, $"{nameof(PetService)} start {nameof(GetAllNotDeletedPets)}");
            var allpetsEntitys = await _petRepository.GetAllPets();
            var petsEntitys = allpetsEntitys.Where(p => !p.IsDeleted && !p.User.IsDeleted);
            var petsResponse = _mapper.Map<List<PetResponse>>(petsEntitys);
            _logger.Log(LogLevel.Info, $"{nameof(PetService)} end {nameof(GetAllNotDeletedPets)}");

            return petsResponse;
        }

        public async Task<PetResponse> GetNotDeletedPetById(int id)
        {
            _logger.Log(LogLevel.Info, $"{nameof(PetService)} start {nameof(GetNotDeletedPetById)}");
            var petEntity = await _petRepository.GetPetById(id);

            if (!petEntity.IsDeleted)
            {
                var petResponse = _mapper.Map<PetResponse>(petEntity);
                _logger.Log(LogLevel.Info, $"{nameof(PetService)} end {nameof(GetNotDeletedPetById)}");

                return petResponse;
            }
            else
            {
                _logger.Log(LogLevel.Debug, $"{nameof(PetService)} {nameof(GetNotDeletedPetById)} {nameof(PetEntity)} with id {id} is deleted.");
                throw new NotFoundException(id, nameof(PetEntity));
            }
        }

        public async Task DeletePetById(int id)
        {
            _logger.Log(LogLevel.Info, $"{nameof(PetService)} start {nameof(DeletePetById)}");
            await _petRepository.DeletePetById(id);
            _logger.Log(LogLevel.Info, $"{nameof(PetService)} end {nameof(DeletePetById)}");
        }

        public async Task<PetResponse> AddPet(PetRequest addPet)
        {
            _logger.Log(LogLevel.Info, $"{nameof(PetService)} start {nameof(AddPet)}");
            var petEntity = _mapper.Map<PetEntity>(addPet);
            var addPetEntity = await _petRepository.AddPet(petEntity);
            var addPetResponse = _mapper.Map<PetResponse>(addPetEntity);
            _logger.Log(LogLevel.Info, $"{nameof(PetService)} end {nameof(AddPet)}");

            return addPetResponse;
        }

        public async Task<PetResponse> UpdatePet(PetUpdate petUpdate)
        {
            _logger.Log(LogLevel.Info, $"{nameof(PetService)} start {nameof(UpdatePet)}");
            var petEntity = _mapper.Map<PetEntity>(petUpdate);
            petUpdate.UserId = (await _userRepository.GetUserWithRoleById(petUpdate.UserId)).Id;
            var updatePetEntity = await _petRepository.UpdatePet(petEntity);
            var updatePetResponse = _mapper.Map<PetResponse>(updatePetEntity);
            _logger.Log(LogLevel.Info, $"{nameof(PetService)} end {nameof(UpdatePet)}");

            return updatePetResponse;
        }
    }
}
