#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CustomPropertyDrawers
{
    [CustomPropertyDrawer(typeof(Vector2Oscillator))]
    public class Vector2OscillatorDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property,
                                                GUIContent label)
        {
            float propHeight = 32f;

            if (!property.FindPropertyRelative("skipX").boolValue)
            {
                propHeight += 48f;
            }

            if (!property.FindPropertyRelative("skipY").boolValue)
            {
                propHeight += 48f;
            }

            return propHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            SerializedProperty xSkipProp = property.FindPropertyRelative("skipX");
            SerializedProperty xOscProp = property.FindPropertyRelative("xOsc");
            SerializedProperty ySkipProp = property.FindPropertyRelative("skipY");
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

            Rect xSkipRect = position.SetHeight(16f);
            pushDown += xSkipRect.height;

            Rect xOscRect = position;
            if (!xSkipProp.boolValue)
            {
                xOscRect = position.PushDown(pushDown).SetHeight(48f);
                pushDown += xOscRect.height;
            }

            Rect ySkipRect = position.PushDown(pushDown).SetHeight(16f);
            pushDown += ySkipRect.height;

            Rect yOscRect = position;
            if (!ySkipProp.boolValue)
            {
                yOscRect = position.PushDown(pushDown).SetHeight(48f);
            }

                
            xSkipProp.boolValue = EditorGUI.ToggleLeft(xSkipRect,
                xSkipProp.displayName, xSkipProp.boolValue);
            if (!xSkipProp.boolValue)
            {
                EditorGUI.PropertyField(xOscRect, xOscProp, GUIContent.none);
            }

            ySkipProp.boolValue = EditorGUI.ToggleLeft(ySkipRect,
                ySkipProp.displayName, ySkipProp.boolValue);
            if (!ySkipProp.boolValue)
            {
                EditorGUI.PropertyField(yOscRect, yOscProp, GUIContent.none);
            }

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
#endif