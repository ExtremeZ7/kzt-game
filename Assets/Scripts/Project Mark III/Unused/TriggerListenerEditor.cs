/*using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(TriggerListener))]
public class TriggerListenerEditor : Editor
{
	SerializedProperty switches;
	SerializedProperty names;
	SerializedProperty listenToActivation;
	SerializedProperty listenToFirstFrameOnDeactivate;
	SerializedProperty flipActivation;
	SerializedProperty objectConfig;

	void OnEnable ()
	{
		switches = serializedObject.FindProperty ("switches");
		names = serializedObject.FindProperty ("names");
		listenToActivation = serializedObject.FindProperty ("listenToActivation");
		listenToFirstFrameOnDeactivate = serializedObject.FindProperty ("listenToFirstFrameOnDeactivate");
		flipActivation = serializedObject.FindProperty ("flipActivation");
		objectConfig = serializedObject.FindProperty ("objectConfigurations");
	}

	public override void OnInspectorGUI ()
	{
		serializedObject.UpdateIfDirtyOrScript ();

		EditorGUILayout.PropertyField (switches, new GUIContent ("Trigger Switches"));
		EditorGUILayout.PropertyField (names, new GUIContent ("Names"));
		EditorGUILayout.PropertyField (listenToActivation, new GUIContent ("Listen To Activation"));
		EditorGUILayout.ToggleLeft (listenToFirstFrameOnDeactivate.displayName, listenToFirstFrameOnDeactivate.boolValue);
		EditorGUILayout.ToggleLeft (flipActivation.displayName, flipActivation.boolValue);
		EditorGUILayout.Space ();
		ShowRelativeProperty (objectConfig, "newName");


		//serializedObject.ApplyModifiedProperties ();
	}

	// Show child property of parent serializedProperty
	void ShowRelativeProperty (SerializedProperty serializedProperty, string propertyName)
	{

		SerializedProperty property = serializedProperty.FindPropertyRelative (propertyName);
		if (property != null) {
			EditorGUI.indentLevel++;
			EditorGUI.BeginChangeCheck ();
			EditorGUILayout.PropertyField (property, true);
			if (EditorGUI.EndChangeCheck ())
				serializedObject.ApplyModifiedProperties ();
			//EditorGUIUtility.LookLikeControls ();
			EditorGUI.indentLevel--;
		}
	}
}
*/