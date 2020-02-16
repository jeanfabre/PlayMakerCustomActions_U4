// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("A power saving setting, allowing the screen to dim some time after the last active user interaction. 0 means never sleep, -1 means revert to system settings, above zero values are in seconds. Most useful for handheld devices, allowing OS to preserve battery life in most efficient ways. Does nothing on non-handheld devices. SleepTimeout is measured in seconds. The default value varies from platform to platform, generally being non-zero.")]
	public class SetSleepTimeOut : FsmStateAction
	{
		
		[Tooltip("0 means never sleep, -1 means revert to system settings, above zero values are in seconds.")]
		public FsmInt sleepTimeout;
		
		public override void Reset()
		{
			sleepTimeout = 0;
		}

		public override void OnEnter()
		{
			if (sleepTimeout.Value<0)
			{
				Screen.sleepTimeout =SleepTimeout.SystemSetting;
			}else if (sleepTimeout.Value==0)
			{
				Screen.sleepTimeout = SleepTimeout.NeverSleep;
			}else
			{	
				Screen.sleepTimeout = sleepTimeout.Value;
			}
			
			Finish();
		}
	}
}
