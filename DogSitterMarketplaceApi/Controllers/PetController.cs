﻿using AutoMapper;
using DogSitterMarketplaceApi.Models.PetsDto.Request;
using DogSitterMarketplaceApi.Models.PetsDto.Response;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Pets.Request;
using DogSitterMarketplaceCore.Exceptions;
using Microsoft.AspNetCore.Mvc;
using ILogger = NLog.ILogger;

namespace DogSitterMarketplaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IPetService _petService;

        private readonly ILogger _logger;

        public PetController(IPetService petService, IMapper mapper, ILogger nLogger)
        {
            _petService = petService;
            _mapper = mapper;
            _logger = nLogger;
        }

        [HttpGet(Name = "GetAllNotDeletedPets")]
        public async Task<ActionResult<List<PetResponseDto>>> GetAllPets()
        {
            try
            {
                var petsResponse = await _petService.GetAllNotDeletedPets();
                var petsResponseDto = _mapper.Map<List<PetResponseDto>>(petsResponse);

                return Ok(petsResponseDto);
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(PetController)} {nameof(GetAllPets)}");
                return BadRequest();
            }
        }

        [HttpGet("{id}", Name = "GetNotDeletedPetById")]
        public async Task<ActionResult<PetResponseDto>> GetPetById(int id)
        {
            try
            {
                var petResponse = await _petService.GetNotDeletedPetById(id);
                var petResponseDto = _mapper.Map<PetResponseDto>(petResponse);

                return Ok(petResponseDto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(PetController)} {nameof(GetPetById)}");
                return BadRequest();
            }
        }

        [HttpDelete("{id}", Name = "DeletePetById")]
        public async Task<IActionResult> DeletePetById(int id)
        {
            try
            {
                await _petService.DeletePetById(id);

                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(PetController)} {nameof(DeletePetById)}");
                return BadRequest();
            }
        }

        [HttpPost(Name = "AddPet")]
        public async Task<ActionResult<PetResponseDto>> AddPet(PetRequestDto addPet)
        {
            try
            {
                var petRequst = _mapper.Map<PetRequest>(addPet);
                var addPetResponse = await _petService.AddPet(petRequst);
                var addPetResponseDto = _mapper.Map<PetResponseDto>(addPetResponse);

                return Created(new Uri("api/Pet", UriKind.Relative), addPetResponseDto);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(PetController)} {nameof(AddPet)}");
                return BadRequest();
            }
        }

        [HttpPut("{id}", Name = "UpdatePet")]
        public async Task<ActionResult<PetResponseDto>> UpdatePet(PetUpdateDto petUpdateDto)
        {
            try
            {
                var petUpdate = _mapper.Map<PetUpdate>(petUpdateDto);
                var petResponse = await _petService.UpdatePet(petUpdate);
                var petResponseDto = _mapper.Map<PetResponseDto>(petResponse);

                return Ok(petResponseDto);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log(NLog.LogLevel.Error, $" {ex} {nameof(PetController)} {nameof(UpdatePet)}");
                return BadRequest();
            }
        }
    }
}

