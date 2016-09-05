using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;


[CustomActionEditor(typeof(QuaternionSlerp))]
public class QuaternionSlerpCustomEditor : QuaternionCustomEditorBase
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
