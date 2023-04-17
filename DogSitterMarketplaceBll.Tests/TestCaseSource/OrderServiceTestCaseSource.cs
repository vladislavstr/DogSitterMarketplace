using DogSitterMarketplaceBll.Models.Appeals.Response;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceBll.Models.Pets.Response;
using DogSitterMarketplaceBll.Models.Users.Response;
using DogSitterMarketplaceBll.Models.Works.Response;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.Models.Works;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DogSitterMarketplaceBll.Tests.TestCaseSource
{
    public class OrderServiceTestCaseSource
    {
        public static IEnumerable DeleteOrderByIdTestCaseSource()
        {
            int id = 2;

            yield return new object[] { id };
        }

        public static IEnumerable DeleteOrderByIdTest_WhenIdIsNotExist_ShouldNotFoundExceptionTestCaseSource()
        {
            int id = 14;

            yield return new object[] { id };
        }

        public static IEnumerable AddOrderTestCaseSource()
        {
            List<int> petsId = new List<int> { 1 };
            OrderCreateRequest newOrder = new OrderCreateRequest
            {
                Comment = "comment",
                OrderStatusId = 1,
                SitterWorkId = 10,
                Summ = 100,
                DateStart = new DateTime(2023, 04, 12),
                DateEnd = new DateTime(2023, 04, 13),
                LocationId = 1000,
                Pets = petsId
            };
            List<PetEntity> allPets = new List<PetEntity>
            {
            new PetEntity
                {
                    Id =1,
                    Name = "name",
                    Characteristics = "height",
                    Type = new AnimalTypeEntity
                                {
                                    Id =30,
                                    Name= "nameType",
                                    Parameters = "param",
                                    IsDeleted = false
                                },
                    TypeId = 30,
                    User = new UserEntity
                    {
                        Id = 11,
                        Email = "email",
                        PhoneNumber= "1234567890",
                        Name= "name",
                        Password = "password",
                        Pets = new List<PetEntity>(),
                        Role = new UserRoleEntity
                                    {
                                        Id = 9,
                                        Name = "9"
                                    }
                    },
                    UserId = 11,
                    IsDeleted = false
                }
            };
            OrderEntity orderEntity = new OrderEntity
            {
                Comment = "comment",
                OrderStatusId = 1,
                SitterWorkId = 10,
                Summ = 100,
                DateStart = new DateTime(2023, 04, 12),
                DateEnd = new DateTime(2023, 04, 13),
                LocationId = 1000
            };
            orderEntity.Pets.AddRange(allPets);
            OrderEntity addOrderEntity = new OrderEntity
            {
                Id = 1,
                Comment = "comment",
                OrderStatusId = 1,
                SitterWorkId = 10,
                Summ = 100,
                DateStart = new DateTime(2023, 04, 12),
                DateEnd = new DateTime(2023, 04, 13),
                LocationId = 1000,

                OrderStatus = new OrderStatusEntity
                {
                    Id = 1,
                    Name = "1",
                },
                SitterWork = new SitterWorkEntity
                {
                    Id = 10
                },                
                Location = new LocationEntity
                {
                    Id = 1000
                }
            };
            addOrderEntity.Pets.AddRange(allPets);
            List<PetResponse> petsResponse = new List<PetResponse>
            {
            new PetResponse
                {
                    Id =1,
                    Name = "name",
                    Characteristics = "height",
                    Type = new AnimalTypeResponse
                                {
                                    Id =30,
                                    Name= "nameType",
                                    Parameters = "param"
                                },
                    User = new UserShortResponse
                    {
                        Id = 11,
                        Email = "email",
                        PhoneNumber= "1234567890",
                        Name= "name"
                    }
                }
            };
            List<string> messagesOfIsDeleted = new List<string>();
            OrderResponse expected = new OrderResponse
            {
                Id = 1,
                Comment = "comment",
                OrderStatus = new OrderStatusResponse
                {
                    Id = 1,
                    Name = "1",
                },
                SitterWork = new SitterWorkResponse
                {
                    Id = 10
                },
                Summ = 100,
                DateStart = new DateTime(2023, 04, 12),
                DateEnd = new DateTime(2023, 04, 13),
                Location = new LocationResponse
                {
                    Id = 1000
                },
                Comments = new List<CommentResponse>(),
                Appeals = new List<AppealResponse>(),
                Pets = petsResponse,
                Messages = new List<string>()
            };

            yield return new object[] {  petsId, allPets, messagesOfIsDeleted, orderEntity,
                                 addOrderEntity, newOrder, expected};
        }
    }
}

//(List<int> petsId, List<PetEntity> allPets,OrderCreateRequest newOrder,OrderEntity orderEntity, OrderEntity addOrderEntity, OrderResponse addOrderResponse,
//
//List<string> messagesOfIsDeleted, 
//   OrderResponse expected)