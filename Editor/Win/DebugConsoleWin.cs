using UnityEditor;
using UnityEngine;
using Cobilas.Collections;
using Cobilas.Unity.Utility;
using System.Collections.Generic;

namespace Cobilas.Unity.Editor.Utility.Win {
    public class DebugConsoleWin : EditorWindow {
        [MenuItem("Window/Debug Console")]
        private static void DoIt() {
            DebugConsoleWin debug = GetWindow<DebugConsoleWin>();
            debug.titleContent = new GUIContent("Debug Console");

            DebugConsole.SetModule("TDS-MDL");
            DebugConsole.ClearModule();
            DebugConsole.ConsoleLog("TDS-1");
            DebugConsole.ConsoleLog("TDS-2");
            DebugConsole.ConsoleLog("TDS-3");
            DebugConsole.ConsoleLog("TDS-4");
            debug.OnEnable();
            debug.Show();
        }

        private int selectedIndex;
        private Vector2 scrollView;
        private string[] nameModules;
        private DebugLogger[] current;

        private void OnEnable() {
            Dictionary<string, DebugLogger[]> keys = DebugConsole.Logs;
            nameModules = new string[keys.Count];
            current = (DebugLogger[])null;
            int count = 0;
            foreach (var item in keys)
                nameModules[count++] = item.Key;
            if (!ArrayManipulation.EmpytArray(nameModules))
                current = keys[nameModules[selectedIndex]];
        }

        private void OnGUI() {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar, GUILayout.ExpandWidth(true));
            if (ToolBarButton("Clear module", 85)) {
                DebugConsole.ClearModule();
                ArrayManipulation.ClearArraySafe(ref current);
            }
            if (ToolBarButton("Refresh", 55))
                OnEnable();
            if (ToolBarButton("Clear all module", 100)) {
                DebugConsole.ClearAllModules();
                ArrayManipulation.ClearArraySafe(ref current);
                ArrayManipulation.ClearArraySafe(ref nameModules);
            }
            ToolBarPopup();
            EditorGUILayout.EndHorizontal();
            scrollView = EditorGUILayout.BeginScrollView(scrollView);
            for (int I = 0; I < ArrayManipulation.ArrayLength(current); I++) {
                EditorGUILayout.BeginHorizontal(EditorStyles.toolbar, GUILayout.ExpandWidth(true));
                current[I].foldout = EditorGUILayout.Foldout(current[I].foldout,
                    EditorGUIUtility.TrTempContent(string.Format("[{0}]{1}", current[I].Time, current[I].MSM))
                    );
                EditorGUILayout.EndHorizontal();
                if (current[I].foldout) {
                    ++EditorGUI.indentLevel;
                    EditorGUILayout.BeginVertical(CreateToolBar(), GUILayout.ExpandWidth(true));
                    EditorGUILayout.LabelField(current[I].MSM.TrimEnd(), CreateLabel());
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.BeginVertical(CreateToolBar(), GUILayout.ExpandWidth(true));
                    EditorGUILayout.LabelField(current[I].Tracking.TrimEnd(), CreateLabel());
                    EditorGUILayout.EndVertical();
                    --EditorGUI.indentLevel;
                }
            }
            EditorGUILayout.EndScrollView();
        }

        private GUIStyle CreateToolBar() {
            GUIStyle label = new GUIStyle(EditorStyles.toolbar);
            label.fixedHeight = 0f;
            return label;
        }

        private GUIStyle CreateLabel() {
            GUIStyle label = new GUIStyle(EditorStyles.label);
            label.wordWrap = true;
            return label;
        }

        private bool ToolBarButton(string txt, float width)
            => GUILayout.Button(txt, EditorStyles.toolbarButton, GUILayout.Width(width));

        private void ToolBarPopup() {
            EditorGUI.BeginChangeCheck();
            selectedIndex = EditorGUILayout.Popup(selectedIndex, nameModules, EditorStyles.toolbarPopup, GUILayout.Width(130f));
            if (EditorGUI.EndChangeCheck() && !ArrayManipulation.EmpytArray(nameModules))
                current = DebugConsole.Logs[nameModules[selectedIndex]];
        }
    }
}