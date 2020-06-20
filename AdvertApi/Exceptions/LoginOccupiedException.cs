using System;

namespace AdvertApi.Exceptions
{
    public class LoginOccupiedException : Exception
    {
        public LoginOccupiedException(string message) : base(message)
        {
        }

        public LoginOccupiedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public LoginOccupiedException()
        {
        }
    }
}