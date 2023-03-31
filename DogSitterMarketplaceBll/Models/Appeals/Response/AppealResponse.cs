using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceBll.Models.Users;
using DogSitterMarketplaceBll.Models.Users.Response;

namespace DogSitterMarketplaceBll.Models.Appeals.Response
{
    public class AppealResponse
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public AppealTypeResponse Type { get; set; }

        public AppealStatusResponse Status { get; set; }

        public OrderResponse? Order { get; set; }

        public UserResponse AppealFromUser { get; set; }

        public UserResponse? AppealToUser { get; set; }
    }
}
