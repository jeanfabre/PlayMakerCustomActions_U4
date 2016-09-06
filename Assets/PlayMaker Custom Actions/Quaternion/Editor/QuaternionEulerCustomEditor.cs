using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;


[CustomActionEditor(typeof(QuaternionEuler))]
public class QuaternionEulerCustomEditor : QuaternionCustomEditorBase
{

    public override bool OnGUI()
    {
		EditField("eulerAngles");
		EditField("result");
		
		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
    }


}
