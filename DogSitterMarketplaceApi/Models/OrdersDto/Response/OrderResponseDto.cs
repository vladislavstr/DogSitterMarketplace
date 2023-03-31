using DogSitterMarketplaceApi.Models.PetsDto.Response;
using DogSitterMarketplaceApi.Models.WorksDto;
using DogSitterMarketplaceApi.Models.WorksDto.Response;

namespace DogSitterMarketplaceApi.Models.OrdersDto.Response
{
    public class OrderResponseDto
    {
        public int Id { get; set; }

        public string? Comment { get; set; }

        public OrderStatusResponseDto OrderStatus { get; set; }

        public SitterWorkResponseDto SitterWork { get; set; }

        public int Summ { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public LocationResponseDto Location { get; set; }

        public List<PetResponseDto> Pets { get; set; }
    }
}