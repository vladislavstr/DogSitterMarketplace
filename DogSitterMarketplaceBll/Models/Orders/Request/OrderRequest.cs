using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceBll.Models.Services;

namespace DogSitterMarketplaceBll.Models.Orders.Request
{
    public class OrderRequest
    {
        public string? Comment { get; set; }
        public int OrderStatusId { get; set; }
        public int SitterServiceId { get; set; }
        public int Summ { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int LocationId { get; set; }
    }
}
