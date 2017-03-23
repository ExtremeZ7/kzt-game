using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class Tag
{
    public bool enabled;
    public string name;

    public string Name
    { 
        get{ return enabled ? name : ""; } 
        set { name = value; } 
    }
}

#if UNITY_EDITOR
namespace CustomPropertyDrawers
{
    [CustomPropertyDrawer(typeof(Tag))]
    public class TagDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
                                   GUIContent label)
        {
            SerializedProperty enabledProp =
                property.FindPropertyRelative("enabled");
            SerializedProperty nameProp = property.FindPropertyRelative("name");

            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            //
            position = EditorGUI.PrefixLabel(position,
                GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be intended
            //
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            //Draw the toggle field
            //
            enabledProp.boolValue = EditorGUI.Toggle(
                position.SqueezeRight(position.width - 20), enabledProp.boolValue);

            if (enabledProp.boolValue)
            {
                //Make sure name is not empty and is 'Untagged' by default
                //
                if (nameProp.stringValue == "")
                {
                    nameProp.stringValue = "Untagged";
                }

                // Replace the string field with a tag field 
                //
                nameProp.stringValue = EditorGUI.TagField(position.SqueezeLeft(20),
                    nameProp.stringValue);
            }
            else
            {
                EditorGUI.LabelField(position.SqueezeLeft(20), "(Unused)");
            }


            // Set indent back to what it was
            //
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}
#endif