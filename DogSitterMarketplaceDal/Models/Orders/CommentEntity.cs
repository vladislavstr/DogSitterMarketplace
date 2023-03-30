namespace DogSitterMarketplaceDal.Models.Orders
{
    public class CommentEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public OrderEntity Order { get; set; }
        public UserEntity CommentFromUser { get; set; }
        public UserEntity CommentToUser { get; set; }
        public bool IsDeleted { get; set; }
    }
}
