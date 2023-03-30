namespace DogSitterMarketplaceApi.Models.Appeals.RequestDto
{
    public class AppealRequestDto
    {
        public string Text { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public int? OrderId { get; set; }
        public int AppealFromUserId { get; set; }
        public int? AppealToUserId { get; set; }
    }
}
