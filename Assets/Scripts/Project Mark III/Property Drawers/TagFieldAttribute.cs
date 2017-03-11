using UnityEngine;
using UnityEditor;

namespace CustomPropertyDrawers
{

    public class TagFieldAttribute : PropertyAttribute
    {
        
    }

    [CustomPropertyDrawer(typeof(TagFieldAttribute))]
    public class TagFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
                                   GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            //
            position = EditorGUI.PrefixLabel(position,
                GUIUtility.GetControlID(FocusType.Passive), label);

            // Replace the string field with a tag field 
            //
            property.stringValue = EditorGUI.TagField(position, 
                property.stringValue);

            EditorGUI.EndProperty();
        }
    }
}