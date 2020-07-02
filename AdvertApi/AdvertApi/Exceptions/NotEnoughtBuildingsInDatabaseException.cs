using System;

namespace AdvertApi.Exceptions
{
    public class NotEnoughtBuildingsInDatabaseException : Exception
    {
        public NotEnoughtBuildingsInDatabaseException(string message) : base(message)
        {
        }

        public NotEnoughtBuildingsInDatabaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NotEnoughtBuildingsInDatabaseException()
        {
        }
    }
}