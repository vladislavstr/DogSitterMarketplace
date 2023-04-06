using DogSitterMarketplaceApi.Models.AppealsDto.Request;
using DogSitterMarketplaceApi.Models.PetsDto.Request;
using DogSitterMarketplaceDal.Models.Appeals;
using DogSitterMarketplaceDal.Models.Orders;

namespace DogSitterMarketplaceApi.Models.OrdersDto.Request
{
    public class OrderRequestDto
    {
        public string? Comment { get; set; }

        public int OrderStatusId { get; set; }

        public int SitterWorkId { get; set; }

        public int Summ { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public int LocationId { get; set; }

        public List<int> Pets { get; set; }

        public List<int>? Comments { get; set; }

        public List<int>? Appeals { get; set; }
    }
}
