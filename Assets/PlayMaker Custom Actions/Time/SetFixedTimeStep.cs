// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Scales time: 1 = normal, 0.5 = half speed, 2 = double speed.")]
	public class SetFixedTimeStep : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Set the Timestep to a vaule")]
		public FsmFloat timeStep;


		[Tooltip("Repeat every frame. Useful when animating the value.")]
		public bool everyFrame;

		public override void Reset()
		{
            timeStep = 1.0f;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
            DoSetFixedDeltaTime();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		public override void OnUpdate()
		{
            DoSetFixedDeltaTime();
		}
		
		void DoSetFixedDeltaTime()
		{
		    Time.fixedDeltaTime = timeStep.Value;
		}
	}
}