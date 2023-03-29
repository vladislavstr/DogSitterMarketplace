using DogSitterMarketplaceApi.Models.Pets;
using DogSitterMarketplaceApi.Models.Services;
using DogSitterMarketplaceBll.Models.Services;

namespace DogSitterMarketplaceApi.Models.Orders.Response
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public OrderStatusResponce OrderStatus { get; set; }
        public SitterServiceResponseDto SitterService { get; set; }
        public int Summ { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public LocationResponseDto Location { get; set; }
        public List<PetResponseDto> Pets { get; set; }

    }
}
