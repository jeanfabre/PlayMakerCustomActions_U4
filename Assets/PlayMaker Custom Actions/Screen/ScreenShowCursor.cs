// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Screen)]
	[Tooltip("Show or hide the mouse Cursor.")]
	public class ScreenShowCursor : FsmStateAction
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
				#if UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7
				_orig = Screen.showCursor;
				#else
				_orig = Cursor.visible;
				#endif
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
			#if UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7
			Screen.showCursor = showCursor.Value;
			#else
				Cursor.visible = showCursor.Value;
			#endif
		}

		public override void OnExit()
		{
			if (resetOnExit)
			{
				#if UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7
				Screen.showCursor = _orig;
				#else
				Cursor.visible = _orig;
				#endif
			}
		}

	}
}
