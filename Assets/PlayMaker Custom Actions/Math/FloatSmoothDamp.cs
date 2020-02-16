// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Gradually changes a value towards a desired goal over time. The value is smoothed by some spring-damper like function, which will never overshoot. The function can be used to smooth any kind of value, positions, colors, scalars.")]
	public class FloatSmoothDamp: FsmStateAction
	{
		[RequiredField]
		[Tooltip("start Float")]
		[UIHint(UIHint.Variable)]
		public FsmFloat current;
		
		[RequiredField]
		[Tooltip("Target Float")]
		public FsmFloat target;

		[Tooltip("The current velocity, this value is modified by the function every time you call it.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat currentVelocity;
		
		[Tooltip("Approximately the time it will take to reach the target. A smaller value will reach the target faster.")]
		public FsmFloat smoothTime;

		[Tooltip("Optionally allows you to clamp the maximum speed.")]
		public FsmFloat maxSpeed;

		[ActionSection("Result")]
		[Tooltip("Event sent when current value is almost equal to the target value")]
		public FsmEvent done;

		[Tooltip("true when current value is almost equal to the target value")]
		public FsmBool isDone;

		float _velocity;

		bool _isDone;

		public override void Reset()
		{
			current = null;
			target = 1f;
			smoothTime =1f;
			currentVelocity = null;
			maxSpeed = new FsmFloat(){UseVariable=true};

			done = null;
			isDone = null;
		}

		public override void OnEnter()
		{
			_isDone = false;
		}


		public override void OnUpdate()
		{
			DoFloatDamp();
		}

		void DoFloatDamp()
		{
			if (!maxSpeed.IsNone)
			{
				current.Value = Mathf.SmoothDamp(current.Value, target.Value, ref _velocity, smoothTime.Value,maxSpeed.Value);
			}else{
				current.Value = Mathf.SmoothDamp(current.Value, target.Value, ref _velocity, smoothTime.Value);
			}

			if (!currentVelocity.IsNone)
			{
				currentVelocity.Value = _velocity;
			}

			if (done != null || !isDone.IsNone)
			{
				if (IsApproximately(current.Value,target.Value))
				{
					if (!_isDone)
					{
						_isDone = true;
						this.Fsm.Event(done);
					}
				}else{
					if (_isDone)
					{
						_isDone = false;
						isDone.Value = false;
					}

				}

			}


		}

		bool IsApproximately(float a, float b, float tolerance = 0.01f) {
			return Mathf.Abs(a - b) < tolerance;
		}
	}
}

