#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;

namespace CustomPropertyDrawers
{
    public class DisplayScriptNameAttribute : PropertyAttribute
    {
        public string customName;

        public DisplayScriptNameAttribute(string customName = "No Script")
        {
            this.customName = customName;
        }
    }

    [CustomPropertyDrawer(typeof(DisplayScriptNameAttribute))]
    public class DisplayScriptNameDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            //
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Setup the rectangles
            var propertyRect = new Rect(position.x, position.y, 35, position.height);
            var nameRect = new Rect(position.x + 35, position.y, 300, position.height);

            // Draw the fields
            EditorGUI.PropertyField(propertyRect, property, GUIContent.none);

            string name;

            if (property.objectReferenceValue != null)
            {
                Type type = property.objectReferenceValue.GetType();
                switch (type.ToString())
                {
                    case "TriggerSwitch":
                        name = ((TriggerSwitch)
                            property.objectReferenceValue).scriptName;
                        break;
            
                    case "TimerSwitch":
                        name = ((TimerSwitch)
                            property.objectReferenceValue).scriptName;
                        break;

                    default:
                        name = property.objectReferenceValue.name
                        + property.GetHashCode();
                        break;
                }
            }
            else
            {
                name = ((DisplayScriptNameAttribute)attribute).customName;
            }
                
            if (name == "")
            {
                name = "<No Script!>";
            }

            EditorGUI.LabelField(nameRect, name);

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
#endif