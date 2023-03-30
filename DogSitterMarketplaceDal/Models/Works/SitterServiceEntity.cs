namespace DogSitterMarketplaceDal.Models.Works
{
    public class SitterWorkEntity
    {
        public int Id { get; set; }

        public string? Comment { get; set; }

        public UserEntity User { get; set; }

        public WorkTypeEntity Type { get; set; }

        public bool IsDeleted { get; set; }
    }
}
