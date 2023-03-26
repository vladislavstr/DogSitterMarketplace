namespace DogSitterMarketplaceApp.Models.User
{
    public class Users
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public UsersPassportData PassportData { get; set; }
        public UsersRoles Role { get; set;}
        public UsersStatuses Status { get; set;}
    }
}
