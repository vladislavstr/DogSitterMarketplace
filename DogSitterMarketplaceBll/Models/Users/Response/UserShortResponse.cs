﻿namespace DogSitterMarketplaceBll.Models.Users.Response
{
    public class UserShortResponse
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public int RoleId { get; set; }
    }
}
