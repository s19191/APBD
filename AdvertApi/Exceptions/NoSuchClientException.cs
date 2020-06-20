using System;

namespace AdvertApi.Exceptions
{
    public class NoSuchClientException : Exception
    {
        public NoSuchClientException(string message) : base(message)
        {
        }

        public NoSuchClientException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NoSuchClientException()
        {
        }
    }
}