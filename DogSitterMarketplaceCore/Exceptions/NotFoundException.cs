namespace DogSitterMarketplaceCore.Exceptions
{
    public class NotFoundException : Exception
    {
        public int Id { get; set; }

        public string EntityName { get; set; }

        public NotFoundException(int id, string entityName) : base($"{entityName} with id {id} not found.")
        {
            Id = id;
            EntityName = entityName;
        }
    }
}
