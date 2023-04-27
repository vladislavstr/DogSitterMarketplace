namespace DogSitterMarketplaceCore
{
    public class OrderStatus
    {
        public const string UnderConsideration = "under consideration";

        public const string AtWork = "at work";

        public const string Completed = "completed";

        public const string Rejected = "rejected";
    }

    public class UserRole
    {
        public const string Admin = "Admin";

        public const string Client = "Client";

        public const string Sitter = "Sitter";
    }
}