namespace DogSitterMarketplaceDal.Models.Services
{
    public class SitterServiceEntity
    {
        public int Id { get; set; }

        public string? Comment { get; set; }

        public UserEntity User { get; set; }

        public ServiceTypeEntity Type { get; set; }

        public bool IsDeleted { get; set; }
    }
}
