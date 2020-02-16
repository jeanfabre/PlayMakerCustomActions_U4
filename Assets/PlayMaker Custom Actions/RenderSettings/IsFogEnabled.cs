// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Check if a the fog is enabled")]
	public class IsFogEnabled : FsmStateAction
	{
		[Tooltip("Event sent is Fog is enabled")]
		public FsmEvent trueEvent;
		
		[Tooltip("Event sent is Fog is disabled")]
		public FsmEvent falseEvent;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("True is fog is enabled.")]
		public FsmBool store;
		
		[Tooltip("Repeat every frame. Useful if the Enable Fog setting is changing.")]
		public bool everyFrame;
		
		public override void Reset()
		{
			trueEvent = null;
			falseEvent = null;
			store = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoIsFogEnabled();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoIsFogEnabled();
		}

		void DoIsFogEnabled()
		{
			store.Value = RenderSettings.fog;

			Fsm.Event(RenderSettings.fog ? trueEvent : falseEvent);
		}
	}
}

