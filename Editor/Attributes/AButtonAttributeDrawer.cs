using UnityEditor;
using UnityEngine;
using Cobilas.Unity.Utility;

namespace Cobilas.Unity.Editor.Utility {
    [CustomPropertyDrawer(typeof(AButtonAttribute))]
    public class AButtonAttributeDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            if (property.type != "bool")
                _ = EditorGUI.PropertyField(position, property, label);
            else property.boolValue = GUI.Button(position, label);
        }
    }
}