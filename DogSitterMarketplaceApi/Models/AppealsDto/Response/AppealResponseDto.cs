using DogSitterMarketplaceApi.Models.OrdersDto.Response;
using DogSitterMarketplaceApi.Models.UsersDto.Response;

namespace DogSitterMarketplaceApi.Models.AppealsDto.Response
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
