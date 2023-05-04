namespace DogSitterMarketplaceCore.Exceptions
{
    public class ExcepsionOfWorkOnLocation : Exception
    {
        public string ValueDataString { get; set; }

        public ExcepsionOfWorkOnLocation(string value = null) : base($"Error working with location data {value} : ")
        {
            ValueDataString = value;
        }
    }
}
