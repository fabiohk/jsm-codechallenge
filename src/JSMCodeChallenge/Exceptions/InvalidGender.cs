using System;

namespace JSMCodeChallenge.Exceptions
{
    public class InvalidGenderException : Exception
    {
        public InvalidGenderException() { }

        public InvalidGenderException(string message) : base(message) { }

        public InvalidGenderException(string message, Exception inner) : base(message, inner) { }
    }
}