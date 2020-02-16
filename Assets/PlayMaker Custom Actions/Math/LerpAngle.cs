// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// __ECO__ __PLAYMAKER__ __ACTION__ 
// http://hutonggames.com/playmakerforum/index.php?topic=12557.msg58654#msg58654

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

[ActionCategory (ActionCategory.Math)]
	[Tooltip ("Allows lerp of angle value without problems when exceeding 360 degrees")]
	public class LerpAngle : FsmStateAction
	{
		[RequiredField]
		[Tooltip ("From Angle")]
		public FsmFloat fromAngle;

		[RequiredField]
		[Tooltip ("To Angle")]
		public FsmFloat toAngle;

		[RequiredField]
		[Tooltip ("Interpolate between FromFloat and ToFloat by this amount. Value is clamped to 0-1 range. 0 = FromFloat; 1 = ToFloat; 0.5 = half way in between.")]
		public FsmFloat amount;

		[RequiredField]
		[UIHint (UIHint.Variable)]
		[Tooltip ("Store the result in this float variable.")]
		public FsmFloat LerpedAngle;

		[Tooltip ("Repeat every frame. Useful if any of the values are changing.")]
		public bool everyFrame;


		public override void Reset ()
		{
			fromAngle = new FsmFloat { UseVariable = true };
			toAngle = new FsmFloat { UseVariable = true };
			LerpedAngle = null;
			everyFrame = true;
		}

			
		public override void OnEnter ()
		{
			DoAngleLerp ();

			if (!everyFrame) {
				Finish ();
			}
		}
			
		public override void OnUpdate ()
		{
			DoAngleLerp ();
		}


		void DoAngleLerp ()
		{
			LerpedAngle.Value = Mathf.LerpAngle (fromAngle.Value, toAngle.Value, amount.Value);

		}
	}

}
