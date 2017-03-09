using UnityEngine;
using UnityEditor;

namespace CustomPropertyDrawers
{
    [CustomPropertyDrawer(typeof(GameObjectConfig.CustomScale))]
    public class CustomScaleDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //Get the class's serialized properties
            SerializedProperty scaleConfig = property.FindPropertyRelative("scaleConfig");
            SerializedProperty custom = property.FindPropertyRelative("custom");

            EditorGUI.BeginProperty(position, label, property);

            //Set the rectangle of the scale config enum
            position = position.MoveDown(0, 
                EditorGUI.GetPropertyHeight(scaleConfig));

            //Display the scale config enum
            EditorGUI.PropertyField(position, scaleConfig);

            //Set the rectangle of the custom scale
            position = position.MoveDown(EditorGUI.GetPropertyHeight(scaleConfig), 
                EditorGUI.GetPropertyHeight(custom));

            //Check to see if the scale config is not set to "None"
            if (scaleConfig.enumValueIndex != 0)
            {
                //Display the custom scale
                EditorGUI.PropertyField(position.SqueezeLeft(16f), custom);
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float propertyHeight = 16.0f;
            if (property.FindPropertyRelative("scaleConfig").enumValueIndex != 0)
            {
                propertyHeight += 16.0f;
            }
            return propertyHeight;
        }
    }
}