namespace DogSitterMarketplaceApi.Models.AppealsDto.Request
{
    public class AppealUpdateDto
    {
        public int Id { get; set; }

        public string ResponseText { get; set; }

        //public DateTime DateOfResponse { get; set; }

        public int StatusId { get; set; }
    }
}
