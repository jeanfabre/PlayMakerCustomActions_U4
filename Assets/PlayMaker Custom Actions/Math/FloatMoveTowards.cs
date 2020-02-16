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
	[Tooltip("Moves a value current towards target. This is essentially the same as Lerping but instead the function will ensure that the speed never exceeds maxDelta. Negative values of maxDelta pushes the value away from target.")]
	public class FloatMoveTowards : FsmStateActionAdvanced
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("Float variable to move.")]
		public FsmFloat floatVariable;

		[RequiredField]
        [Tooltip("The target value to move towards.")]
		public FsmFloat targetValue;

		[RequiredField]
		[Tooltip("The maximum change that should be applied to the value.")]
		public FsmFloat maxDelta;

		[Tooltip("If true, maxDelta is multiplied by Time.DeltaTime for framerate independant animations")]
		public bool timeBasedMaxDelta;

		[Tooltip("Distance at which the move is considered finished, and the Finish Event is sent. Set to none for continous execution")]
		public FsmFloat finishDistance;

		[ActionSection("Result")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The resulting value")]
		public FsmFloat result;


		[UIHint(UIHint.Variable)]
		[Tooltip("The remaining distance from the floatvariable to the targetValue")]
		public FsmFloat remainingDistance;


		[Tooltip("Event to send when the Finish Distance is reached.")]
		public FsmEvent finishEvent;

		float _maxDelta;
		float _distance;
		float _result;

		public override void Reset()
		{
			base.Reset();
			floatVariable = null;
			targetValue = null;
			maxDelta = null;
			finishDistance = new FsmFloat (){ UseVariable=true };
			timeBasedMaxDelta = true;

		}

		public override void OnActionUpdate()
		{
			DoMoveTowards();
		}
		
		void DoMoveTowards()
		{
			_maxDelta = maxDelta.Value;
			if (timeBasedMaxDelta) {
				_maxDelta *= Time.deltaTime;
			}

			_result = Mathf.MoveTowards(floatVariable.Value, targetValue.Value, _maxDelta);

			result.Value = _result;

			_distance = targetValue.Value - _result;

			if (! remainingDistance.IsNone) {
				remainingDistance.Value = _distance;
			}

			if (!finishDistance.IsNone && Mathf.Abs(_distance) <= Mathf.Abs(finishDistance.Value))
			{
				Fsm.Event(finishEvent);
				Finish();
			}

		}
	}
}
