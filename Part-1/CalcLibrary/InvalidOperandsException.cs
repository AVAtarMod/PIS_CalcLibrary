using System;

namespace CalcLibrary
{

    [Serializable]
    public class InvalidOperandsException : ArgumentException
    {
        public InvalidOperandsException() { }
        public InvalidOperandsException(string message) : base(message) { }
        public InvalidOperandsException(string message, Exception inner) : base(message, inner) { }
        protected InvalidOperandsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
