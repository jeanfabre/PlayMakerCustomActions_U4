// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// original action created by collidernyc: http://hutonggames.com/playmakerforum/index.php?topic=7075.msg37373#msg37373



using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Rotates a 2d Game Object on it's z axis so its forward vector points at a Target. Optional clamp between angles")]
	public class LookAt2dGameObjectClamped : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to rotate.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The GameObject to Look At.")]
		public FsmGameObject targetObject;
		
		[Tooltip("Set the GameObject starting offset. In degrees. 0 if your object is facing right, 180 if facing left etc...")]
		public FsmFloat rotationOffset;

		[Tooltip("The minimum angle limit (the limit being -180 degrees). Leave to none for no effect ")]
		public FsmFloat MinAngleLimit;
		
		[Tooltip("The maximum angle limit (the limit being 180 degrees). Leave to none for no effect ")]
		public FsmFloat MaxAngleLimit;

		[Title("Draw Debug Line")]
		[Tooltip("Draw a debug line from the GameObject to the Target.")]
		public FsmBool debug;
		
		[Tooltip("Color to use for the debug line.")] 
		public FsmColor debugLineColor;
		
		[Tooltip("Repeat every frame.")]
		public bool everyFrame = true;
		
		private GameObject go;
		private GameObject goTarget;
		private Vector3 lookAtPos;
		
		
		
		public override void Reset()
		{
			gameObject = null;
			targetObject = null;

			MinAngleLimit = new FsmFloat(){UseVariable=true};
			MaxAngleLimit = new FsmFloat(){UseVariable=true};

			debug = false;
			debugLineColor = Color.green;
			everyFrame = true;
		}
		
		public override void OnEnter()
		{
			DoLookAt();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoLookAt();
		}
		
		void DoLookAt()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);
			goTarget = targetObject.Value;
			
			if (go == null || targetObject == null)
			{
				return;
			}
			
			Vector3 diff = goTarget.transform.position - go.transform.position;
			diff.Normalize();
			
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - rotationOffset.Value;

			float final_rot_z = rot_z;
			if (!MinAngleLimit.IsNone || !MaxAngleLimit.IsNone)
			{
				float min = MinAngleLimit.IsNone?-180f:MinAngleLimit.Value;
				float max = MaxAngleLimit.IsNone?180f:MaxAngleLimit.Value;
				final_rot_z = Mathf.Clamp(final_rot_z,min,max);
			}

			go.transform.rotation = Quaternion.Euler(0f, 0f, final_rot_z );
			
			if (debug.Value)
			{
				if ( Mathf.Abs(rot_z - final_rot_z) > 0.01f) // not almost equal
				{
					float distance = (goTarget.transform.position-go.transform.position).magnitude;
					Vector3 RealPointingtip = go.transform.position + go.transform.TransformDirection(Vector3.right)*distance;
					Debug.DrawLine(go.transform.position, RealPointingtip, debugLineColor.Value);
					Debug.DrawLine(go.transform.position, goTarget.transform.position, debugLineColor.Value);
					Debug.DrawLine(RealPointingtip, goTarget.transform.position, debugLineColor.Value);
				}else
				{
					Debug.DrawLine(go.transform.position, goTarget.transform.position, debugLineColor.Value);
				}
			}
			
		}
		
	}
}
