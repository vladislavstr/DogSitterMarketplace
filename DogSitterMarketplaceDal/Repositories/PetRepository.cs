using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Pets;
using Microsoft.EntityFrameworkCore;

namespace DogSitterMarketplaceDal.Repositories
{
    public class PetRepository : IPetRepository
    {
        private static OrdersAndPetsAndCommentsContext _context;

        public PetRepository()
        {
            _context = new OrdersAndPetsAndCommentsContext();
        }

        public List<PetEntity> GetAllPets()
        {
            return _context.Pets
                .Include(p => p.Type)
                .Include(p => p.User)
                .Where(p => !p.IsDeleted).ToList();
        }

        public PetEntity GetPetById(int id)
        {
            return _context.Pets
                    .Include(p => p.Type)
                    .Include(p => p.User)
                    .Single(p => !p.IsDeleted && p.Id == id);
        }

        public void DeletePetById(int id)
        {
            var petDB = _context.Pets.Single(p => !p.IsDeleted && p.Id == id);
            petDB.IsDeleted = true;

            _context.SaveChanges();
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
            var petDB = _context.Pets.Single(p => !p.IsDeleted && p.Id == updatePet.Id);
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
    }
}
