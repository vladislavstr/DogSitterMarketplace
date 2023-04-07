using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Pets;
using Microsoft.EntityFrameworkCore;

namespace DogSitterMarketplaceDal.Repositories
{
    public class PetRepository : IPetRepository
    {
        private static OrdersAndPetsContext _context;

        public PetRepository()
        {
            _context = new OrdersAndPetsContext();
        }

        public List<PetEntity> GetAllPets()
        {
            return _context.Pets
                .Include(p => p.Type)
                .Include(p => p.User)
                .Where(p => !p.IsDeleted).ToList();
        }
    }
}
