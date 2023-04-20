using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Users;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace DogSitterMarketplaceDal.Repositories
{
    public class PetRepository : IPetRepository
    {
        private static OrdersAndPetsAndCommentsContext _context;

        //private readonly ILogger<IPetRepository> _logger;

        private readonly ILogger _logger;

        public PetRepository(OrdersAndPetsAndCommentsContext context, ILogger nLogger)
        {
            _context = context;
            _logger = nLogger;
        }

        public List<PetEntity> GetAllPets()
        {
            //return _context.Pets
            //    .Include(p => p.Type)
            //    .Include(p => p.User)
            //    .Where(p => !p.IsDeleted
            //    && !p.Type.IsDeleted
            //    && !p.User.IsDeleted)
            //    .ToList();

            return _context.Pets
                .Include(p => p.Type)
                .Include(p => p.User)
                .AsNoTracking()
                .ToList();
        }

        public PetEntity GetPetById(int id)
        {
            try
            {
                //return _context.Pets
                //        .Include(p => p.Type)
                //        .Include(p => p.User)
                //        .Single(p => !p.IsDeleted && p.Id == id
                //         && !p.Type.IsDeleted
                //         && !p.User.IsDeleted);

                return _context.Pets
                        .Include(p => p.Type)
                        .Include(p => p.User)
                        .Single(p => p.Id == id);
            }
            catch (InvalidOperationException ex)
            {
                // _logger.LogDebug($"{nameof(PetEntity)} with id {id} not found");
                _logger.Log(LogLevel.Debug, $"{nameof(PetEntity)} with id {id} not found");
                throw new NotFoundException(id, nameof(PetEntity));
            }
        }

        public void DeletePetById(int id)
        {
            try
            {
                var petDB = _context.Pets.Single(p => !p.IsDeleted && p.Id == id);
                petDB.IsDeleted = true;
                _context.SaveChanges();
            }
            catch (InvalidOperationException ex)
            {
                //  _logger.LogDebug($"{nameof(PetEntity)} with id {id} not found");
                _logger.Log(LogLevel.Debug, $"{nameof(PetEntity)} with id {id} not found");
                throw new NotFoundException(id, nameof(PetEntity));
            }
        }

        public PetEntity AddPet(PetEntity addPet)
        {
            try
            {
                _context.Pets.Add(addPet);
                _context.SaveChanges();

                return _context.Pets
                    .Include(p => p.Type)
                    .Include(p => p.User)
                    .Single(p => !p.IsDeleted && p.Id == addPet.Id);
            }
            catch (Exception ex)
            {
                // _logger.LogDebug($"{ex}, {nameof(PetRepository)} {nameof(PetEntity)} {nameof(AddPet)}");
                _logger.Log(LogLevel.Debug, $"{ex}, {nameof(PetRepository)} {nameof(PetEntity)} {nameof(AddPet)}");
                throw new ArgumentException();
            }
        }

        public PetEntity UpdatePet(PetEntity updatePet)
        {
            try
            {
                var petDB = _context.Pets.SingleOrDefault(p => !p.IsDeleted && p.Id == updatePet.Id);

                if (petDB == null)
                {
                    // _logger.LogDebug($"{nameof(PetEntity)} with id {updatePet.Id} not found");
                    _logger.Log(LogLevel.Debug, $"{nameof(PetEntity)} with id {updatePet.Id} not found");
                    throw new NotFoundException(updatePet.Id, nameof(PetEntity));
                }

                petDB.Name = updatePet.Name;
                petDB.Characteristics = updatePet.Characteristics;
                petDB.Type = updatePet.Type;
                petDB.TypeId = updatePet.TypeId;
                petDB.UserId = updatePet.UserId;

                _context.SaveChanges();

                return petDB;
            }
            catch (Exception ex)
            {
                // _logger.LogDebug($"{ex}, {nameof(PetRepository)} {nameof(PetEntity)} {nameof(AddPet)}");
                _logger.Log(LogLevel.Debug, $"{ex}, {nameof(PetRepository)} {nameof(PetEntity)} {nameof(AddPet)}");
                throw new ArgumentException();
            }
        }

        public List<PetEntity> GetPetsInOrderEntities(List<int> pets)
        {
            if (!pets.Any())
            {
                return new List<PetEntity>();
            }

            //return _context.Pets
            //                .Include(p => p.Type)
            //                .Include(p => p.User)
            //                .Where(p => !p.IsDeleted && pets.Contains(p.Id)
            //                       && !p.Type.IsDeleted
            //                       && !p.User.IsDeleted)
            //                .ToList();

            return _context.Pets
                            .Include(p => p.Type)
                            .Include(p => p.User)
                            .Where(p => pets.Contains(p.Id)).ToList();
        }

        public AnimalTypeEntity GetAnimalTypeById(int id)
        {
            try
            {
                return _context.AnimalsTypes.Single(at => at.Id == id && !at.IsDeleted);
            }
            catch (InvalidOperationException)
            {
                // _logger.LogDebug($"{nameof(PetRepository)} {nameof(GetAnimalTypeById)} {nameof(AnimalTypeEntity)} with id {id} not found");
                _logger.Log(LogLevel.Debug, $"{nameof(PetRepository)} {nameof(GetAnimalTypeById)} {nameof(AnimalTypeEntity)} with id {id} not found");
                throw new NotFoundException(id, nameof(AnimalTypeEntity));
            }
        }

        // перенести в Юзера
        public UserEntity GetUserById(int id)
        {
            try
            {
                return _context.Users
                    .Include(u => u.Role)
                    .Single(u => u.Id == id && !u.IsDeleted);
            }
            catch (InvalidOperationException)
            {
                //  _logger.LogDebug($"{nameof(PetRepository)} {nameof(GetExistAndNotDeletedUserById)} {nameof(UserEntity)} with id {id} not found.");
                _logger.Log(LogLevel.Debug, $"{nameof(PetRepository)} {nameof(GetUserById)} {nameof(UserEntity)} with id {id} not found.");
                throw new NotFoundException(id, nameof(UserEntity));
            }
        }
    }
}
