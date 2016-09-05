using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;


[CustomActionEditor(typeof(QuaternionLerp))]
public class QuaternionLerpCustomEditor : QuaternionCustomEditorBase
{

    public override bool OnGUI()
    {
		EditField("fromQuaternion");
		EditField("toQuaternion");
		EditField("amount");
		EditField("lerpAgainstDeltaTime");
		EditField("storeResult");
		
		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
    }


}
