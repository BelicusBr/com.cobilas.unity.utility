using System;
using UnityEngine;

namespace Cobilas.Unity.Utility {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class ReadOnlyVarAttribute : PropertyAttribute { }
}