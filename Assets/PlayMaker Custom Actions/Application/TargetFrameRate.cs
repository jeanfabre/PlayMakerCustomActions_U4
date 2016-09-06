// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
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
