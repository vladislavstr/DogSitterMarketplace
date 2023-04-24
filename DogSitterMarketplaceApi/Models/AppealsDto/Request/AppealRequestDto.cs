namespace DogSitterMarketplaceApi.Models.AppealsDto.Request
{
    public class AppealRequestDto
    {
        public string Text { get; set; }

        public DateTime DateOfCreate { get; set; }

        public string? ResponseText { get; set; }

        public DateTime? DateOfResponse { get; set; }

        public int TypeId { get; set; }

        public int StatusId { get; set; }

        public int? OrderId { get; set; }

        public int AppealFromUserId { get; set; }

        public int? AppealToUserId { get; set; }
    }
}
