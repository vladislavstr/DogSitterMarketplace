using DogSitterMarketplaceBll.Models.Users;

namespace DogSitterMarketplaceBll.Models.Works
{
    public class SitterWorkResponse
    {
        public int Id { get; set; }

        public string? Comment { get; set; }

        public UserResponse User { get; set; }

        public WorkTypeResponse Type { get; set; }
    }
}
