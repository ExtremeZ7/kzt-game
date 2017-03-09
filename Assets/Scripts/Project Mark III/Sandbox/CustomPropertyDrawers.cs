using UnityEngine;
using UnityEditor;
using System.Linq;

namespace CustomPropertyDrawers
{
    [CustomPropertyDrawer(typeof(GameObjectConfig.CustomScale))]
    public class CustomScale : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            SerializedProperty scaleConfig = property.FindPropertyRelative("scaleConfig");
            SerializedProperty custom = property.FindPropertyRelative("custom");

            EditorGUI.BeginProperty(position, label, property);

            position = position.MoveDown(0, 
                EditorGUI.GetPropertyHeight(scaleConfig));

            EditorGUI.PropertyField(position, scaleConfig);

            position = position.MoveDown(EditorGUI.GetPropertyHeight(scaleConfig), 
                EditorGUI.GetPropertyHeight(custom));

            if (scaleConfig.enumValueIndex != 0)
            {
                EditorGUI.PropertyField(position, custom);
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

    public class ToggleLeft : PropertyAttribute
    {
    }

    [CustomPropertyDrawer(typeof(ToggleLeft))]
    public class ToggleLeftDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            property.boolValue = EditorGUI.ToggleLeft(position, 
                GUIUtils.PrettyPrintVariableName(property.name), property.boolValue);

            EditorGUI.EndProperty();
        }
    }

    public class IncrementalChange : PropertyAttribute
    {
        public float increment;

        public IncrementalChange(float increment)
        {
            this.increment = increment;
        }
    }

    [CustomPropertyDrawer(typeof(IncrementalChange))]
    public class IncrementalChangeDrawer : PropertyDrawer
    {
        const int textHeight = 16;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            int origIndent = EditorGUI.indentLevel;

            // Retrieve the increment from the attribute
            //
            float increment = ((IncrementalChange)attribute).increment;

            // Hidden slider allows us to replicate the slide over name to change the value
            //
            EditorGUI.Slider(new Rect(position.xMin, position.yMin, position.width / 3f, textHeight),
                property, property.floatValue - 200, property.floatValue + 200);

            /*EditorGUI.LabelField (new Rect (position.xMin, position.yMin, position.width / 3f, textHeight),
				GUIUtils.PrettyPrintVariableName (property.name));*/

            property.floatValue = EditorGUI.FloatField(
                new Rect(position.xMax / 3f, position.yMin,
                    position.width - (position.width / 3f),
                    textHeight), Mathf.Round(property.floatValue / increment) * increment);

            EditorGUI.indentLevel = origIndent;
            EditorGUI.EndProperty();
        }
    }

    public class PopupAttribute : PropertyAttribute
    {
        public string[] items;

        public PopupAttribute(params string[] values)
        {
            items = values;
        }
    }

    [CustomPropertyDrawer(typeof(PopupAttribute))]
    public class PopupDrawer : PropertyDrawer
    {
        const int textHeight = 16;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            int origIndent = EditorGUI.indentLevel;
            string[] values = ((PopupAttribute)attribute).items;
            int index = 0;
            for (; index < values.Count() - 1; index++)
            {
                if (values[index].Equals(property.stringValue))
                    break;
            }

            EditorGUI.LabelField(new Rect(position.xMin, position.yMin, position.width / 3f, textHeight),
                GUIUtils.PrettyPrintVariableName(property.name));

            index = EditorGUI.Popup(
                new Rect(position.xMax / 3f,
                    position.yMin, position.width - (position.width / 3f),
                    textHeight), index, values);

            if (index < values.Count())
                property.stringValue = values[index];
			
            EditorGUI.indentLevel = origIndent;
            EditorGUI.EndProperty();
        }
    }

    [CustomPropertyDrawer(typeof(Ingredient))]
    public class IngredientDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property menas that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            //
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be intended
            //
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            //
            var amountRect = new Rect(position.x, position.y, 30, position.height);
            var unitRect = new Rect(position.x + 35, position.y, 50, position.height);
            var nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

            // Draw fields - pas GUIContent.none to each so they are drawn without labels
            //
            EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
            EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("unit"), GUIContent.none);
            EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);

            // Set indent back to what it was
            //
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
