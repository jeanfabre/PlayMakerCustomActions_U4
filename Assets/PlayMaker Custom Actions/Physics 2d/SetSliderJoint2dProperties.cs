// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Physics 2d")]
	[Tooltip("Sets the various properties of a SliderJoint2d component")]
	public class SetSliderJoint2dProperties : FsmStateAction
	{
		
		[RequiredField]
		[Tooltip("The SliderJoint2d target")]
		[CheckForComponent(typeof(SliderJoint2D))]
		public FsmOwnerDefault gameObject;

		[Tooltip("The world angle along which the suspension will move. This provides 2D constrained motion similar to a SliderJoint2D. This is typically how suspension works in the real world.")]
		public FsmFloat angle;

		[ActionSection("Motor")]

		[Tooltip("Should a motor force be applied automatically to the Rigidbody2D?")]
		public FsmBool useMotor;

		[Tooltip("The desired speed for the Rigidbody2D to reach as it moves with the joint.")]
		public FsmFloat motorSpeed;

		[Tooltip("The maximum force that can be applied to the Rigidbody2D at the joint to attain the target speed.")]
		public FsmFloat maxMotorTorque;

		[ActionSection("Limits")]

		[Tooltip("Should motion limits be used?")]
		public FsmBool useLimits;

		[Tooltip("Minimum distance the Rigidbody2D object can move from the Slider Joint's anchor.")]
		public FsmFloat min;

		[Tooltip("Maximum distance the Rigidbody2D object can move from the Slider Joint's anchor.")]
		public FsmFloat max;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		SliderJoint2D _joint;
		JointMotor2D _motor;
		JointTranslationLimits2D _limits;

		public override void Reset()
		{
			angle = new FsmFloat() {UseVariable=true};
			useMotor = new FsmBool() {UseVariable=true};
			motorSpeed = new FsmFloat() {UseVariable=true};
			maxMotorTorque = new FsmFloat() {UseVariable=true};

			useLimits = new FsmBool() {UseVariable=true};
			min = new FsmFloat() {UseVariable=true};
			max = new FsmFloat() {UseVariable=true};
			everyFrame = false;

		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			if (go != null)
			{
				_joint = go.GetComponent<SliderJoint2D>();

				if(_joint!=null)
				{
					_motor = _joint.motor;
					_limits = _joint.limits;
				}
			}

			SetProperties();

			if(!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			SetProperties();
		}

		void SetProperties()
		{
			if(_joint==null)
			{
				return;
			}

			if (!angle.IsNone)
			{
				_joint.angle = angle.Value;
			}

			//NOTE: If the motor is changed then useMotor is automatically true.

			if (!useMotor.IsNone)
			{
				_joint.useMotor = useMotor.Value;
			}

			if (!motorSpeed.IsNone)
			{
				_motor.motorSpeed = motorSpeed.Value;
				_joint.motor = _motor;
			}

			if (!maxMotorTorque.IsNone)
			{
				_motor.maxMotorTorque = maxMotorTorque.Value;
				_joint.motor = _motor;
			}

			//NOTE: If the motor is changed then useMotor is automatically true.

			if (!useLimits.IsNone)
			{
				_joint.useLimits = useLimits.Value;
			}
			
			if (!min.IsNone)
			{
				_limits.min = min.Value;
				_joint.limits = _limits;
			}
			if (!max.IsNone)
			{
				_limits.max = max.Value;
				_joint.limits = _limits;
			}


		}
	}
}
