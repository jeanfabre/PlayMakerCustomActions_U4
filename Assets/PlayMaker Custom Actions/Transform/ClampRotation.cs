// (c) Copyright// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Trigonometry")]
	[Tooltip("Clamp a rotation around a local axis, optionally define the defaukt orientation, useful to control the initial rotation to clamp from. Clamp is done on Lateupdate")]
	public class ClampRotation : FsmStateAction
	{
		public enum ConstraintAxis {x = 0,y,z};
		
		[RequiredField]
		[Tooltip("The GameObject to clamp its rotation.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The default rotation. If none, will use the gameobject target.")]
		public FsmVector3 defaultRotation;

		[ObjectType(typeof(ConstraintAxis))]
		[Tooltip("The axis to constraint the rotation")]
		public FsmEnum constraintAxis;

		[Tooltip("The minimum angle allowed")]
		public FsmFloat minAngle;

		[Tooltip("The maximum angle allowed")]
		public FsmFloat maxAngle;

		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		float angleFromMin;
		float angleFromMax;

		Transform thisTransform;
		Vector3 rotateAround;
		Quaternion minQuaternion;
		Quaternion maxQuaternion;
		float range;

		ConstraintAxis axis;
		int axisIndex;
		Quaternion axisRotation;

		public override void Reset()
		{
			gameObject = null;
			defaultRotation = new FsmVector3 (){UseVariable = true};

			constraintAxis = null;
			minAngle = -45f;
			maxAngle = 45f;

			everyFrame = false;
		}
		
		public override void OnPreprocess()
		{
			#if PLAYMAKER_1_8_5_OR_NEWER
			Fsm.HandleLateUpdate = true;
			#endif
		}

		Vector3 _defaultRotation;

		public override void OnEnter ()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}
			thisTransform = go.transform;

			axis = (ConstraintAxis)constraintAxis.Value;
			axisIndex = (int)axis;
			// Set the axis that we will rotate around
			switch ( axis )
			{
			case ConstraintAxis.x:
				rotateAround = Vector3.right;
				break;
				
			case ConstraintAxis.y:
				rotateAround = Vector3.up;
				break;
				
			case ConstraintAxis.z:
				rotateAround = Vector3.forward;
				break;
			}

			if (!defaultRotation.IsNone) {
				_defaultRotation = defaultRotation.Value;

			} else {
				_defaultRotation = thisTransform.localRotation.eulerAngles;
			}

			ComputeRange ();


		}
		public override void OnLateUpdate()
		{
			DoClampRotation();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		void DoClampRotation()
		{

			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			thisTransform = go.transform;

			if (!defaultRotation.IsNone && _defaultRotation!=defaultRotation.Value) {
				_defaultRotation = defaultRotation.Value;
				ComputeRange ();
			}

			axisRotation = Quaternion.AngleAxis( thisTransform.localRotation.eulerAngles[axisIndex], rotateAround );

			angleFromMin = Quaternion.Angle( axisRotation, minQuaternion );
			angleFromMax = Quaternion.Angle( axisRotation, maxQuaternion );
			
			if ( angleFromMin <= range && angleFromMax <= range )
				return; // within range
			else
			{      
				// Let's keep the current rotations around other axes and only
				// correct the axis that has fallen out of range.
				Vector3 euler =  thisTransform.localRotation.eulerAngles;         
				if ( angleFromMin > angleFromMax )
					euler[ axisIndex ] = maxQuaternion.eulerAngles[axisIndex];
				else
					euler[ axisIndex ] = minQuaternion.eulerAngles[axisIndex];
				
				thisTransform.localEulerAngles = euler;    
			}

		}

		void ComputeRange()
		{
			axisRotation = Quaternion.AngleAxis( defaultRotation.Value[axisIndex], rotateAround );
			
			minQuaternion = axisRotation * Quaternion.AngleAxis( minAngle.Value, rotateAround );
			maxQuaternion = axisRotation * Quaternion.AngleAxis( maxAngle.Value, rotateAround );
			range = maxAngle.Value - minAngle.Value;
		}
		
	}
}
