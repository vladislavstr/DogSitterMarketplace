using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceBll.Models.Pets.Response;
using DogSitterMarketplaceBll.Models.Users.Response;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Users;
using System.Collections;

namespace DogSitterMarketplaceBll.Tests.TestCaseSource
{
    public class CommentServiceTestCaseSource
    {
        public static IEnumerable AddComment_WhenCommentFromUserToSitterTestCaseSource()
        {
            //Случай, когда Коммент оставляет Клинет на Ситтера

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
                IsDeleted = false,
                CommentFromUser = userCommentFromEntity,
                CommentToUser = userCommentToEntity,
                Order = new OrderEntity
                {
                    Id = 11,
                    IsDeleted = false
                },
                Score = 5,
                Text = "5"
            };
            addCommentEntity.Order.Pets.AddRange(new List<PetEntity>());
            CommentRequest addComment = new CommentRequest
            {
                OrderId = 11,
                CommentFromUserId = 1,
                CommentToUserId = 2
            };
            CommentOrderResponse expected = new CommentOrderResponse
            {
                Id = 100,
                Order = new OrderResponse { Id = 11, Pets = new List<PetResponse>() },
                CommentFromUser = new UserShortResponse
                {
                    Id = 1,
                    Email = "1@ru",
                    PhoneNumber = "111",
                    Name = "111",
                    RoleId = 10
                },
                CommentToUser = new UserShortResponse
                {
                    Id = 2,
                    Email = "2@ru",
                    PhoneNumber = "222",
                    Name = "222",
                    RoleId = 20
                },
                Score = 5,
                Text = "5"
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

        public static IEnumerable AddComment_WhenCommentFromSitterToUserTestCaseSource()
        {
            //Случай, когда Коммент оставляет  Ситтер на Клиента

            int userCommentFromId = 14;
            UserEntity userCommentFromEntity = new UserEntity
            {
                Id = 14,
                UserRole = new UserRoleEntity
                {
                    Id = 104,
                    Name = "Sitter"
                },
                Email = "14@ru",
                PhoneNumber = "1114",
                Name = "1114",
                UserRoleId = 104,
                IsDeleted = false
            };
            int userCommentToId = 24;
            UserEntity userCommentToEntity = new UserEntity
            {
                Id = 24,
                UserRole = new UserRoleEntity
                {
                    Id = 204,
                    Name = "Client"
                },
                Email = "24@ru",
                PhoneNumber = "2224",
                Name = "2224",
                UserRoleId = 204,
                IsDeleted = false
            };
            int userRoleCommentFromId = 104;
            UserRoleEntity userRoleCommentFromEntity = new UserRoleEntity
            {
                Id = 104,
                Name = "Sitter"
            };
            int userRoleCommentToId = 204;
            UserRoleEntity userRoleCommentToEntity = new UserRoleEntity
            {
                Id = 204,
                Name = "Client"
            };
            List<OrderEntity> allOrders = new List<OrderEntity>
            {
                new OrderEntity
                {
                    Id = 114,
                    IsDeleted= false
                }
            };
            CommentEntity commentEntity = new CommentEntity
            {
                OrderId = 114,
                CommentFromUserId = 14,
                CommentToUserId = 24,
                IsDeleted = false
            };
            CommentEntity addCommentEntity = new CommentEntity
            {
                Id = 1004,
                OrderId = 114,
                CommentFromUserId = 14,
                CommentToUserId = 24,
                IsDeleted = false,
                CommentFromUser = userCommentFromEntity,
                CommentToUser = userCommentToEntity,
                Order = new OrderEntity
                {
                    Id = 114,
                    IsDeleted = false
                },
                Score = 5,
                Text = "54"
            };
            addCommentEntity.Order.Pets.AddRange(new List<PetEntity>());
            CommentRequest addComment = new CommentRequest
            {
                OrderId = 114,
                CommentFromUserId = 14,
                CommentToUserId = 24
            };
            CommentOrderResponse expected = new CommentOrderResponse
            {
                Id = 1004,
                Order = new OrderResponse { Id = 114, Pets = new List<PetResponse>() },
                CommentFromUser = new UserShortResponse
                {
                    Id = 14,
                    Email = "14@ru",
                    PhoneNumber = "1114",
                    Name = "1114",
                    RoleId = 104
                },
                CommentToUser = new UserShortResponse
                {
                    Id = 24,
                    Email = "24@ru",
                    PhoneNumber = "2224",
                    Name = "2224",
                    RoleId = 204
                },
                Score = 5,
                Text = "54"
            };
            int orderId = 114;
            OrderResponse orderResponse = new OrderResponse
            {
                Id = 114
            };

            yield return new object[] { userCommentFromId, userCommentFromEntity,  userCommentToId, userCommentToEntity,  userRoleCommentFromId,
                                         userRoleCommentFromEntity,  userRoleCommentToId, userRoleCommentToEntity, allOrders,
                                         commentEntity, addCommentEntity, addComment, expected, orderId, orderResponse};
        }
    }
}