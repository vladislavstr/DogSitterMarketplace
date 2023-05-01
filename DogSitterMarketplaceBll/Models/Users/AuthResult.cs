namespace DogSitterMarketplaceBll.Models.Users
{
    public class AuthResult
    {
        public string? Token { get; set; }

        public bool Success { get; set; }

        public IEnumerable<string> Error { get; set; }
    }
}
