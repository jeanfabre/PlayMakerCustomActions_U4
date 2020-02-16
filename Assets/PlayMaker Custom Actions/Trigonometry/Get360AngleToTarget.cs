// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
/* original action on https://github.com/pedrosanta/PlaymakerActionsPack */

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Trigonometry")]
	[Tooltip("Gets the Angle (in degreees, 0 to 360 ) between a Game Object's given axis and a Target. " +
		"The Target can be defined as a Game Object or a world Position. " +
		"If you specify both, then the Position will be used as a local offset from the Object's position.")]
	public class Get360AngleToTarget : FsmStateAction
	{
		public enum GetSignedAngleToTargetDirection {x,y,z};
		
		[RequiredField]
		public FsmOwnerDefault gameObject;
		
		public FsmGameObject targetObject;
		
		public FsmVector3 targetPosition;

		public GetSignedAngleToTargetDirection direction;
		
		public FsmBool invertDirection;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeAngle;
		
		public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
			targetObject = null;
			targetPosition = new FsmVector3 { UseVariable = true};
			direction = GetSignedAngleToTargetDirection.x;
			storeAngle = null;
			invertDirection = false;
			everyFrame = false;
		}

		public override void OnPreprocess()
		{
			#if PLAYMAKER_1_8_5_OR_NEWER
			Fsm.HandleLateUpdate = true;
			#endif
		}

		public override void OnLateUpdate()
		{
			DoGetAngleToTarget();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		void DoGetAngleToTarget()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}
			
			var goTarget = targetObject.Value;
			if (goTarget == null && targetPosition.IsNone)
			{
				return;
			}

			Vector3 targetPos;
			if (goTarget != null)
			{
				targetPos = !targetPosition.IsNone ? 
					goTarget.transform.TransformPoint(targetPosition.Value) : 
					goTarget.transform.position;
			}
			else
			{
				targetPos = targetPosition.Value;
			}

			
			var localTarget = go.transform.InverseTransformPoint(targetPos);
			
			float _mult = invertDirection.Value ?-1f:1f;

			float _angle = 0f;
			switch(direction)
			{
			case GetSignedAngleToTargetDirection.x:
				_angle = Mathf.Atan2(localTarget.y, localTarget.z) * Mathf.Rad2Deg * _mult;
				break;
			case GetSignedAngleToTargetDirection.y:
				_angle = Mathf.Atan2(localTarget.z, localTarget.x) * Mathf.Rad2Deg * _mult;
				break;
			case GetSignedAngleToTargetDirection.z:
				_angle = Mathf.Atan2(localTarget.x, localTarget.y) * Mathf.Rad2Deg * _mult;
				break;
			}

			if (_angle < 0) {
				_angle = 360f + _angle;
			}


			storeAngle.Value = _angle;
			
		}

	}
}
