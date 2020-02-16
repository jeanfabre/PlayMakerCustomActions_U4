// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Rect)]
	[Tooltip("Gradually changes a rect towards a desired goal over time. The value is smoothed by some spring-damper like function, which will never overshoot. The function can be used to smooth any kind of value, positions, colors, scalars.")]
	public class RectSmoothDamp: FsmStateAction
	{
		[RequiredField]
		[Tooltip("Start/Current rect")]
		[UIHint(UIHint.Variable)]
		public FsmRect current;
		
		[RequiredField]
		[Tooltip("Target rect")]
		public FsmRect target;

		[Tooltip("The current velocity, this value is modified by the function every time you call it.")]
		[UIHint(UIHint.Variable)]
		public FsmRect currentVelocity;
		
		[Tooltip("Approximately the time it will take to reach the target. A smaller value will reach the target faster.")]
		public FsmFloat smoothTime;

		[Tooltip("Optionally allows you to clamp the maximum speed.")]
		public FsmFloat maxSpeed;

		[ActionSection("Result")]
		[Tooltip("Event sent when current value is almost equal to the target value")]
		public FsmEvent done;

		[Tooltip("true when current value is almost equal to the target value")]
		public FsmBool isDone;

		Rect _velocity_cache;
		float _velocity_x;
		float _velocity_y;
		float _velocity_w;
		float _velocity_h;

		Rect _cache;

		bool _isDone;

		public override void Reset()
		{
			current = null;
			target = new Rect(0f,0f,1f,1f);
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
			DoRectDamp();
		}

		void DoRectDamp()
		{
			if (!maxSpeed.IsNone)
			{
				_cache.x = Mathf.SmoothDamp(current.Value.x, target.Value.x, ref _velocity_x, smoothTime.Value,maxSpeed.Value);
				_cache.y = Mathf.SmoothDamp(current.Value.y, target.Value.y, ref _velocity_y, smoothTime.Value,maxSpeed.Value);
				_cache.width = Mathf.SmoothDamp(current.Value.width, target.Value.width, ref _velocity_h, smoothTime.Value,maxSpeed.Value);
				_cache.height = Mathf.SmoothDamp(current.Value.height, target.Value.height, ref _velocity_w, smoothTime.Value,maxSpeed.Value);
			}else{
				_cache.x = Mathf.SmoothDamp(current.Value.x, target.Value.x, ref _velocity_x, smoothTime.Value);
				_cache.y = Mathf.SmoothDamp(current.Value.y, target.Value.y, ref _velocity_y, smoothTime.Value);
				_cache.width = Mathf.SmoothDamp(current.Value.height, target.Value.width, ref _velocity_h, smoothTime.Value);
				_cache.height = Mathf.SmoothDamp(current.Value.width, target.Value.height, ref _velocity_w, smoothTime.Value);
			}

			current.Value = _cache;

			if (!currentVelocity.IsNone)
			{
				_velocity_cache.x = _velocity_x;
				_velocity_cache.y = _velocity_y;
				_velocity_cache.width = _velocity_w;
				_velocity_cache.height = _velocity_h;
				currentVelocity.Value = _velocity_cache;
			}

			if (done!= null|| !isDone.IsNone)
			{
				if (	IsApproximately(current.Value.x,target.Value.x) && 
				    	IsApproximately(current.Value.y,target.Value.y) &&
					IsApproximately(current.Value.width,target.Value.width) && 
					IsApproximately(current.Value.height,target.Value.height))
				{
					if (!_isDone)
					{
						_isDone = true;
						isDone.Value = true;
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

