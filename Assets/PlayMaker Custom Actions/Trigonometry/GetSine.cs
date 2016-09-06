// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Trigonometry")]
	[Tooltip("Get the sine. You can use degrees, simply check on the DegToRad conversion")]
	public class GetSine : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The angle. Note: You can use degrees, simply check DegtoRad if the angle is expressed in degrees.")]
		public FsmFloat angle;
		
		[Tooltip("Check on if the angle is expressed in degrees.")]
		public FsmBool DegToRad;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The angle tan")]
		public FsmFloat result;
				
		
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			angle = null;
			DegToRad = true;
			everyFrame = false;
			result = null;
		}

		public override void OnEnter()
		{
			DoSine();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSine();
		}
		
		void DoSine()
		{
			float _angle = angle.Value;
			if (DegToRad.Value)
			{
				_angle = _angle*Mathf.Deg2Rad;
			}
			result.Value = Mathf.Sin(_angle);
		}
	}
}
