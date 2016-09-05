using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;


[CustomActionEditor(typeof(GetQuaternionFromRotation))]
public class GetQuaternionFromRotationCustomEditor : QuaternionCustomEditorBase
{

    public override bool OnGUI()
    {
		EditField("fromDirection");
		EditField("toDirection");
		EditField("result");
		
		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
    }


}
