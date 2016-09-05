using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;


[CustomActionEditor(typeof(GetQuaternionEulerAngles))]
public class GetQuaternionEulerAnglesCustomEditor : QuaternionCustomEditorBase
{

    public override bool OnGUI()
    {
		EditField("quaternion");
		EditField("eulerAngles");
		
		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
    }


}
