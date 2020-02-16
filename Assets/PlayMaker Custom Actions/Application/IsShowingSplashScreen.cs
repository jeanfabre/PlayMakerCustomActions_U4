// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("Sends Events if Is Showing Splash Screen or not")]
	public class IsShowingSplashScreen : FsmStateAction
	{
        [Tooltip("Event to send if is Showing Splash Screen is True.")]
		public FsmEvent isTrue;

        [Tooltip("Event to send if is Showing Splash Screen is false.")]
		public FsmEvent isFalse;

        [Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		public override void Reset()
		{
			isTrue = null;
			isFalse = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
#if UNITY_5 && !UNITY_5_0 && !UNITY_5_1
			Fsm.Event(Application.isShowingSplashScreen ? isTrue : isFalse);
#else
	 Debug.Log("This Action only works on Unity 5.2 and upwards");
#endif
			
			if (!everyFrame)
			{
			    Finish();
			}
		}
		
		public override void OnUpdate()
		{
#if UNITY_5 &&  !UNITY_5_0 && !UNITY_5_1
			Fsm.Event(Application.isShowingSplashScreen ? isTrue : isFalse);
#else
			Debug.Log("This Action only works on Unity 5.2 and upwards");
			Finish();
#endif
		}
	}
}
