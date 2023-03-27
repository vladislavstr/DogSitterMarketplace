using DogSitterMarketplaceApp.Models.Orders;
using DogSitterMarketplaceApp.Models.Users;

namespace DogSitterMarketplaceApp.Models.Appeals
{
    public class Appeal
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsDeleted { get; set; }
        public TypeOfAppeal Type { get; set; }
        public AppealStatus Status { get; set; }
        public Order Order { get; set; }
        public User AppealToUser { get; set; }
        public User AppealFromUser { get; set; }
    }
}
