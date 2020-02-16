// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Gradually changes a vector3 towards a desired goal over time. The value is smoothed by some spring-damper like function, which will never overshoot. The function can be used to smooth any kind of value, positions, colors, scalars.")]
	public class Vector3SmoothDamp: FsmStateAction
	{
		[RequiredField]
		[Tooltip("Start/Current vector")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 current;
		
		[RequiredField]
		[Tooltip("Target vector")]
		public FsmVector3 target;

		[Tooltip("The current velocity, this value is modified by the function every time you call it.")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 currentVelocity;
		
		[Tooltip("Approximately the time it will take to reach the target. A smaller value will reach the target faster.")]
		public FsmFloat smoothTime;

		[Tooltip("Optionally allows you to clamp the maximum speed.")]
		public FsmFloat maxSpeed;

		[ActionSection("Result")]
		[Tooltip("Event sent when current value is almost equal to the target value")]
		public FsmEvent done;

		[Tooltip("true when current value is almost equal to the target value")]
		public FsmBool isDone;

		Vector3 _velocity_cache;
		float _velocity_x;
		float _velocity_y;
		float _velocity_z;

		Vector3 _cache;

		bool _isDone;
		public override void Reset()
		{
			current = null;
			target = Vector3.one;
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
			DoVector3Damp();
		}

		void DoVector3Damp()
		{
			if (!maxSpeed.IsNone)
			{
				_cache.x = Mathf.SmoothDamp(current.Value.x, target.Value.x, ref _velocity_x, smoothTime.Value,maxSpeed.Value);
				_cache.y = Mathf.SmoothDamp(current.Value.y, target.Value.y, ref _velocity_y, smoothTime.Value,maxSpeed.Value);
				_cache.z = Mathf.SmoothDamp(current.Value.z, target.Value.z, ref _velocity_z, smoothTime.Value,maxSpeed.Value);
			}else{
				_cache.x = Mathf.SmoothDamp(current.Value.x, target.Value.x, ref _velocity_x, smoothTime.Value);
				_cache.y = Mathf.SmoothDamp(current.Value.y, target.Value.y, ref _velocity_y, smoothTime.Value);
				_cache.z = Mathf.SmoothDamp(current.Value.z, target.Value.z, ref _velocity_z, smoothTime.Value);
			}

			current.Value = _cache;

			if (!currentVelocity.IsNone)
			{
				_velocity_cache.x = _velocity_x;
				_velocity_cache.y = _velocity_y;
				_velocity_cache.z = _velocity_z;
				currentVelocity.Value = _velocity_cache;
			}

			if (done!= null || !isDone.IsNone)
			{
				if ( IsApproximately(current.Value.x,target.Value.x) && 
				    IsApproximately(current.Value.y,target.Value.y) &&
				    IsApproximately(current.Value.z,target.Value.z))
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

