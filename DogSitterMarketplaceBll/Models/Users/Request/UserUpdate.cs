﻿using DogSitterMarketplaceBll.Models.Pets.Request;

namespace DogSitterMarketplaceBll.Models.Users.Request
{
    public class UserUpdate
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public UserPassportDataRequest PassportData { get; set; }

        public UserRoleRequest Role { get; set; }

        public UserStatusRequest? Status { get; set; }

        public ICollection<PetRequest>? Pets { get; set; }
    }
}
