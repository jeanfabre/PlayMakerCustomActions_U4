// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 

using System;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomActionEditor(typeof(GetCSS3Color))]
public class GetCSS3ColorEditor : CustomActionEditor
{
	GetCSS3Color _action;

	string[] _colorKeys;

	public override bool OnGUI()
	{
		bool _changed = false;
		_action = (GetCSS3Color)target;

		if (_colorKeys==null)
		{
			_colorKeys = GetCSS3Color.CSS3Colors.Keys.ToArray();
			ArrayUtility.Insert<string>(ref _colorKeys,0,"Use Variable");
		}

		int _choiceIndex = EditorGUILayout.Popup("Selection",_action.colorindex+1, _colorKeys) -1;

		if (_choiceIndex!= _action.colorindex)
		{
			if (_action.colorindex == -1)
			{
				_action.colorName.UseVariable = false;
			}

			_changed = true;
			_action.colorindex = _choiceIndex;
			if (_choiceIndex>=0)
			{
				_action.colorName.Value = _colorKeys[_choiceIndex+1];

			}

		}

		if (_choiceIndex==-1)
		{
			EditField("colorName");
		}


		EditField("alpha");
		EditField("storeResult");
		EditField("everyframe");


		return GUI.changed || _changed;
	}
	
	
}