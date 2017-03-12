#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CustomPropertyDrawers
{
    public class IncrementalChange : PropertyAttribute
    {
        public float increment;

        public IncrementalChange(float increment)
        {
            this.increment = increment;
        }
    }

    [CustomPropertyDrawer(typeof(IncrementalChange))]
    public class IncrementalChangeDrawer : PropertyDrawer
    {
        const int textHeight = 16;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            int origIndent = EditorGUI.indentLevel;

            // Retrieve the increment from the attribute
            //
            float increment = ((IncrementalChange)attribute).increment;

            // Hidden slider allows us to replicate the slide over name to change the value
            //
            EditorGUI.Slider(new Rect(position.xMin, position.yMin, position.width / 3f, textHeight),
                property, property.floatValue - 200, property.floatValue + 200);

            /*EditorGUI.LabelField (new Rect (position.xMin, position.yMin, position.width / 3f, textHeight),
                GUIUtils.PrettyPrintVariableName (property.name));*/

            property.floatValue = EditorGUI.FloatField(
                new Rect(position.xMax / 3f, position.yMin,
                    position.width - (position.width / 3f),
                    textHeight), Mathf.Round(property.floatValue / increment) * increment);

            EditorGUI.indentLevel = origIndent;
            EditorGUI.EndProperty();
        }
    }
}
#endif