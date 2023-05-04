using Azure.Core;
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
            //1. Внутри Ордера передан корректный, неудаленный пет; ордер попадает в расписание и других заказов в работе в этот день нет

            List<int> petsId = new List<int> { 1 };
            OrderCreateRequest newOrder = new OrderCreateRequest
            {
                Comment = "comment",
                SitterWorkId = 10,
                Summ = 100,
                DateStart = new DateTime(2023, 04, 17, 12, 00, 00),
                DateEnd = new DateTime(2023, 04, 17, 13, 00, 00),
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
                        UserRole = new UserRoleEntity
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
                OrderStatusId = 3,
                SitterWorkId = 10,
                Summ = 100,
                DateStart = new DateTime(2023, 04, 17, 12, 00, 00),
                DateEnd = new DateTime(2023, 04, 17, 13, 00, 00),
                LocationId = 1000
            };
            orderEntity.Pets.AddRange(allPets);
            OrderEntity addOrderEntity = new OrderEntity
            {
                Id = 1,
                Comment = "comment",
                OrderStatusId = 3,
                SitterWorkId = 10,
                Summ = 100,
                DateStart = new DateTime(2023, 04, 17, 12, 00, 00),
                DateEnd = new DateTime(2023, 04, 17, 13, 00, 00),
                LocationId = 1000,

                OrderStatus = new OrderStatusEntity
                {
                    Id = 3,
                    Name = "3",
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
                        Name= "name",
                        RoleId = 9
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
                    Id = 3,
                    Name = "3",
                },
                SitterWork = new SitterWorkResponse
                {
                    Id = 10
                },
                Summ = 100,
                DateStart = new DateTime(2023, 04, 17, 12, 00, 00),
                DateEnd = new DateTime(2023, 04, 17, 13, 00, 00),
                Location = new LocationResponse
                {
                    Id = 1000
                },
                Comments = new List<CommentResponse>(),
                Appeals = new List<AppealResponse>(),
                Pets = petsResponse,
                Messages = new List<string>()
            };
            int sitterId = 11;
            int sitterWorkId = 10;
            SitterWorkEntity sitterWork = new SitterWorkEntity
            {
                Id = 10,
                Comment = "comment",
                User = new UserEntity
                {
                    Id = 11,
                    Email = "email",
                    PhoneNumber = "1234567890",
                    Name = "name",
                    Password = "password",
                    Pets = new List<PetEntity>(),
                    UserRole = new UserRoleEntity
                    {
                        Id = 9,
                        Name = "9"
                    }
                },
                UserId = 11,
                WorkType = new WorkTypeEntity
                {
                    Id = 101,
                    Name = "type101",
                    IsDeleted = false
                },
                WorkTypeId = 101,
                IsDeleted = false,
            };
            List<SitterWorkEntity> allSitterWorks = new List<SitterWorkEntity>
            {
                new SitterWorkEntity
                {
                LocationsWork = new List<LocationWorkEntity>
                    {
                      new LocationWorkEntity
                      {
                       LocationId = 1000
                      }
                    }
                }
            };
            allSitterWorks[0].LocationsWork.First().TimingLocationWorks.Add(
                                     new TimingLocationWorkEntity
                                     {
                                         Id = 11,
                                         Start = new TimeSpan(10, 00, 00),
                                         Stop = new TimeSpan(16, 00, 00),
                                         DayOfWeek = new DayOfWeekEntity
                                         {
                                             Id = 1,
                                             Name = "Monday"
                                         },
                                         DayOfWeekId = 1,
                                         LocationWork = new LocationWorkEntity
                                         {
                                             Id = 117
                                         },
                                         LocationWorkId = 117
                                     });
            DateTime startDateOrder = new DateTime(2023, 04, 17, 12, 00, 00);
            List<OrderEntity> allOrdersBySitter = new List<OrderEntity>();
            OrderStatusEntity orderStatusUnderConsideration = new OrderStatusEntity
            {
                Id = 3,
                Name = "under consideration",
                IsDeleted = false
            };

            yield return new object[] {  petsId, allPets, messagesOfIsDeleted, orderEntity, addOrderEntity, newOrder,
                                     expected, sitterId, sitterWork, allSitterWorks, startDateOrder, allOrdersBySitter, sitterWorkId,
                                     orderStatusUnderConsideration};

            //2. Внутри ордера передано 2 корректных, не удаленных пета; ордер попадает в расписание и других заказов в работе в этот день нет

            petsId = new List<int> { 12, 22 };
            newOrder = new OrderCreateRequest
            {
                Comment = "comment2",
                SitterWorkId = 102,
                Summ = 1002,
                DateStart = new DateTime(2023, 04, 18, 12, 30, 00),
                DateEnd = new DateTime(2023, 04, 18, 13, 30, 00),
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
                            UserRole = new UserRoleEntity
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
                            UserRole = new UserRoleEntity
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
                OrderStatusId = 3,
                SitterWorkId = 102,
                Summ = 1002,
                DateStart = new DateTime(2023, 04, 18, 12, 30, 00),
                DateEnd = new DateTime(2023, 04, 18, 13, 30, 00),
                LocationId = 10002
            };
            orderEntity.Pets.AddRange(allPets);
            addOrderEntity = new OrderEntity
            {
                Id = 12,
                Comment = "comment2",
                OrderStatusId = 3,
                SitterWorkId = 102,
                Summ = 1002,
                DateStart = new DateTime(2023, 04, 18, 12, 30, 00),
                DateEnd = new DateTime(2023, 04, 18, 13, 30, 00),
                LocationId = 10002,
                OrderStatus = new OrderStatusEntity
                {
                    Id = 3,
                    Name = "3",
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
                            Name= "name2",
                            RoleId = 92
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
                            Name= "name2",
                            RoleId = 92
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
                    Id = 3,
                    Name = "3",
                },
                SitterWork = new SitterWorkResponse
                {
                    Id = 102
                },
                Summ = 1002,
                DateStart = new DateTime(2023, 04, 18, 12, 30, 00),
                DateEnd = new DateTime(2023, 04, 18, 13, 30, 00),
                Location = new LocationResponse
                {
                    Id = 10002
                },
                Comments = new List<CommentResponse>(),
                Appeals = new List<AppealResponse>(),
                Pets = petsResponse,
                Messages = new List<string>()
            };
            sitterId = 112;
            sitterWorkId = 102;
            sitterWork = new SitterWorkEntity
            {
                Id = 102,
                Comment = "comment",
                User = new UserEntity
                {
                    Id = 112,
                    Email = "email2",
                    PhoneNumber = "12345678902",
                    Name = "name2",
                    Password = "password2",
                    Pets = new List<PetEntity>(),
                    UserRole = new UserRoleEntity
                    {
                        Id = 92,
                        Name = "92"
                    }
                },
                UserId = 112,
                WorkType = new WorkTypeEntity
                {
                    Id = 1012,
                    Name = "type1012",
                    IsDeleted = false
                },
                WorkTypeId = 1012,
                IsDeleted = false,
            };
            allSitterWorks = new List<SitterWorkEntity>
            {
                new SitterWorkEntity
                {
                LocationsWork = new List<LocationWorkEntity>
                    {
                      new LocationWorkEntity
                      {
                       LocationId = 10002
                      }
                    }
                }
            };
            allSitterWorks[0].LocationsWork.First().TimingLocationWorks.Add(
                                     new TimingLocationWorkEntity
                                     {
                                         Id = 112,
                                         Start = new TimeSpan(10, 30, 00),
                                         Stop = new TimeSpan(15, 00, 00),
                                         DayOfWeek = new DayOfWeekEntity
                                         {
                                             Id = 2,
                                             Name = "Tuesday"
                                         },
                                         DayOfWeekId = 2,
                                         LocationWork = new LocationWorkEntity
                                         {
                                             Id = 1172
                                         },
                                         LocationWorkId = 1172
                                     });
            startDateOrder = new DateTime(2023, 04, 18, 12, 30, 00);
            allOrdersBySitter = new List<OrderEntity>();
            orderStatusUnderConsideration = new OrderStatusEntity
            {
                Id = 3,
                Name = "under consideration",
                IsDeleted = false
            };

            yield return new object[] {  petsId, allPets, messagesOfIsDeleted, orderEntity, addOrderEntity, newOrder,
                                     expected, sitterId, sitterWork, allSitterWorks, startDateOrder, allOrdersBySitter, sitterWorkId, orderStatusUnderConsideration};

            //3. Внутри Ордера передан 1 корректный, неудаленный пет и 2 -удаленный (он не добавился, вывелось сообщение); ордер попадает в расписание и других заказов в работе в этот день нет

            petsId = new List<int> { 123, 223 };
            newOrder = new OrderCreateRequest
            {
                Comment = "comment23",
                SitterWorkId = 1023,
                Summ = 10023,
                DateStart = new DateTime(2023, 04, 19, 14, 30, 00),
                DateEnd = new DateTime(2023, 04, 19, 15, 30, 00),
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
                            UserRole = new UserRoleEntity
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
                            UserRole = new UserRoleEntity
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
                            UserRole = new UserRoleEntity
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
                OrderStatusId = 3,
                SitterWorkId = 1023,
                Summ = 10023,
                DateStart = new DateTime(2023, 04, 19, 14, 30, 00),
                DateEnd = new DateTime(2023, 04, 19, 15, 30, 00),
                LocationId = 100023
            };
            orderEntity.Pets.AddRange(allPetsNotDeleted);
            addOrderEntity = new OrderEntity
            {
                Id = 123,
                Comment = "comment23",
                OrderStatusId = 3,
                SitterWorkId = 1023,
                Summ = 10023,
                DateStart = new DateTime(2023, 04, 19, 14, 30, 00),
                DateEnd = new DateTime(2023, 04, 19, 15, 30, 00),
                LocationId = 100023,
                OrderStatus = new OrderStatusEntity
                {
                    Id = 3,
                    Name = "3",
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
                            Name= "name23",
                            RoleId = 923
                        }
                    },
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
                    Id = 3,
                    Name = "3",
                },
                SitterWork = new SitterWorkResponse
                {
                    Id = 1023
                },
                Summ = 10023,
                DateStart = new DateTime(2023, 04, 19, 14, 30, 00),
                DateEnd = new DateTime(2023, 04, 19, 15, 30, 00),
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
            sitterId = 1123;
            sitterWorkId = 1023;
            sitterWork = new SitterWorkEntity
            {
                Id = 1023,
                Comment = "comment3",
                User = new UserEntity
                {
                    Id = 1123,
                    Email = "email23",
                    PhoneNumber = "123456789023",
                    Name = "name23",
                    Password = "password23",
                    Pets = new List<PetEntity>(),
                    UserRole = new UserRoleEntity
                    {
                        Id = 923,
                        Name = "923"
                    }
                },
                UserId = 1123,
                WorkType = new WorkTypeEntity
                {
                    Id = 10123,
                    Name = "type10123",
                    IsDeleted = false
                },
                WorkTypeId = 10123,
                IsDeleted = false,
            };
            allSitterWorks = new List<SitterWorkEntity>
            {
                new SitterWorkEntity
                {
                LocationsWork = new List<LocationWorkEntity>
                    {
                      new LocationWorkEntity
                      {
                       LocationId = 100023
                      }
                    }
                }
            };
            allSitterWorks[0].LocationsWork.First().TimingLocationWorks.Add(
                                     new TimingLocationWorkEntity
                                     {
                                         Id = 1123,
                                         Start = new TimeSpan(09, 30, 00),
                                         Stop = new TimeSpan(15, 50, 00),
                                         DayOfWeek = new DayOfWeekEntity
                                         {
                                             Id = 3,
                                             Name = "Wednesday"
                                         },
                                         DayOfWeekId = 3,
                                         LocationWork = new LocationWorkEntity
                                         {
                                             Id = 11723
                                         },
                                         LocationWorkId = 11723
                                     });
            startDateOrder = new DateTime(2023, 04, 19, 14, 30, 00);
            allOrdersBySitter = new List<OrderEntity>();
            orderStatusUnderConsideration = new OrderStatusEntity
            {
                Id = 3,
                Name = "under consideration",
                IsDeleted = false
            };

            yield return new object[] {  petsId, allPets, messagesOfIsDeleted, orderEntity, addOrderEntity, newOrder,
                                     expected, sitterId, sitterWork, allSitterWorks, startDateOrder, allOrdersBySitter, sitterWorkId, orderStatusUnderConsideration};

            //4. Внутри Ордера передан корректный, неудаленный пет; ордер попадает в расписание и есть другой заказ в этот день(но они не пересекаются)

            petsId = new List<int> { 14 };
            newOrder = new OrderCreateRequest
            {
                Comment = "comment4",
                SitterWorkId = 104,
                Summ = 1004,
                DateStart = new DateTime(2023, 04, 20, 12, 10, 00),
                DateEnd = new DateTime(2023, 04, 20, 12, 50, 00),
                LocationId = 10004,
                Pets = petsId
            };
            allPets = new List<PetEntity>
            {
            new PetEntity
                {
                    Id =14,
                    Name = "name4",
                    Characteristics = "height4",
                    Type = new AnimalTypeEntity
                                {
                                    Id =304,
                                    Name= "nameType4",
                                    Parameters = "param4",
                                    IsDeleted = false
                                },
                    TypeId = 304,
                    User = new UserEntity
                    {
                        Id = 114,
                        Email = "email4",
                        PhoneNumber= "12345678904",
                        Name= "name4",
                        Password = "password4",
                        Pets = new List<PetEntity>(),
                        UserRole = new UserRoleEntity
                                    {
                                        Id = 94,
                                        Name = "94"
                                    }
                    },
                    UserId = 114,
                    IsDeleted = false
                }
            };
            orderEntity = new OrderEntity
            {
                Comment = "comment4",
                OrderStatusId = 3,
                SitterWorkId = 104,
                Summ = 1004,
                DateStart = new DateTime(2023, 04, 20, 12, 10, 00),
                DateEnd = new DateTime(2023, 04, 20, 12, 50, 00),
                LocationId = 10004
            };
            orderEntity.Pets.AddRange(allPets);
            addOrderEntity = new OrderEntity
            {
                Id = 14,
                Comment = "comment4",
                OrderStatusId = 3,
                SitterWorkId = 104,
                Summ = 1004,
                DateStart = new DateTime(2023, 04, 20, 12, 10, 00),
                DateEnd = new DateTime(2023, 04, 20, 12, 50, 00),
                LocationId = 10004,

                OrderStatus = new OrderStatusEntity
                {
                    Id = 3,
                    Name = "3",
                },
                SitterWork = new SitterWorkEntity
                {
                    Id = 104
                },
                Location = new LocationEntity
                {
                    Id = 10004
                }
            };
            addOrderEntity.Pets.AddRange(allPets);
            petsResponse = new List<PetResponse>
            {
            new PetResponse
                {
                    Id =14,
                    Name = "name4",
                    Characteristics = "height4",
                    Type = new AnimalTypeResponse
                                {
                                    Id =304,
                                    Name= "nameType4",
                                    Parameters = "param4"
                                },
                    User = new UserShortResponse
                    {
                        Id = 114,
                        Email = "email4",
                        PhoneNumber= "12345678904",
                        Name= "name4",
                        RoleId = 94
                    }
                }
            };
            messagesOfIsDeleted = new List<string>();
            expected = new OrderResponse
            {
                Id = 14,
                Comment = "comment4",
                OrderStatus = new OrderStatusResponse
                {
                    Id = 3,
                    Name = "3",
                },
                SitterWork = new SitterWorkResponse
                {
                    Id = 104
                },
                Summ = 1004,
                DateStart = new DateTime(2023, 04, 20, 12, 10, 00),
                DateEnd = new DateTime(2023, 04, 20, 12, 50, 00),
                Location = new LocationResponse
                {
                    Id = 10004
                },
                Comments = new List<CommentResponse>(),
                Appeals = new List<AppealResponse>(),
                Pets = petsResponse,
                Messages = new List<string>()
            };
            sitterId = 114;
            sitterWorkId = 104;
            sitterWork = new SitterWorkEntity
            {
                Id = 104,
                Comment = "comment4",
                User = new UserEntity
                {
                    Id = 114,
                    Email = "email4",
                    PhoneNumber = "12345678904",
                    Name = "name4",
                    Password = "password4",
                    Pets = new List<PetEntity>(),
                    UserRole = new UserRoleEntity
                    {
                        Id = 94,
                        Name = "94"
                    }
                },
                UserId = 114,
                WorkType = new WorkTypeEntity
                {
                    Id = 1014,
                    Name = "type1014",
                    IsDeleted = false
                },
                WorkTypeId = 1014,
                IsDeleted = false,
            };
            allSitterWorks = new List<SitterWorkEntity>
            {
                new SitterWorkEntity
                {
                LocationsWork = new List<LocationWorkEntity>
                    {
                      new LocationWorkEntity
                      {
                       LocationId = 10004
                      }
                    }
                }
            };
            allSitterWorks[0].LocationsWork.First().TimingLocationWorks.Add(
                                     new TimingLocationWorkEntity
                                     {
                                         Id = 114,
                                         Start = new TimeSpan(08, 30, 00),
                                         Stop = new TimeSpan(12, 50, 00),
                                         DayOfWeek = new DayOfWeekEntity
                                         {
                                             Id = 4,
                                             Name = "Thursday"
                                         },
                                         DayOfWeekId = 4,
                                         LocationWork = new LocationWorkEntity
                                         {
                                             Id = 1174
                                         },
                                         LocationWorkId = 1174
                                     });
            startDateOrder = new DateTime(2023, 04, 20, 12, 10, 00);
            allOrdersBySitter = new List<OrderEntity>
            {
                new OrderEntity
                {
                    Id = 140,
                Comment = "comment40",
                OrderStatusId = 3,
                SitterWorkId = 1040,
                Summ = 10040,
                DateStart = new DateTime(2023, 04, 20,  08, 30, 00),
                DateEnd = new DateTime(2023, 04, 20, 09, 50, 00),
                LocationId = 10004,

                    OrderStatus = new OrderStatusEntity
                    {
                        Id = 3,
                        Name = "3",
                    },
                    SitterWork = new SitterWorkEntity
                    {
                        Id = 1040
                    },
                    Location = new LocationEntity
                    {
                        Id = 10004
                    }
                }
            };
            orderStatusUnderConsideration = new OrderStatusEntity
            {
                Id = 3,
                Name = "under consideration",
                IsDeleted = false
            };

            yield return new object[] {  petsId, allPets, messagesOfIsDeleted, orderEntity, addOrderEntity, newOrder,
                                     expected, sitterId, sitterWork, allSitterWorks, startDateOrder, allOrdersBySitter, sitterWorkId, orderStatusUnderConsideration};

            //5. Внутри Ордера передан корректный, неудаленный пет; Ордер захватывает два дня(вечер пт-ночь сб), ордер попадает в расписание и есть другой заказ в 2 день(но они не пересекаются)

            petsId = new List<int> { 145 };
            newOrder = new OrderCreateRequest
            {
                Comment = "comment45",
                SitterWorkId = 1045,
                Summ = 10045,
                DateStart = new DateTime(2023, 04, 21, 23, 10, 00),
                DateEnd = new DateTime(2023, 04, 22, 01, 50, 00),
                LocationId = 100045,
                Pets = petsId
            };
            allPets = new List<PetEntity>
            {
            new PetEntity
                {
                    Id =145,
                    Name = "name45",
                    Characteristics = "height45",
                    Type = new AnimalTypeEntity
                                {
                                    Id =3045,
                                    Name= "nameType45",
                                    Parameters = "param45",
                                    IsDeleted = false
                                },
                    TypeId = 3045,
                    User = new UserEntity
                    {
                        Id = 1145,
                        Email = "email45",
                        PhoneNumber= "123456789045",
                        Name= "name45",
                        Password = "password45",
                        Pets = new List<PetEntity>(),
                        UserRole = new UserRoleEntity
                                    {
                                        Id = 945,
                                        Name = "945"
                                    }
                    },
                    UserId = 1145,
                    IsDeleted = false
                }
            };
            orderEntity = new OrderEntity
            {
                Comment = "comment45",
                OrderStatusId = 3,
                SitterWorkId = 1045,
                Summ = 10045,
                DateStart = new DateTime(2023, 04, 21, 23, 10, 00),
                DateEnd = new DateTime(2023, 04, 22, 01, 50, 00),
                LocationId = 100045
            };
            orderEntity.Pets.AddRange(allPets);
            addOrderEntity = new OrderEntity
            {
                Id = 145,
                Comment = "comment45",
                OrderStatusId = 3,
                SitterWorkId = 1045,
                Summ = 10045,
                DateStart = new DateTime(2023, 04, 21, 23, 10, 00),
                DateEnd = new DateTime(2023, 04, 22, 01, 50, 00),
                LocationId = 100045,

                OrderStatus = new OrderStatusEntity
                {
                    Id = 3,
                    Name = "3",
                },
                SitterWork = new SitterWorkEntity
                {
                    Id = 1045
                },
                Location = new LocationEntity
                {
                    Id = 100045
                }
            };
            addOrderEntity.Pets.AddRange(allPets);
            petsResponse = new List<PetResponse>
            {
            new PetResponse
                {
                    Id =145,
                    Name = "name45",
                    Characteristics = "height45",
                    Type = new AnimalTypeResponse
                                {
                                    Id =3045,
                                    Name= "nameType45",
                                    Parameters = "param45"
                                },
                    User = new UserShortResponse
                    {
                        Id = 1145,
                        Email = "email45",
                        PhoneNumber= "123456789045",
                        Name= "name45",
                        RoleId = 945
                    }
                }
            };
            messagesOfIsDeleted = new List<string>();
            expected = new OrderResponse
            {
                Id = 145,
                Comment = "comment45",
                OrderStatus = new OrderStatusResponse
                {
                    Id = 3,
                    Name = "3",
                },
                SitterWork = new SitterWorkResponse
                {
                    Id = 1045
                },
                Summ = 10045,
                DateStart = new DateTime(2023, 04, 21, 23, 10, 00),
                DateEnd = new DateTime(2023, 04, 22, 01, 50, 00),
                Location = new LocationResponse
                {
                    Id = 100045
                },
                Comments = new List<CommentResponse>(),
                Appeals = new List<AppealResponse>(),
                Pets = petsResponse,
                Messages = new List<string>()
            };
            sitterId = 1145;
            sitterWorkId = 1045;
            sitterWork = new SitterWorkEntity
            {
                Id = 1045,
                Comment = "comment45",
                User = new UserEntity
                {
                    Id = 1145,
                    Email = "email45",
                    PhoneNumber = "123456789045",
                    Name = "name45",
                    Password = "password45",
                    Pets = new List<PetEntity>(),
                    UserRole = new UserRoleEntity
                    {
                        Id = 945,
                        Name = "945"
                    }
                },
                UserId = 1145,
                WorkType = new WorkTypeEntity
                {
                    Id = 10145,
                    Name = "type10145",
                    IsDeleted = false
                },
                WorkTypeId = 10145,
                IsDeleted = false,
            };
            allSitterWorks = new List<SitterWorkEntity>
            {
                new SitterWorkEntity
                {
                LocationsWork = new List<LocationWorkEntity>
                    {
                      new LocationWorkEntity
                      {
                       LocationId = 100045
                      }
                    }
                }
            };
            allSitterWorks[0].LocationsWork.First().TimingLocationWorks.Add(
                                     new TimingLocationWorkEntity
                                     {
                                         Id = 1145,
                                         Start = new TimeSpan(21, 30, 00),
                                         Stop = new TimeSpan(23, 59, 59),
                                         DayOfWeek = new DayOfWeekEntity
                                         {
                                             Id = 5,
                                             Name = "Friday"
                                         },
                                         DayOfWeekId = 5,
                                         LocationWork = new LocationWorkEntity
                                         {
                                             Id = 11745
                                         },
                                         LocationWorkId = 11745
                                     });
            allSitterWorks[0].LocationsWork.First().TimingLocationWorks.Add(
                                     new TimingLocationWorkEntity
                                     {
                                         Id = 1155,
                                         Start = new TimeSpan(00, 00, 00),
                                         Stop = new TimeSpan(04, 00, 00),
                                         DayOfWeek = new DayOfWeekEntity
                                         {
                                             Id = 6,
                                             Name = "Saturday"
                                         },
                                         DayOfWeekId = 6,
                                         LocationWork = new LocationWorkEntity
                                         {
                                             Id = 11745
                                         },
                                         LocationWorkId = 11745
                                     });
            startDateOrder = new DateTime(2023, 04, 21, 23, 10, 00);
            allOrdersBySitter = new List<OrderEntity>
            {
                new OrderEntity
                {
                    Id = 1405,
                Comment = "comment405",
                OrderStatusId = 3,
                SitterWorkId = 10405,
                Summ = 100405,
                DateStart = new DateTime(2023, 04, 22, 02, 50, 00),
                DateEnd = new DateTime(2023, 04, 22, 04, 00, 00),
                LocationId = 100045,

                    OrderStatus = new OrderStatusEntity
                    {
                        Id = 3,
                        Name = "3",
                    },
                    SitterWork = new SitterWorkEntity
                    {
                        Id = 10405
                    },
                    Location = new LocationEntity
                    {
                        Id = 100045
                    }
                }
            };
            orderStatusUnderConsideration = new OrderStatusEntity
            {
                Id = 3,
                Name = "under consideration",
                IsDeleted = false
            };

            yield return new object[] {  petsId, allPets, messagesOfIsDeleted, orderEntity, addOrderEntity, newOrder,
                                     expected, sitterId, sitterWork, allSitterWorks, startDateOrder, allOrdersBySitter, sitterWorkId, orderStatusUnderConsideration};
        }

        public static IEnumerable AddOrder_WhenPetIsNotExist_ShouldArgumentException_TestCaseSource()
        {
            // Внутри Ордера передан удаленный пет; ордер попадает в расписание и других заказов в работе в этот день нет

            List<int> petsId = new List<int> { 16 };
            OrderCreateRequest newOrder = new OrderCreateRequest
            {
                Comment = "comment6",
                SitterWorkId = 106,
                Summ = 1006,
                DateStart = new DateTime(2023, 04, 17, 12, 00, 00),
                DateEnd = new DateTime(2023, 04, 17, 13, 00, 00),
                LocationId = 10006,
                Pets = petsId
            };
            List<PetEntity> allPets = new List<PetEntity>
            {
            new PetEntity
                {
                    Id =16,
                    Name = "name6",
                    Characteristics = "height6",
                    Type = new AnimalTypeEntity
                                {
                                    Id =306,
                                    Name= "nameType6",
                                    Parameters = "param6",
                                    IsDeleted = false
                                },
                    TypeId = 306,
                    User = new UserEntity
                    {
                        Id = 116,
                        Email = "email6",
                        PhoneNumber= "12345678906",
                        Name= "name6",
                        Password = "password6",
                        Pets = new List<PetEntity>(),
                        UserRole = new UserRoleEntity
                                    {
                                        Id = 96,
                                        Name = "96"
                                    }
                    },
                    UserId = 116,
                    IsDeleted = true
                }
            };
            int sitterId = 116;
            int sitterWorkId = 106;
            DateTime startDateOrder = new DateTime(2023, 04, 17, 12, 00, 00);

            yield return new object[] { petsId, allPets, newOrder, sitterId, startDateOrder, sitterWorkId };
        }

        public static IEnumerable AddOrder_WhenPetsBelongToDifferentUsers_ShouldArgumentException_TestCaseSource()
        {
            // Внутри Ордера переданы корректные петыб но они принадлежат разным Юзерам

            List<int> petsId = new List<int> { 167, 1678 };
            OrderCreateRequest newOrder = new OrderCreateRequest
            {
                Comment = "comment67",
                SitterWorkId = 1067,
                Summ = 10067,
                DateStart = new DateTime(2023, 04, 17, 12, 00, 00),
                DateEnd = new DateTime(2023, 04, 17, 13, 00, 00),
                LocationId = 100067,
                Pets = petsId
            };
            List<PetEntity> allPets = new List<PetEntity>
            {
            new PetEntity
                {
                    Id =167,
                    Name = "name67",
                    Characteristics = "height67",
                    Type = new AnimalTypeEntity
                                {
                                    Id =3067,
                                    Name= "nameType67",
                                    Parameters = "param67",
                                    IsDeleted = false
                                },
                    TypeId = 3067,
                    User = new UserEntity
                    {
                        Id = 1167,
                        Email = "email67",
                        PhoneNumber= "123456789067",
                        Name= "name67",
                        Password = "password67",
                        Pets = new List<PetEntity>(),
                        UserRole = new UserRoleEntity
                                    {
                                        Id = 967,
                                        Name = "967"
                                    }
                    },
                    UserId = 1167,
                    IsDeleted = false
                },
              new PetEntity
                {
                    Id =1678,
                    Name = "name678",
                    Characteristics = "height678",
                    Type = new AnimalTypeEntity
                                {
                                    Id =3067,
                                    Name= "nameType67",
                                    Parameters = "param67",
                                    IsDeleted = false
                                },
                    TypeId = 3067,
                    User = new UserEntity
                    {
                        Id = 11678,
                        Email = "email678",
                        PhoneNumber= "1234567890678",
                        Name= "name678",
                        Password = "password678",
                        Pets = new List<PetEntity>(),
                        UserRole = new UserRoleEntity
                                    {
                                        Id = 967,
                                        Name = "967"
                                    }
                    },
                    UserId = 11678,
                    IsDeleted = false
                }
            };
            int sitterId = 1167;
            int sitterWorkId = 1067;
            DateTime startDateOrder = new DateTime(2023, 04, 17, 12, 00, 00);

            yield return new object[] { petsId, allPets, newOrder, sitterId, startDateOrder, sitterWorkId };
        }

        public static IEnumerable AddOrder_WhenDateStartOrderNotEarlierThenDateEndOrder_ShouldArgumentException_TestCaseSource()
        {
            // Внутри Ордера передан корректный пет, но дата окончания заказа раньше даты начала заказа

            List<int> petsId = new List<int> { 1679 };
            OrderCreateRequest newOrder = new OrderCreateRequest
            {
                Comment = "comment679",
                SitterWorkId = 10679,
                Summ = 100679,
                DateStart = new DateTime(2023, 04, 17, 12, 00, 00),
                DateEnd = new DateTime(2023, 04, 16, 13, 00, 00),
                LocationId = 1000679,
                Pets = petsId
            };
            List<PetEntity> allPets = new List<PetEntity>
            {
            new PetEntity
                {
                    Id =1679,
                    Name = "name679",
                    Characteristics = "height679",
                    Type = new AnimalTypeEntity
                                {
                                    Id =30679,
                                    Name= "nameType679",
                                    Parameters = "param679",
                                    IsDeleted = false
                                },
                    TypeId = 30679,
                    User = new UserEntity
                    {
                        Id = 11679,
                        Email = "email679",
                        PhoneNumber= "1234567890679",
                        Name= "name679",
                        Password = "password679",
                        Pets = new List<PetEntity>(),
                        UserRole = new UserRoleEntity
                                    {
                                        Id = 9679,
                                        Name = "9679"
                                    }
                    },
                    UserId = 11679,
                    IsDeleted = false
                }
            };
            int sitterId = 11679;
            int sitterWorkId = 10679;
            DateTime startDateOrder = new DateTime(2023, 04, 17, 12, 00, 00);

            yield return new object[] { petsId, allPets, newOrder, sitterId, startDateOrder, sitterWorkId };
        }

        public static IEnumerable AddOrder_WhenSitterHasNotTimingToOrder_ShouldArgumentException_TestCaseSource()
        {
            //1. Внутри Ордера передан корректный пет, но дата заказа(это пн) вообще не входит в промежуток времени расписания ситтера(он работает в пт)

            List<int> petsId = new List<int> { 16798 };
            OrderCreateRequest newOrder = new OrderCreateRequest
            {
                Comment = "comment6798",
                SitterWorkId = 106798,
                Summ = 1006798,
                DateStart = new DateTime(2023, 04, 03, 13, 00, 00),
                DateEnd = new DateTime(2023, 04, 03, 14, 00, 00),
                LocationId = 10006798,
                Pets = petsId
            };
            List<PetEntity> allPets = new List<PetEntity>
            {
            new PetEntity
                {
                    Id =16798,
                    Name = "name6798",
                    Characteristics = "height6798",
                    Type = new AnimalTypeEntity
                                {
                                    Id =306798,
                                    Name= "nameType6798",
                                    Parameters = "param6798",
                                    IsDeleted = false
                                },
                    TypeId = 306798,
                    User = new UserEntity
                    {
                        Id = 116798,
                        Email = "email6798",
                        PhoneNumber= "12345678906798",
                        Name= "name6798",
                        Password = "password6798",
                        Pets = new List<PetEntity>(),
                        UserRole = new UserRoleEntity
                                    {
                                        Id = 96798,
                                        Name = "96798"
                                    }
                    },
                    UserId = 116798,
                    IsDeleted = false
                }
            };
            int sitterId = 116798;
            int sitterWorkId = 106798;
            DateTime startDateOrder = new DateTime(2023, 04, 03, 13, 00, 00);
            SitterWorkEntity sitterWork = new SitterWorkEntity
            {
                Id = 10458,
                Comment = "comment458",
                User = new UserEntity
                {
                    Id = 116798,
                    Email = "email6798",
                    PhoneNumber = "12345678906798",
                    Name = "name6798",
                    Password = "password6798",
                    Pets = new List<PetEntity>(),
                    UserRole = new UserRoleEntity
                    {
                        Id = 96798,
                        Name = "96798"
                    }
                },
                UserId = 116798,
                WorkType = new WorkTypeEntity
                {
                    Id = 101458,
                    Name = "type101458",
                    IsDeleted = false
                },
                WorkTypeId = 101458,
                IsDeleted = false,
            };
            List<SitterWorkEntity> allSitterWorks = new List<SitterWorkEntity>
            {
                new SitterWorkEntity
                {
                LocationsWork = new List<LocationWorkEntity>
                    {
                      new LocationWorkEntity
                      {
                       LocationId = 10006798
                      }
                    }
                }
            };
            allSitterWorks[0].LocationsWork.First().TimingLocationWorks.Add(
                                     new TimingLocationWorkEntity
                                     {
                                         Id = 11458,
                                         Start = new TimeSpan(12, 30, 00),
                                         Stop = new TimeSpan(16, 00, 00),
                                         DayOfWeek = new DayOfWeekEntity
                                         {
                                             Id = 5,
                                             Name = "Friday"
                                         },
                                         DayOfWeekId = 5,
                                         LocationWork = new LocationWorkEntity
                                         {
                                             Id = 10006798
                                         },
                                         LocationWorkId = 10006798
                                     });

            yield return new object[] { petsId, allPets, newOrder, sitterId, startDateOrder, sitterWorkId, sitterWork, allSitterWorks };

            //2. Внутри Ордера передан корректный пет, но дата заказа(пт) только частично входит в промежуток времени расписания ситтера(он работает в пт)

            petsId = new List<int> { 167987 };
            newOrder = new OrderCreateRequest
            {
                Comment = "comment67987",
                SitterWorkId = 1067987,
                Summ = 10067987,
                DateStart = new DateTime(2023, 04, 07, 18, 00, 00),
                DateEnd = new DateTime(2023, 04, 07, 19, 00, 00),
                LocationId = 100067987,
                Pets = petsId
            };
            allPets = new List<PetEntity>
            {
            new PetEntity
                {
                    Id =167987,
                    Name = "name67987",
                    Characteristics = "height67987",
                    Type = new AnimalTypeEntity
                                {
                                    Id =3067987,
                                    Name= "nameType67987",
                                    Parameters = "param67987",
                                    IsDeleted = false
                                },
                    TypeId = 3067987,
                    User = new UserEntity
                    {
                        Id = 1167987,
                        Email = "email67987",
                        PhoneNumber= "123456789067987",
                        Name= "name67987",
                        Password = "password67987",
                        Pets = new List<PetEntity>(),
                        UserRole = new UserRoleEntity
                                    {
                                        Id = 967987,
                                        Name = "967987"
                                    }
                    },
                    UserId = 1167987,
                    IsDeleted = false
                }
            };
            sitterId = 1167987;
            sitterWorkId = 1067987;
            startDateOrder = new DateTime(2023, 04, 07, 18, 00, 00);
            sitterWork = new SitterWorkEntity
            {
                Id = 104587,
                Comment = "comment4587",
                User = new UserEntity
                {
                    Id = 1167987,
                    Email = "email67987",
                    PhoneNumber = "123456789067987",
                    Name = "name67987",
                    Password = "password67987",
                    Pets = new List<PetEntity>(),
                    UserRole = new UserRoleEntity
                    {
                        Id = 967987,
                        Name = "967987"
                    }
                },
                UserId = 1167987,
                WorkType = new WorkTypeEntity
                {
                    Id = 1014587,
                    Name = "type1014587",
                    IsDeleted = false
                },
                WorkTypeId = 1014587,
                IsDeleted = false,
            };
            allSitterWorks = new List<SitterWorkEntity>
            {
                new SitterWorkEntity
                {
                LocationsWork = new List<LocationWorkEntity>
                    {
                      new LocationWorkEntity
                      {
                       LocationId = 100067987
                      }
                    }
                }
            };
            allSitterWorks[0].LocationsWork.First().TimingLocationWorks.Add(
                                     new TimingLocationWorkEntity
                                     {
                                         Id = 114587,
                                         Start = new TimeSpan(19, 00, 00),
                                         Stop = new TimeSpan(21, 30, 00),
                                         DayOfWeek = new DayOfWeekEntity
                                         {
                                             Id = 57,
                                             Name = "Friday"
                                         },
                                         DayOfWeekId = 57,
                                         LocationWork = new LocationWorkEntity
                                         {
                                             Id = 100067987
                                         },
                                         LocationWorkId = 100067987
                                     });

            yield return new object[] { petsId, allPets, newOrder, sitterId, startDateOrder, sitterWorkId, sitterWork, allSitterWorks };

            //3. Внутри Ордера передан корректный пет, но дата заказа(пт) входит в промежуток времени расписания ситтера(он работает в пт), но другая локация

            petsId = new List<int> { 1679876 };
            newOrder = new OrderCreateRequest
            {
                Comment = "comment679876",
                SitterWorkId = 10679876,
                Summ = 100679876,
                DateStart = new DateTime(2023, 04, 07, 17, 00, 00),
                DateEnd = new DateTime(2023, 04, 07, 18, 00, 00),
                LocationId = 6,
                Pets = petsId
            };
            allPets = new List<PetEntity>
            {
            new PetEntity
                {
                    Id =1679876,
                    Name = "name679876",
                    Characteristics = "height679876",
                    Type = new AnimalTypeEntity
                                {
                                    Id =30679876,
                                    Name= "nameType679876",
                                    Parameters = "param679876",
                                    IsDeleted = false
                                },
                    TypeId = 30679876,
                    User = new UserEntity
                    {
                        Id = 11679876,
                        Email = "email679876",
                        PhoneNumber= "1234567890679876",
                        Name= "name679876",
                        Password = "password679876",
                        Pets = new List<PetEntity>(),
                        UserRole = new UserRoleEntity
                                    {
                                        Id = 9679876,
                                        Name = "9679876"
                                    }
                    },
                    UserId = 11679876,
                    IsDeleted = false
                }
            };
            sitterId = 11679876;
            sitterWorkId = 10679876;
            startDateOrder = new DateTime(2023, 04, 07, 17, 00, 00);
            sitterWork = new SitterWorkEntity
            {
                Id = 1045876,
                Comment = "comment45876",
                User = new UserEntity
                {
                    Id = 11679876,
                    Email = "email679876",
                    PhoneNumber = "1234567890679876",
                    Name = "name679876",
                    Password = "password679876",
                    Pets = new List<PetEntity>(),
                    UserRole = new UserRoleEntity
                    {
                        Id = 9679876,
                        Name = "9679876"
                    }
                },
                UserId = 11679876,
                WorkType = new WorkTypeEntity
                {
                    Id = 10145876,
                    Name = "type10145876",
                    IsDeleted = false
                },
                WorkTypeId = 10145876,
                IsDeleted = false,
            };
            allSitterWorks = new List<SitterWorkEntity>
            {
                new SitterWorkEntity
                {
                LocationsWork = new List<LocationWorkEntity>
                    {
                      new LocationWorkEntity
                      {
                       LocationId = 1000679876
                      }
                    }
                }
            };
            allSitterWorks[0].LocationsWork.First().TimingLocationWorks.Add(
                                     new TimingLocationWorkEntity
                                     {
                                         Id = 1145876,
                                         Start = new TimeSpan(13, 00, 00),
                                         Stop = new TimeSpan(21, 00, 00),
                                         DayOfWeek = new DayOfWeekEntity
                                         {
                                             Id = 576,
                                             Name = "Friday"
                                         },
                                         DayOfWeekId = 576,
                                         LocationWork = new LocationWorkEntity
                                         {
                                             Id = 1000679876
                                         },
                                         LocationWorkId = 1000679876
                                     });

            yield return new object[] { petsId, allPets, newOrder, sitterId, startDateOrder, sitterWorkId, sitterWork, allSitterWorks };
        }

        public static IEnumerable AddOrder_WhenSitterIsNotFreeToNewOrder_ShouldArgumentException_TestCaseSource()
        {
            //1. Внутри Ордера передан корректный пет, дата заказа попадает внутрь внутрь расписания, но на часть времени пересекается с уже существующим заказом

            List<int> petsId = new List<int> { 167985 };
            OrderCreateRequest newOrder = new OrderCreateRequest
            {
                Comment = "comment67985",
                SitterWorkId = 1067985,
                Summ = 10067985,
                DateStart = new DateTime(2023, 04, 03, 12, 00, 00),
                DateEnd = new DateTime(2023, 04, 03, 13, 00, 00),
                LocationId = 100067985,
                Pets = petsId
            };
            List<PetEntity> allPets = new List<PetEntity>
            {
            new PetEntity
                {
                    Id =167985,
                    Name = "name67985",
                    Characteristics = "height67985",
                    Type = new AnimalTypeEntity
                                {
                                    Id =3067985,
                                    Name= "nameType67985",
                                    Parameters = "param67985",
                                    IsDeleted = false
                                },
                    TypeId = 3067985,
                    User = new UserEntity
                    {
                        Id = 1167985,
                        Email = "email67985",
                        PhoneNumber= "123456789067985",
                        Name= "name67985",
                        Password = "password67985",
                        Pets = new List<PetEntity>(),
                        UserRole = new UserRoleEntity
                                    {
                                        Id = 967985,
                                        Name = "967985"
                                    }
                    },
                    UserId = 1167985,
                    IsDeleted = false
                }
            };
            int sitterId = 1167985;
            int sitterWorkId = 1067985;
            DateTime startDateOrder = new DateTime(2023, 04, 03, 12, 00, 00);
            SitterWorkEntity sitterWork = new SitterWorkEntity
            {
                Id = 104585,
                Comment = "comment4585",
                User = new UserEntity
                {
                    Id = 1167985,
                    Email = "email67985",
                    PhoneNumber = "123456789067985",
                    Name = "name67985",
                    Password = "password67985",
                    Pets = new List<PetEntity>(),
                    UserRole = new UserRoleEntity
                    {
                        Id = 967985,
                        Name = "967985"
                    }
                },
                UserId = 1167985,
                WorkType = new WorkTypeEntity
                {
                    Id = 1014585,
                    Name = "type1014585",
                    IsDeleted = false
                },
                WorkTypeId = 1014585,
                IsDeleted = false,
            };
            List<SitterWorkEntity> allSitterWorks = new List<SitterWorkEntity>
            {
                new SitterWorkEntity
                {
                LocationsWork = new List<LocationWorkEntity>
                    {
                      new LocationWorkEntity
                      {
                       LocationId = 100067985
                      }
                    }
                }
            };
            allSitterWorks[0].LocationsWork.First().TimingLocationWorks.Add(
                                     new TimingLocationWorkEntity
                                     {
                                         Id = 114585,
                                         Start = new TimeSpan(10, 30, 00),
                                         Stop = new TimeSpan(20, 00, 00),
                                         DayOfWeek = new DayOfWeekEntity
                                         {
                                             Id = 55,
                                             Name = "Monday"
                                         },
                                         DayOfWeekId = 55,
                                         LocationWork = new LocationWorkEntity
                                         {
                                             Id = 100067985
                                         },
                                         LocationWorkId = 100067985
                                     });
            List<OrderEntity> allOrdersBySitter = new List<OrderEntity>
            {
                new OrderEntity
                {
                    Id = 1405,
                Comment = "comment405",
                OrderStatusId = 4,
                SitterWorkId = 10405,
                Summ = 100405,
                DateStart = new DateTime(2023, 04, 03, 12, 50, 00),
                DateEnd = new DateTime(2023, 04, 03, 14, 00, 00),
                LocationId = 100067985,

                    OrderStatus = new OrderStatusEntity
                    {
                        Id = 4,
                        Name = "at work",
                    },
                    SitterWork = new SitterWorkEntity
                    {
                        Id = 100067985
                    },
                    Location = new LocationEntity
                    {
                        Id = 100067985
                    }
                }
            };

            yield return new object[] { petsId, allPets, newOrder, sitterId, startDateOrder, sitterWorkId, sitterWork, allSitterWorks, allOrdersBySitter };

            //2. Внутри Ордера передан корректный пет, дата заказа попадает внутрь внутрь расписания, новый заказ не пересекается по времени с существующим, но между ними разница меньше 30 мин.

            petsId = new List<int> { 1679854 };
            newOrder = new OrderCreateRequest
            {
                Comment = "comment679854",
                SitterWorkId = 10679854,
                Summ = 100679854,
                DateStart = new DateTime(2023, 04, 08, 14, 00, 00),
                DateEnd = new DateTime(2023, 04, 08, 15, 00, 00),
                LocationId = 1000679854,
                Pets = petsId
            };
            allPets = new List<PetEntity>
            {
            new PetEntity
                {
                    Id =1679854,
                    Name = "name679854",
                    Characteristics = "height679854",
                    Type = new AnimalTypeEntity
                                {
                                    Id =30679854,
                                    Name= "nameType679854",
                                    Parameters = "param679854",
                                    IsDeleted = false
                                },
                    TypeId = 30679854,
                    User = new UserEntity
                    {
                        Id = 11679854,
                        Email = "email679854",
                        PhoneNumber= "1234567890679854",
                        Name= "name679854",
                        Password = "password679854",
                        Pets = new List<PetEntity>(),
                        UserRole = new UserRoleEntity
                                    {
                                        Id = 9679854,
                                        Name = "9679854"
                                    }
                    },
                    UserId = 11679854,
                    IsDeleted = false
                }
            };
            sitterId = 11679854;
            sitterWorkId = 10679854;
            startDateOrder = new DateTime(2023, 04, 08, 14, 00, 00);
            sitterWork = new SitterWorkEntity
            {
                Id = 1045854,
                Comment = "comment45854",
                User = new UserEntity
                {
                    Id = 11679854,
                    Email = "email679854",
                    PhoneNumber = "1234567890679854",
                    Name = "name679854",
                    Password = "password679854",
                    Pets = new List<PetEntity>(),
                    UserRole = new UserRoleEntity
                    {
                        Id = 9679854,
                        Name = "9679854"
                    }
                },
                UserId = 11679854,
                WorkType = new WorkTypeEntity
                {
                    Id = 10145854,
                    Name = "type10145854",
                    IsDeleted = false
                },
                WorkTypeId = 10145854,
                IsDeleted = false,
            };
            allSitterWorks = new List<SitterWorkEntity>
            {
                new SitterWorkEntity
                {
                LocationsWork = new List<LocationWorkEntity>
                    {
                      new LocationWorkEntity
                      {
                       LocationId = 1000679854
                      }
                    }
                }
            };
            allSitterWorks[0].LocationsWork.First().TimingLocationWorks.Add(
                                     new TimingLocationWorkEntity
                                     {
                                         Id = 1145854,
                                         Start = new TimeSpan(11, 30, 00),
                                         Stop = new TimeSpan(20, 30, 00),
                                         DayOfWeek = new DayOfWeekEntity
                                         {
                                             Id = 554,
                                             Name = "Saturday"
                                         },
                                         DayOfWeekId = 554,
                                         LocationWork = new LocationWorkEntity
                                         {
                                             Id = 1000679854
                                         },
                                         LocationWorkId = 1000679854
                                     });
            allOrdersBySitter = new List<OrderEntity>
            {
                new OrderEntity
                {
                    Id = 14054,
                Comment = "comment4054",
                OrderStatusId = 4,
                SitterWorkId = 104054,
                Summ = 1004054,
                DateStart = new DateTime(2023, 04, 08, 15, 20, 00),
                DateEnd = new DateTime(2023, 04, 08, 17, 00, 00),
                LocationId = 1000679854,

                    OrderStatus = new OrderStatusEntity
                    {
                        Id = 4,
                        Name = "at work",
                    },
                    SitterWork = new SitterWorkEntity
                    {
                        Id = 1000679854
                    },
                    Location = new LocationEntity
                    {
                        Id = 1000679854
                    }
                }
            };

            yield return new object[] { petsId, allPets, newOrder, sitterId, startDateOrder, sitterWorkId, sitterWork, allSitterWorks, allOrdersBySitter };

            //2. Внутри Ордера передан корректный пет, дата заказа попадает внутрь внутрь расписания. Добавляется заказ на два дня (вечер сб-ночь вс), но на вс по времени частично пересекатся(лаг в полчаса не соблюден) с уже существующим заказом

            petsId = new List<int> { 16798543 };
            newOrder = new OrderCreateRequest
            {
                Comment = "comment6798543",
                SitterWorkId = 43,
                Summ = 1006798543,
                DateStart = new DateTime(2023, 04, 15, 23, 00, 00),
                DateEnd = new DateTime(2023, 04, 16, 01, 00, 00),
                LocationId = 798543,
                Pets = petsId
            };
            allPets = new List<PetEntity>
            {
            new PetEntity
                {
                    Id =16798543,
                    Name = "name6798543",
                    Characteristics = "height6798543",
                    Type = new AnimalTypeEntity
                                {
                                    Id =306798543,
                                    Name= "nameType6798543",
                                    Parameters = "param6798543",
                                    IsDeleted = false
                                },
                    TypeId = 306798543,
                    User = new UserEntity
                    {
                        Id = 116798543,
                        Email = "email6798543",
                        PhoneNumber= "12345678906798543",
                        Name= "name6798543",
                        Password = "password6798543",
                        Pets = new List<PetEntity>(),
                        UserRole = new UserRoleEntity
                                    {
                                        Id = 96798543,
                                        Name = "96798543"
                                    }
                    },
                    UserId = 116798543,
                    IsDeleted = false
                }
            };
            sitterId = 116798543;
            sitterWorkId = 43;
            startDateOrder = new DateTime(2023, 04, 15, 23, 00, 00);
            sitterWork = new SitterWorkEntity
            {
                Id = 43,
                Comment = "comment458543",
                User = new UserEntity
                {
                    Id = 116798543,
                    Email = "email6798543",
                    PhoneNumber = "12345678906798543",
                    Name = "name6798543",
                    Password = "password6798543",
                    Pets = new List<PetEntity>(),
                    UserRole = new UserRoleEntity
                    {
                        Id = 96798543,
                        Name = "96798543"
                    }
                },
                UserId = 116798543,
                WorkType = new WorkTypeEntity
                {
                    Id = 101458543,
                    Name = "type101458543",
                    IsDeleted = false
                },
                WorkTypeId = 101458543,
                IsDeleted = false,
            };
            allSitterWorks = new List<SitterWorkEntity>
            {
                new SitterWorkEntity
                {
                LocationsWork = new List<LocationWorkEntity>
                    {
                      new LocationWorkEntity
                      {
                       LocationId = 798543
                      }
                    }
                }
            };
            allSitterWorks[0].LocationsWork.First().TimingLocationWorks.Add(
                                     new TimingLocationWorkEntity
                                     {
                                         Id = 11458543,
                                         Start = new TimeSpan(20, 30, 00),
                                         Stop = new TimeSpan(23, 59, 59),
                                         DayOfWeek = new DayOfWeekEntity
                                         {
                                             Id = 5543,
                                             Name = "Saturday"
                                         },
                                         DayOfWeekId = 5543,
                                         LocationWork = new LocationWorkEntity
                                         {
                                             Id = 543
                                         },
                                         LocationWorkId = 543
                                     });
            allSitterWorks[0].LocationsWork.First().TimingLocationWorks.Add(
                                    new TimingLocationWorkEntity
                                    {
                                        Id = 114585432,
                                        Start = new TimeSpan(00, 00, 00),
                                        Stop = new TimeSpan(05, 00, 00),
                                        DayOfWeek = new DayOfWeekEntity
                                        {
                                            Id = 55432,
                                            Name = "Sunday"
                                        },
                                        DayOfWeekId = 55432,
                                        LocationWork = new LocationWorkEntity
                                        {
                                            Id = 543
                                        },
                                        LocationWorkId = 543
                                    });
            allOrdersBySitter = new List<OrderEntity>
            {
                new OrderEntity
                {
                    Id = 140543,
                Comment = "comment40543",
                OrderStatusId = 4,
                SitterWorkId = 43,
                Summ = 10040543,
                DateStart = new DateTime(2023, 04, 16, 01, 29, 00),
                DateEnd = new DateTime(2023, 04, 16, 03, 00, 00),
                LocationId = 798543,

                    OrderStatus = new OrderStatusEntity
                    {
                        Id = 4,
                        Name = "at work",
                    },
                    SitterWork = new SitterWorkEntity
                    {
                        Id = 43
                    },
                    Location = new LocationEntity
                    {
                        Id = 798543
                    }
                }
            };

            yield return new object[] { petsId, allPets, newOrder, sitterId, startDateOrder, sitterWorkId, sitterWork, allSitterWorks, allOrdersBySitter };
        }

        public static IEnumerable ChangeOrderStatusTestCaseSource()
        {
            //1. Случай, когда меняем с "Отклонено" в "Работе" - первый case

            int orderId = 12;
            OrderEntity orderEntity = new OrderEntity
            {
                Id = 12,
                Comment = "comment12",
                OrderStatusId = 6,
                SitterWorkId = 12,
                Summ = 1002,
                DateStart = new DateTime(2023, 04, 17, 12, 20, 00),
                DateEnd = new DateTime(2023, 04, 17, 13, 20, 00),
                LocationId = 12,

                OrderStatus = new OrderStatusEntity
                {
                    Id = 6,
                    Name = "rejected",
                },
                SitterWork = new SitterWorkEntity
                {
                    Id = 12
                },
                Location = new LocationEntity
                {
                    Id = 12
                }
            };
            orderEntity.Pets.AddRange(new List<PetEntity>());
            int orderStatusId = 4;
            OrderStatusEntity orderStatus = new OrderStatusEntity
            {
                Id = 4,
                Name = "at work",
                Comment = "at work",
                IsDeleted = false
            };
            OrderEntity updateOrderEntity = new OrderEntity
            {
                Id = 12,
                Comment = "comment12",
                OrderStatusId = 4,
                SitterWorkId = 12,
                Summ = 1002,
                DateStart = new DateTime(2023, 04, 17, 12, 20, 00),
                DateEnd = new DateTime(2023, 04, 17, 13, 20, 00),
                LocationId = 1,

                OrderStatus = new OrderStatusEntity
                {
                    Id = 4,
                    Name = "at work",
                },
                SitterWork = new SitterWorkEntity
                {
                    Id = 12
                },
                Location = new LocationEntity
                {
                    Id = 12
                }
            };
            updateOrderEntity.Pets.AddRange(new List<PetEntity>());
            OrderResponse expected = new OrderResponse
            {
                Id = 12,
                Comment = "comment12",
                Summ = 1002,
                DateStart = new DateTime(2023, 04, 17, 12, 20, 00),
                DateEnd = new DateTime(2023, 04, 17, 13, 20, 00),
                OrderStatus = new OrderStatusResponse
                {
                    Id = 4,
                    Name = "at work",
                },
                SitterWork = new SitterWorkResponse
                {
                    Id = 12
                },
                Location = new LocationResponse
                {
                    Id = 12
                },
                Pets = new List<PetResponse>()
            };

            yield return new object[] { orderId, orderEntity, orderStatusId, orderStatus, updateOrderEntity, expected };

            //2. Случай, когда меняем с "На рассмотрении" в "Работе" - первый case

            orderId = 1;
            orderEntity = new OrderEntity
            {
                Id = 1,
                Comment = "comment1",
                OrderStatusId = 3,
                SitterWorkId = 1,
                Summ = 100,
                DateStart = new DateTime(2023, 04, 17, 12, 00, 00),
                DateEnd = new DateTime(2023, 04, 17, 13, 00, 00),
                LocationId = 1,

                OrderStatus = new OrderStatusEntity
                {
                    Id = 3,
                    Name = "under consideration",
                },
                SitterWork = new SitterWorkEntity
                {
                    Id = 1
                },
                Location = new LocationEntity
                {
                    Id = 1
                }
            };
            orderEntity.Pets.AddRange(new List<PetEntity>());
            orderStatusId = 4;
            orderStatus = new OrderStatusEntity
            {
                Id = 4,
                Name = "at work",
                Comment = "at work",
                IsDeleted = false
            };
            updateOrderEntity = new OrderEntity
            {
                Id = 1,
                Comment = "comment1",
                OrderStatusId = 4,
                SitterWorkId = 1,
                Summ = 100,
                DateStart = new DateTime(2023, 04, 17, 12, 00, 00),
                DateEnd = new DateTime(2023, 04, 17, 13, 00, 00),
                LocationId = 1,
                OrderStatus = new OrderStatusEntity
                {
                    Id = 4,
                    Name = "at work",
                },
                SitterWork = new SitterWorkEntity
                {
                    Id = 1
                },
                Location = new LocationEntity
                {
                    Id = 1
                }
            };
            updateOrderEntity.Pets.AddRange(new List<PetEntity>());
            expected = new OrderResponse
            {
                Id = 1,
                Comment = "comment1",
                Summ = 100,
                DateStart = new DateTime(2023, 04, 17, 12, 00, 00),
                DateEnd = new DateTime(2023, 04, 17, 13, 00, 00),
                OrderStatus = new OrderStatusResponse
                {
                    Id = 4,
                    Name = "at work",
                },
                SitterWork = new SitterWorkResponse
                {
                    Id = 1
                },
                Location = new LocationResponse
                {
                    Id = 1
                },
                Pets = new List<PetResponse>()
            };

            yield return new object[] { orderId, orderEntity, orderStatusId, orderStatus, updateOrderEntity, expected };

            //3. Случай, когда меняем с "Работе" на "Завершено" - второй case

            orderId = 13;
            orderEntity = new OrderEntity
            {
                Id = 13,
                Comment = "comment13",
                OrderStatusId = 4,
                SitterWorkId = 13,
                Summ = 1003,
                DateStart = new DateTime(2023, 04, 17, 12, 00, 30),
                DateEnd = new DateTime(2023, 04, 17, 13, 00, 30),
                LocationId = 3,

                OrderStatus = new OrderStatusEntity
                {
                    Id = 4,
                    Name = "at work",
                },
                SitterWork = new SitterWorkEntity
                {
                    Id = 13
                },
                Location = new LocationEntity
                {
                    Id = 13
                }
            };
            orderEntity.Pets.AddRange(new List<PetEntity>());
            orderStatusId = 5;
            orderStatus = new OrderStatusEntity
            {
                Id = 5,
                Name = "completed",
                Comment = "completed",
                IsDeleted = false
            };
            updateOrderEntity = new OrderEntity
            {
                Id = 13,
                Comment = "comment13",
                OrderStatusId = 5,
                SitterWorkId = 13,
                Summ = 1003,
                DateStart = new DateTime(2023, 04, 17, 12, 00, 30),
                DateEnd = new DateTime(2023, 04, 17, 13, 00, 30),
                LocationId = 13,
                OrderStatus = new OrderStatusEntity
                {
                    Id = 5,
                    Name = "completed",
                },
                SitterWork = new SitterWorkEntity
                {
                    Id = 13
                },
                Location = new LocationEntity
                {
                    Id = 13
                }
            };
            updateOrderEntity.Pets.AddRange(new List<PetEntity>());
            expected = new OrderResponse
            {
                Id = 13,
                Comment = "comment13",
                Summ = 1003,
                DateStart = new DateTime(2023, 04, 17, 12, 00, 30),
                DateEnd = new DateTime(2023, 04, 17, 13, 00, 30),
                OrderStatus = new OrderStatusResponse
                {
                    Id = 5,
                    Name = "completed",
                },
                SitterWork = new SitterWorkResponse
                {
                    Id = 13
                },
                Location = new LocationResponse
                {
                    Id = 13
                },
                Pets = new List<PetResponse>()
            };

            yield return new object[] { orderId, orderEntity, orderStatusId, orderStatus, updateOrderEntity, expected };

            //4. Случай, когда меняем с "На рассмотрении" на "Отклонено" - третий case

            orderId = 134;
            orderEntity = new OrderEntity
            {
                Id = 134,
                Comment = "comment134",
                OrderStatusId = 3,
                SitterWorkId = 134,
                Summ = 10034,
                DateStart = new DateTime(2023, 04, 17, 12, 40, 30),
                DateEnd = new DateTime(2023, 04, 17, 13, 00, 40),
                LocationId = 34,

                OrderStatus = new OrderStatusEntity
                {
                    Id = 3,
                    Name = "under consideration",
                },
                SitterWork = new SitterWorkEntity
                {
                    Id = 34
                },
                Location = new LocationEntity
                {
                    Id = 34
                }
            };
            orderEntity.Pets.AddRange(new List<PetEntity>());
            orderStatusId = 6;
            orderStatus = new OrderStatusEntity
            {
                Id = 6,
                Name = "rejected",
                Comment = "rejected",
                IsDeleted = false
            };
            updateOrderEntity = new OrderEntity
            {
                Id = 134,
                Comment = "comment134",
                OrderStatusId = 6,
                SitterWorkId = 134,
                Summ = 10034,
                DateStart = new DateTime(2023, 04, 17, 12, 40, 30),
                DateEnd = new DateTime(2023, 04, 17, 13, 00, 40),
                LocationId = 134,
                OrderStatus = new OrderStatusEntity
                {
                    Id = 6,
                    Name = "rejected",
                },
                SitterWork = new SitterWorkEntity
                {
                    Id = 134
                },
                Location = new LocationEntity
                {
                    Id = 134
                }
            };
            updateOrderEntity.Pets.AddRange(new List<PetEntity>());
            expected = new OrderResponse
            {
                Id = 134,
                Comment = "comment134",
                Summ = 10034,
                DateStart = new DateTime(2023, 04, 17, 12, 40, 30),
                DateEnd = new DateTime(2023, 04, 17, 13, 00, 40),
                OrderStatus = new OrderStatusResponse
                {
                    Id = 6,
                    Name = "rejected",
                },
                SitterWork = new SitterWorkResponse
                {
                    Id = 134
                },
                Location = new LocationResponse
                {
                    Id = 134
                },
                Pets = new List<PetResponse>()
            };

            yield return new object[] { orderId, orderEntity, orderStatusId, orderStatus, updateOrderEntity, expected };
        }

        public static IEnumerable ChangeOrderStatus_WhenNewOrderStatusIsNotExist_ShouldBeArgumentException_TestCaseSource()
        {
            // Случай, когда в аргументы передали статус, которого нет в свитче

            int orderId = 125;
            OrderEntity orderEntity = new OrderEntity
            {
                Id = 125,
                Comment = "comment125",
                OrderStatusId = 65,
                SitterWorkId = 125,
                Summ = 10025,
                DateStart = new DateTime(2023, 04, 15, 12, 20, 00),
                DateEnd = new DateTime(2023, 04, 17, 15, 20, 00),
                LocationId = 125,

                OrderStatus = new OrderStatusEntity
                {
                    Id = 65,
                    Name = "in progress",
                },
                SitterWork = new SitterWorkEntity
                {
                    Id = 125
                },
                Location = new LocationEntity
                {
                    Id = 125
                }
            };
            orderEntity.Pets.AddRange(new List<PetEntity>());
            int orderStatusId = 45;
            OrderStatusEntity orderStatus = new OrderStatusEntity
            {
                Id = 45,
                Name = "done",
                Comment = "not exist status",
                IsDeleted = false
            };

            yield return new object[] { orderId, orderEntity, orderStatusId, orderStatus };
        }

        public static IEnumerable ChangeOrderStatus_WhenCanNotChangeToAtWork_ShouldBeArgumentException_TestCaseSource()
        {
            // Случай, когда меняем на статус "Работе" - первый case, но со статуса "Сделано"(из которого нельзя поменять на статус "В работе")

            int orderId = 126;
            OrderEntity orderEntity = new OrderEntity
            {
                Id = 126,
                Comment = "comment126",
                OrderStatusId = 66,
                SitterWorkId = 126,
                Summ = 10026,
                DateStart = new DateTime(2023, 04, 16, 12, 20, 00),
                DateEnd = new DateTime(2023, 04, 17, 16, 20, 00),
                LocationId = 126,

                OrderStatus = new OrderStatusEntity
                {
                    Id = 66,
                    Name = "done",
                },
                SitterWork = new SitterWorkEntity
                {
                    Id = 126
                },
                Location = new LocationEntity
                {
                    Id = 126
                }
            };
            orderEntity.Pets.AddRange(new List<PetEntity>());
            int orderStatusId = 46;
            OrderStatusEntity orderStatus = new OrderStatusEntity
            {
                Id = 46,
                Name = "at work",
                Comment = "at work",
                IsDeleted = false
            };

            yield return new object[] { orderId, orderEntity, orderStatusId, orderStatus };
        }

        public static IEnumerable GetAllOrdersUnderConsiderationBySitterIdTestCaseSource()
        {
            //1. Случай, когда у ситтера один заказ на рассмотрении(только он и вернется), другой - завершен

            int userId = 1;
            UserEntity userEntity = new UserEntity
            {
                Id = 1,
                UserRoleId = 11,
                IsDeleted = false,
            };
            List<OrderEntity> allOrdersEntities = new List<OrderEntity>
            {
                new OrderEntity
                {
                    Id = 1,
                    OrderStatus = new OrderStatusEntity {Name = "under consideration"},
                    IsDeleted= false,
                },
                new OrderEntity
                {
                    Id = 2,
                    OrderStatus = new OrderStatusEntity {Name = "completed"},
                    IsDeleted= false,
                }
            };
            int userRoleId = 11;
            UserRoleEntity userRoleEntity = new UserRoleEntity
            {
                Id = 11,
                Name = "Sitter"
            };
            List<OrderResponse> expected = new List<OrderResponse>
            {
                new OrderResponse
                {
                    Id = 1,
                    OrderStatus = new OrderStatusResponse {Name = "under consideration"},
                    Pets = new List<PetResponse>()
                }
            };

            yield return new object[] { userId, userEntity, allOrdersEntities, userRoleId, userRoleEntity, expected };

            //2. Случай, когда у ситтера два заказа: один - в работе, другой - завершен; вернется пустой лист

            userId = 12;
            userEntity = new UserEntity
            {
                Id = 12,
                UserRoleId = 112,
                IsDeleted = false,
            };
            allOrdersEntities = new List<OrderEntity>
            {
                new OrderEntity
                {
                    Id = 12,
                    OrderStatus = new OrderStatusEntity {Name = "at work"},
                    IsDeleted= false,
                },
                new OrderEntity
                {
                    Id = 22,
                    OrderStatus = new OrderStatusEntity {Name = "completed"},
                    IsDeleted= false,
                }
            };
            userRoleId = 112;
            userRoleEntity = new UserRoleEntity
            {
                Id = 112,
                Name = "Sitter"
            };
            expected = new List<OrderResponse>();

            yield return new object[] { userId, userEntity, allOrdersEntities, userRoleId, userRoleEntity, expected };
        }

        public static IEnumerable GetAllOrdersUnderConsiderationBySitterId_WhenUserIsNotExist_ShouldBeNotFoundException_TestCaseSource()
        {
            int userId = 12;
            int userRoleId = 3;

            yield return new object[] { userId, userRoleId };
        }

        public static IEnumerable GetAllOrdersUnderConsiderationBySitterId_WhenUserRoleIsNotSitter_ShouldBeArgumentException_TestCaseSource()
        {
            int userId = 14;
            UserEntity userEntity = new UserEntity
            {
                Id = 14,
                UserRoleId = 114,
                IsDeleted = false,
            };
            List<OrderEntity> allOrdersEntities = new List<OrderEntity>
            {
                new OrderEntity
                {
                    Id = 14,
                    OrderStatus = new OrderStatusEntity {Name = "under consideration"},
                    IsDeleted= false,
                }
            };
            int userRoleId = 114;
            UserRoleEntity userRoleEntity = new UserRoleEntity
            {
                Id = 114,
                Name = "Client"
            };

            yield return new object[] { userId, userEntity, allOrdersEntities, userRoleId, userRoleEntity };
        }
    }
}

