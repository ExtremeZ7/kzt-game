#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace CustomPropertyDrawers
{
    [CustomPropertyDrawer(typeof(Oscillator))]
    public class OscillatorDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property,
                                                GUIContent label)
        {
            return 48f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty curveProp =
                property.FindPropertyRelative("curve");
            
            SerializedProperty timeProp =
                property.FindPropertyRelative("cycleTime")
                    .FindPropertyRelative("value");
            SerializedProperty tVarProp =
                property.FindPropertyRelative("cycleTime")
                .FindPropertyRelative("variation");

            SerializedProperty delayProp =
                property.FindPropertyRelative("delay")
                .FindPropertyRelative("value");
            SerializedProperty dVarProp =
                property.FindPropertyRelative("delay")
                    .FindPropertyRelative("variation");

            SerializedProperty phaseProp =
                property.FindPropertyRelative("phase")
                    .FindPropertyRelative("value");
            SerializedProperty pVarProp =
                property.FindPropertyRelative("phase")
                    .FindPropertyRelative("variation");
            
            SerializedProperty magnProp =
                property.FindPropertyRelative("magnitude")
                    .FindPropertyRelative("value");
            SerializedProperty mVarProp =
                property.FindPropertyRelative("magnitude")
                    .FindPropertyRelative("variation");
            
            SerializedProperty offsetProp =
                property.FindPropertyRelative("valueOffset")
                    .FindPropertyRelative("value");
            SerializedProperty oVarProp =
                property.FindPropertyRelative("valueOffset")
                    .FindPropertyRelative("variation");

            //phaseProp.floatValue = phaseProp.floatValue.RestrictRange(0f, 1f);
            pVarProp.floatValue = pVarProp.floatValue.RestrictPositive();
            timeProp.floatValue = timeProp.floatValue.RestrictPositive();
            tVarProp.floatValue = tVarProp.floatValue.RestrictPositive();
            delayProp.floatValue = delayProp.floatValue.RestrictPositive();
            dVarProp.floatValue = dVarProp.floatValue.RestrictPositive();           
            mVarProp.floatValue = mVarProp.floatValue.RestrictPositive();
            oVarProp.floatValue = oVarProp.floatValue.RestrictPositive();

            // Using BeginProperty / EndProperty on the parent property menas that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            //
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            float squeezeWidth;
            const float labelWidth = 32f;

            position = position.SetHeight(16f);

            Rect curveRect = position
                .SetWidth(position.width / 2);
            squeezeWidth = curveRect.width;

            Rect phaseLabel = position.SqueezeLeft(squeezeWidth)
                .SetWidth(labelWidth);
            squeezeWidth += phaseLabel.width;

            Rect phaseRect = position.SqueezeLeft(squeezeWidth)
                .SetWidth(position.width * 3 / 4 - squeezeWidth);
            squeezeWidth += phaseRect.width;

            Rect pVarLabel = position.SqueezeLeft(squeezeWidth)
                .SetWidth(labelWidth);
            squeezeWidth += pVarLabel.width;

            Rect pVarRect = position.SqueezeLeft(squeezeWidth)
                .SetWidth(position.width - squeezeWidth);

            // Line Break

            Rect timeLabel = position
                .PushDown(16f).SetWidth(labelWidth);
            squeezeWidth = timeLabel.width;

            Rect timeRect = position.SqueezeLeft(squeezeWidth)
                .PushDown(16f).SetWidth(position.width / 4 - squeezeWidth);
            squeezeWidth += timeRect.width;

            Rect tVarLabel = position.SqueezeLeft(squeezeWidth)
                .PushDown(16f).SetWidth(labelWidth);
            squeezeWidth += tVarLabel.width;

            Rect tVarRect = position.SqueezeLeft(squeezeWidth)
                .PushDown(16f).SetWidth(position.width / 2 - squeezeWidth);
            squeezeWidth += tVarRect.width;

            Rect delayLabel = position.SqueezeLeft(squeezeWidth)
                .PushDown(16f).SetWidth(labelWidth);
            squeezeWidth += delayLabel.width;

            Rect delayRect = position.SqueezeLeft(squeezeWidth)
                .PushDown(16f).SetWidth(position.width * 3 / 4 - squeezeWidth);
            squeezeWidth += delayRect.width;

            Rect dVarLabel = position.SqueezeLeft(squeezeWidth)
                .PushDown(16f).SetWidth(labelWidth);
            squeezeWidth += dVarLabel.width;

            Rect dVarRect = position.SqueezeLeft(squeezeWidth)
                .PushDown(16f).SetWidth(position.width - squeezeWidth);

            // Line Break

            Rect magnLabel = position
                .PushDown(32f).SetWidth(labelWidth);
            squeezeWidth = magnLabel.width;

            Rect magnRect = position.SqueezeLeft(squeezeWidth)
                .PushDown(32f).SetWidth(position.width / 4 - squeezeWidth);
            squeezeWidth += magnRect.width;

            Rect mVarLabel = position.SqueezeLeft(squeezeWidth)
                .PushDown(32f).SetWidth(labelWidth);
            squeezeWidth += mVarLabel.width;

            Rect mVarRect = position.SqueezeLeft(squeezeWidth)
                .PushDown(32f).SetWidth(position.width / 2 - squeezeWidth);
            squeezeWidth += mVarRect.width;

            Rect offsetLabel = position.SqueezeLeft(squeezeWidth)
                .PushDown(32f).SetWidth(labelWidth);
            squeezeWidth += offsetLabel.width;

            Rect offsetRect = position.SqueezeLeft(squeezeWidth)
                .PushDown(32f).SetWidth(position.width * 3 / 4 - squeezeWidth);
            squeezeWidth += offsetRect.width;

            Rect oVarLabel = position.SqueezeLeft(squeezeWidth)
                .PushDown(32f).SetWidth(labelWidth);
            squeezeWidth += oVarLabel.width;

            Rect oVarRect = position.SqueezeLeft(squeezeWidth)
                .PushDown(32f).SetWidth(position.width - squeezeWidth);

            EditorGUI.PropertyField(curveRect, curveProp, GUIContent.none);
            EditorGUI.LabelField(phaseLabel, "Phs.");
            EditorGUI.Slider(phaseRect, phaseProp, 0f, 1f, GUIContent.none);
            EditorGUI.LabelField(pVarLabel, "Var.");
            EditorGUI.PropertyField(pVarRect, pVarProp, GUIContent.none);

            // Line Break

            EditorGUI.BeginDisabledGroup(
                curveProp.animationCurveValue.keys.Length < 2);

            EditorGUI.LabelField(timeLabel, "Time");
            EditorGUI.PropertyField(timeRect, timeProp, GUIContent.none);
            EditorGUI.LabelField(tVarLabel, "Var.");
            EditorGUI.PropertyField(tVarRect, tVarProp, GUIContent.none);
            EditorGUI.LabelField(delayLabel, "Del.");
            EditorGUI.PropertyField(delayRect, delayProp, GUIContent.none);
            EditorGUI.LabelField(dVarLabel, "Var.");
            EditorGUI.PropertyField(dVarRect, dVarProp, GUIContent.none);

            // Line Break

            EditorGUI.LabelField(magnLabel, "Mag.");
            EditorGUI.PropertyField(magnRect, magnProp, GUIContent.none);
            EditorGUI.LabelField(mVarLabel, "Var.");
            EditorGUI.PropertyField(mVarRect, mVarProp, GUIContent.none);

            EditorGUI.EndDisabledGroup();

            EditorGUI.LabelField(offsetLabel, "Off.");
            EditorGUI.PropertyField(offsetRect, offsetProp, GUIContent.none);
            EditorGUI.LabelField(oVarLabel, "Var.");
            EditorGUI.PropertyField(oVarRect, oVarProp, GUIContent.none);

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
#endif