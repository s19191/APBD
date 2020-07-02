using System;

namespace AdvertApi.Exceptions
{
    public class NotOnTheSameStreetOrCityException : Exception
    {
        public NotOnTheSameStreetOrCityException(string message) : base(message)
        {
        }

        public NotOnTheSameStreetOrCityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NotOnTheSameStreetOrCityException()
        {
        }
    }
}