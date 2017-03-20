#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CustomPropertyDrawers
{
    [CustomPropertyDrawer(typeof(ColorToPrefab))]
    public class ColorToPrefabDrawer : PropertyDrawer
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
            var objRect = new Rect(position.x, position.y,
                              position.width - 65, position.height);
            var colorRect = new Rect(position.x + position.width - 60,
                                position.y, 60, position.height);

            // Draw fields - pas GUIContent.none to each so they are drawn without labels
            //
            EditorGUI.PropertyField(objRect,
                property.FindPropertyRelative("prefab"), GUIContent.none);
            EditorGUI.PropertyField(colorRect,
                property.FindPropertyRelative("color"), GUIContent.none);

            // Set indent back to what it was
            //
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
#endif
