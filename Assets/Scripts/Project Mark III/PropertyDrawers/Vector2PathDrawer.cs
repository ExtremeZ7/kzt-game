#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Common.Extensions;

namespace CustomPropertyDrawers
{
    [CustomPropertyDrawer(typeof(Vector2Path))]
    public class Vector2PathDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty xEnabledProp = property.FindPropertyRelative("xEnabled");
            SerializedProperty xPathProp = property.FindPropertyRelative("x");
            SerializedProperty yEnabledProp = property.FindPropertyRelative("yEnabled");
            SerializedProperty yPathProp = property.FindPropertyRelative("y");


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
            Rect xLabelRect = position.SetWidth(16f);
            Rect xEnabledRect = position.AddToX(16f).SetWidth(16f);
            Rect xPathRect = position
                .SqueezeLeft(32f).SetWidth(position.width / 2f - 32f);
            
            Rect yLabelRect = position
                .AddToX(position.width / 2f + 4f).SetWidth(16f);
            Rect yEnabledRect = position
                .SqueezeLeft(position.width / 2f + 16f).SetWidth(16f);
            Rect yPathRect = position
                .SqueezeLeft(position.width / 2f + 32f);

            // Draw fields
            //
            EditorGUI.LabelField(xLabelRect, "X");
            EditorGUI.PropertyField(xEnabledRect, xEnabledProp,
                GUIContent.none);
            if (xEnabledProp.boolValue)
            {
                EditorGUI.PropertyField(xPathRect, xPathProp,
                    GUIContent.none);
            }
            EditorGUI.LabelField(yLabelRect, "Y");
            EditorGUI.PropertyField(yEnabledRect, yEnabledProp,
                GUIContent.none);
            if (yEnabledProp.boolValue)
            {
                EditorGUI.PropertyField(yPathRect, yPathProp,
                    GUIContent.none);
            }
            // Set indent back to what it was
            //
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
#endif
