#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Common.Extensions;

namespace CustomPropertyDrawers
{
    [CustomPropertyDrawer(typeof(GameObjectConfig.CustomRotation))]
    public class CustomRotationDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //Get the class's serialized properties
            SerializedProperty rotationConfig = 
                property.FindPropertyRelative("rotationConfig");
            SerializedProperty custom = property.FindPropertyRelative("custom");

            EditorGUI.BeginProperty(position, label, property);

            //Set the rectangle of the scale rotation enum
            position = position.PushVertical(0).SetHeight(
                EditorGUI.GetPropertyHeight(rotationConfig));

            //Display the rotation config enum
            EditorGUI.PropertyField(position, rotationConfig);

            //Set the rectangle of the custom rotatiom
            position = position.PushVertical(EditorGUI.GetPropertyHeight(rotationConfig)).SetHeight(
                EditorGUI.GetPropertyHeight(custom));

            //Check to see if the rotation config is not set to "None"
            if (rotationConfig.enumValueIndex != 0)
            {
                //Display the custom rotation
                EditorGUI.PropertyField(position.SqueezeLeft(16f), custom);
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float propertyHeight = 16.0f;
            if (property.FindPropertyRelative("rotationConfig").enumValueIndex != 0)
            {
                propertyHeight += 16.0f;
            }
            return propertyHeight;
        }
    }
}
#endif