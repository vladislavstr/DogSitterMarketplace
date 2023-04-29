
using DogSitterMarketplaceBll.Services;
using DogSitterMarketplaceCore;
using DogSitterMarketplaceDal.Models.Orders;

namespace DogSitterMarketplaceBll.Models.Orders.Response
{
    public class AvgScoreCommentsResponse<T> where T : CommentResponse
    {
        public decimal AverageScore { get; set; }

        public List<T> Comments { get; set; } = new(); //CommentWithUserShortResponse
    }    
}





