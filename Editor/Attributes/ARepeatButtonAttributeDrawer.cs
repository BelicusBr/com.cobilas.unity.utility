using UnityEditor;
using UnityEngine;
using Cobilas.Unity.Utility;

namespace Cobilas.Unity.Editor.Utility {
    [CustomPropertyDrawer(typeof(ARepeatButtonAttribute))]
    public class ARepeatButtonAttributeDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            if (property.type != "bool")
                _ = EditorGUI.PropertyField(position, property, label);
            else property.boolValue = GUI.RepeatButton(position, label);
        }
    }
}