using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Pets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DogSitterMarketplaceDal.Repositories
{
    public class PetRepository : IPetRepository
    {
        private static OrdersAndPetsAndCommentsContext _context;

        private readonly ILogger<IPetRepository> _logger;

        public PetRepository(ILogger<IPetRepository> logger)
        {
            _context = new OrdersAndPetsAndCommentsContext();
            _logger = logger;
        }

        public List<PetEntity> GetAllPets()
        {
            return _context.Pets
                .Include(p => p.Type)
                .Include(p => p.User)
                .Where(p => !p.IsDeleted
                && !p.Type.IsDeleted
                && !p.User.IsDeleted)
                .ToList();
        }

        public PetEntity GetPetById(int id)
        {
            try
            {
                return _context.Pets
                        .Include(p => p.Type)
                        .Include(p => p.User)
                        .Single(p => !p.IsDeleted && p.Id == id
                         && !p.Type.IsDeleted
                         && !p.User.IsDeleted);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogDebug($"{nameof(PetEntity)} with id {id} not found");
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
                _logger.LogDebug($"{nameof(PetEntity)} with id {id} not found");
                throw new NotFoundException(id, nameof(PetEntity));
            }
        }

        public PetEntity AddPet(PetEntity addPet)
        {
            _context.Pets.Add(addPet);
            _context.SaveChanges();

            return _context.Pets
                .Include(p => p.Type)
                .Include(p => p.User)
                .Single(p => p.Id == addPet.Id);
        }

        public int UpdatePet(PetEntity updatePet)
        {
            var petDB = _context.Pets.SingleOrDefault(p => !p.IsDeleted && p.Id == updatePet.Id);

            if (petDB == null)
            {
                _logger.LogDebug($"{nameof(PetEntity)} with id {updatePet.Id} not found");
                throw new NotFoundException(updatePet.Id, nameof(PetEntity));
            }

            petDB.Name = updatePet.Name;
            petDB.Characteristics = updatePet.Characteristics;
            petDB.Type = updatePet.Type;
            petDB.TypeId = updatePet.TypeId;
          //  petDB.User = updatePet.User;
            petDB.UserId = updatePet.UserId;
            // petDB.IsDeleted = updatePet.IsDeleted;
          //  petDB.Orders.Clear();
           // petDB.Orders.AddRange(updatePet.Orders);

            _context.SaveChanges();

            return petDB.Id;
        }

        public List<PetEntity> GetPetsInOrderEntities(List<int> pets)
        {
            if (!pets.Any())
            {
                return new List<PetEntity>();
            }

            return _context.Pets
                .Include(p => p.Type)
                .Include(p => p.User)
                .Where(p => !p.IsDeleted && pets.Contains(p.Id)
                && !p.Type.IsDeleted
                && !p.User.IsDeleted)
                .ToList();
        }
    }
}
