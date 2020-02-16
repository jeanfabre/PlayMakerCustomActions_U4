// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 

using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomActionEditor(typeof(FsmVector4Action))]
public class FsmVector4ActionEditor : CustomActionEditor
{
    public override bool OnGUI()
    {
		bool edited = false;
		FsmVector4Action _target = (FsmVector4Action)target;
		
		_target.vector4 = GUILayoutPlayMakerVector4Field("Vector4",_target.vector4,out edited);
		
		EditField("storeVector4");
		EditField("everyframe");
		
		return GUI.changed || edited;
    }
	
	private FsmQuaternion GUILayoutPlayMakerVector4Field(string label,FsmQuaternion fsmVector4,out bool changed)
	{
		if (fsmVector4==null)
		{
			fsmVector4 = new FsmQuaternion();
		}

		bool edited = false;
		
		Vector4 _source = new Vector4(fsmVector4.Value.x,fsmVector4.Value.y,fsmVector4.Value.z,fsmVector4.Value.w);
		Vector4 _new =	EditorGUILayout.Vector4Field(label,_source);
		if (_new!=_source)
		{
			Quaternion _quat = new Quaternion(_new.x,_new.y,_new.z,_new.w); 
			fsmVector4 = _quat;
			edited = true;
		}
		
		changed = GUI.changed || edited;
		
		return fsmVector4;
	}

}
