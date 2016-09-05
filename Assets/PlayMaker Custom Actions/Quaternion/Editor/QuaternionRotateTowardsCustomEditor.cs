using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;


[CustomActionEditor(typeof(QuaternionRotateTowards))]
public class QuaternionRotateTowardsCustomEditor : QuaternionCustomEditorBase
{

    public override bool OnGUI()
    {
		EditField("fromQuaternion");
		EditField("toQuaternion");
		EditField("maxDegreesDelta");
		EditField("storeResult");
		
		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
    }


}
