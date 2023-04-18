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
            //1. Внутри Ордера передан корректный, неудаленный пет

            List<int> petsId = new List<int> { 1 };
            OrderCreateRequest newOrder = new OrderCreateRequest
            {
                Comment = "comment",
               // OrderStatusId = 1,
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

            //2. Внутри ордера передано 2 корректных, не удаленных пета

            petsId = new List<int> { 12, 22 };
            newOrder = new OrderCreateRequest
            {
                Comment = "comment2",
                //OrderStatusId = 12,
                SitterWorkId = 102,
                Summ = 1002,
                DateStart = new DateTime(2023, 04, 14),
                DateEnd = new DateTime(2023, 04, 16),
                LocationId = 10002,
                Pets = petsId
            };
            allPets = new List<PetEntity>
            {
                 new PetEntity
                    {
                        Id =12,
                        Name = "name2",
                        Characteristics = "height2",
                        Type = new AnimalTypeEntity
                                    {
                                        Id =302,
                                        Name= "nameType2",
                                        Parameters = "param2",
                                        IsDeleted = false
                                    },
                        TypeId = 302,
                        User = new UserEntity
                        {
                            Id = 112,
                            Email = "email2",
                            PhoneNumber= "12345678902",
                            Name= "name2",
                            Password = "password2",
                            Pets = new List<PetEntity>(),
                            Role = new UserRoleEntity
                                        {
                                            Id = 92,
                                            Name = "92"
                                        }
                        },
                        UserId = 112,
                        IsDeleted = false
                    },
                  new PetEntity
                    {
                        Id = 22,
                        Name = "name22",
                        Characteristics = "height22",
                        Type = new AnimalTypeEntity
                        {
                            Id = 3022,
                            Name = "nameType22",
                            Parameters = "param22",
                            IsDeleted = false
                        },
                        TypeId = 3022,
                        User = new UserEntity
                        {
                            Id = 112,
                            Email = "email2",
                            PhoneNumber = "12345678902",
                            Name = "name2",
                            Password = "password2",
                            Pets = new List<PetEntity>(),
                            Role = new UserRoleEntity
                            {
                                Id = 92,
                                Name = "92"
                            }
                        },
                        UserId = 112,
                        IsDeleted = false
                    }
        };
            orderEntity = new OrderEntity
            {
                Comment = "comment2",
              //  OrderStatusId = 12,
                SitterWorkId = 102,
                Summ = 1002,
                DateStart = new DateTime(2023, 04, 14),
                DateEnd = new DateTime(2023, 04, 16),
                LocationId = 10002
            };
            orderEntity.Pets.AddRange(allPets);
            addOrderEntity = new OrderEntity
            {
                Id = 12,
                Comment = "comment2",
                OrderStatusId = 12,
                SitterWorkId = 102,
                Summ = 1002,
                DateStart = new DateTime(2023, 04, 14),
                DateEnd = new DateTime(2023, 04, 16),
                LocationId = 10002,
                OrderStatus = new OrderStatusEntity
                {
                    Id = 12,
                    Name = "12",
                },
                SitterWork = new SitterWorkEntity
                {
                    Id = 102
                },
                Location = new LocationEntity
                {
                    Id = 10002
                }
            };
            addOrderEntity.Pets.AddRange(allPets);
            petsResponse = new List<PetResponse>
            {
                new PetResponse
                    {
                        Id =12,
                        Name = "name2",
                        Characteristics = "height2",
                        Type = new AnimalTypeResponse
                                    {
                                        Id =302,
                                        Name= "nameType2",
                                        Parameters = "param2"
                                    },
                        User = new UserShortResponse
                        {
                            Id = 112,
                            Email = "email2",
                            PhoneNumber= "12345678902",
                            Name= "name2"
                        }
                    },
                new PetResponse
                    {
                        Id =22,
                        Name = "name22",
                        Characteristics = "height22",
                        Type = new AnimalTypeResponse
                                    {
                                        Id =3022,
                                        Name= "nameType22",
                                        Parameters = "param22"
                                    },
                        User = new UserShortResponse
                        {
                            Id = 112,
                            Email = "email2",
                            PhoneNumber= "12345678902",
                            Name= "name2"
                        }
                    }
            };
            messagesOfIsDeleted = new List<string>();
            expected = new OrderResponse
            {
                Id = 12,
                Comment = "comment2",
                OrderStatus = new OrderStatusResponse
                {
                    Id = 12,
                    Name = "12",
                },
                SitterWork = new SitterWorkResponse
                {
                    Id = 102
                },
                Summ = 1002,
                DateStart = new DateTime(2023, 04, 14),
                DateEnd = new DateTime(2023, 04, 16),
                Location = new LocationResponse
                {
                    Id = 10002
                },
                Comments = new List<CommentResponse>(),
                Appeals = new List<AppealResponse>(),
                Pets = petsResponse,
                Messages = new List<string>()
            };

            yield return new object[] {  petsId, allPets, messagesOfIsDeleted, orderEntity,
                                 addOrderEntity, newOrder, expected };

            //3. Внутри Ордера передан 1 корректный, неудаленный пет и 2 -удаленный (он не добавился, вывелось сообщение)

            petsId = new List<int> { 123, 223 };
            newOrder = new OrderCreateRequest
            {
                Comment = "comment23",
              //  OrderStatusId = 123,
                SitterWorkId = 1023,
                Summ = 10023,
                DateStart = new DateTime(2023, 04, 17),
                DateEnd = new DateTime(2023, 04, 19),
                LocationId = 100023,
                Pets = petsId
            };
            allPets = new List<PetEntity>
            {
                 new PetEntity
                    {
                        Id =123,
                        Name = "name23",
                        Characteristics = "height23",
                        Type = new AnimalTypeEntity
                                    {
                                        Id =3023,
                                        Name= "nameType23",
                                        Parameters = "param23",
                                        IsDeleted = false
                                    },
                        TypeId = 3023,
                        User = new UserEntity
                        {
                            Id = 1123,
                            Email = "email23",
                            PhoneNumber= "123456789023",
                            Name= "name23",
                            Password = "password23",
                            Pets = new List<PetEntity>(),
                            Role = new UserRoleEntity
                                        {
                                            Id = 923,
                                            Name = "923"
                                        }
                        },
                        UserId = 1123,
                        IsDeleted = false
                    },
                  new PetEntity
                    {
                        Id = 223,
                        Name = "name223",
                        Characteristics = "height223",
                        Type = new AnimalTypeEntity
                        {
                            Id = 30223,
                            Name = "nameType223",
                            Parameters = "param223",
                            IsDeleted = false
                        },
                        TypeId = 30223,
                        User = new UserEntity
                        {
                            Id = 1123,
                            Email = "email23",
                            PhoneNumber = "123456789023",
                            Name = "name23",
                            Password = "password23",
                            Pets = new List<PetEntity>(),
                            Role = new UserRoleEntity
                            {
                                Id = 923,
                                Name = "923"
                            }
                        },
                        UserId = 1123,
                        IsDeleted = true
                    }
        };
            List<PetEntity> allPetsNotDeleted = new List<PetEntity>
            {
                 new PetEntity
                    {
                        Id =123,
                        Name = "name23",
                        Characteristics = "height23",
                        Type = new AnimalTypeEntity
                                    {
                                        Id =3023,
                                        Name= "nameType23",
                                        Parameters = "param23",
                                        IsDeleted = false
                                    },
                        TypeId = 3023,
                        User = new UserEntity
                        {
                            Id = 1123,
                            Email = "email23",
                            PhoneNumber= "123456789023",
                            Name= "name23",
                            Password = "password23",
                            Pets = new List<PetEntity>(),
                            Role = new UserRoleEntity
                                        {
                                            Id = 923,
                                            Name = "923"
                                        }
                        },
                        UserId = 1123,
                        IsDeleted = false
                    }
            };
            orderEntity = new OrderEntity
            {
                Comment = "comment23",
                OrderStatusId = 123,
                SitterWorkId = 1023,
                Summ = 10023,
                DateStart = new DateTime(2023, 04, 17),
                DateEnd = new DateTime(2023, 04, 19),
                LocationId = 100023
            };
            orderEntity.Pets.AddRange(allPetsNotDeleted);
            addOrderEntity = new OrderEntity
            {
                Id = 123,
                Comment = "comment23",
                OrderStatusId = 123,
                SitterWorkId = 1023,
                Summ = 10023,
                DateStart = new DateTime(2023, 04, 17),
                DateEnd = new DateTime(2023, 04, 19),
                LocationId = 100023,
                OrderStatus = new OrderStatusEntity
                {
                    Id = 123,
                    Name = "123",
                },
                SitterWork = new SitterWorkEntity
                {
                    Id = 1023
                },
                Location = new LocationEntity
                {
                    Id = 100023
                }
            };
            addOrderEntity.Pets.AddRange(allPetsNotDeleted);
            petsResponse = new List<PetResponse>
            {
                new PetResponse
                    {
                        Id =123,
                        Name = "name23",
                        Characteristics = "height23",
                        Type = new AnimalTypeResponse
                                    {
                                        Id =3023,
                                        Name= "nameType23",
                                        Parameters = "param23"
                                    },
                        User = new UserShortResponse
                        {
                            Id = 1123,
                            Email = "email23",
                            PhoneNumber= "123456789023",
                            Name= "name23"
                        }
                    },
                //new PetResponse
                //    {
                //        Id =22,
                //        Name = "name22",
                //        Characteristics = "height22",
                //        Type = new AnimalTypeResponse
                //                    {
                //                        Id =3022,
                //                        Name= "nameType22",
                //                        Parameters = "param22"
                //                    },
                //        User = new UserShortResponse
                //        {
                //            Id = 112,
                //            Email = "email2",
                //            PhoneNumber= "12345678902",
                //            Name= "name2"
                //        }
                //    }
            };
            messagesOfIsDeleted = new List<string>
                {
                "Pet with id 223 is deleted."
                };
            expected = new OrderResponse
            {
                Id = 123,
                Comment = "comment23",
                OrderStatus = new OrderStatusResponse
                {
                    Id = 123,
                    Name = "123",
                },
                SitterWork = new SitterWorkResponse
                {
                    Id = 1023
                },
                Summ = 10023,
                DateStart = new DateTime(2023, 04, 17),
                DateEnd = new DateTime(2023, 04, 19),
                Location = new LocationResponse
                {
                    Id = 100023
                },
                Comments = new List<CommentResponse>(),
                Appeals = new List<AppealResponse>(),
                Pets = petsResponse,
                Messages = new List<string>
                    {
                    "Pet with id 223 is deleted."
                    }
        };

        yield return new object[] {  petsId, allPets, messagesOfIsDeleted, orderEntity,
                                 addOrderEntity, newOrder, expected


    };
}

    }
}