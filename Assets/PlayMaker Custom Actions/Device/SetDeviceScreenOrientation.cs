// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Specifies logical orientation of the screen. Currently screen orientation is only relevant on mobile platforms")]
	public class SetDeviceScreenOrientation : FsmStateAction
	{
		[Tooltip("Amount of acceleration under which to trigger the event. Use low numbers.")]
		public ScreenOrientation orientation;
		
		public override void Reset()
		{
			orientation = ScreenOrientation.Landscape;
		}
		
		public override void OnEnter()
		{
			Screen.orientation = orientation;
			Finish();
		}

	}
}
