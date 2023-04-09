using AutoMapper;
using DogSitterMarketplaceApi.Models.OrdersDto.Response;
using DogSitterMarketplaceApi.Models.PetsDto.Request;
using DogSitterMarketplaceApi.Models.PetsDto.Response;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceBll.Models.Pets.Request;
using DogSitterMarketplaceBll.Services;
using DogSitterMarketplaceCore.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace DogSitterMarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly ILogger<PetController> _logger;

        private readonly IMapper _mapper;

        private readonly IPetService _petService;

        public PetController(IPetService petService, IMapper mapper, ILogger<PetController> logger)
        {
            _petService = petService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet(Name = "GetAllPets")]
        public ActionResult<List<PetResponseDto>> GetAllPets()
        {
            try
            {
                var petsResponse = _petService.GetAllPets();
                var petsResponseDto = _mapper.Map<List<PetResponseDto>>(petsResponse);

                return Ok(petsResponseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(PetController)} {nameof(GetAllPets)}");
                return Problem();
            }
        }

        [HttpGet("{id}", Name = "GetPetById")]
        public ActionResult<PetResponseDto> GetPetById(int id)
        {
            try
            {
                var petResponse = _petService.GetPetById(id);
                var petResponseDto = _mapper.Map<PetResponseDto>(petResponse);

                return Ok(petResponseDto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}", Name = "DeletePetById")]
        public IActionResult DeletePetById(int id)
        {
            try
            {
                _petService.DeletePetById(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost(Name = "AddPet")]
        public ActionResult<PetResponseDto> AddPet(PetRequestDto addPet)
        {
            try
            {
                var petRequst = _mapper.Map<PetRequest>(addPet);
                var addPetResponse = _petService.AddPet(petRequst);
                var addPetResponseDto = _mapper.Map<PetResponseDto>(addPetResponse);

                return Created(new Uri("api/Pet", UriKind.Relative), addPetResponseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(PetController)} {nameof(AddPet)}");
                return Problem();
            }
        }

        [HttpPut("{id}", Name = "UpdatePet")]
        public ActionResult UpdatePet(PetUpdateDto petUpdateDto)
        {
            try
            {
                var petUpdate = _mapper.Map<PetUpdate>(petUpdateDto);
                var id = _petService.UpdatePet(petUpdate);

                return Ok(id);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}

