namespace DogSitterMarketplaceBll.Models.Users.Request
{
    public class UserRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        //public bool IsDeleted { get; set; }

        public int? UserPassportDataId { get; set; }

        public int UserRoleId { get; set; }

        public int UserStatusId { get; set; }

        //public ICollection<PetRequest>? Pets { get; set; }
    }
}
