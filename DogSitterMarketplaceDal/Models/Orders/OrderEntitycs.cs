using DogSitterMarketplaceDal.Models.Services;

namespace DogSitterMarketplaceDal.Models.Orders
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public OrderStatusEntity OrderStatus { get; set; }
        public SitterServiceEntity SitterService { get; set; }
        public int Summ { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public LocationEntity Location { get; set; }
        public bool IsDeleted { get; set; }
    }
}
