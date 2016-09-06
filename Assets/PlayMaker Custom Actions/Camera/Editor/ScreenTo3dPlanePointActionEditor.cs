// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomActionEditor(typeof(ScreenTo3dPlanePoint))]
public class ScreenTo3dPlanePointActionEditor : CustomActionEditor
{
	public override bool OnGUI()
	{

		ScreenTo3dPlanePoint _target = (ScreenTo3dPlanePoint)target;
		
		EditField("camera");

		EditField("groundAsTransform");

		if (_target.groundAsTransform)
		{

			EditField("groundTransform");
		}else{

			EditField("groundPoint");
			EditField("groundNormal");
		}

		EditField("screenPoint");
		EditField("worldPointResult");
		EditField("everyFrame");
		
		return GUI.changed;
	}
	

}
