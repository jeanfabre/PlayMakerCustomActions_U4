// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Move keyboard focus to a named control.")]
	public class GuiFocusControl: FsmStateAction
	{
		[RequiredField]
		[Tooltip("The the name of the control to focus")]		
		public FsmString controlName;
		
		//public bool OnlyForce
		public override void Reset()
		{
			controlName = "control 1";
		}
		
		public override void OnGUI()
		{
			
			if (GUI.GetNameOfFocusedControl() == string.Empty)
			{
				GUI.FocusControl(controlName.Value);
			}
		}
	}
}
