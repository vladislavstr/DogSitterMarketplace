﻿using DogSitterMarketplaceDal.Models.Pets.PetEntity;

namespace DogSitterMarketplaceDal.Models.Users
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public UserPassportDataEntity? PassportData { get; set; }
        public UserRoleEntity Role { get; set; }
        public UserStatusEntity Status { get; set; }
        public ICollection<PetEntity>? Pets { get; set; }
    }
}
}
