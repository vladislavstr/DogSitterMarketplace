using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceBll.Models.Pets.Response;
using DogSitterMarketplaceBll.Models.Users.Response;
using DogSitterMarketplaceCore;
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

        public static IEnumerable AddComment__WhenOrderBetweenSitterAndClientIsNotExist_CommentFromClientToUser_ShouldBeArgumentException_TestCaseSource()
        {
            //Случай, когда Коммент оставляет Клинет на Ситтера, но при этом между ними нет ордера, в рамках которого можно было бы оставить коммент

            int userCommentFromId = 19;
            UserEntity userCommentFromEntity = new UserEntity
            {
                Id = 19,
                UserRole = new UserRoleEntity
                {
                    Id = 109,
                    Name = "Client"
                },
                Email = "19@ru",
                PhoneNumber = "1119",
                Name = "1119",
                UserRoleId = 109,
                IsDeleted = false
            };
            int userCommentToId = 29;
            UserEntity userCommentToEntity = new UserEntity
            {
                Id = 29,
                UserRole = new UserRoleEntity
                {
                    Id = 209,
                    Name = "Sitter"
                },
                Email = "29@ru",
                PhoneNumber = "2229",
                Name = "2229",
                UserRoleId = 209,
                IsDeleted = false
            };
            int userRoleCommentFromId = 109;
            UserRoleEntity userRoleCommentFromEntity = new UserRoleEntity
            {
                Id = 109,
                Name = "Client"
            };
            int userRoleCommentToId = 209;
            UserRoleEntity userRoleCommentToEntity = new UserRoleEntity
            {
                Id = 209,
                Name = "Sitter"
            };

            CommentRequest addComment = new CommentRequest
            {
                OrderId = 119,
                CommentFromUserId = 19,
                CommentToUserId = 29
            };

            int orderId = 119;
            OrderResponse orderResponse = new OrderResponse
            {
                Id = 119
            };

            yield return new object[] { userCommentFromId, userCommentFromEntity,  userCommentToId, userCommentToEntity,  userRoleCommentFromId,
                                         userRoleCommentFromEntity,  userRoleCommentToId, userRoleCommentToEntity,addComment, orderId, orderResponse};
        }

        public static IEnumerable AddComment_WhenOrderIsNotExist_ShouldBeNotFoundException_TestCaseSource()
        {
            //Случай, когда Коммент оставляет Клинет на Ситтера, но при этом переданный в моделе ордер не существует

            int userCommentFromId = 198;
            UserEntity userCommentFromEntity = new UserEntity
            {
                Id = 198,
                UserRole = new UserRoleEntity
                {
                    Id = 1098,
                    Name = "Client"
                },
                Email = "198@ru",
                PhoneNumber = "11198",
                Name = "11198",
                UserRoleId = 1098,
                IsDeleted = false
            };
            int userCommentToId = 298;
            UserEntity userCommentToEntity = new UserEntity
            {
                Id = 298,
                UserRole = new UserRoleEntity
                {
                    Id = 2098,
                    Name = "Sitter"
                },
                Email = "298@ru",
                PhoneNumber = "22298",
                Name = "22298",
                UserRoleId = 2098,
                IsDeleted = false
            };
            int userRoleCommentFromId = 1098;
            int userRoleCommentToId = 2098;
            CommentRequest addComment = new CommentRequest
            {
                OrderId = 11900,
                CommentFromUserId = 198,
                CommentToUserId = 298
            };
            int orderId = 11900;

            yield return new object[] { userCommentFromId, userCommentFromEntity,  userCommentToId, userCommentToEntity,
                                        userRoleCommentFromId, userRoleCommentToId, addComment, orderId};
        }

        public static IEnumerable GetCommentsAndScoresForUserAboutHim_ForClientAboutHimTestCaseSource()
        {
            //1. Клиент получает комменты о нем - толкьо 1 коммент

            int userId = 1;
            UserEntity userEntity = new UserEntity
            {
                Id = 1,
                UserRole = new UserRoleEntity
                {
                    Id = 11,
                    Name = "Client"
                },
                IsDeleted = false
            };
            List<CommentEntity> commentsEntities = new List<CommentEntity>
            {
                new CommentEntity
                {
                 Id = 9,
                 Score = 3,
                 Text = "comment 3",
                 Order = new OrderEntity
                 {
                     DateStart = new DateTime(2023-04-28),
                     DateEnd = new DateTime(2023-04-29),
                 },
                 CommentFromUser = new UserEntity
                 {
                    Id = 7,
                    Email = "7@ru",
                    PhoneNumber = "77777",
                    Name = "7",
                    UserRoleId =66,
                    UserRole = new UserRoleEntity
                    {
                    Id = 66
                    }
                 }
                }
            };
            int userRoleId = 11;
            UserRoleEntity userRole = new UserRoleEntity
            {
                Id = 11,
                Name = "Client"
            };
            AvgScoreCommentsResponse<CommentWithUserShortResponse> expected = new AvgScoreCommentsResponse<CommentWithUserShortResponse>
            {
                AverageScore = 3,
                Comments = new List<CommentWithUserShortResponse>
                {
                    new CommentWithUserShortResponse
                    {
                        Id = 9,
                        Score = 3,
                        Text = "comment 3",
                        CommentFromUser = new UserShortResponse
                        {
                            Id = 7,
                            Email = "7@ru",
                            PhoneNumber = "77777",
                            Name = "7",
                            RoleId = 66
                        }
                    }
                }
            };

            yield return new object[] { userId, userEntity, commentsEntities, userRoleId, userRole, expected };

            //2. Клиент получает комменты о нем - 2 коммента

            userId = 12;
            userEntity = new UserEntity
            {
                Id = 12,
                UserRole = new UserRoleEntity
                {
                    Id = 112,
                    Name = "Client"
                },
                IsDeleted = false
            };
            commentsEntities = new List<CommentEntity>
            {
                new CommentEntity
                {
                 Id = 92,
                 Score = 5,
                 Text = "comment 32",
                 Order = new OrderEntity
                 {
                     DateStart = new DateTime(2023-04-20),
                     DateEnd = new DateTime(2023-04-21),
                 },
                 CommentFromUser = new UserEntity
                 {
                    Id = 72,
                    Email = "72@ru",
                    PhoneNumber = "777772",
                    Name = "72",
                    UserRoleId =662,
                    UserRole = new UserRoleEntity
                    {
                    Id = 662
                    }
                 }
                },
                new CommentEntity
                {
                 Id = 923,
                 Score = 2,
                 Text = "comment 323",
                 Order = new OrderEntity
                 {
                     DateStart = new DateTime(2023-03-28),
                     DateEnd = new DateTime(2023-03-29),
                 },
                 CommentFromUser = new UserEntity
                 {
                    Id = 723,
                    Email = "723@ru",
                    PhoneNumber = "7777723",
                    Name = "723",
                    UserRoleId =662,
                    UserRole = new UserRoleEntity
                    {
                    Id = 662
                    }
                 }
                }
            };
            userRoleId = 112;
            userRole = new UserRoleEntity
            {
                Id = 112,
                Name = "Client"
            };
            expected = new AvgScoreCommentsResponse<CommentWithUserShortResponse>
            {
                AverageScore = 3.5M,
                Comments = new List<CommentWithUserShortResponse>
                {
                     new CommentWithUserShortResponse
                    {
                        Id = 923,
                        Score = 2,
                        Text = "comment 323",
                        CommentFromUser = new UserShortResponse
                        {
                            Id = 723,
                            Email = "723@ru",
                            PhoneNumber = "7777723",
                            Name = "723",
                            RoleId = 662
                        }
                    },
                    new CommentWithUserShortResponse
                    {
                        Id = 92,
                        Score = 5,
                        Text = "comment 32",
                        CommentFromUser = new UserShortResponse
                        {
                            Id = 72,
                            Email = "72@ru",
                            PhoneNumber = "777772",
                            Name = "72",
                            RoleId = 662
                        }
                    }
                }
            };

            yield return new object[] { userId, userEntity, commentsEntities, userRoleId, userRole, expected };

            //3. Клиент получает комменты о нем - комментов нет - пустой лист

            userId = 124;
            userEntity = new UserEntity
            {
                Id = 124,
                UserRole = new UserRoleEntity
                {
                    Id = 1124,
                    Name = "Client"
                },
                IsDeleted = false
            };
            commentsEntities = new List<CommentEntity>();
            userRoleId = 1124;
            userRole = new UserRoleEntity
            {
                Id = 1124,
                Name = "Client"
            };
            expected = new AvgScoreCommentsResponse<CommentWithUserShortResponse>
            {
                AverageScore = 0,
                Comments = new List<CommentWithUserShortResponse>()
            };

            yield return new object[] { userId, userEntity, commentsEntities, userRoleId, userRole, expected };
        }

        public static IEnumerable GetCommentsAndScoresForUserAboutHim_ForSitterAboutHimTestCaseSource()
        {
            //1. Ситтер получает комменты о нем - толкьо 1 коммент

            int userId = 17;
            UserEntity userEntity = new UserEntity
            {
                Id = 17,
                UserRole = new UserRoleEntity
                {
                    Id = 117,
                    Name = "Sitter"
                },
                IsDeleted = false
            };
            List<CommentEntity> commentsEntities = new List<CommentEntity>
            {
                new CommentEntity
                {
                 Id = 97,
                 Score = 4,
                 Text = "comment 37",
                 Order = new OrderEntity
                 {
                     DateStart = new DateTime(2023-01-27),
                     DateEnd = new DateTime(2023-01-29),
                 },
                 CommentFromUser = new UserEntity
                 {
                    Id = 77,
                    Email = "77@ru",
                    PhoneNumber = "777777",
                    Name = "77",
                    UserRoleId =667,
                    UserRole = new UserRoleEntity
                    {
                    Id = 667
                    }
                 }
                }
            };
            int userRoleId = 117;
            UserRoleEntity userRole = new UserRoleEntity
            {
                Id = 117,
                Name = "Sitter"
            };
            AvgScoreCommentsResponse<CommentResponse> expected = new AvgScoreCommentsResponse<CommentResponse>
            {
                AverageScore = 4,
                Comments = new List<CommentResponse>
                {
                  new CommentResponse
                  {
                      Id = 97,
                      Score = 4,
                      Text = "comment 37"
                  }
                }
            };

            yield return new object[] { userId, userEntity, commentsEntities, userRoleId, userRole, expected };

            //2. Ситтер получает комменты о нем - 2 коммента

            userId = 176;
            userEntity = new UserEntity
            {
                Id = 176,
                UserRole = new UserRoleEntity
                {
                    Id = 1176,
                    Name = "Sitter"
                },
                IsDeleted = false
            };
            commentsEntities = new List<CommentEntity>
            {
                new CommentEntity
                {
                 Id = 976,
                 Score = 3,
                 Text = "comment 376",
                 Order = new OrderEntity
                 {
                     DateStart = new DateTime(2023-01-27),
                     DateEnd = new DateTime(2023-01-29),
                 },
                 CommentFromUser = new UserEntity
                 {
                    Id = 776,
                    Email = "776@ru",
                    PhoneNumber = "7777776",
                    Name = "776",
                    UserRoleId =6676,
                    UserRole = new UserRoleEntity
                    {
                    Id = 6676
                    }
                 }
                },
                new CommentEntity
                {
                 Id = 9765,
                 Score = 1,
                 Text = "comment 3765",
                 Order = new OrderEntity
                 {
                     DateStart = new DateTime(2023-01-21),
                     DateEnd = new DateTime(2023-01-22),
                 },
                 CommentFromUser = new UserEntity
                 {
                    Id = 7765,
                    Email = "7765@ru",
                    PhoneNumber = "77777765",
                    Name = "7765",
                    UserRoleId =66765,
                    UserRole = new UserRoleEntity
                    {
                    Id = 66765
                    }
                 }
                }
            };
            userRoleId = 1176;
            userRole = new UserRoleEntity
            {
                Id = 1176,
                Name = "Sitter"
            };
            expected = new AvgScoreCommentsResponse<CommentResponse>
            {
                AverageScore = 2,
                Comments = new List<CommentResponse>
                {
                   new CommentResponse
                   {
                     Id = 9765,
                     Score = 1,
                     Text = "comment 3765"
                   },
                  new CommentResponse
                  {
                      Id = 976,
                      Score = 3,
                      Text = "comment 376",
                  }
                }
            };

            yield return new object[] { userId, userEntity, commentsEntities, userRoleId, userRole, expected };

            //3. Ситтер получает комменты о нем - комментов нет -пустой лист

            userId = 1765;
            userEntity = new UserEntity
            {
                Id = 1765,
                UserRole = new UserRoleEntity
                {
                    Id = 11765,
                    Name = "Sitter"
                },
                IsDeleted = false
            };
            commentsEntities = new List<CommentEntity>();
            userRoleId = 11765;
            userRole = new UserRoleEntity
            {
                Id = 11765,
                Name = "Sitter"
            };
            expected = new AvgScoreCommentsResponse<CommentResponse>
            {
                AverageScore = 0,
                Comments = new List<CommentResponse>()
            };

            yield return new object[] { userId, userEntity, commentsEntities, userRoleId, userRole, expected };
        }

        public static IEnumerable GetCommentsAndScoresForUserAboutHim_ForClientAboutHim_WhenUserIsNotExist_ShouldBeNotFoundException_TestCaseSource()
        {
            int userId = 13;
            int userRoleId = 29;

            yield return new object[] { userId, userRoleId};
        }

        public static IEnumerable GetCommentsAndScoresForUserAboutHim_ForClientAboutHim_WhenUserRoleIsNotExist_ShouldBeNotFoundException_TestCaseSource()
        {
           int userId = 3;
           UserEntity userEntity = new UserEntity
            {
                Id = 3,
                UserRole = new UserRoleEntity
                {
                    Id = 61,
                    Name = "Client"
                },
                IsDeleted = false
            };
            List<CommentEntity> commentsEntities = new List<CommentEntity>
            {
                new CommentEntity
                {
                 Id = 924,
                 Score = 4,
                 Text = "comment 324",
                 Order = new OrderEntity
                 {
                     DateStart = new DateTime(2023-04-24),
                     DateEnd = new DateTime(2023-04-25),
                 },
                 CommentFromUser = new UserEntity
                 {
                    Id = 724,
                    Email = "724@ru",
                    PhoneNumber = "7777724",
                    Name = "724",
                    UserRoleId =6624,
                    UserRole = new UserRoleEntity
                    {
                    Id = 6624
                    }
                 }
                }
            };
            int userRoleId = 61;

            yield return new object[] { userId, userEntity, commentsEntities, userRoleId };
        }

        public static IEnumerable GetCommentsAndScoresForUserAboutHim_ForClientAboutHim_WhenUserRoleIsNotClient_ShouldBeNotFoundException_TestCaseSource()
        {
            int userId = 18;
            UserEntity userEntity = new UserEntity
            {
                Id = 18,
                UserRole = new UserRoleEntity
                {
                    Id = 118,
                    Name = "Admin"
                },
                IsDeleted = false
            };
            List<CommentEntity> commentsEntities = new List<CommentEntity>
            {
                new CommentEntity
                {
                 Id = 98,
                 Score = 4,
                 Text = "comment 38",
                 Order = new OrderEntity
                 {
                     DateStart = new DateTime(2023-04-27),
                     DateEnd = new DateTime(2023-04-28),
                 },
                 CommentFromUser = new UserEntity
                 {
                    Id = 78,
                    Email = "78@ru",
                    PhoneNumber = "777778",
                    Name = "78",
                    UserRoleId =668,
                    UserRole = new UserRoleEntity
                    {
                    Id = 668
                    }
                 }
                }
            };
            int userRoleId = 118;
            UserRoleEntity userRole = new UserRoleEntity
            {
                Id = 118,
                Name = "Admin"
            };

            yield return new object[] { userId, userEntity, commentsEntities, userRoleId, userRole };
        }

        public static IEnumerable GetCommentsAndScoresForUserAboutHim_ForSitterAboutHim_WhenUserIsNotExist_ShouldBeNotFoundException_TestCaseSource()
        {
            int userId = 131;
            int userRoleId = 291;

            yield return new object[] { userId, userRoleId };
        }

        public static IEnumerable GetCommentsAndScoresForUserAboutHim_ForSitterAboutHim_WhenUserRoleIsNotExist_ShouldBeNotFoundException_TestCaseSource()
        {
            int userId = 31;
            UserEntity userEntity = new UserEntity
            {
                Id = 31,
                UserRole = new UserRoleEntity
                {
                    Id = 611,
                    Name = "Sitter"
                },
                IsDeleted = false
            };
            List<CommentEntity> commentsEntities = new List<CommentEntity>
            {
                new CommentEntity
                {
                 Id = 9241,
                 Score = 41,
                 Text = "comment 3241",
                 Order = new OrderEntity
                 {
                     DateStart = new DateTime(2023-01-24),
                     DateEnd = new DateTime(2023-01-25),
                 },
                 CommentFromUser = new UserEntity
                 {
                    Id = 7241,
                    Email = "7241@ru",
                    PhoneNumber = "77777241",
                    Name = "7241",
                    UserRoleId =66241,
                    UserRole = new UserRoleEntity
                    {
                    Id = 66241
                    }
                 }
                }
            };
            int userRoleId = 611;

            yield return new object[] { userId, userEntity, commentsEntities, userRoleId };
        }

        public static IEnumerable GetCommentsAndScoresForUserAboutHim_ForSitterAboutHim_WhenUserRoleIsNotClient_ShouldBeNotFoundException_TestCaseSource()
        {
            int userId = 181;
            UserEntity userEntity = new UserEntity
            {
                Id = 181,
                UserRole = new UserRoleEntity
                {
                    Id = 1181,
                    Name = "Admin"
                },
                IsDeleted = false
            };
            List<CommentEntity> commentsEntities = new List<CommentEntity>
            {
                new CommentEntity
                {
                 Id = 981,
                 Score = 41,
                 Text = "comment 381",
                 Order = new OrderEntity
                 {
                     DateStart = new DateTime(2021-04-27),
                     DateEnd = new DateTime(2021-04-28),
                 },
                 CommentFromUser = new UserEntity
                 {
                    Id = 781,
                    Email = "781@ru",
                    PhoneNumber = "7777781",
                    Name = "781",
                    UserRoleId =6681,
                    UserRole = new UserRoleEntity
                    {
                    Id = 6681
                    }
                 }
                }
            };
            int userRoleId = 1181;
            UserRoleEntity userRole = new UserRoleEntity
            {
                Id = 1181,
                Name = "Admin"
            };

            yield return new object[] { userId, userEntity, commentsEntities, userRoleId, userRole };
        }
    }
}