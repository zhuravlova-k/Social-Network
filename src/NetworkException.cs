using System;

namespace SocialTopology
{
    // власний тип помилки
    public class NetworkException : Exception
    {
        public NetworkException(string message) : base(message) { }
    }
}