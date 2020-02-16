// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Gets the last measured Gyroscope data of a device and stores it in Variables.")]
	public class GetDeviceGyroscope : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("If set, this gameObject will show the attitude of the device, processed with the initial rotation of this GameObject")]
		public FsmOwnerDefault device;

		[UIHint(UIHint.Variable)]
		[Tooltip("Rotation rate as measured by the device's gyroscope")]
		public FsmVector3 rotationRate;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Unbiased rotation rate as measured by the device's gyroscope")]
		public FsmVector3 rotationRateUnbiased;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The gravity acceleration vector expressed in the device's reference frame.")]
		public FsmVector3 gravity;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The acceleration that the user is giving to the device.")]
		public FsmVector3 userAcceleration;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("the attitude of the device")]
		public FsmQuaternion attitude;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("the attitude of the device in euler angles")]
		public FsmVector3 attitudeAngles;

		public bool debug;
		
		
		private Transform deviceTransform;
		
		private Quaternion initialRotation;
		private Quaternion gyroInitialInvertedRotation;
		
		public override void Reset()
		{
			device = null;
			rotationRate = null;
			rotationRateUnbiased = null;
			gravity = null;
			userAcceleration = null;
			attitude = null;
			attitudeAngles = null;
			
			debug = true;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(device);
			if (go!=null)
			{
				Debug.Log("hello "+go.name);
				deviceTransform = go.transform;
				initialRotation = deviceTransform.rotation;
				gyroInitialInvertedRotation = Quaternion.Inverse( Input.gyro.attitude * new Quaternion(0f,0f,1f,0f));
			}
			
			
			DoGetDeviceGyro();
		}
		

		public override void OnUpdate()
		{
		
			DoGetDeviceGyro();
		}
		
		void DoGetDeviceGyro()
		{
			if (!Input.gyro.enabled)
			{
				return;
			}
			
			rotationRate.Value = Input.gyro.rotationRate;
			rotationRateUnbiased.Value  = Input.gyro.rotationRateUnbiased;
			gravity.Value  = Input.gyro.gravity;
			userAcceleration.Value  = Input.gyro.userAcceleration;
			attitude.Value  = Input.gyro.attitude;
			attitudeAngles.Value = Input.gyro.attitude.eulerAngles;
			
			
			if (deviceTransform!=null)
			{
				deviceTransform.rotation = initialRotation * gyroInitialInvertedRotation * Input.gyro.attitude * new Quaternion(0f,0f,1f,0f);
			}
			
			if (debug)
			{
				Debug.DrawLine(Vector3.zero,Input.gyro.rotationRate,Color.blue);
				Debug.DrawLine(Vector3.zero,Input.gyro.rotationRateUnbiased,Color.cyan);
				Debug.DrawLine(Vector3.zero,Input.gyro.gravity,Color.red);
				Debug.DrawLine(Vector3.zero,Input.gyro.userAcceleration,Color.green);
				Debug.DrawLine(Vector3.zero,Input.gyro.attitude.eulerAngles,Color.yellow);
			}
		}
		
	}
}
