using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEditor.Build;
using Cobilas.Collections;
using Cobilas.Unity.Utility;
using UnityEditor.Compilation;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;

namespace Cobilas.Unity.Editor.Utility.ChangeVersion {
    public class ChangeVersionWin : EditorWindow {
        private Vector2 scrollView;
        private string[] nameFormats;
        private KeyValuePair<string, VersionInfo>[] formats;
        private const string txt_defaultFormat = "{0}";

        private static VersionInfo infoTemp;
        private static ChangeVersionConfig config;
        private static string ChangeVersionFolder => UnityPath.Combine(UnityPath.ProjectFolderPath, "ChangeVersion");
        private static string ChangeVersionConfigFile => UnityPath.Combine(ChangeVersionFolder, "VersionConfig.txt");
        private static string ChangeConfigFile => UnityPath.Combine(ChangeVersionFolder, "Config.txt");

        [MenuItem("Window/Change version")]
        static void Init() {
            ChangeVersionWin change = GetWindow<ChangeVersionWin>();
            change.titleContent = new GUIContent("Change version");
            change.Show();
        }

        private void OnEnable() {
            infoTemp = LoadVersion();
            config = LoadConfig();
            formats = GetFormats();
            nameFormats = new string[ArrayManipulation.ArrayLength(formats)];
            for (int I = 0; I < nameFormats.Length; I++)
                nameFormats[I] = formats[I].Key;
        }

        private void OnDestroy() {
            UnloadConfig(config);
            UnloadVersion(infoTemp);
        }

        private void OnGUI() {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar, GUILayout.ExpandWidth(true));
            if (GUILayout.Button(CV_WinText.bt_Save, EditorStyles.toolbarButton, GUILayout.Width(40f)))
                OnDestroy();
            if (GUILayout.Button(CV_WinText.bt_Add, EditorStyles.toolbarButton, GUILayout.Width(40f)))
                infoTemp.Add($"Item-{infoTemp.versions.Count}");
            if (GUILayout.Button(CV_WinText.bt_Clear, EditorStyles.toolbarButton, GUILayout.Width(40f)))
                infoTemp.versions.Clear();
            EditorGUI.BeginChangeCheck();
            config.isAlpha = GUILayout.Toggle(config.isAlpha, CV_WinText.ckb_IsAlpha, EditorStyles.toolbarButton, GUILayout.Width(60f));
            if (EditorGUI.EndChangeCheck() && config.isAlpha)
                config.isBeta = false;
            EditorGUI.BeginChangeCheck();
            config.isBeta = GUILayout.Toggle(config.isBeta, CV_WinText.ckb_IsBeta, EditorStyles.toolbarButton, GUILayout.Width(60f));
            if (EditorGUI.EndChangeCheck() && config.isBeta)
                config.isAlpha = false;

            EditorGUI.BeginChangeCheck();
            int ind = EditorGUILayout.Popup(0, nameFormats, EditorStyles.toolbarPopup, GUILayout.Width(130f));
            if (EditorGUI.EndChangeCheck() && nameFormats.Length != 0) {
                infoTemp = formats[ind].Value;
                OnDestroy();
            }
            EditorGUILayout.EndHorizontal();

            scrollView = EditorGUILayout.BeginScrollView(scrollView);
            for (int I = 0; I < infoTemp.versions.Count; I++) {
                VersionValue temp = infoTemp[I];
                EditorGUILayout.BeginHorizontal();
                EditorGUI.BeginDisabledGroup(!temp.Rename);
                temp.name = EditorGUILayout.TextField(temp.name, GUILayout.Width(150f));
                EditorGUI.EndDisabledGroup();
                temp.Rename = GUILayout.Toggle(temp.Rename, CV_WinText.ckb_Rename, EditorStyles.radioButton, GUILayout.Width(15f));

                if (temp.ChangeLevel)
                    temp.level = EditorGUILayout.LongField(temp.level, GUILayout.Width(100f));
                else {
                    string form = string.IsNullOrEmpty(temp.format) || temp.ChangeFormat ? txt_defaultFormat : temp.format;
                    try {
                        EditorGUILayout.LabelField(string.Format(form, temp.level), EditorStyles.textField, GUILayout.Width(100f));
                    } catch {
                        Debug.LogError($"invalid format{form}");
                        temp.format = (string)null;
                    }
                }

                if (GUILayout.Button(CV_WinText.bt_Up, GUILayout.Width(46f))) {
                    ++temp.level;
                    if (temp.updateWhenClose)
                        temp.AlreadyUpdatedWhenClose = true;
                }
                if (GUILayout.Button(CV_WinText.bt_Down, GUILayout.Width(46f))) {
                    --temp.level;
                    if (temp.updateWhenClose)
                        temp.AlreadyUpdatedWhenClose = true;
                }

                temp.level = temp.level < 0 ? 0 : temp.level;

                temp.ChangeLevel = GUILayout.Toggle(temp.ChangeLevel, CV_WinText.ckb_ChangeLevel, EditorStyles.radioButton, GUILayout.Width(15f));

                if (temp.ChangeFormat)
                    temp.format = EditorGUILayout.TextField(temp.format, GUILayout.Width(150f));

                temp.ChangeFormat = GUILayout.Toggle(temp.ChangeFormat, CV_WinText.ckb_ChangeFormat, EditorStyles.radioButton, GUILayout.Width(15f));
                temp.isBuild = GUILayout.Toggle(temp.isBuild, CV_WinText.ckb_IsBuild, EditorStyles.radioButton, GUILayout.Width(15f));
                temp.isMRACOC = GUILayout.Toggle(temp.isMRACOC, CV_WinText.ckb_MRACOC, EditorStyles.radioButton, GUILayout.Width(15f));
                EditorGUI.BeginChangeCheck();
                temp.updateWhenClose = GUILayout.Toggle(temp.updateWhenClose, CV_WinText.ckb_updateWhenClose, EditorStyles.radioButton, GUILayout.Width(15f));
                if (EditorGUI.EndChangeCheck())
                    if (!temp.updateWhenClose)
                        temp.AlreadyUpdatedWhenClose = false;
                infoTemp[I] = temp;
                if (GUILayout.Button(CV_WinText.bt_Remove, GUILayout.Width(60f))) {
                    infoTemp.versions.RemoveAt(I);
                    break;
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
        }

        private static void UnloadConfig(ChangeVersionConfig config) {
            if (!Directory.Exists(ChangeVersionFolder))
                Directory.CreateDirectory(ChangeVersionFolder);

            using (StreamWriter writer = File.CreateText(ChangeConfigFile)) {
                writer.WriteLine("IsAlpha:{0}", config.isAlpha);
                writer.WriteLine("IsBeta:{0}", config.isBeta);
                writer.WriteLine("MRACOC:{0}", config.markRevisionAfterCompletionOfCompilation);
            }
        }

        private static ChangeVersionConfig LoadConfig() {
            if (!File.Exists(ChangeConfigFile)) return new ChangeVersionConfig();
            ChangeVersionConfig res = new ChangeVersionConfig();
            using (StreamReader reader = File.OpenText(ChangeConfigFile)) {
                string temp = reader.ReadLine();
                res.isAlpha = bool.Parse(temp.Remove(0, temp.IndexOf(':') + 1));
                temp = reader.ReadLine();
                res.isBeta = bool.Parse(temp.Remove(0, temp.IndexOf(':') + 1));
                temp = reader.ReadLine();
                res.markRevisionAfterCompletionOfCompilation = bool.Parse(temp.Remove(0, temp.IndexOf(':') + 1));
            }
            return res;
        }

        private static void UnloadVersion(VersionInfo infoTemp) {
            if (!Directory.Exists(ChangeVersionFolder))
                Directory.CreateDirectory(ChangeVersionFolder);

            PlayerSettings.bundleVersion = GetVersion();

            using (StreamWriter writer = File.CreateText(ChangeVersionConfigFile))
                for (int I = 0; I < infoTemp.versions.Count; I++) {
                    writer.WriteLine("-n:{0}", infoTemp.versions[I].name);
                    writer.WriteLine("-v:{0}", infoTemp.versions[I].level);
                    writer.WriteLine("-f:{0}", infoTemp.versions[I].format);
                    writer.WriteLine("-b:{0}", infoTemp.versions[I].isBuild);
                    writer.WriteLine("-m:{0}", infoTemp.versions[I].isMRACOC);
                    writer.WriteLine("-u:{0}", infoTemp.versions[I].updateWhenClose);
					writer.WriteLine("-au:{0}", infoTemp.versions[I].AlreadyUpdatedWhenClose);
                }
        }

        private static VersionInfo LoadVersion() {
            if (!File.Exists(ChangeVersionConfigFile)) return new VersionInfo();
            VersionInfo res = new VersionInfo();

            using (StreamReader reader = File.OpenText(ChangeVersionConfigFile)) {
                string name = (string)null;
                VersionValue vTemp = default;
                while (!reader.EndOfStream) {
                    string temp = reader.ReadLine();
                    string id = temp.Remove(temp.IndexOf(':')).Trim();
                    string value = temp.Remove(0, temp.IndexOf(':') + 1).Trim();
                    switch (id) {
                        case "-n":
                            res.Add(name = value);
                            break;
                        case "-v":
                            vTemp = res[name];
                            vTemp.level = long.Parse(value);
                            res[name] = vTemp;
                            break;
                        case "-f":
                            vTemp = res[name];
                            vTemp.format = value;
                            res[name] = vTemp;
                            break;
                        case "-b":
                            vTemp = res[name];
                            vTemp.isBuild = bool.Parse(value);
                            res[name] = vTemp;
                            break;
                        case "-m":
                            vTemp = res[name];
                            vTemp.isMRACOC = bool.Parse(value);
                            res[name] = vTemp;
                            break;
                        case "-u":
                            vTemp = res[name];
                            vTemp.updateWhenClose = bool.Parse(value);
                            res[name] = vTemp;
                            break;
                        case "-au":
                            vTemp = res[name];
                            vTemp.AlreadyUpdatedWhenClose = bool.Parse(value);
                            res[name] = vTemp;
                            break;
                    }
                }
            }
            return res;
        }

        [MenuItem("Tools/ChangeVersion/Print version")]
        private static void PrintVersion()
            => Debug.Log(GetVersion());

        [MenuItem("Tools/ChangeVersion/Copy version")]
        private static void CopyVersion()
            => EditorGUIUtility.systemCopyBuffer = GetVersion();

        [InitializeOnLoadMethod]
        private static void Init2() {
            CompilationPipeline.assemblyCompilationFinished += (s, l) => {
                for (int I = 0; I < ArrayManipulation.ArrayLength(l); I++)
                    if (l[I].type == CompilerMessageType.Error)
                        return;
                infoTemp = LoadVersion();
                for (int I = 0; I < infoTemp.versions.Count; I++)
                    if (infoTemp[I].isMRACOC) {
                        VersionValue temp = infoTemp[I];
                        ++temp.level;
                        infoTemp[I] = temp;
                    }
                UnloadVersion(infoTemp);
            };

            EditorApplication.quitting += () => {
                infoTemp = LoadVersion();
                for (int I = 0; I < infoTemp.versions.Count; I++)
                    if (infoTemp[I].updateWhenClose && !infoTemp[I].AlreadyUpdatedWhenClose) {
                        VersionValue temp = infoTemp[I];
                        ++temp.level;
                        infoTemp[I] = temp;
                    }
                UnloadVersion(infoTemp);
            };

            PostprocessBuild.EventOnPostprocessBuild += (br) => {
                if (br.summary.totalErrors != 0) return;
                infoTemp = LoadVersion();
                for (int I = 0; I < infoTemp.versions.Count; I++)
                    if (infoTemp[I].isBuild) {
                        VersionValue temp = infoTemp[I];
                        ++temp.level;
                        infoTemp[I] = temp;
                    }
                UnloadVersion(infoTemp);
            };
        }

        private static KeyValuePair<string, VersionInfo>[] GetFormats() {
            KeyValuePair<string, VersionInfo>[] res = new KeyValuePair<string, VersionInfo>[0];
            foreach (Type item in UnityTypeUtility.GetAllTypes())
                if (item.IsSubclassOf(typeof(BaseVersionFormats)))
                    ArrayManipulation.Add((Activator.CreateInstance(item) as BaseVersionFormats).GetFormats(), ref res);
            return res;
        }

        private static string GetVersion()
            => string.Format("{0}{1}", infoTemp, config.isAlpha ? "-alpha" : (config.isBeta ? "-beta" : ""));

        private class PostprocessBuild : IPostprocessBuildWithReport {
            int IOrderedCallback.callbackOrder => 0;

            public static event Action<BuildReport> EventOnPostprocessBuild;

            void IPostprocessBuildWithReport.OnPostprocessBuild(BuildReport report)
                => EventOnPostprocessBuild?.Invoke(report);
        }

        private static class CV_WinText {
            public static GUIContent bt_Add = new GUIContent("Add");
            public static GUIContent bt_Save = new GUIContent("Save");
            public static GUIContent bt_Clear = new GUIContent("Clear");
            public static GUIContent bt_Remove = new GUIContent("Remove");
            public static GUIContent ckb_IsBeta = new GUIContent("Is beta");
            public static GUIContent ckb_IsAlpha = new GUIContent("Is alpha");
            public static GUIContent bt_Up = new GUIContent("Up", "Update version");
            public static GUIContent ckb_Rename = new GUIContent(string.Empty, "Rename");
            public static GUIContent bt_Down = new GUIContent("Down", "Downgrade version");
            public static GUIContent ckb_ChangeLevel = new GUIContent(string.Empty, "Change level");
            public static GUIContent ckb_ChangeFormat = new GUIContent(string.Empty, "Change format");
            public static GUIContent ckb_updateWhenClose = new GUIContent(string.Empty, "Update When Close");
            public static GUIContent ckb_IsBuild = new GUIContent(string.Empty, "Check to update with each build");
            public static GUIContent ckb_MRACOC = new GUIContent(string.Empty, "Mark revision after completion of compilation");
        }
    }
}