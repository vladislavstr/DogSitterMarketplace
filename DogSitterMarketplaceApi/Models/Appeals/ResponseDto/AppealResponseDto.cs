using DogSitterMarketplaceApi.Models.Orders.ResponseDto;
using DogSitterMarketplaceApi.Models.Users.Response;

namespace DogSitterMarketplaceApi.Models.Appeals.ResponseDto
{
    public class AppealResponseDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public AppealTypeResponseDto Type { get; set; }

        public AppealStatusResponseDto Status { get; set; }

        public OrderResponseDto? Order { get; set; }

        public UserResponseDto AppealFromUser { get; set; }

        public UserResponseDto? AppealToUser { get; set; }
    }
}
