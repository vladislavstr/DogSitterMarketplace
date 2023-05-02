namespace DogSitterMarketplaceApi.Models.UsersDto.Response
{
    public class AuthResponse
    {
        public string? Token { get; set; }

        public bool Success { get; set; }

        public IEnumerable<string> Error { get; set; }
    }
}
