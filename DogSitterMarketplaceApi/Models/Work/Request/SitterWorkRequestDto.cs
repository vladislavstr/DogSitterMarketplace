using DogSitterMarketplaceApi.Models.Services;

namespace DogSitterMarketplaceApi.Models.Work
{
    public class SitterWorkRequestDto
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public int UserId { get; set; }

        public int WorkTypeId { get; set; }
    }
}