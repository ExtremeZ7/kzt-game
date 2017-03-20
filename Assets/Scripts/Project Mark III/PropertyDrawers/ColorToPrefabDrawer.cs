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

            SerializedProperty matrixProp =
                property.FindPropertyRelative("pixelMatrix");
            SerializedProperty classProp =
                property.FindPropertyRelative("tileClass");

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
                              position.width - 65, 16f);
            var colorRect = new Rect(position.x + position.width - 60,
                                position.y, 60, 16f);

            // Draw fields - pas GUIContent.none to each so they are drawn without labels
            //
            EditorGUI.PropertyField(objRect,
                property.FindPropertyRelative("prefab"), GUIContent.none);
            EditorGUI.PropertyField(colorRect,
                property.FindPropertyRelative("color"), GUIContent.none);

            EditorGUI.LabelField(new Rect(position.x, position.y + 24f,
                    48f, 16f), "Matrix");

            Rect[] matrixRect = new Rect[4];

            matrixRect[0] = new Rect(position.x + 48f, position.y + 16f,
                16f, 16f);
            matrixRect[1] = new Rect(position.x + 64f, position.y + 16f,
                16f, 16f);
            matrixRect[2] = new Rect(position.x + 48f, position.y + 32f,
                16f, 16f);
            matrixRect[3] = new Rect(position.x + 64f, position.y + 32f,
                16f, 16f);

            matrixProp.arraySize = 4;

            for (int i = 0; i < 4; i++)
            {
                EditorGUI.PropertyField(matrixRect[i],
                    matrixProp.GetArrayElementAtIndex(i), GUIContent.none);
            }

            EditorGUI.LabelField(new Rect(position.x + 84f, position.y + 24f,
                    48f, 16f), "Class");

            EditorGUI.PropertyField(new Rect(position.x + 128f,
                    position.y + 24, position.width - 128f, 16f),
                classProp, GUIContent.none);

            // Set indent back to what it was
            //
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 48.0f;
        }
    }
}
#endif
