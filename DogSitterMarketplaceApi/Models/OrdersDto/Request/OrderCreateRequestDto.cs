namespace DogSitterMarketplaceApi.Models.OrdersDto.Request
{
    public class OrderCreateRequestDto
    {
        public string? Comment { get; set; }

        public int SitterWorkId { get; set; }

      //  public decimal Summ { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public int LocationId { get; set; }

        public List<int> Pets { get; set; }
    }
}
