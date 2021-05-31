using System;

namespace App.Exceptions
{
    [Serializable]
    internal class ExternalApiUnreachableException : Exception
    {
        public ExternalApiUnreachableException() : base("Cannot reach external api") { }
        public ExternalApiUnreachableException(string api) : base($"Cannot reach external api: {api}") { }
    }
}
