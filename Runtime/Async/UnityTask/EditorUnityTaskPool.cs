#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;
using Cobilas.Collections;

namespace Cobilas.Unity.Utility {
    internal static class EditorUnityTaskPool {
        private static UnityTask[] tasks;

        [InitializeOnLoadMethod]
        private static void Init() {
            EditorApplication.update += () => {
                if (!ArrayManipulation.EmpytArray(tasks))
                    foreach (var item in tasks)
                        if (item.IsFaulted)
                            Debug.LogException(item.Exception);
            };
            EditorApplication.playModeStateChanged += (p) => {
                switch (p) {
                    case PlayModeStateChange.EnteredEditMode:
                        for (int I = 0; I < ArrayManipulation.ArrayLength(tasks); I++)
                            if (!tasks[I].IsDisposed)
                                tasks[I].Dispose();
                        ArrayManipulation.ClearArraySafe(ref tasks);
                        break;
                    case PlayModeStateChange.ExitingPlayMode:
                        for (int I = 0; I < ArrayManipulation.ArrayLength(tasks); I++)
                            if (!tasks[I].IsCancellationRequested && !tasks[I].IsDisposed)
                                tasks[I].Cancel();
                        break;
                }
            };
        }

        public static void AddTask(UnityTask task) {
            int index = GetIndexCompletedTask();
            if (index < 0) ArrayManipulation.Add(task, ref tasks);
            else {
                try {
                    if (!tasks[index].IsDisposed)
                        tasks[index].Dispose();
                } catch (Exception e) {
                    Debug.LogException(e);
                }
                tasks[index] = task;
            }
        }

        private static int GetIndexCompletedTask() {
            for (int I = 0; I < ArrayManipulation.ArrayLength(tasks); I++) {
                if (tasks[I] == null)
                    return I;
                if (!tasks[I].IsRunning)
                    return I;
            }
            return -1;
        }
    }
}
#endif