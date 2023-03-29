using DogSitterMarketplaceBll.Models.Users.Response;

namespace DogSitterMarketplaceBll.Models.Work.Response
{
    public class SitterWorkRequest
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public UserResponse User { get; set; }

        public WorkTypeResponse WorkType { get; set; }

        public List<LocationWorkResponse> locationWork { get; set; }
    }
}