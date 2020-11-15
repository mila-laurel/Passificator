using System;

namespace Passificator.Exceptions
{
    [Serializable]
    public class DocumentGeneratorException : Exception
    {
        public DocumentGeneratorException() : base() { }
        public DocumentGeneratorException(string message) : base(message) { }
        public DocumentGeneratorException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected DocumentGeneratorException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}