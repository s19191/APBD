using System;

namespace AdvertApi.Exceptions
{
    public class FalseRefreshTokenException : Exception
    {
        public FalseRefreshTokenException(string message) : base(message)
        {
        }

        public FalseRefreshTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FalseRefreshTokenException()
        {
        }
    }
}