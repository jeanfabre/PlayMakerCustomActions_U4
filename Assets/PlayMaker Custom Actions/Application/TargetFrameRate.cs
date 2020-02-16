// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("Sets the target frame rate")]
	public class TargetFrameRate : FsmStateAction {
	
	    [RequiredField]
		[Tooltip("The target frame rate")]
		public FsmInt targetFrameRate;
	
		public override void Reset()
		{
			targetFrameRate = 30;
		}

		public override void OnEnter()
		{
			
			Application.targetFrameRate = targetFrameRate.Value;
			Finish();
		}


	}
}
