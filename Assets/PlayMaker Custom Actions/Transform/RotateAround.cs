// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// http://hutonggames.com/playmakerforum/index.php?topic=1078.msg4509#msg4509
// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Rotates Around a Game Object.")]
	public class RotateAround : FsmStateAction
	{

		[RequiredField]
		public FsmOwnerDefault gameObject;
		
		[ActionSection("Rotation position")]
		[Tooltip("Rotate around this GameObject.")]
		public FsmGameObject aroundGameObject;
		
		[Tooltip("Rotate around this point. If 'aroundGameObject' defined, will offset by 'rotationPoint'")]
		public FsmVector3 rotationPoint;
		
		[ActionSection("Rotation axis")]
		[Tooltip("Rotate around this axis.")]
		public FsmVector3 rotationAxis;
		
		[Tooltip("If 'aroundGameObject' defined and 'useAroundGameObjectAxisSpace' TRUE, 'rotationAxis' will be defined in 'aroundGameObject' local space.")]
		public FsmBool useAroundGameObjectAxisSpace;
		
		[ActionSection("Angle")]
		[Tooltip("Amount to rotate in degrees.")]
		public FsmFloat angle;
		
		[Tooltip("Rotate over one second")]
		public bool perSecond;
		
		[ActionSection("Animation")]
		public bool everyFrame;
		[Tooltip("Amount to rotate in degrees before stopping animation. Zero rotates forever.")]
		public FsmFloat stopAngle;
		FsmFloat rotatedAngle = 0f;
		
		public override void Reset()
		{
			gameObject = null;
			aroundGameObject = null;
			stopAngle = null;
			angle = null;
			rotatedAngle = 0f;
			
			useAroundGameObjectAxisSpace = false;
			
			perSecond = false;
			everyFrame = true;
		}

		public override void OnEnter()
		{
			rotatedAngle.Value = 0;

			DoRotateAround();

			if(!everyFrame)
			{
				Finish();
			} 
			else if (stopAngle.Value != 0f)
			{
				if (rotatedAngle.Value >= stopAngle.Value)
				{	
					Finish();
				}
			}
		}

		public override void OnUpdate()
		{
			DoRotateAround();

			if (stopAngle.Value != 0f)
			{
				if (rotatedAngle.Value >= stopAngle.Value)
				{	
					Finish();
				}
			}
		}

		void DoRotateAround()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			Vector3 _rotationPoint = rotationPoint.Value;
			Vector3 _axis = rotationAxis.Value;
			
			GameObject aroundgo = aroundGameObject.Value;
			if (aroundgo!=null)
			{
				_rotationPoint += aroundgo.transform.position;
				
				
			}
			
			if (useAroundGameObjectAxisSpace.Value)
			{
				_axis = aroundgo.transform.TransformDirection(_axis);
			}
			
			float _angle = angle.Value;
			
			if (perSecond)
			{
				_angle *= Time.deltaTime;
			}

			rotatedAngle.Value += _angle;

			go.transform.RotateAround(_rotationPoint,_axis,_angle);
			
		}
		
		public override string ErrorCheck()
		{
			if (useAroundGameObjectAxisSpace.Value && aroundGameObject.Value==null)
			{
				return "'useAroundGameObjectAxisSpace' is only effective is 'aroundGameObject' defined";
			}
			
			return "";
		}
		
	}
}