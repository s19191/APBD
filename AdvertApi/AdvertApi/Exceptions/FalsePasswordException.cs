using System;

namespace AdvertApi.Exceptions
{
    public class FalsePasswordException : Exception
    {
        public FalsePasswordException(string message) : base(message)
        {
        }

        public FalsePasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FalsePasswordException()
        {
        }
    }
}