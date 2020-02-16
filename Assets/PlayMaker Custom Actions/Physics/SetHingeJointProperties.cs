// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// covers Unity 4 and 5 with JointLimits differences.

#if (UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0 )
#define UNITY_PRE_5_1
#endif

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Set some hinge joints properties")]
	public class SetHingeJointProperties : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(HingeJoint))]
		[Tooltip("JointSpring GameObject to control.")]
		public FsmOwnerDefault gameObject;
		
		[ActionSection("General")]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmGameObject connectedBody;
		
		public FsmFloat breakForce;
		public FsmFloat breakTorque;
		
		public FsmVector3 anchor;
		
		public FsmVector3 axis;
		
		[ActionSection("Spring")]
		
		public FsmBool useSpring;
		
		public FsmFloat spring;
		public FsmFloat damper;
		public FsmFloat targetPosition;
		
		[ActionSection("Motor")]
		
		public FsmBool useMotor;
		
		public FsmFloat targetVelocity;
		public FsmFloat force;
		public FsmBool freeSpin;
		
		[ActionSection("Limits")]
		
		public FsmBool useLimits;
		
		public FsmFloat min;
		public FsmFloat max;
		#if UNITY_PRE_5_1
		
		public FsmFloat minBounce;
		public FsmFloat maxBounce;
		
		#else
		public FsmFloat bounceMinVelocity;
		public FsmFloat bounciness;
		public FsmFloat contactDistance;
		#endif
		
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
		private HingeJoint _joint;
		
		public override void Reset()
		{
			gameObject = null;
			
			connectedBody = new FsmGameObject { UseVariable = true };
			breakForce = new FsmFloat { UseVariable = true };
			breakTorque = new FsmFloat { UseVariable = true };
			
			anchor = new FsmVector3 { UseVariable = true };
			axis = new FsmVector3 { UseVariable = true };
			
			useSpring = new FsmBool { UseVariable = true };
			spring = new FsmFloat { UseVariable = true };
			damper = new FsmFloat { UseVariable = true };
			targetPosition = new FsmFloat { UseVariable = true };
			
			useMotor = new FsmBool { UseVariable = true };
			targetVelocity = new FsmFloat { UseVariable = true };
			force = new FsmFloat { UseVariable = true };
			freeSpin = new FsmBool { UseVariable = true };
			
			useLimits = new FsmBool { UseVariable = true };
			min = new FsmFloat { UseVariable = true };
			max = new FsmFloat { UseVariable = true };
			#if UNITY_PRE_5_1
			minBounce = new FsmFloat { UseVariable = true };
			maxBounce = new FsmFloat { UseVariable = true };
			#else
			bounceMinVelocity = new FsmFloat { UseVariable = true };
			bounciness = new FsmFloat { UseVariable = true };
			contactDistance = new FsmFloat { UseVariable = true };
			#endif
			
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			var go = gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value;
			if (go == null)
			{
				LogWarning("Missing gameObject");
				return;	
			}
			
			_joint = go.GetComponent<HingeJoint>();
			if (_joint == null)
			{
				LogWarning("Missing HingeJoint");
				return;	
			}
			
			DoSetProperties();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetProperties();
		}
		
		void DoSetProperties()
		{
			
			JointSpring _springJoint = _joint.spring;
			JointMotor _jointMotor = _joint.motor;
			JointLimits _jointLimits = _joint.limits;
			
			if (!connectedBody.IsNone)
			{
				_joint.connectedBody = connectedBody.Value.GetComponent<Rigidbody>();
			}
			
			if (!anchor.IsNone)
			{
				_joint.anchor = anchor.Value;
			}
			if (!axis.IsNone)
			{
				_joint.axis = axis.Value;
			}
			if (!useSpring.IsNone)
			{
				_joint.useSpring = useSpring.Value;
			}
			if (!spring.IsNone)
			{
				_springJoint.spring = spring.Value;
				_joint.spring = _springJoint;
			}
			if (!damper.IsNone)
			{
				_springJoint.damper = damper.Value;
				_joint.spring = _springJoint;
			}
			if (!targetPosition.IsNone)
			{
				_springJoint.targetPosition = targetPosition.Value;
				_joint.spring = _springJoint;
			}
			if (!useMotor.IsNone)
			{
				_joint.useMotor = useMotor.Value;
			}
			if (!targetVelocity.IsNone)
			{
				_jointMotor.targetVelocity = targetVelocity.Value;
				_joint.motor = _jointMotor;
			}
			if (!force.IsNone)
			{
				_jointMotor.force = force.Value;
				_joint.motor = _jointMotor;
			}
			if (!freeSpin.IsNone)
			{
				_jointMotor.freeSpin = freeSpin.Value;
				_joint.motor = _jointMotor;
			}
			if (!useLimits.IsNone)
			{
				_joint.useLimits = useLimits.Value;
			}
			if (!min.IsNone)
			{
				_jointLimits.min = min.Value;
				_joint.limits = _jointLimits;
			}
			if (!max.IsNone)
			{
				_jointLimits.max = max.Value;
				_joint.limits = _jointLimits;
			}
			
			#if UNITY_PRE_5_1
			if (!minBounce.IsNone)
			{
				
				_jointLimits.minBounce = minBounce.Value;
				_joint.limits = _jointLimits;
			}
			if (!maxBounce.IsNone)
			{
				_jointLimits.maxBounce = maxBounce.Value;
				_joint.limits = _jointLimits;
			}
			#else
			if (!bounceMinVelocity.IsNone)
			{
				
				_jointLimits.bounceMinVelocity = bounceMinVelocity.Value;
				_joint.limits = _jointLimits;
			}
			if (!bounciness.IsNone)
			{
				_jointLimits.bounciness = bounciness.Value;
				_joint.limits = _jointLimits;
			}
			if (!contactDistance.IsNone)
			{
				
				_jointLimits.contactDistance = contactDistance.Value;
				_joint.limits = _jointLimits;
			}
			
			#endif
		}
	}
}
