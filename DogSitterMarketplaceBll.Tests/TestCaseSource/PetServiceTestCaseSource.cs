using DogSitterMarketplaceBll.Models.Pets.Request;
using DogSitterMarketplaceBll.Models.Pets.Response;
using DogSitterMarketplaceBll.Models.Users.Response;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Users;
using System.Collections;

namespace DogSitterMarketplaceBll.Tests.TestCaseSource
{
    public class PetServiceTestCaseSource
    {
        public static IEnumerable AddPetTestCaseSource()
        {
            PetEntity petEntity = new PetEntity
            {
                Name = "1",
                TypeId = 2,
                UserId = 3,
                Characteristics = "weight - 2",
                IsDeleted = false
            };
            PetEntity addPetEntity = new PetEntity
            {
                Id = 1,
                Name = "1",
                Type = new AnimalTypeEntity
                {
                    Id = 2,
                    Name = "2",
                    Parameters = "weight",
                    IsDeleted = false
                },
                TypeId = 2,
                User = new UserEntity
                {
                    Id = 3,
                    IsDeleted = false
                },
                UserId = 3,
                Characteristics = "weight - 2",
                IsDeleted = false
            };
            PetRequest addPet = new PetRequest
            {
                Name = "1",
                UserId = 3,
                AnimalTypeId = 2,
                Characteristics = "weight - 2"
            };
            PetResponse expected = new PetResponse
            {
                Id = 1,
                Name = "1",
                Characteristics = "weight - 2",
                Type = new AnimalTypeResponse
                {
                    Id = 2,
                    Name = "2",
                    Parameters = "weight"
                },
                User = new UserShortResponse
                {
                    Id = 3
                }
            };

            yield return new object[] { petEntity, addPetEntity, addPet, expected };
        }

        public static IEnumerable UpdatePetTestCaseSource()
        {
            PetEntity petEntity = new PetEntity
            {
                Id = 12,
                Name = "12UPD",
                TypeId = 221,
                UserId = 321,
                Characteristics = "weight - 22UPD",
                IsDeleted = false
            };
            PetEntity updatePetEntity = new PetEntity
            {
                Id = 12,
                Name = "12UPD",
                Type = new AnimalTypeEntity
                {
                    Id = 221,
                    Name = "22",
                    Parameters = "weight",
                    IsDeleted = false
                },
                TypeId = 221,
                User = new UserEntity
                {
                    Id = 321,
                    IsDeleted = false
                },
                UserId = 321,
                Characteristics = "weight - 22UPD",
                IsDeleted = false
            };
            int userId = 321;
            UserEntity userEntity = new UserEntity
            {
                Id = 321,
                IsDeleted = false
            };
            PetUpdate petUpdate = new PetUpdate
            {
                Id = 12,
                Name = "12UPD",
                AnimalTypeId = 221,
                UserId = 321,
                Characteristics = "weight - 22UPD",
            };
            PetResponse expected = new PetResponse
            {
                Id = 12,
                Name = "12UPD",

                Type = new AnimalTypeResponse
                {
                    Id = 221,
                    Name = "22",
                    Parameters = "weight"
                },
                User = new UserShortResponse
                {
                    Id = 321
                },
                Characteristics = "weight - 22UPD",
            };

            yield return new object[] { petEntity, updatePetEntity, userId, userEntity, petUpdate, expected };
        }
    }
}