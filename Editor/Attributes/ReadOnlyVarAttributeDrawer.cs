using UnityEngine;
using UnityEditor;
using Cobilas.Unity.Utility;
using UnityEngine.UIElements;

namespace Cobilas.Unity.Editor.Utility {
    [CustomPropertyDrawer(typeof(ReadOnlyVarAttribute))]
    public class ReadOnlyVarAttributeDrawer : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginDisabledGroup(true);
            PropertyDrawerList.OnGUI(position, property, label, this);
            EditorGUI.EndDisabledGroup();
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property) {
            using (new EditorGUI.DisabledScope(true))
                return PropertyDrawerList.CreatePropertyGUI(property, this);
        }

        public override bool CanCacheInspectorGUI(SerializedProperty property)
            => PropertyDrawerList.CanCacheInspectorGUI(property, this);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => PropertyDrawerList.GetPropertyHeight(property, label, this);
    }
}