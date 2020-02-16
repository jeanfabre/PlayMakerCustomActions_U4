// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Set the name of the next control. Useful to control focus on textfields for example")]
	public class GuiSetNextControlName : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The the name of the next control")]		
		public FsmString controlName;

		public override void Reset()
		{
			controlName = "control 1";
		}
		
		
		public override void OnGUI()
		{
			GUI.SetNextControlName(controlName.Value);
		}
	}
}
