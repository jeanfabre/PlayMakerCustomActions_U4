using UnityEngine;
using System.Collections;

using UnityEditor;

//------------------------------------------------------------------------------------------------------
// Show SendEvent and SendMessage actions in FSMs so you can track down what's going on in
// multi-FSM designs and where you call script methods more easily.
// Put the PlayMakerCommsTool.cs script in with your normal code (NOT in the Assets/Editor directory).
// Put the PlayMakerCommsToolEditor and PlayMakerCommsToolWindow in the Assets/Editor directory.
//
// To use:
// Once the code is in the right place you will find a new menu item "Comm Tool Window" under
// the Playmaker -> Tools menu. Click this, you only need to do it once in a scene. 
// You can delete the PlayMakerCommsTool object from your Hierarchy when finished.
// If you get any issue when you try it, close the Comm Tool window and from the
// Playmaker menu select the Playmaker Editor. Now try the Comm Tool window again.
//------------------------------------------------------------------------------------------------------

[CustomEditor (typeof (PlayMakerCommsTool))]
public class PlayMakerCommsToolEditor : Editor {
	static void Create(){
		GameObject gameObject = new GameObject("PlayMakerCommsTool");
		PlayMakerCommsTool s = gameObject.AddComponent<PlayMakerCommsTool>();
		s.Rebuild(true);
	}
	
	public override void OnInspectorGUI ()
	{
		PlayMakerCommsTool obj;
		
		obj = target as PlayMakerCommsTool;
		
		if (obj == null)
		{
			return;
		}
		
		base.DrawDefaultInspector();
		EditorGUILayout.BeginHorizontal ();
		
		// Rebuild the comms lines when user click the Rebuild button
		if (GUILayout.Button("Re-output Playmaker Comms (to Console)")){
			obj.Rebuild(true);
		}
		EditorGUILayout.EndHorizontal ();
	}

}
