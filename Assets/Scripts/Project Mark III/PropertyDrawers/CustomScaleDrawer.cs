#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Common.Extensions;

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
            position = position.AddToY(0).SetHeight(
                EditorGUI.GetPropertyHeight(scaleConfig));

            //Display the scale config enum
            EditorGUI.PropertyField(position, scaleConfig);

            //Set the rectangle of the custom scale
            position = position.AddToY(EditorGUI.GetPropertyHeight(scaleConfig)).SetHeight(
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
#endif