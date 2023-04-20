using DogSitterMarketplaceApi.Models.UsersDto.Response;
using DogSitterMarketplaceBll.Models.Users.Response;

namespace DogSitterMarketplaceApi.Models.OrdersDto.Response
{
    public class CommentAboutClientsForSitterResponseDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int Score { get; set; }

        //public double AverageScore { get; set; }

        public UserForCommentResponseDto CommentFromUser { get; set; }

        public UserForCommentResponseDto CommentToUser { get; set; }
    }
}
