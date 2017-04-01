#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Common.Extensions;

namespace CustomPropertyDrawers
{
    [CustomPropertyDrawer(typeof(Vector2Oscillator))]
    public class Vector2OscillatorDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property,
                                                GUIContent label)
        {
            float propHeight = 32f;

            if (property.FindPropertyRelative("xEnabled").boolValue)
            {
                propHeight += 48f;
            }

            if (property.FindPropertyRelative("yEnabled").boolValue)
            {
                propHeight += 48f;
            }

            return propHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            SerializedProperty xEnabledProp = property.FindPropertyRelative("xEnabled");
            SerializedProperty xOscProp = property.FindPropertyRelative("xOsc");
            SerializedProperty yEnabledProp = property.FindPropertyRelative("yEnabled");
            SerializedProperty yOscProp = property.FindPropertyRelative("yOsc");

            // Using BeginProperty / EndProperty on the parent property menas that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            //
            position = EditorGUI.PrefixLabel(position,
                GUIUtility.GetControlID(FocusType.Passive), label);

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            float pushDown = 0f;

            Rect xEnabledRect = position.SetHeight(16f);
            pushDown += xEnabledRect.height;

            Rect xOscRect = position;
            if (xEnabledProp.boolValue)
            {
                xOscRect = position.AddToY(pushDown).SetHeight(48f);
                pushDown += xOscRect.height;
            }

            Rect yEnabledRect = position.AddToY(pushDown).SetHeight(16f);
            pushDown += yEnabledRect.height;

            Rect yOscRect = position;
            if (yEnabledProp.boolValue)
            {
                yOscRect = position.AddToY(pushDown).SetHeight(48f);
            }

                
            xEnabledProp.boolValue = EditorGUI.ToggleLeft(xEnabledRect,
                xEnabledProp.displayName, xEnabledProp.boolValue);
            if (xEnabledProp.boolValue)
            {
                EditorGUI.PropertyField(xOscRect, xOscProp, GUIContent.none);
            }

            yEnabledProp.boolValue = EditorGUI.ToggleLeft(yEnabledRect,
                yEnabledProp.displayName, yEnabledProp.boolValue);
            if (yEnabledProp.boolValue)
            {
                EditorGUI.PropertyField(yOscRect, yOscProp, GUIContent.none);
            }

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
#endif