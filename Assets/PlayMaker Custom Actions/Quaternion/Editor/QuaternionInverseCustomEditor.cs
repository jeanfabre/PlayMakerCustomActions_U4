using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;


[CustomActionEditor(typeof(QuaternionInverse))]
public class QuaternionInverseCustomEditor : QuaternionCustomEditorBase
{

    public override bool OnGUI()
    {
		EditField("rotation");
		EditField("result");
		
		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
    }


}
