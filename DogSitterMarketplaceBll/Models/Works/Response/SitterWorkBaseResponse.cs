using DogSitterMarketplaceBll.Models.Users.Response;

namespace DogSitterMarketplaceBll.Models.Works.Response
{
    public class SitterWorkBaseResponse
    {
        public int Id { get; set; }

        public string? Comment { get; set; }

        public UserShortResponse User { get; set; }

        public WorkTypeResponse WorkType { get; set; }
    }
}