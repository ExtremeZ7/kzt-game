#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CustomPropertyDrawers
{
    [CustomPropertyDrawer(typeof(TaggedFloat))]
    public class TaggedFloatDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProp = property.FindPropertyRelative("value");
            SerializedProperty tagProp = property.FindPropertyRelative("useValueAs");


            // Using BeginProperty / EndProperty on the parent property menas that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            //
            position = EditorGUI.PrefixLabel(position,
                GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be intended
            //
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            //
            Rect valueRect = position.SetWidth(position.width / 2f);
            Rect tagRect = position
                .SqueezeLeft(position.width / 2f).SetWidth(position.width / 2f);

            // Draw fields
            //
            EditorGUI.PropertyField(valueRect, valueProp,
                GUIContent.none);
            EditorGUI.PropertyField(tagRect, tagProp,
                GUIContent.none);

            // Set indent back to what it was
            //
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
#endif