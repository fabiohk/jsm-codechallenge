using System;

namespace JSMCodeChallenge.Exceptions
{
    public class CannotDetermineRegionLocationException : Exception
    {
        public CannotDetermineRegionLocationException() { }

        public CannotDetermineRegionLocationException(string message) : base(message) { }

        public CannotDetermineRegionLocationException(string message, Exception inner) : base(message, inner) { }
    }
}