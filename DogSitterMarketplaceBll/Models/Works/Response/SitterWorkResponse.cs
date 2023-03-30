using DogSitterMarketplaceBll.Models.Users.Response;

namespace DogSitterMarketplaceBll.Models.Works.Response
{
    public class SitterWorkResponse
    {
        public int Id { get; set; }

        public string? Comment { get; set; }

        public UserResponse User { get; set; }

        public WorkTypeResponse WorkType { get; set; }

        public List<LocationWorkResponse> LocationsWorks { get; set; }
    }
}