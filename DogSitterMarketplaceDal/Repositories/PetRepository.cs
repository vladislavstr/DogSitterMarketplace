using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Pets;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace DogSitterMarketplaceDal.Repositories
{
    public class PetRepository : IPetRepository
    {
        private static OrdersAndPetsAndCommentsContext _context;

        private readonly ILogger _logger;

        public PetRepository(OrdersAndPetsAndCommentsContext context, ILogger nLogger)
        {
            _context = context;
            _logger = nLogger;
        }

        public async Task<List<PetEntity>> GetAllPets()
        {
            return await _context.Pets
                        .Include(p => p.Type)
                        .Include(p => p.User)
                        .ThenInclude(u => u.UserRole)
                        .AsNoTracking()
                        .ToListAsync();
        }

        public async Task<PetEntity> GetPetById(int id)
        {
            try
            {
                return await _context.Pets
                            .Include(p => p.Type)
                            .Include(p => p.User)
                            .ThenInclude(u => u.UserRole)
                            .SingleAsync(p => p.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(PetEntity)} with id {id} not found");
                throw new NotFoundException(id, nameof(PetEntity));
            }
        }

        public async Task DeletePetById(int id)
        {
            try
            {
                var petDB = await _context.Pets.SingleAsync(p => !p.IsDeleted && p.Id == id);
                petDB.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException ex)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(PetEntity)} with id {id} not found");
                throw new NotFoundException(id, nameof(PetEntity));
            }
        }

        public async Task<PetEntity> AddPet(PetEntity addPet)
        {
            try
            {
                await _context.Pets.AddAsync(addPet);
                await _context.SaveChangesAsync();

                return await _context.Pets
                            .Include(p => p.Type)
                            .Include(p => p.User)
                            .SingleAsync(p => !p.IsDeleted && p.Id == addPet.Id);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Debug, $"{ex}, {nameof(PetRepository)} {nameof(PetEntity)} {nameof(AddPet)}");
                throw new ArgumentException();
            }
        }

        public async Task<PetEntity> UpdatePet(PetEntity updatePet)
        {
            try
            {
                var petDB = await _context.Pets.SingleOrDefaultAsync(p => !p.IsDeleted && p.Id == updatePet.Id);

                if (petDB == null)
                {
                    _logger.Log(LogLevel.Debug, $"{nameof(PetEntity)} with id {updatePet.Id} not found");
                    throw new NotFoundException(updatePet.Id, nameof(PetEntity));
                }

                petDB.Name = updatePet.Name;
                petDB.Characteristics = updatePet.Characteristics;
                //petDB.Type = updatePet.Type;
                petDB.TypeId = updatePet.TypeId;
                petDB.UserId = updatePet.UserId;

                await _context.SaveChangesAsync();

                return await _context.Pets
                                 .Include(p => p.Type)
                                 .Include(p => p.User)
                                 .ThenInclude(u => u.UserRole)
                                 .SingleAsync(p => p.Id == updatePet.Id);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Debug, $"{ex}, {nameof(PetRepository)} {nameof(PetEntity)} {nameof(AddPet)}");
                throw new ArgumentException();
            }
        }

        public async Task<List<PetEntity>> GetPetsInOrderEntities(List<int> pets)
        {
            if (!pets.Any())
            {
                return new List<PetEntity>();
            }

            return await _context.Pets
                            .Include(p => p.Type)
                            .Include(p => p.User)
                            .Where(p => pets.Contains(p.Id)).ToListAsync();
        }

        public async Task<AnimalTypeEntity> GetAnimalTypeById(int id)
        {
            try
            {
                return await _context.AnimalsTypes.SingleAsync(at => at.Id == id && !at.IsDeleted);
            }
            catch (InvalidOperationException)
            {
                _logger.Log(LogLevel.Debug, $"{nameof(PetRepository)} {nameof(GetAnimalTypeById)} {nameof(AnimalTypeEntity)} with id {id} not found");
                throw new NotFoundException(id, nameof(AnimalTypeEntity));
            }
        }
    }
}
