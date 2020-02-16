// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Screen)]
	[Tooltip("Checks if screen is full screen")]
	public class CheckIfFullScreen : FsmStateAction
	{
		[Tooltip("True if Application is full screen")]
		public FsmBool isFullScreen;

		[Tooltip("Event to send if application is full screen")]
		public FsmEvent isFullScreenEvent;
		
		[Tooltip("Event to send if application is not full screen")]
		public FsmEvent isNotFullScreenEvent;

		[Tooltip("Runs everyframe. Event are sent discretly, on action first execution, and then only when changes occur")]
		public bool everyFrame;


		int _cache;

		public override void Reset()
		{
			isFullScreen = null;
			isFullScreenEvent = null;
			isNotFullScreenEvent = null;
			_cache = -1;
		}
		
		public override void OnEnter()
		{
			_cache = -1;

			Check ();

			if (!everyFrame) {
				Finish();
			}
		}

		public override void OnUpdate ()
		{
			Check ();
		}

		void Check()
		{
			if (Screen.fullScreen && _cache != 1) {
				_cache = 1;
				isFullScreen.Value = true;
				Fsm.Event(isFullScreenEvent);
				return;
			}

			if (! Screen.fullScreen && _cache != 0) {
				_cache = 0;
				isFullScreen.Value = false;
				Fsm.Event(isNotFullScreenEvent);
				return;
			}
		}

	}
}
