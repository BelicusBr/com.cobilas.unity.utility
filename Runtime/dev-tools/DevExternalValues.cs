using System.IO;
using UnityEngine;
using Cobilas.Collections;

namespace Cobilas.Unity.Dev {

    public static class DevExternalValues {
        public static bool DevFileExists => File.Exists(FilePath);
        public static string FilePath => Path.Combine(Application.streamingAssetsPath, "dev_temp.txt");
#if COBILAS_UNITY_2019_OR_NEWER
        private static ExternalValue[] values;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
            => values = GetExternalValues();

        public static string GetValue(string name) {
            for (int I = 0; I < ArrayManipulation.ArrayLength(values); I++) {
#else
        public static string GetValue(string name) {
            ExternalValue[] values = GetExternalValues();
            for (int I = 0; I < ArrayManipulation.ArrayLength(values); I++) {
#endif
                if (name == values[I].name)
                    return values[I].value;
            }
            return null;
        }

        private static ExternalValue[] GetExternalValues() {
            ExternalValue[] res = null;
            if (!File.Exists(FilePath)) {
                MonoBehaviour.print($"{FilePath} file does not exist!");
            } else {
                using (StreamReader reader = new StreamReader(FilePath))
                    while (!reader.EndOfStream) {
                        string item = reader.ReadLine();
                        if (string.IsNullOrEmpty(item)) continue;
                        ExternalValue external = new ExternalValue();
                        external.name = item.Remove(item.IndexOf(':'));
                        external.value = item.Remove(0, item.IndexOf(':') + 1);
                        ArrayManipulation.Add(external, ref res);
                    }
            }
            return res;
        }

        private struct ExternalValue {
            public string name;
            public string value;
        }
    }
}
