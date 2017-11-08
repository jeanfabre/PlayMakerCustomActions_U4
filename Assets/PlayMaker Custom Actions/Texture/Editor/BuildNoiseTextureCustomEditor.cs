using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMakerEditor
{
	[CustomActionEditor(typeof(BuildNoiseTexture))]
	public class BuildNoiseTextureCustomEditor : CustomActionEditor
	{
		BuildNoiseTexture _action;

		public override bool OnGUI ()
		{
			_action = (BuildNoiseTexture)target;

			EditField ("gameObject");
			EditField ("resolution");
			EditField ("frequency");
			EditField ("octaves");
			EditField ("lacunarity");
			EditField ("persistence");
			EditField ("dimensions");
			EditField ("type");

	
			//SerializedObject serializedGradient = new SerializedObject(_action);

			//this.
			//SerializedProperty colorGradient = serializedGradient.FindProperty("coloring");

			bool _changed;


			EditorGUI.BeginChangeCheck();
//EditorGUILayout.PropertyField(colorGradient, true, null);
			if(EditorGUI.EndChangeCheck())
			{
			//	serializedGradient.ApplyModifiedProperties();
			}

			return false;
		}


	}
}
