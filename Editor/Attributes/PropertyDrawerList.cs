using System;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using Cobilas.Unity.Utility;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace Cobilas.Unity.Editor.Utility {
    public static class PropertyDrawerList {
        private static Dictionary<string, GUIDrawer> list = new Dictionary<string, GUIDrawer>();

        static PropertyDrawerList()
            => list = new Dictionary<string, GUIDrawer>();

        public static VisualElement CreatePropertyGUI(SerializedProperty property, PropertyDrawer drawer) {
            GUIDrawer temp = GetGUIDrawerAndAddList(drawer.fieldInfo.FieldType);
            if (temp == null || !temp.GetType().IsSubclassOf(typeof(PropertyDrawer)))
                return drawer.CreatePropertyGUI(property);
            SetValue(temp, drawer.attribute, "m_Attribute");
            SetValue(temp, drawer.fieldInfo, "m_FieldInfo");
            return (temp as PropertyDrawer).CreatePropertyGUI(property);
        }

        public static bool CanCacheInspectorGUI(SerializedProperty property, PropertyDrawer drawer) {
            GUIDrawer temp = GetGUIDrawerAndAddList(drawer.fieldInfo.FieldType);
            if (temp != null)
                if (temp.GetType().IsSubclassOf(typeof(PropertyDrawer))) {
                    SetValue(temp, drawer.attribute, "m_Attribute");
                    SetValue(temp, drawer.fieldInfo, "m_FieldInfo");
                    return (temp as PropertyDrawer).CanCacheInspectorGUI(property);
                } else if (temp.GetType().IsSubclassOf(typeof(DecoratorDrawer))) {
                    SetValue(temp, drawer.attribute, "m_Attribute");
                    return (temp as DecoratorDrawer).CanCacheInspectorGUI();
                }
            return false;
        }

        public static void OnGUI(Rect position, SerializedProperty property, GUIContent label, PropertyDrawer drawer) {
            GUIDrawer temp = GetGUIDrawerAndAddList(drawer.fieldInfo.FieldType);
            if (temp != null) {
                if (temp.GetType().IsSubclassOf(typeof(PropertyDrawer))) {
                    SetValue(temp, drawer.attribute, "m_Attribute");
                    SetValue(temp, drawer.fieldInfo, "m_FieldInfo");
                    (temp as PropertyDrawer).OnGUI(position, property, label);
                } else if (temp.GetType().IsSubclassOf(typeof(DecoratorDrawer))) {
                    SetValue(temp, drawer.attribute, "m_Attribute");
                    (temp as DecoratorDrawer).OnGUI(position);
                }
            } else _ = EditorGUI.PropertyField(position, property, label);
        }

        public static float GetPropertyHeight(SerializedProperty property, GUIContent label, PropertyDrawer drawer) {
            GUIDrawer temp = GetGUIDrawerAndAddList(drawer.fieldInfo.FieldType);
            if (temp == null) return EditorGUIUtility.singleLineHeight;
            else if (temp.GetType().IsSubclassOf(typeof(PropertyDrawer))) {
                SetValue(temp, drawer.attribute, "m_Attribute");
                SetValue(temp, drawer.fieldInfo, "m_FieldInfo");
                return (temp as PropertyDrawer).GetPropertyHeight(property, label);
            } else if (temp.GetType().IsSubclassOf(typeof(DecoratorDrawer))) {
                SetValue(temp, drawer.attribute, "m_Attribute");
                return (temp as DecoratorDrawer).GetHeight();
            }
            return drawer.GetPropertyHeight(property, label);
        }

        private static GUIDrawer GetGUIDrawerAndAddList(Type type) {
            if (list.ContainsKey(type.FullName))
                return list[type.FullName];
            GUIDrawer temp = GetGUIDrawer(type);
            list.Add(type.FullName, temp);
            return temp;
        }

        private static GUIDrawer GetGUIDrawer(Type type) {
            Type[] types = UnityTypeUtility.GetAllTypes();
            foreach (Type item in types) {
                if (item.IsSubclassOf(typeof(PropertyDrawer)) || item.IsSubclassOf(typeof(DecoratorDrawer))) {
                    CustomPropertyDrawer[] attributes = item.GetAttributes<CustomPropertyDrawer>(true);
                    foreach (CustomPropertyDrawer item2 in attributes) {
                        Type m_Type = (Type)GetValue(item2, "m_Type");
                        bool m_UseForChildren = (bool)GetValue(item2, "m_UseForChildren");
                        if (m_UseForChildren && (type.IsSubclassOf(m_Type) || type == m_Type))
                            return (GUIDrawer)Activator.CreateInstance(item);
                        else if (type == m_Type)
                            return (GUIDrawer)Activator.CreateInstance(item);
                    }
                }
            }
            return null;
        }

        private static void SetValue(object item, object value, string fieldName)
            => item.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic).SetValue(item, value);

        private static object GetValue(object item, string fieldName)
            => item.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(item);
    }
}