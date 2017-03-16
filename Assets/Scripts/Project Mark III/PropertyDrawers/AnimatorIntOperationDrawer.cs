#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CustomPropertyDrawers
{
    [CustomPropertyDrawer(typeof(SetAnimatorIntsOnTrigger.AnimatorIntOperation))]
    public class AnimatorIntOperationDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
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
            var nameRect = new Rect(position.x, position.y,
                               position.width - 125, position.height);
            var operationRect = new Rect(position.x + position.width - 120, position.y,
                                    90, position.height);
            var operandRect = new Rect(position.x + position.width - 25, position.y,
                                  25, position.height);

            // Draw fields - pas GUIContent.none to each so they are drawn without labels
            //
            EditorGUI.PropertyField(nameRect,
                property.FindPropertyRelative("name"), GUIContent.none);
            EditorGUI.PropertyField(operationRect,
                property.FindPropertyRelative("operation"), GUIContent.none);
            EditorGUI.PropertyField(operandRect,
                property.FindPropertyRelative("operand"), GUIContent.none);

            // Set indent back to what it was
            //
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
#endif