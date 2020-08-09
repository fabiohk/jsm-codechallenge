using System;

namespace JSMCodeChallenge.Exceptions
{
    public class InvalidLocationStateException : Exception
    {
        public InvalidLocationStateException() { }

        public InvalidLocationStateException(string message) : base(message) { }

        public InvalidLocationStateException(string message, Exception inner) : base(message, inner) { }
    }
}