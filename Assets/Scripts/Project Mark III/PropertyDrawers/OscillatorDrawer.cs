#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CustomPropertyDrawers
{
    [CustomPropertyDrawer(typeof(Oscillator))]
    public class OscillatorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty curveProp =
                property.FindPropertyRelative("curve");
            SerializedProperty cycleProp =
                property.FindPropertyRelative("cycleTime");
            SerializedProperty tVarProp =
                property.FindPropertyRelative("timeVariation");
            SerializedProperty delayProp =
                property.FindPropertyRelative("delay");
            SerializedProperty phaseProp =
                property.FindPropertyRelative("phase");
            SerializedProperty pVarProp =
                property.FindPropertyRelative("phaseVariation");
            SerializedProperty dVarProp =
                property.FindPropertyRelative("delayVariation");
            SerializedProperty magnProp =
                property.FindPropertyRelative("magnitude");
            SerializedProperty mVarProp =
                property.FindPropertyRelative("magnitudeVariation");
            
            SerializedProperty vOffsetProp =
                property.FindPropertyRelative("valueOffset");
            SerializedProperty vVariationProp =
                property.FindPropertyRelative("valueVariation");

            cycleProp.floatValue = cycleProp.floatValue.RestrictPositive();
            delayProp.floatValue = delayProp.floatValue.RestrictPositive();
            phaseProp.floatValue = phaseProp.floatValue.RestrictRange(0f, 1f);
            vVariationProp.floatValue = vVariationProp.floatValue.RestrictPositive();

            // Using BeginProperty / EndProperty on the parent property menas that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            //
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            Rect curveRect = position.SetHeight(16f);

            Rect cycleLabel = position
                .PushDown(16f).SetHeight(16f).SetWidth(32f);

            Rect cycleRect = position.SqueezeLeft(cycleLabel.width)
                .PushDown(16f).SetHeight(16f).SetWidth(32f);

            Rect delayLabel = position.SqueezeLeft(cycleLabel.width + cycleRect.width)
                .PushDown(16f).SetHeight(16f).SetWidth(40f);
            
            Rect delayRect = position.SqueezeLeft(
                                 cycleLabel.width + cycleRect.width + delayLabel.width)
                .PushDown(16f).SetHeight(16f).SetWidth(32f);

            Rect magnLabel = position.SqueezeLeft(
                                 cycleLabel.width + cycleRect.width + delayLabel.width + delayRect.width)
                .PushDown(16f).SetHeight(16f).SetWidth(40f);

            Rect magnRect = position.SqueezeLeft(
                                cycleLabel.width + cycleRect.width + delayLabel.width + delayRect.width + magnLabel.width)
                .PushDown(16f).SetHeight(16f);

            Rect phaseLabel = position
                .PushDown(32f).SetHeight(16f).SetWidth(40f);

            Rect phaseRect = position.SqueezeLeft(phaseLabel.width)
                .PushDown(32f).SetWidth(32).SetHeight(16f);

            Rect vOffsetLabel = position.SqueezeLeft(
                                    phaseLabel.width + phaseRect.width)
                .PushDown(32f).SetWidth(40f).SetHeight(16f);

            Rect vOffsetRect = position.SqueezeLeft(
                                   phaseLabel.width + phaseRect.width + vOffsetLabel.width)
                .PushDown(32f).SetWidth(32f).SetHeight(16f);

            Rect vVariationLabel = position.SqueezeLeft(
                                       phaseLabel.width + phaseRect.width + vOffsetLabel.width + vOffsetRect.width)
                .PushDown(32f).SetWidth(24f).SetHeight(16f);
            
            Rect vVariationRect = position.SqueezeLeft(
                                      phaseLabel.width + phaseRect.width + vOffsetLabel.width + vOffsetRect.width + vVariationLabel.width)
                .PushDown(32f).SetHeight(16f);

            EditorGUI.PropertyField(curveRect, curveProp, GUIContent.none);
            EditorGUI.LabelField(cycleLabel, "Time");
            EditorGUI.PropertyField(cycleRect, cycleProp, GUIContent.none);
            EditorGUI.LabelField(delayLabel, "Delay");
            EditorGUI.PropertyField(delayRect, delayProp, GUIContent.none);
            EditorGUI.LabelField(magnLabel, "Magn");
            EditorGUI.PropertyField(magnRect, magnProp, GUIContent.none);
            EditorGUI.LabelField(phaseLabel, "Phase");
            EditorGUI.PropertyField(phaseRect, phaseProp, GUIContent.none);
            EditorGUI.LabelField(vOffsetLabel, "Offset");
            EditorGUI.PropertyField(vOffsetRect, vOffsetProp, GUIContent.none);
            EditorGUI.LabelField(vVariationLabel, "Var");
            EditorGUI.PropertyField(vVariationRect, vVariationProp,
                GUIContent.none);

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property,
                                                GUIContent label)
        {
            return 48f;
        }
    }
}
#endif