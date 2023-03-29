using DogSitterMarketplaceDal.Models.Users;

namespace DogSitterMarketplaceDal.Models.Work
{
    public class SitterWorkEntity
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public UserEntity User { get; set; }

        public WorkTypeEntity WorkType { get; set; }

        public bool IsDeleted { get; set; }
    }
}