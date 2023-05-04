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

            yield return new object[] { userId, userRoleId };
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

        public static IEnumerable GetCommentsAndScoresAboutOtherUsers_ForClientAboutSitterTestCaseSource()
        {
            //1. Клиент получает комменты о ситтере - 1 коммент

            int userIdFromComment = 1;
            UserEntity userEntityFromComment = new UserEntity
            {
                Id = 1,
                UserRoleId = 11,
                UserRole = new UserRoleEntity
                {
                    Id = 11,
                    Name = "Client"
                }
            };
            int userIdToComment = 2;
            UserEntity userEntityToComment = new UserEntity
            {
                Id = 2,
                UserRoleId = 22,
                UserRole = new UserRoleEntity
                {
                    Id = 22,
                    Name = "Sitter"
                }
            };
            List<CommentEntity> commentsEntities = new List<CommentEntity>
            {
             new CommentEntity
                 {
                 Id = 7,
                 Text = "7",
                 Score = 5,
                 Order = new OrderEntity
                     {
                     Id = 5,
                     DateStart = new DateTime (2023-05-01),
                     DateEnd = new DateTime (2023-05-02),
                     },
                 CommentFromUser = new UserEntity
                    {
                     Name = "Man"
                    }
                 },
            };
            int userRoleWhoGetCommentId = 11;
            UserRoleEntity userRoleWhoGetComment = new UserRoleEntity
            {
                Id = 11,
                Name = "Client"
            };
            int userRoleCommentToId = 22;
            UserRoleEntity userRoleCommentTo = new UserRoleEntity
            {
                Id = 22,
                Name = "Sitter"
            };
            AvgScoreCommentsResponse<CommentsAboutOtherUsersResponse> expected = new AvgScoreCommentsResponse<CommentsAboutOtherUsersResponse>
            {
                AverageScore = 5,
                Comments = new List<CommentsAboutOtherUsersResponse>
                {
                    new CommentsAboutOtherUsersResponse
                    {
                    Id = 7,
                    Text = "7",
                    Score = 5,
                    CommentFromUser = new UserForCommentResponse
                        {
                          Name = "Man"
                        }
                    }
                }
            };

            yield return new object[] {userIdToComment, userEntityToComment, userIdFromComment, userEntityFromComment, commentsEntities, userRoleWhoGetCommentId,
                                         userRoleWhoGetComment, userRoleCommentToId, userRoleCommentTo, expected };

            //2. Клиент получает комменты о ситтере - 2 коммента

            userIdFromComment = 12;
            userEntityFromComment = new UserEntity
            {
                Id = 12,
                UserRoleId = 112,
                UserRole = new UserRoleEntity
                {
                    Id = 112,
                    Name = "Client"
                }
            };
            userIdToComment = 22;
            userEntityToComment = new UserEntity
            {
                Id = 22,
                UserRoleId = 222,
                UserRole = new UserRoleEntity
                {
                    Id = 222,
                    Name = "Sitter"
                }
            };
            commentsEntities = new List<CommentEntity>
            {
             new CommentEntity
                 {
                 Id = 72,
                 Text = "72",
                 Score = 5,
                 Order = new OrderEntity
                     {
                     Id = 52,
                     DateStart = new DateTime (2023-02-01),
                     DateEnd = new DateTime (2023-02-02),
                     },
                 CommentFromUser = new UserEntity
                    {
                     Name = "Man2"
                    }
                 },
             new CommentEntity
                 {
                 Id = 721,
                 Text = "721",
                 Score = 4,
                 Order = new OrderEntity
                     {
                     Id = 521,
                     DateStart = new DateTime (2023-01-01),
                     DateEnd = new DateTime (2023-01-02),
                     },
                 CommentFromUser = new UserEntity
                    {
                     Name = "Man21"
                    }
                 },
            };
            userRoleWhoGetCommentId = 112;
            userRoleWhoGetComment = new UserRoleEntity
            {
                Id = 112,
                Name = "Client"
            };
            userRoleCommentToId = 222;
            userRoleCommentTo = new UserRoleEntity
            {
                Id = 222,
                Name = "Sitter"
            };
            expected = new AvgScoreCommentsResponse<CommentsAboutOtherUsersResponse>
            {
                AverageScore = 4.5M,
                Comments = new List<CommentsAboutOtherUsersResponse>
                {
                    new CommentsAboutOtherUsersResponse
                 {
                 Id = 721,
                 Text = "721",
                 Score = 4,
                 CommentFromUser = new UserForCommentResponse
                    {
                     Name = "Man21"
                    }
                 },
                    new CommentsAboutOtherUsersResponse
                    {
                    Id = 72,
                    Text = "72",
                    Score = 5,
                    CommentFromUser = new UserForCommentResponse
                        {
                          Name = "Man2"
                        }
                    }
                }
            };

            yield return new object[] {userIdToComment, userEntityToComment, userIdFromComment, userEntityFromComment, commentsEntities, userRoleWhoGetCommentId,
                                         userRoleWhoGetComment, userRoleCommentToId, userRoleCommentTo, expected };

            // 3. Клиент получает комменты о ситтере - 0 комментов - пустой лист

            userIdFromComment = 123;
            userEntityFromComment = new UserEntity
            {
                Id = 123,
                UserRoleId = 1123,
                UserRole = new UserRoleEntity
                {
                    Id = 1123,
                    Name = "Client"
                }
            };
            userIdToComment = 223;
            userEntityToComment = new UserEntity
            {
                Id = 223,
                UserRoleId = 2223,
                UserRole = new UserRoleEntity
                {
                    Id = 2223,
                    Name = "Sitter"
                }
            };
            commentsEntities = new List<CommentEntity>();
            userRoleWhoGetCommentId = 1123;
            userRoleWhoGetComment = new UserRoleEntity
            {
                Id = 1123,
                Name = "Client"
            };
            userRoleCommentToId = 2223;
            userRoleCommentTo = new UserRoleEntity
            {
                Id = 2223,
                Name = "Sitter"
            };
            expected = new AvgScoreCommentsResponse<CommentsAboutOtherUsersResponse>
            {
                AverageScore = 0,
                Comments = new List<CommentsAboutOtherUsersResponse>()
            };

            yield return new object[] {userIdToComment, userEntityToComment, userIdFromComment, userEntityFromComment, commentsEntities, userRoleWhoGetCommentId,
                                         userRoleWhoGetComment, userRoleCommentToId, userRoleCommentTo, expected };
        }

        public static IEnumerable GetCommentsAndScoresAboutOtherUsers_ForClientAboutSitter_WhenSitterIsNotExist_ShouldBeNotFoundException_TestCaseSource()
        {
            int userIdToComment = 9;
            int userIdFromComment = 8;
            int userRoleWhoGetCommentId = 7;
            int userRoleCommentToId = 6;

            yield return new object[] { userIdToComment, userIdFromComment, userRoleWhoGetCommentId, userRoleCommentToId };
        }

        public static IEnumerable GetCommentsAndScoresAboutOtherUsers_ForClientAboutSitter_WhenClientIsNotExist_ShouldBeNotFoundException_TestCaseSource()
        {
            int userIdToComment = 9;
            UserEntity userToCommentEntity = new UserEntity
            {
                Id = 9,
                IsDeleted = false
            };
            int userIdFromComment = 8;
            int userRoleWhoGetCommentId = 7;
            int userRoleCommentToId = 6;

            yield return new object[] { userIdToComment, userIdFromComment, userToCommentEntity, userRoleWhoGetCommentId, userRoleCommentToId };
        }

        public static IEnumerable GetCommentsAndScoresAboutOtherUsers_ForClientAboutSitter_WhenUserRoleWhoGetCommentIsNotExist_ShouldBeNotFoundException_TestCaseSource()
        {
            int userIdFromComment = 17;
            UserEntity userEntityFromComment = new UserEntity
            {
                Id = 17,
                UserRoleId = 117,
                UserRole = new UserRoleEntity
                {
                    Id = 117,
                    Name = "Client"
                }
            };
            int userIdToComment = 27;
            UserEntity userEntityToComment = new UserEntity
            {
                Id = 27,
                UserRoleId = 227,
                UserRole = new UserRoleEntity
                {
                    Id = 227,
                    Name = "Sitter"
                }
            };
            List<CommentEntity> commentsEntities = new List<CommentEntity>
            {
             new CommentEntity
                 {
                 Id = 77,
                 Text = "77",
                 Score = 2,
                 Order = new OrderEntity
                     {
                     Id = 57,
                     DateStart = new DateTime (2023-07-01),
                     DateEnd = new DateTime (2023-07-02),
                     },
                 CommentFromUser = new UserEntity
                    {
                     Name = "Man7"
                    }
                 },
            };
            int userRoleWhoGetCommentId = 117;
            int userRoleCommentToId = 227;

            yield return new object[] {userIdToComment, userEntityToComment, userIdFromComment, userEntityFromComment,
                                        commentsEntities, userRoleWhoGetCommentId, userRoleCommentToId};
        }

        public static IEnumerable GetCommentsAndScoresAboutOtherUsers_ForClientAboutSitter_WWhenUserRoleCommentToIsNotExist_ShouldBeNotFoundException_TestCaseSource()
        {
            int userIdFromComment = 176;
            UserEntity userEntityFromComment = new UserEntity
            {
                Id = 176,
                UserRoleId = 1176,
                UserRole = new UserRoleEntity
                {
                    Id = 1176,
                    Name = "Client"
                }
            };
            int userIdToComment = 276;
            UserEntity userEntityToComment = new UserEntity
            {
                Id = 276,
                UserRoleId = 2276,
                UserRole = new UserRoleEntity
                {
                    Id = 2276,
                    Name = "Sitter"
                }
            };
            List<CommentEntity> commentsEntities = new List<CommentEntity>
            {
             new CommentEntity
                 {
                 Id = 776,
                 Text = "776",
                 Score = 4,
                 Order = new OrderEntity
                     {
                     Id = 576,
                     DateStart = new DateTime (2023-06-01),
                     DateEnd = new DateTime (2023-06-02),
                     },
                 CommentFromUser = new UserEntity
                    {
                     Name = "Man76"
                    }
                 },
            };
            int userRoleWhoGetCommentId = 1176;
            int userRoleCommentToId = 2276;
            UserRoleEntity userRoleWhoGetComment = new UserRoleEntity
            {
                Id = 1176,
                Name = "Client"
            };

            yield return new object[] {userIdToComment, userRoleWhoGetComment, userEntityToComment, userIdFromComment, userEntityFromComment,
                                        commentsEntities, userRoleWhoGetCommentId, userRoleCommentToId};
        }

        public static IEnumerable GetCommentsAndScoresAboutOtherUsers_ForClientAboutSitter_WhenUserRoleWhoGetCommentIsNotClient_ShouldBeNotFoundException_TestCaseSource()
        {
            int userIdFromComment = 1765;
            UserEntity userEntityFromComment = new UserEntity
            {
                Id = 1765,
                UserRoleId = 11765,
                UserRole = new UserRoleEntity
                {
                    Id = 11765,
                    Name = "Admin"
                }
            };
            int userIdToComment = 2765;
            UserEntity userEntityToComment = new UserEntity
            {
                Id = 2765,
                UserRoleId = 165,
                UserRole = new UserRoleEntity
                {
                    Id = 165,
                    Name = "Sitter"
                }
            };
            List<CommentEntity> commentsEntities = new List<CommentEntity>
            {
             new CommentEntity
                 {
                 Id = 7765,
                 Text = "7765",
                 Score = 1,
                 Order = new OrderEntity
                     {
                     Id = 576,
                     DateStart = new DateTime (2023-05-01),
                     DateEnd = new DateTime (2023-05-02),
                     },
                 CommentFromUser = new UserEntity
                    {
                     Name = "Man765"
                    }
                 },
            };
            int userRoleWhoGetCommentId = 11765;
            int userRoleCommentToId = 165;
            UserRoleEntity userRoleWhoGetComment = new UserRoleEntity
            {
                Id = 11765,
                Name = "Admin"
            };
            UserRoleEntity userRoleCommentTo = new UserRoleEntity
            {
                Id = 165,
                Name = "Sitter"
            };

            yield return new object[] {userIdToComment, userRoleWhoGetComment, userEntityToComment, userIdFromComment, userEntityFromComment,
                                        commentsEntities, userRoleWhoGetCommentId, userRoleCommentToId, userRoleCommentTo};
        }

        public static IEnumerable GetCommentsAndScoresAboutOtherUsers_ForClientAboutSitter_WhenUserRoleCommentToIsNotSitter_ShouldBeNotFoundException_TestCaseSource()
        {
            int userIdFromComment = 17654;
            UserEntity userEntityFromComment = new UserEntity
            {
                Id = 17654,
                UserRoleId = 117654,
                UserRole = new UserRoleEntity
                {
                    Id = 117654,
                    Name = "Client"
                }
            };
            int userIdToComment = 27654;
            UserEntity userEntityToComment = new UserEntity
            {
                Id = 27654,
                UserRoleId = 1654,
                UserRole = new UserRoleEntity
                {
                    Id = 1654,
                    Name = "Client"
                }
            };
            List<CommentEntity> commentsEntities = new List<CommentEntity>
            {
             new CommentEntity
                 {
                 Id = 77654,
                 Text = "77654",
                 Score = 5,
                 Order = new OrderEntity
                     {
                     Id = 576,
                     DateStart = new DateTime (2023-05-04),
                     DateEnd = new DateTime (2023-05-05),
                     },
                 CommentFromUser = new UserEntity
                    {
                     Name = "Man7654"
                    }
                 },
            };
            int userRoleWhoGetCommentId = 117654;
            int userRoleCommentToId = 1654;
            UserRoleEntity userRoleWhoGetComment = new UserRoleEntity
            {
                Id = 117654,
                Name = "Client"
            };
            UserRoleEntity userRoleCommentTo = new UserRoleEntity
            {
                Id = 1654,
                Name = "Client"
            };

            yield return new object[] {userIdToComment, userRoleWhoGetComment, userEntityToComment, userIdFromComment, userEntityFromComment,
                                        commentsEntities, userRoleWhoGetCommentId, userRoleCommentToId, userRoleCommentTo};
        }

        public static IEnumerable GetCommentsAndScoresAboutOtherUsers_ForSitterAboutClientTestCaseSource()
        {
            //1. Ситтер получает комменты о клиенте - 1 коммент

            int userIdFromComment = 19;
            UserEntity userEntityFromComment = new UserEntity
            {
                Id = 19,
                UserRoleId = 119,
                UserRole = new UserRoleEntity
                {
                    Id = 119,
                    Name = "Sitter"
                }
            };
            int userIdToComment = 29;
            UserEntity userEntityToComment = new UserEntity
            {
                Id = 29,
                UserRoleId = 229,
                UserRole = new UserRoleEntity
                {
                    Id = 229,
                    Name = "Client"
                }
            };
            List<CommentEntity> commentsEntities = new List<CommentEntity>
            {
             new CommentEntity
                 {
                 Id = 79,
                 Text = "79",
                 Score = 3,
                 Order = new OrderEntity
                     {
                     Id = 59,
                     DateStart = new DateTime (2023-09-01),
                     DateEnd = new DateTime (2023-09-02),
                     },
                 CommentFromUser = new UserEntity
                    {
                     Name = "Man9"
                    }
                 },
            };
            int userRoleWhoGetCommentId = 119;
            UserRoleEntity userRoleWhoGetComment = new UserRoleEntity
            {
                Id = 119,
                Name = "Sitter"
            };
            int userRoleCommentToId = 229;
            UserRoleEntity userRoleCommentTo = new UserRoleEntity
            {
                Id = 229,
                Name = "Client"
            };
            AvgScoreCommentsResponse<CommentsAboutOtherUsersResponse> expected = new AvgScoreCommentsResponse<CommentsAboutOtherUsersResponse>
            {
                AverageScore = 3,
                Comments = new List<CommentsAboutOtherUsersResponse>
                {
                    new CommentsAboutOtherUsersResponse
                    {
                    Id = 79,
                    Text = "79",
                    Score = 3,
                    CommentFromUser = new UserForCommentResponse
                        {
                          Name = "Man9"
                        }
                    }
                }
            };

            yield return new object[] {userIdToComment, userEntityToComment, userIdFromComment, userEntityFromComment, commentsEntities, userRoleWhoGetCommentId,
                                         userRoleWhoGetComment, userRoleCommentToId, userRoleCommentTo, expected };

            //2. Ситтер получает комменты о клиенте - 2 коммента

            userIdFromComment = 128;
            userEntityFromComment = new UserEntity
            {
                Id = 128,
                UserRoleId = 1128,
                UserRole = new UserRoleEntity
                {
                    Id = 1128,
                    Name = "Sitter"
                }
            };
            userIdToComment = 228;
            userEntityToComment = new UserEntity
            {
                Id = 228,
                UserRoleId = 2228,
                UserRole = new UserRoleEntity
                {
                    Id = 2228,
                    Name = "Client"
                }
            };
            commentsEntities = new List<CommentEntity>
            {
             new CommentEntity
                 {
                 Id = 728,
                 Text = "728",
                 Score = 2,
                 Order = new OrderEntity
                     {
                     Id = 52,
                     DateStart = new DateTime (2023-02-08),
                     DateEnd = new DateTime (2023-02-09),
                     },
                 CommentFromUser = new UserEntity
                    {
                     Name = "Man28"
                    }
                 },
             new CommentEntity
                 {
                 Id = 7218,
                 Text = "7218",
                 Score = 4,
                 Order = new OrderEntity
                     {
                     Id = 521,
                     DateStart = new DateTime (2023-01-08),
                     DateEnd = new DateTime (2023-01-08),
                     },
                 CommentFromUser = new UserEntity
                    {
                     Name = "Man218"
                    }
                 },
            };
            userRoleWhoGetCommentId = 1128;
            userRoleWhoGetComment = new UserRoleEntity
            {
                Id = 1128,
                Name = "Sitter"
            };
            userRoleCommentToId = 2228;
            userRoleCommentTo = new UserRoleEntity
            {
                Id = 2228,
                Name = "Client"
            };
            expected = new AvgScoreCommentsResponse<CommentsAboutOtherUsersResponse>
            {
                AverageScore = 3,
                Comments = new List<CommentsAboutOtherUsersResponse>
                {
                    new CommentsAboutOtherUsersResponse
                 {
                 Id = 7218,
                 Text = "7218",
                 Score = 4,
                 CommentFromUser = new UserForCommentResponse
                    {
                     Name = "Man218"
                    }
                 },
                    new CommentsAboutOtherUsersResponse
                    {
                    Id = 728,
                    Text = "728",
                    Score = 2,
                    CommentFromUser = new UserForCommentResponse
                        {
                          Name = "Man28"
                        }
                    }
                }
            };

            yield return new object[] {userIdToComment, userEntityToComment, userIdFromComment, userEntityFromComment, commentsEntities, userRoleWhoGetCommentId,
                                         userRoleWhoGetComment, userRoleCommentToId, userRoleCommentTo, expected };

            // 3. Ситтер получает комменты о клиенте  - 0 комментов - пустой лист

            userIdFromComment = 1237;
            userEntityFromComment = new UserEntity
            {
                Id = 1237,
                UserRoleId = 11237,
                UserRole = new UserRoleEntity
                {
                    Id = 11237,
                    Name = "Sitter"
                }
            };
            userIdToComment = 2237;
            userEntityToComment = new UserEntity
            {
                Id = 2237,
                UserRoleId = 22237,
                UserRole = new UserRoleEntity
                {
                    Id = 22237,
                    Name = "Client"
                }
            };
            commentsEntities = new List<CommentEntity>();
            userRoleWhoGetCommentId = 11237;
            userRoleWhoGetComment = new UserRoleEntity
            {
                Id = 11237,
                Name = "Sitter"
            };
            userRoleCommentToId = 22237;
            userRoleCommentTo = new UserRoleEntity
            {
                Id = 22237,
                Name = "Client"
            };
            expected = new AvgScoreCommentsResponse<CommentsAboutOtherUsersResponse>
            {
                AverageScore = 0,
                Comments = new List<CommentsAboutOtherUsersResponse>()
            };

            yield return new object[] {userIdToComment, userEntityToComment, userIdFromComment, userEntityFromComment, commentsEntities, userRoleWhoGetCommentId,
                                         userRoleWhoGetComment, userRoleCommentToId, userRoleCommentTo, expected };
        }

    }
}
