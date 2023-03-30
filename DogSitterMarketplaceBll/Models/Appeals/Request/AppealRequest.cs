using DogSitterMarketplaceBll.Models.Appeals.Response;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceBll.Models.Users;

namespace DogSitterMarketplaceBll.Models.Appeals.Request
{
    public class AppealRequest
    {
        public string Text { get; set; }

        public int TypeId { get; set; }

        public int StatusId { get; set; }

        public int? OrderId { get; set; }

        public int AppealFromUserId { get; set; }

        public int? AppealToUserId { get; set; }
    }
}
