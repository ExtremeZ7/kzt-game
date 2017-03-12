#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace CustomPropertyDrawers
{
    public class PopupAttribute : PropertyAttribute
    {
        public string[] items;

        public PopupAttribute(params string[] values)
        {
            items = values;
        }
    }

    [CustomPropertyDrawer(typeof(PopupAttribute))]
    public class PopupDrawer : PropertyDrawer
    {
        const int textHeight = 16;

        public override void OnGUI(Rect position, SerializedProperty property,
                                   GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            int origIndent = EditorGUI.indentLevel;
            string[] values = ((PopupAttribute)attribute).items;
            int index = 0;
            for (; index < values.Count() - 1; index++)
            {
                if (values[index].Equals(property.stringValue))
                    break;
            }

            EditorGUI.LabelField(new Rect(position.xMin, position.yMin,
                    position.width / 3f, textHeight),
                GUIUtils.PrettyPrintVariableName(property.name));

            index = EditorGUI.Popup(
                new Rect(position.xMax / 3f,
                    position.yMin, position.width - (position.width / 3f),
                    textHeight), index, values);

            if (index < values.Count())
                property.stringValue = values[index];

            EditorGUI.indentLevel = origIndent;
            EditorGUI.EndProperty();
        }
    }
}
#endif