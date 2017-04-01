#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Common.Extensions;

namespace CustomPropertyDrawers
{
    [CustomPropertyDrawer(typeof(GameObjectConfig.CustomVelocity))]
    public class CustomVelocityDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //Get the class's serialized properties
            SerializedProperty velocityConfig = property.FindPropertyRelative(
                                                    "velocityConfig");
            SerializedProperty custom = property.FindPropertyRelative("custom");
            SerializedProperty rotate = property.FindPropertyRelative(
                                            "rotateFromRotation");

            EditorGUI.BeginProperty(position, label, property);

            //Set the rectangle of the scale config enum
            position = position.AddToY(0).SetHeight(
                EditorGUI.GetPropertyHeight(velocityConfig));

            //Display the scale config enum
            EditorGUI.PropertyField(position, velocityConfig);

            //Set the rectangle of the custom scale
            position = position.AddToY(EditorGUI.GetPropertyHeight(velocityConfig)).SetHeight(
                EditorGUI.GetPropertyHeight(custom));

            //Check to see if the scale config is not set to "None"
            if (velocityConfig.enumValueIndex != 0)
            {
                //Display the custom scale
                EditorGUI.PropertyField(position.SqueezeLeft(16f), custom);

                //Set the rectangle of the rotation boolean
                position = position.AddToY(EditorGUI.GetPropertyHeight(custom)).SetHeight(EditorGUI.GetPropertyHeight(custom));

                // Display the rotate toggle
                rotate.boolValue = EditorGUI.ToggleLeft(
                    position.SqueezeLeft(16f),
                    GUIUtils.PrettyPrintVariableName(rotate.name),
                    rotate.boolValue);
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float propertyHeight = 16.0f;
            if (property.FindPropertyRelative("velocityConfig").enumValueIndex != 0)
            {
                propertyHeight += 32.0f;
            }
            return propertyHeight;
        }
    }
}
#endif