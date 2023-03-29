using DogSitterMarketplaceApp.Models.Orders;
using DogSitterMarketplaceApp.Models.Users;

namespace DogSitterMarketplaceApp.Models.Appeals
{
    public class AppealOutput
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public TypeOfAppeal Type { get; set; }
        public AppealStatus Status { get; set; }
        public Order? Order { get; set; }
        public User AppealFromUser { get; set; }
        public User? AppealToUser { get; set; }
    }

    public class AppealIntput
    {
        public string Text { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public int? Order { get; set; }
        public int AppealFromUser { get; set; }
        public int? AppealToUser { get; set; }
    }
}
