using System;

namespace Cobilas.Unity.Utility {
    [Obsolete("Use GenerateErrorNull()")]
    public static class PurposefulErrors {
        public static void GerarErroNulo()
            => GenerateErrorNull();

        public static void GenerateErrorNull()
            => GenerateError<NullReferenceException>();

        public static void GenerateError<T>() where T : Exception
            => throw (T)Activator.CreateInstance(typeof(T));
    }
}
