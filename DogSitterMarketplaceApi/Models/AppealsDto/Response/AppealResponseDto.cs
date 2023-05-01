using DogSitterMarketplaceApi.Models.UsersDto.Response;

namespace DogSitterMarketplaceApi.Models.AppealsDto.Response
{
    public class AppealResponseDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime DateOfCreate { get; set; }

        public string? ResponseText { get; set; }

        public DateTime? DateOfResponse { get; set; }

        public AppealTypeResponseDto Type { get; set; }

        public AppealStatusResponseDto Status { get; set; }

        public OrderResponseDto? Order { get; set; }

        public UserShortResponseDto AppealFromUser { get; set; }

        public UserShortResponseDto? AppealToUser { get; set; }
    }
}
