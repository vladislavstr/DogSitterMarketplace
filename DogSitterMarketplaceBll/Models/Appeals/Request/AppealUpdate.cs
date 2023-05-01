namespace DogSitterMarketplaceBll.Models.Appeals.Request
{
    public class AppealUpdate
    {
        public int Id { get; set; }

        public string ResponseText { get; set; }

        public DateTime DateOfResponse { get; set; }

        public int StatusId { get; set; }

    }
}
