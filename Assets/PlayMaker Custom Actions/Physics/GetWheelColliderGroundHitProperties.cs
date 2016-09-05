// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets ground collision data for the wheel")]
	public class GetWheelColliderGroundHitProperties : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(WheelCollider))]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The other Collider the wheel is hitting.")]
		public FsmGameObject collider;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The point of contact between the wheel and the ground.")]
		public FsmVector3 point;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The normal at the point of contact.")]
		public FsmVector3 normal;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The direction the wheel is pointing in.")]
		public FsmVector3 forwardDir;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The magnitude of the force being applied for the contact.")]
		public FsmFloat force;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Tire slip in the rolling direction. Acceleration slip is negative, braking slip is positive.")]
		public FsmFloat forwardSlip;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Tire slip in the sideways direction.")]
		public FsmFloat sidewaysSlip;
		
		public bool everyFrame;
		
		
		WheelCollider _wheel;
		
		WheelHit _wheelhit;
		
		public override void Reset()
		{
			gameObject = null;
			collider = null;
			point = null;
			normal = null;
			forwardDir = null;
			force = null;
			forwardSlip = null;
			sidewaysSlip = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			if (go != null)
			{
				_wheel = go.GetComponent<WheelCollider>();
			}
			
			
			DoGetGroundHit();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoGetGroundHit();
		}
		
		void DoGetGroundHit()
		{
			
			if (_wheel==null)
			{
				return;
			}


			_wheel.GetGroundHit(out _wheelhit);

			if (!collider.IsNone)
			{
				if (_wheelhit.collider!=null)
				{
					collider.Value = _wheelhit.collider.gameObject;
				}else{
					UnityEngine.Debug.LogWarning("Missing Collider in wheelhit data");
				}
			}
			if (!point.IsNone) point.Value = _wheelhit.point;
			if (!normal.IsNone) normal.Value = _wheelhit.normal;
			if (!forwardDir.IsNone) forwardDir.Value = _wheelhit.forwardDir;
			if (!force.IsNone) force.Value = _wheelhit.force;
			if (!forwardSlip.IsNone) forwardSlip.Value = _wheelhit.forwardSlip;
			if (!sidewaysSlip.IsNone) sidewaysSlip.Value = _wheelhit.sidewaysSlip;
		}
	}
}
