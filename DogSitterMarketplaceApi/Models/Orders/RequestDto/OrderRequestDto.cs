using DogSitterMarketplaceApi.Models.Services;

namespace DogSitterMarketplaceApi.Models.Orders.RequestDto
{
    public class OrderRequestDto
    {
        public string? Comment { get; set; }
        public OrderStatusRequestDto OrderStatus { get; set; }
        public SitterServiceRequestDto SitterService { get; set; }
        public int Summ { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public LocationRequestDto Location { get; set; }
    }
}
