using UnityEditor;
using UnityEngine;
using HutongGames.PlayMaker;

using System.Collections;

/// <summary>
/// PlayMaker Fsm State Synchronizer inspector.
/// This is only to remove the first script pointer field, which is totally unnecessary and takes vertical space in the editor
/// </summary>
[CustomEditor(typeof(PlayMakerFsmStateSynchronizer))]
public class PlayMakerFsmStateSynchronizerInspector : Editor {
	
	public override void OnInspectorGUI()
	{
		serializedObject.UpdateIfDirtyOrScript();
		
		SerializedProperty fsmSource = serializedObject.FindProperty("fsmSource");
		EditorGUILayout.PropertyField(fsmSource);
		
		SerializedProperty fsmTarget = serializedObject.FindProperty("fsmTarget");
		EditorGUILayout.PropertyField(fsmTarget);

		SerializedProperty matchType = serializedObject.FindProperty("matchType");
		EditorGUILayout.PropertyField(matchType);

		SerializedProperty throwSynchErrors = serializedObject.FindProperty("throwSynchErrors");
		EditorGUILayout.PropertyField(throwSynchErrors);

		SerializedProperty debug = serializedObject.FindProperty("debug");
		EditorGUILayout.PropertyField(debug);

		SerializedProperty everyFrame = serializedObject.FindProperty("everyFrame");
		EditorGUILayout.PropertyField(everyFrame);

		serializedObject.ApplyModifiedProperties();
	}
	
	
}
