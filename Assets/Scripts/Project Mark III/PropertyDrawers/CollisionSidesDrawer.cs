#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Common.Extensions;

namespace CustomPropertyDrawers
{
    [CustomPropertyDrawer(typeof(CollisionSwitch.CollisionSides))]
    public class CollisionSidesDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 48f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty topProp = property.FindPropertyRelative("top");
            SerializedProperty bottomProp = property.FindPropertyRelative("bottom");
            SerializedProperty leftProp = property.FindPropertyRelative("left");
            SerializedProperty rightProp = property.FindPropertyRelative("right");

            // Using BeginProperty / EndProperty on the parent property menas that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            //
            //position = EditorGUI.PrefixLabel(position,
            //    GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be intended
            //
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            position.width = 16f;
            position.height = 16f;

            // Calculate rects
            //
            var labelRect = position.AddToY(16f).SetWidth(48f);
            var topRect = position.AddToX(64f);
            var bottomRect = position.AddToX(64f).AddToY(32f);
            var leftRect = position.AddToX(48f).AddToY(16f);
            var rightRect = position.AddToX(80f).AddToY(16f);

            EditorGUI.LabelField(labelRect, "Sides");
            EditorGUI.PropertyField(topRect, topProp, GUIContent.none);
            EditorGUI.PropertyField(bottomRect, bottomProp, GUIContent.none);
            EditorGUI.PropertyField(leftRect, leftProp, GUIContent.none);
            EditorGUI.PropertyField(rightRect, rightProp, GUIContent.none);

            // Set indent back to what it was
            //
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();

        }
    }
}
#endif
