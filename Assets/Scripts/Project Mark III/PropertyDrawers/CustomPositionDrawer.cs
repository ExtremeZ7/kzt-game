#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CustomPropertyDrawers
{
    [CustomPropertyDrawer(typeof(GameObjectConfig.CustomPosition))]
    public class CustomPositionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //Get the class's serialized properties
            SerializedProperty positionConfig = 
                property.FindPropertyRelative("positionConfig");
            SerializedProperty custom = property.FindPropertyRelative("custom");

            EditorGUI.BeginProperty(position, label, property);

            //Set the rectangle of the scale rotation enum
            position = position.PushDown(0).SetHeight(
                EditorGUI.GetPropertyHeight(positionConfig));

            //Display the rotation config enum
            EditorGUI.PropertyField(position, positionConfig);

            //Set the rectangle of the custom rotatiom
            position = position.PushDown(EditorGUI.GetPropertyHeight(positionConfig)).SetHeight(
                EditorGUI.GetPropertyHeight(custom));

            //Check to see if the rotation config is not set to "None"
            if (positionConfig.enumValueIndex != 0)
            {
                //Display the custom position
                EditorGUI.PropertyField(position.SqueezeLeft(16f), custom);
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float propertyHeight = 16.0f;
            if (property.FindPropertyRelative("positionConfig").enumValueIndex != 0)
            {
                propertyHeight += 16.0f;
            }
            return propertyHeight;
        }
    }
}
#endif