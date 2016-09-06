// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("Gets the Screen size ratio (width/height).")]
	public class GetScreenSizeRatio : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Screen size ratio (width/height).")]
		public FsmFloat storeScreenSizeRatio;

		public override void Reset()
		{
			storeScreenSizeRatio = null;
		}
		
		public override void OnEnter()
		{
			storeScreenSizeRatio.Value = 1f*Screen.height/Screen.width;
			Finish();
		}
		
	}
}
