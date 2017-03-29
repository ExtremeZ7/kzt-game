#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Common.Extensions;

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
            SerializedProperty offsetPosProp =
                property.FindPropertyRelative("offsetPosition");
            SerializedProperty offsetSclProp =
                property.FindPropertyRelative("offsetScale");
            SerializedProperty offsetRotProp =
                property.FindPropertyRelative("offsetRotation");

            // Draw label
            //
            //position = EditorGUI.PrefixLabel(position,
            //    GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be intended
            //
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            //
            var objRect = new Rect(position.x, position.y,
                              position.width, 16f);

            // Draw fields - pas GUIContent.none to each so they are drawn without labels
            //
            EditorGUI.PropertyField(objRect,
                property.FindPropertyRelative("prefab"), GUIContent.none);

            EditorGUI.LabelField(new Rect(position.x, position.y + 24f,
                    48f, 16f), "Matrix");

            var matrixRect = new Rect[4];

            matrixRect[0] = new Rect(position.x + 48f, position.y + 32f,
                16f, 16f);
            matrixRect[1] = new Rect(position.x + 64f, position.y + 32f,
                16f, 16f);
            matrixRect[2] = new Rect(position.x + 48f, position.y + 16f,
                16f, 16f);
            matrixRect[3] = new Rect(position.x + 64f, position.y + 16f,
                16f, 16f);

            matrixProp.arraySize = 4;

            for (int i = 0; i < 4; i++)
            {
                SerializedProperty matrixCelProp =
                    matrixProp.GetArrayElementAtIndex(i);

                matrixCelProp.colorValue = 
                    EditorGUI.ColorField(matrixRect[i],
                    GUIContent.none,
                    matrixCelProp.colorValue,
                    false, false, false, null);

                /*matrixCelProp.colorValue = 
                    new Color32(
                    matrixCelProp.colorValue.r.ToColorByte(),
                    matrixCelProp.colorValue.g.ToColorByte(),
                    matrixCelProp.colorValue.b.ToColorByte(),
                    255
                );*/
            }

            position = position.SetHeight(16f);

            Rect classRect = position.PushVertical(16f)
                .SqueezeLeft(132f).SetWidth(position.width / 2f - 132f);
            Rect posRect = position.PushVertical(16f)
                .SqueezeLeft(position.width / 2f + 52f);
            Rect rotRect = position.PushVertical(32f)
                .SqueezeLeft(136f).SetWidth(position.width / 2f - 136f);
            Rect sclRect = position.PushVertical(32f)
                .SqueezeLeft(position.width / 2f + 52f);

            EditorGUI.LabelField(new Rect(position.x + 84f, position.y + 16f,
                    52f, 16f), "Parent");

            EditorGUI.PropertyField(classRect, classProp, GUIContent.none);

            EditorGUI.LabelField(position
                .PushVertical(16f).SqueezeLeft(position.width / 2f)
                .SetWidth(52f), "Off. Pos.");

            EditorGUI.PropertyField(posRect, offsetPosProp, GUIContent.none);

            EditorGUI.LabelField(new Rect(position.x + 84f, position.y + 32f,
                    52f, 16f), "Off. Rot.");

            EditorGUI.Slider(rotRect, offsetRotProp, 0f, 360f, GUIContent.none);

            EditorGUI.LabelField(position
                .PushVertical(32f).SqueezeLeft(position.width / 2f)
                .SetWidth(52f), "Off. Scl.");

            EditorGUI.PropertyField(sclRect, offsetSclProp, GUIContent.none);

            // Set indent back to what it was
            //
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property,
                                                GUIContent label)
        {
            return 48.0f;
        }
    }
}
#endif
