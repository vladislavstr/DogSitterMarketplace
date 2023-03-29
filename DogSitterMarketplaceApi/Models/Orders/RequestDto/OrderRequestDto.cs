using DogSitterMarketplaceApi.Models.Pets;
using DogSitterMarketplaceApi.Models.Services;

namespace DogSitterMarketplaceApi.Models.Orders.RequestDto
{
    public class OrderRequestDto
    {
        public string? Comment { get; set; }
        public int OrderStatusId { get; set; }
        public int SitterServiceId { get; set; }
        public int Summ { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int LocationId { get; set; }
        public List<PetRequestDto> Pets { get; set; }
    }
}
