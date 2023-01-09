using System;

namespace Cobilas.Unity.Utility {

    [Serializable]
    public class ComTaskException : Exception
    {
        public ComTaskException() { }
        public ComTaskException(string message) : base(message) { }
        public ComTaskException(string message, Exception inner) : base(message, inner) { }
        protected ComTaskException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public static ComTaskException StartException()
            => new ComTaskException($"The object {nameof(ComTask)} has already been or is being used!");

        public static ComTaskException StopException()
            => new ComTaskException($"The object {nameof(ComTask)} has not been started!");
    }
}
