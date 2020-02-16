// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Gradually changes a vector3 towards a desired goal over time. The value is smoothed by some spring-damper like function, which will never overshoot. The function can be used to smooth any kind of value, positions, colors, scalars.")]
	public class PositionSmoothDamp: FsmStateAction
	{
		[RequiredField]
		[Tooltip("Start/Current GameObject Position")]
		[UIHint(UIHint.Variable)]
		public FsmOwnerDefault gameObject;

		[Tooltip("Target Vector, If 'OrTargetGameObject' is set, target will be ignored")]
		public FsmVector3 target;

		[Tooltip("Or Target GameObject")]
		public FsmOwnerDefault OrTargetGameObject;

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
		Vector3 _current;
		Vector3 _target;
		GameObject _goCurrent;
		GameObject _goTarget;

		bool _isDone;

		public override void Reset()
		{
			gameObject = null;
			target = Vector3.one;
			OrTargetGameObject = new FsmOwnerDefault(){OwnerOption= OwnerDefaultOption.SpecifyGameObject};
			OrTargetGameObject.GameObject = new FsmGameObject(){UseVariable=true};

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
			_goCurrent = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_goCurrent!=null)
			{
				_current = _goCurrent.transform.position;
			}

			_target = target.Value;
			_goTarget = Fsm.GetOwnerDefaultTarget(OrTargetGameObject);
			if (_goTarget!=null)
			{
				_target = _goTarget.transform.position;
			}


			if (!maxSpeed.IsNone)
			{
				_cache.x = Mathf.SmoothDamp(_current.x, _target.x, ref _velocity_x, smoothTime.Value,maxSpeed.Value);
				_cache.y = Mathf.SmoothDamp(_current.y, _target.y, ref _velocity_y, smoothTime.Value,maxSpeed.Value);
				_cache.z = Mathf.SmoothDamp(_current.z, _target.z, ref _velocity_z, smoothTime.Value,maxSpeed.Value);
			}else{
				_cache.x = Mathf.SmoothDamp(_current.x, _target.x, ref _velocity_x, smoothTime.Value);
				_cache.y = Mathf.SmoothDamp(_current.y, _target.y, ref _velocity_y, smoothTime.Value);
				_cache.z = Mathf.SmoothDamp(_current.z, _target.z, ref _velocity_z, smoothTime.Value);
			}

			if (_goCurrent!=null)
			{
				_goCurrent.transform.position = _cache;
			}

			if (!currentVelocity.IsNone)
			{
				_velocity_cache.x = _velocity_x;
				_velocity_cache.y = _velocity_y;
				_velocity_cache.z = _velocity_z;
				currentVelocity.Value = _velocity_cache;
			}

			if (done != null || !isDone.IsNone)
			{
				if ( IsApproximately(_cache.x,_target.x) && 
				    IsApproximately(_cache.y,_target.y) &&
				    IsApproximately(_cache.z,_target.z))
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

