using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceBll.Models.Users.Response;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Users;
using System.Collections;

namespace DogSitterMarketplaceBll.Tests.TestCaseSource
{
    public class CommentServiceTestCaseSource
    {
        public static IEnumerable AddCommentTestCaseSource()
        {
            int userCommentFromId = 1;
            UserEntity userCommentFromEntity = new UserEntity
            {
                Id = 1,
                UserRole = new UserRoleEntity
                {
                    Id = 10,
                    Name = "Client"
                },
                Email = "1@ru",
                PhoneNumber = "111",
                Name = "111",
                UserRoleId = 10,
                IsDeleted = false
            };
            int userCommentToId = 2;
            UserEntity userCommentToEntity = new UserEntity
            {
                Id = 2,
                UserRole = new UserRoleEntity
                {
                    Id = 20,
                    Name = "Sitter"
                },
                Email = "2@ru",
                PhoneNumber = "222",
                Name = "222",
                UserRoleId = 20,
                IsDeleted = false
            };
            int userRoleCommentFromId = 10;
            UserRoleEntity userRoleCommentFromEntity = new UserRoleEntity
            {
                Id = 10,
                Name = "Client"
            };
            int userRoleCommentToId = 20;
            UserRoleEntity userRoleCommentToEntity = new UserRoleEntity
            {
                Id = 20,
                Name = "Sitter"
            };
            List<OrderEntity> allOrders = new List<OrderEntity>
            {
                new OrderEntity
                {
                    Id = 11,
                    IsDeleted= false
                }
            };
            CommentEntity commentEntity = new CommentEntity
            {
                OrderId = 11,
                CommentFromUserId = 1,
                CommentToUserId = 2,
                IsDeleted = false
            };
            CommentEntity addCommentEntity = new CommentEntity
            {
                Id = 100,
                OrderId = 11,
                CommentFromUserId = 1,
                CommentToUserId = 2,
                IsDeleted = false
            };
            CommentRequest addComment = new CommentRequest
            {
                OrderId = 11,
                CommentFromUserId = 1,
                CommentToUserId = 2
            };
            CommentOrderResponse expected = new CommentOrderResponse
            {
                Id = 100,
                Order = new OrderResponse { Id = 11 },
                CommentFromUser = new UserShortResponse { Id = 2 },
                CommentToUser = new UserShortResponse { Id = 2 },
            };
            int orderId = 11;
            OrderResponse orderResponse = new OrderResponse
            {
                Id = 11
            };

            yield return new object[] { userCommentFromId, userCommentFromEntity,  userCommentToId, userCommentToEntity,  userRoleCommentFromId,
                                         userRoleCommentFromEntity,  userRoleCommentToId, userRoleCommentToEntity, allOrders,
                                         commentEntity, addCommentEntity, addComment, expected, orderId, orderResponse};
        }
    }
}
// int userCommentFromId, UserEntity userCommentFromEntity, int userCommentToId, UserEntity userCommentToEntity, int userRoleCommentFromId,
//UserRoleEntity userRoleCommentFromEntity, int userRoleCommentToId, UserRoleEntity userRoleCommentToEntity, List<OrderEntity> allOrders,
//CommentEntity commentEntity, CommentEntity addCommentEntity, CommentRequest addComment, CommentOrderResponse expected