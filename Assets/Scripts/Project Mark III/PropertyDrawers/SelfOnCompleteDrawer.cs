#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CustomPropertyDrawers
{
    [CustomPropertyDrawer(typeof(CreateObjectsOnTrigger.SelfOnComplete))]
    public class SelfOnCompleteDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //Get the class's serialized properties
            SerializedProperty actionOnComplete = property.FindPropertyRelative("actionOnComplete");
            SerializedProperty depth = property.FindPropertyRelative("parentDepth");

            if (depth.intValue < 0)
            {
                depth.intValue = 0;
            }

            EditorGUI.BeginProperty(position, label, property);

            //Set the rectangle of the actionOnComplete enum
            position = position.PushVertical(0).SetHeight(
                EditorGUI.GetPropertyHeight(actionOnComplete));

            //Display the actionOnComplete enum
            EditorGUI.PropertyField(position, actionOnComplete);

            //Set the rectangle of the parent depth
            position = position.PushVertical(EditorGUI.GetPropertyHeight(actionOnComplete)).SetHeight(
                EditorGUI.GetPropertyHeight(depth));

            //Check to see if the actionOnComplete is not set to "Nothing"
            if (actionOnComplete.enumValueIndex != 0)
            {
                //Display the parent depth
                EditorGUI.PropertyField(position.SqueezeLeft(16f), depth);
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float propertyHeight = 16.0f;
            if (property.FindPropertyRelative("actionOnComplete").enumValueIndex != 0)
            {
                propertyHeight += 16.0f;
            }
            return propertyHeight;
        }
    }
}
#endif