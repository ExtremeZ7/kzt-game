#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CustomPropertyDrawers
{
    [CustomPropertyDrawer(typeof(CreateObjectsOnTrigger.DelayedObject))]
    public class DelayedObjectDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
                                   GUIContent label)
        {
            SerializedProperty obj = property.FindPropertyRelative("gameObject");
            SerializedProperty delay = property.FindPropertyRelative("delay");

            // Using BeginProperty / EndProperty on the parent property means
            // that prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            //
            position = EditorGUI.PrefixLabel(position,
                GUIUtility.GetControlID(FocusType.Passive),
                new GUIContent("Object | Delay"));

            // Don't make child fields be intended
            //
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            //
            var objRect = new Rect(position.x, position.y,
                              position.width - 30, position.height);
            var delayRect = new Rect(position.x + position.width - 25, position.y,
                                25, position.height);

            if (delay.floatValue < 0f)
            {
                delay.floatValue = 0f;
            }

            // Draw fields - pass GUIContent.none to each so they are drawn
            // without labels
            EditorGUI.PropertyField(objRect, obj, GUIContent.none);
            EditorGUI.PropertyField(delayRect, delay, GUIContent.none);

            // Set indent back to what it was
            //
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
#endif
