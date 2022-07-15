using System;
using System.Reflection;
using Cobilas.Collections;

namespace Cobilas.Unity.Utility {
    public static class UnityTypeUtility {
        public static Assembly[] GetAssemblies()
            => CobilasTypeUtility.GetAssemblies();

        public static bool TypeExist(string fullName)
            => CobilasTypeUtility.TypeExist(fullName);

        public static Type GetType(string fullName) {
            Type[] temp = GetAllTypes();
            for (int I = 0; I < ArrayManipulation.ArrayLength(temp); I++)
                if (temp[I].FullName == fullName)
                    return temp[I];
            return null;
        }

        public static Type[] GetAllTypes() {
            Type[] types = null;
            Assembly[] temp = GetAssemblies();
            for (int I = 0; I < ArrayManipulation.ArrayLength(temp); I++)
                ArrayManipulation.Add(temp[I].GetTypes(), ref types);
            return types;
        }
    }
}