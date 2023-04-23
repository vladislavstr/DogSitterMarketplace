namespace DogSitterMarketplaceCore.Exceptions
{
    public class InvalidWriteTimeException : Exception
    {
        public string ExceptionValue { get; private set; }

        public InvalidWriteTimeException (string value ) : base ($"The time interval is written incorrectly: {value} ((")
        {
            ExceptionValue = value;
        }
    }
}
