using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;


[CustomActionEditor(typeof(GetQuaternionMultipliedByQuaternion))]
public class GetQuaternionMultipliedByQuaternionCustomEditor : QuaternionCustomEditorBase
{

    public override bool OnGUI()
    {
		EditField("quaternionA");
		EditField("quaternionB");
		EditField("result");
		
		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
    }


}
