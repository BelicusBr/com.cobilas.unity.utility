using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEditor.Build;
using Cobilas.Unity.Utility;
using UnityEditor.Compilation;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;

namespace Cobilas.Unity.Editor.Utility {
    public static class ChangeVersion {
        //Mark revision after completion of compilation
        private const string tg_UpRevision = "Tools/Change version/Up revision update";
        private const string tg_DownRevision = "Tools/Change version/Down revision update";
        private const string tg_UpMajor = "Tools/Change version/Up major update";
        private const string tg_DownMajor = "Tools/Change version/Down major update";
        private const string tg_UpMinor = "Tools/Change version/Up minor update";
        private const string tg_DownMinor = "Tools/Change version/Down minor update";
        private const string tg_IsBeta = "Tools/Change version/Is beta";
        private const string tg_IsAlpha = "Tools/Change version/Is alpha";
        private const string tg_PrintVersion = "Tools/Change version/Print version";
        private const string tg_PrintBuildCount = "Tools/Change version/Print build count";
        private const string tg_mracoc = "Tools/Change version/Mark revision after completion of compilation";
        private static bool isBeta;
        private static bool MRACOC;
        private static bool isAlpha;
        private static List<ConfigItem> configs;
        private static string PersistentBuildCountFolder => CobilasPaths.Combine(CobilasPaths.ProjectFolderPath, "BuildCount");
        private static string PersistentBuildCountFile => CobilasPaths.Combine(PersistentBuildCountFolder, "MyCount.txt");
        private static string ChangeVersionFolder => CobilasPaths.Combine(CobilasPaths.ProjectFolderPath, "ChangeVersion");
        private static string ChangeVersionConfigFile => CobilasPaths.Combine(ChangeVersionFolder, "Config.txt");

        [InitializeOnLoadMethod]
        private static void Init() {
            configs = new List<ConfigItem>();
            CreateChangeVersionFolder();
            LoadConfigs();

            bool mracoc_Mark = EditorPrefs.GetBool("mracoc_Mark", false);

            EditorApplication.delayCall += () => {
                ActionIsBeta(isBeta);
                ActionMRACOC(MRACOC);
                ActionIsAlpha(isAlpha);
                EditorPrefs.SetBool("mracoc_Mark", mracoc_Mark);
            };

            EditorApplication.quitting += () => {
                mracoc_Mark = EditorPrefs.GetBool("mracoc_Mark", false);
                _ = GetVersion();
                if (!mracoc_Mark) return;
                ConfigItem config = GetConfigItem("Revision");
                int length;
                if (string.IsNullOrEmpty(config.name))
                    SetConfig("Revision", "1");
                else {
                    int.TryParse(config.value, out length);
                    SetConfig("Revision", (length + 1).ToString());
                }
            };

            CompilationPipeline.assemblyCompilationFinished += (s, l) => {
                int length = l == null ? 0 : l.Length;
                for (int I = 0; I < length; I++)
                    if (l[I].type == CompilerMessageType.Error)
                        return;
                if (MRACOC) {
                    ConfigItem config = GetConfigItem("Revision");
                    if (string.IsNullOrEmpty(config.name))
                        SetConfig("Revision", "1");
                    else {
                        int.TryParse(config.value, out length);
                        SetConfig("Revision", (length + 1).ToString());
                    }
                } else {
                    EditorPrefs.SetBool("mracoc_Mark", true);
                }
            };

            PostprocessBuild.EventOnPostprocessBuild += (br) => {
                if (br.summary.totalErrors != 0) return;
                int count;
                int.TryParse(GetConfigItem("Build").value, out count);
                SetConfig("Build", (count + 1).ToString());
            };
        }

        [MenuItem(tg_UpMajor, priority = 11)]
        private static void UpMajor()
            => MoveUpdate(true, true);

        [MenuItem(tg_DownMajor, priority = 11)]
        private static void DownMajor()
            => MoveUpdate(false, true);

        [MenuItem(tg_UpMinor, priority = 11)]
        private static void UpMinor()
            => MoveUpdate(true, false);

        [MenuItem(tg_DownMinor, priority = 11)]
        private static void DownMinor()
            => MoveUpdate(false, false);

        [MenuItem(tg_UpRevision, priority = 11)]
        private static void UpRevision()
            => MoveRevision(true);

        [MenuItem(tg_DownRevision, priority = 11)]
        private static void DownRevision()
            => MoveRevision(false);

        private static void MoveRevision(bool isUp) {
            int value;
            string name;
            ConfigItem config = GetConfigItem(name = "Revision");
            if (!MRACOC) EditorPrefs.SetBool("mracoc_Mark", false);
            if (string.IsNullOrEmpty(config.name)) {
                SetConfig(name, "1");
                return;
            }
            int.TryParse(config.value, out value);
            value = value + (isUp ? 1 : -1);
            value = value < 0 ? 0 : value;
            SetConfig(name, value.ToString());
        }

        private static void MoveUpdate(bool isUp, bool isMajor) {
            int value;
            string name;
            ConfigItem config = GetConfigItem(name = isMajor ? "Major" : "Minor");
            if (string.IsNullOrEmpty(config.name)) {
                SetConfig(name, "1");
                return;
            }
            int.TryParse(config.value, out value);
            value = value + (isUp ? 1 : -1);
            value = value < 0 ? 0 : value;
            SetConfig(name, value.ToString());
        }

        [MenuItem(tg_PrintBuildCount, priority = 22)]
        public static void PrintBuildCount()
            => Debug.Log(string.Format("Build count : {0}", GetConfigItem("Build").value));

        [MenuItem(tg_PrintVersion, priority = 22)]
        public static void PrintVersion()
            => Debug.Log(string.Format("Version : {0}", GetVersion()));

        private static string GetVersion() {
            string pfx = GetConfigItem("Prefix").value;
            pfx = pfx == "IsAlpha" ? "-alpha" : (pfx == "IsBeta" ? "-beta" : string.Empty);
            string bld = GetConfigItem("Build").value;
            bld = string.IsNullOrEmpty(bld) ? "0" : bld;
            string rvs = GetConfigItem("Revision").value;
            rvs = string.IsNullOrEmpty(rvs) ? "0" : rvs;
            string mjr = GetConfigItem("Major").value;
            mjr = string.IsNullOrEmpty(mjr) ? "0" : mjr;
            string mnr = GetConfigItem("Minor").value;
            mnr = string.IsNullOrEmpty(mnr) ? "0" : mnr;
            return PlayerSettings.bundleVersion = string.Format("{0}.{1}.{2}.{3}{4}", mjr, mnr, bld, rvs, pfx);
        }

        [MenuItem("Tools/Change version/Copy version", priority = 22)]
        private static void CopyVersion()
            => EditorGUIUtility.systemCopyBuffer = GetVersion();

        [MenuItem(tg_IsAlpha, priority = 0)]
        private static void ToggleIsAlpha()
            => ActionIsAlpha(!isAlpha);

        private static void ActionIsAlpha(bool _isAlpha) {
            Menu.SetChecked(tg_IsAlpha, _isAlpha);
            EditorPrefs.SetBool(tg_IsAlpha, _isAlpha);
            if (isAlpha = _isAlpha) {
                isBeta = false;
                Menu.SetChecked(tg_IsBeta, isBeta);
                EditorPrefs.SetBool(tg_IsBeta, isBeta);
                SetConfig("Prefix", "IsAlpha");
            } else SetConfig("Prefix", "Empty");
        }

        [MenuItem(tg_IsBeta, priority = 0)]
        private static void ToggleIsBeta()
            => ActionIsBeta(!isBeta);

        private static void ActionIsBeta(bool _isBeta) {
            Menu.SetChecked(tg_IsBeta, _isBeta);
            EditorPrefs.SetBool(tg_IsBeta, _isBeta);
            if (isBeta = _isBeta) {
                isAlpha = false;
                Menu.SetChecked(tg_IsAlpha, isAlpha);
                EditorPrefs.SetBool(tg_IsAlpha, isAlpha);
                SetConfig("Prefix", "IsBeta");
            } else SetConfig("Prefix", "Empty");
        }

        [MenuItem(tg_mracoc, priority = 0)]
        private static void ToggleMRACOC()
            => ActionMRACOC(!MRACOC);

        private static void ActionMRACOC(bool _MRACOC) {
            Menu.SetChecked(tg_mracoc, _MRACOC);
            EditorPrefs.SetBool(tg_mracoc, _MRACOC);
            SetConfig("MRACOC", _MRACOC ? "1" : "0");
        }

        private static void LoadConfigs() {
            char[] seps = new char[] { ':' };
            using (StreamReader reader = File.OpenText(ChangeVersionConfigFile))
                while (!reader.EndOfStream) {
                    string line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;
                    try {
                        string[] res = line.Split(seps, StringSplitOptions.RemoveEmptyEntries);
                        configs.Add(new ConfigItem(res[0].Trim(), res[1].Trim()));
                    } catch (Exception e) {
                        Debug.LogWarning(e);
                    }
                }
            ConfigItem config = GetConfigItem("Prefix");
            switch (config.value) {
                case "IsAlpha": ActionIsAlpha(true); break;
                case "IsBeta": ActionIsBeta(true); break;
                default:
                    ActionIsAlpha(false);
                    ActionIsBeta(false);
                    break;
            }
            config = GetConfigItem("MRACOC");
            MRACOC = config.value == "1";
        }

        private static void SetConfig(string name, string value) {
            ConfigItem config = GetConfigItem(name);
            if (string.IsNullOrEmpty(config.name))
                configs.Add(new ConfigItem(name, value));
            else config.value = value;
            UnloadConfigs();
        }

        private static void CreateChangeVersionFolder() {
            if (!Directory.Exists(ChangeVersionFolder))
                Directory.CreateDirectory(ChangeVersionFolder);
            
            if (!File.Exists(ChangeVersionConfigFile))
                using (StreamWriter writer = File.CreateText(ChangeVersionConfigFile)) {
                    if (File.Exists(PersistentBuildCountFile)) {
                        writer.Write("Build:{0}\r\n", File.ReadAllText(PersistentBuildCountFile));
                        Directory.Delete(PersistentBuildCountFolder, true);
                    } else writer.Write("Build:0\r\n");
                    writer.Write("Major:0\r\n");
                    writer.Write("Minor:0\r\n");
                    writer.Write("Revision:0\r\n");
                    writer.Write("Prefix:Empty\r\n");
                }
        }

        private static void UnloadConfigs() {
            StringBuilder builder = new StringBuilder();
            for (int I = 0; I < configs.Count; I++)
                builder.AppendFormat("{0}:{1}\r\n", configs[I].name, configs[I].value);
            File.WriteAllText(ChangeVersionConfigFile, builder.ToString());
        }

        private static ConfigItem GetConfigItem(string name) {
            for (int I = 0; I < configs.Count; I++)
                if (configs[I].name == name)
                    return configs[I];
            return new ConfigItem();
        }

        private sealed class ConfigItem {
            public string name;
            public string value;

            public ConfigItem() { }

            public ConfigItem(string name, string value) {
                this.name = name;
                this.value = value;
            }
        }

        private class PostprocessBuild : IPostprocessBuildWithReport {
            int IOrderedCallback.callbackOrder => 0;

            public static event Action<BuildReport> EventOnPostprocessBuild;

            void IPostprocessBuildWithReport.OnPostprocessBuild(BuildReport report)
                => EventOnPostprocessBuild?.Invoke(report);
        }
    }
}
