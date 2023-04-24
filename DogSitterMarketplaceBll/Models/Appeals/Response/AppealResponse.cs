using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceBll.Models.Users.Response;

namespace DogSitterMarketplaceBll.Models.Appeals.Response
{
    public class AppealResponse
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime DateOfCreate { get; set; }

        public string? ResponseText { get; set; }

        public DateTime? DateOfResponse { get; set; }

        public AppealTypeResponse Type { get; set; }

        public AppealStatusResponse Status { get; set; }

        public OrderResponse? Order { get; set; }

        public UserShortResponse AppealFromUser { get; set; }

        public UserShortResponse? AppealToUser { get; set; }
    }
}
