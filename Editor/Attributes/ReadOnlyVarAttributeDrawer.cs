using UnityEngine;
using UnityEditor;
using Cobilas.Unity.Utility;

namespace Cobilas.Unity.Editor.Utility {
    [CustomPropertyDrawer(typeof(ReadOnlyVarAttribute))]
    public class ReadOnlyVarAttributeDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginDisabledGroup(true);
            //base.OnGUI(position, property, label);
            _ = EditorGUI.PropertyField(position, property, label);
            EditorGUI.EndDisabledGroup();
        }
    }
}