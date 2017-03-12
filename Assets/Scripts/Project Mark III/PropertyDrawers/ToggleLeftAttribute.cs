#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CustomPropertyDrawers
{
    public class ToggleLeft : PropertyAttribute
    {
    }

    [CustomPropertyDrawer(typeof(ToggleLeft))]
    public class ToggleLeftDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            property.boolValue = EditorGUI.ToggleLeft(position, 
                GUIUtils.PrettyPrintVariableName(property.name), property.boolValue);

            EditorGUI.EndProperty();
        }
    }
}
#endif