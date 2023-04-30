namespace DogSitterMarketplaceBll.Models.Users.Request
{
    public class UserRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public int PassportData { get; set; }

        public int Role { get; set; }

        public int Status { get; set; }

        //public ICollection<PetRequest>? Pets { get; set; }
    }
}
