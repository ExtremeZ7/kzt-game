using UnityEngine;
using UnityEditor;
using System;

namespace CustomPropertyDrawers
{

    [Serializable]
    public class Tag : System.Object
    {
        public bool enabled;
        public string name;

        public string Name
        { 
            get{ return enabled ? name : ""; } 
            set { name = value; } 
        }
    }

    [CustomPropertyDrawer(typeof(Tag))]
    public class TagDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
                                   GUIContent label)
        {
            SerializedProperty enabled = property.FindPropertyRelative("enabled");
            SerializedProperty name = property.FindPropertyRelative("name");

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
            enabled.boolValue = EditorGUI.Toggle(
                position.SqueezeRight(position.width - 20), enabled.boolValue);

            if (enabled.boolValue)
            {
                //Make sure name is not empty and is 'Untagged' by default
                //
                if (name.stringValue == "")
                {
                    name.stringValue = "Untagged";
                }

                // Replace the string field with a tag field 
                //
                name.stringValue = EditorGUI.TagField(position.SqueezeLeft(20),
                    name.stringValue);
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