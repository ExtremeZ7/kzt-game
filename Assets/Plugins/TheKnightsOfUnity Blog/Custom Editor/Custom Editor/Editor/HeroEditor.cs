using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(Hero))]
public class HeroEditor : Editor
{
	SerializedProperty health;
	SerializedProperty maxHealth;
	SerializedProperty withShield;
	SerializedProperty shield;

	void OnEnable ()
	{
		health = serializedObject.FindProperty ("health");
		maxHealth = serializedObject.FindProperty ("maxHealth");
		withShield = serializedObject.FindProperty ("withShield");
		shield = serializedObject.FindProperty ("shield");
	}

	public override void OnInspectorGUI ()
	{
		serializedObject.UpdateIfDirtyOrScript ();

		EditorGUILayout.IntSlider (health, 0, maxHealth.intValue, "Health");

		EditorGUI.indentLevel++;
		EditorGUILayout.PropertyField (maxHealth, new GUIContent ("Max Health"));
		EditorGUI.indentLevel--;

		EditorGUILayout.Space ();

		EditorGUILayout.PropertyField (withShield, new GUIContent ("With Shield"));

		if (withShield.boolValue) {
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField (shield, new GUIContent ("Shield"));
			EditorGUI.indentLevel--;
		}

		serializedObject.ApplyModifiedProperties ();
	}
}