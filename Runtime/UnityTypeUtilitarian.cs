using System;
using System.Reflection;
using Cobilas.Collections;

namespace Cobilas.Unity.Utility {
    public static class UnityTypeUtility {
        public static Assembly[] GetAssemblies()
            => AppDomain.CurrentDomain.GetAssemblies();

        public static bool TypeExist(string fullName) {
            Assembly[] assemblies = GetAssemblies();
            for (int A = 0; A < ArrayManipulation.ArrayLength(assemblies); A++) {
                Type[] types = assemblies[A].GetTypes();
                for (int B = 0; B < ArrayManipulation.ArrayLength(types); B++)
                    if (types[B].Name == fullName)
                        return true;
            }
            return false;
        }

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