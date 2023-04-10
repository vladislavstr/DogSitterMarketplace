using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;

namespace DogSitterMarketplaceBll.IServices
{
    public interface ICommentService
    {
        public List<CommentOrderResponse> GetAllComments();

        public CommentOrderResponse GetCommentById(int id);

        public void DeleteCommentById(int id);

        public CommentOrderResponse AddComment(CommentRequest commentRequest);
    }
}
