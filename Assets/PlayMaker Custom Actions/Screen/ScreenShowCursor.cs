// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// __ECO__ __ACTION__

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Screen)]
	[Tooltip("Show or hide the mouse Cursor.")]
	public class ScreenSowCursor : FsmStateAction
	{
		[Tooltip("The flag to show or hide the cursor")]
		public FsmBool showCursor;

		[Tooltip("Revert the cursor to its visibility before the action began")]
		public bool resetOnExit;

		[Tooltip("Use this to animate the cursor visibility over time")]
		public bool everyFrame;

		bool _orig;

		public override void Reset()
		{
			showCursor = false;
			resetOnExit = false;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{

			if (resetOnExit)
			{
				_orig = Screen.showCursor;
			}

			DoAction();

			if(!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoAction();
		}

		void DoAction()
		{
			Screen.showCursor = showCursor.Value;
		}

		public override void OnExit()
		{
			if (resetOnExit)
			{
				Screen.showCursor = _orig;
			}
		}

	}
}
