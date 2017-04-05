using System;
using System.Collections;

using UnityEditor;
using UnityEngine;

namespace HutongGames.PlayMaker.Ecosystem.UI
{
	[CustomEditor(typeof(PlaymakerDpCanvasScaler))]
	public class PlaymakerDpCanvasScalerInspector : UnityEditor.Editor {
		
		public override void OnInspectorGUI()
		{
			
			//PlaymakerDpCanvasScaler _target = (PlaymakerDpCanvasScaler)this.target;

			DrawDefaultInspector();
		}
	}
}
