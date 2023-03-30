using DogSitterMarketplaceBll.Models.Users;

namespace DogSitterMarketplaceBll.Models.Works
{
    public class SitterWorkRequest
    {
        public string? Comment { get; set; }

        public UserRequest User { get; set; }

        public WorkTypeRequest Type { get; set; }
    }
}
