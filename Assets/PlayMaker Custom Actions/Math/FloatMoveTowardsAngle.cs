// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__Internal/FsmStateActionAdvanced.cs"
					]
}
EcoMetaEnd
---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Moves a value current towards target,and makes sure the values interpolate correctly when they wrap around 360 degrees.\n\nVariables current and target are assumed to be in degrees. For optimization reasons, negative values of maxDelta are not supported and may cause oscillation. To push current away from a target angle, add 180 to that angle instead.")]
	public class FloatMoveTowardsAngle : FsmStateActionAdvanced
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("Float Angle variable to move in degrees.")]
		public FsmFloat angleVariable;

		[RequiredField]
        [Tooltip("The target value to move towards.")]
		public FsmFloat targetAngle;

		[RequiredField]
		[Tooltip("The maximum change that should be applied to the value.")]
		public FsmFloat maxDelta;

		[Tooltip("If true, maxDelta is multiplied by Time.DeltaTime for framerate independant animations")]
		public bool timeBasedMaxDelta;

		[Tooltip("Distance at which the move is considered finished, and the Finish Event is sent. Set to none for continous execution")]
		public FsmFloat finishDistance;

		[ActionSection("Result")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The resulting value wrapped around 360°")]
		public FsmFloat result;

		[UIHint(UIHint.Variable)]
		[Tooltip("The resulting value in radian")]
		public FsmFloat resultInRadian;

		[UIHint(UIHint.Variable)]
		[Tooltip("The remaining distance from the floatvariable to the targetValue")]
		public FsmFloat remainingAngle;

		[UIHint(UIHint.Variable)]
		[Tooltip("The remaining distance from the floatvariable to the targetValue in Radian")]
		public FsmFloat remainingAngleinRadian;


		[Tooltip("Event to send when the Finish Distance is reached.")]
		public FsmEvent finishEvent;

		float _maxDelta;
		float _distance;
		float _result;

		public override void Reset()
		{
			base.Reset();
			angleVariable = null;
			targetAngle = null;
			maxDelta = null;
			result = null;
			resultInRadian = null;
			remainingAngle = null;
			remainingAngleinRadian = null;
			finishDistance = new FsmFloat (){ UseVariable=true };
			timeBasedMaxDelta = true;

		}

		public override void OnActionUpdate()
		{
			DoMoveTowardsAngle();
		}
		
		void DoMoveTowardsAngle()
		{
			_maxDelta = maxDelta.Value;
			if (timeBasedMaxDelta) {
				_maxDelta *= Time.deltaTime;
			}

			_result = Mathf.MoveTowards(angleVariable.Value, targetAngle.Value, _maxDelta);

			result.Value = _result;

			if (!resultInRadian.IsNone) {
				resultInRadian.Value = Mathf.Deg2Rad*_result;
			}

			_distance = targetAngle.Value - _result;

			if (! remainingAngle.IsNone) {
				remainingAngle.Value = _distance;
			}
			if (!remainingAngleinRadian.IsNone) {
				remainingAngleinRadian.Value = Mathf.Deg2Rad*_distance;
			}


			if (!finishDistance.IsNone  && Mathf.Abs(_distance) < Mathf.Abs(finishDistance.Value))
			{
				Fsm.Event(finishEvent);
				Finish();
			}

		}
	}
}
