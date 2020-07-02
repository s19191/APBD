using System;

namespace AdvertApi.Exceptions
{
    public class NoSuchBuildingExsistsException : Exception
    {
        public NoSuchBuildingExsistsException(string message) : base(message)
        {
        }

        public NoSuchBuildingExsistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NoSuchBuildingExsistsException()
        {
        }
    }
}