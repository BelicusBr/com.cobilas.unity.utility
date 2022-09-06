using UnityEngine;
using UnityEditor;

namespace Cobilas.Unity.Editor.Utility {
    /// <summary>Pega informações de itens na aba projeto.</summary>
    public static class CheckItem {
        [MenuItem("Tools/Check item")]
        private static void CheckSelectionItem()
            => MonoBehaviour.print($"Path:{GetAssetPath()}|Type:{GetSelectionItemType()}");

        private static string GetAssetPath()
            => AssetDatabase.GetAssetPath(Selection.activeObject);

        private static System.Type GetSelectionItemType()
            => Selection.activeObject.GetType();
    }
}
