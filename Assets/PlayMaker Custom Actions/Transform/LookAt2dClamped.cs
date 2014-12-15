// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
// original action created by collidernyc: http://hutonggames.com/playmakerforum/index.php?topic=7075.msg37373#msg37373
//--- __ECO__ __ACTION__ ---//


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Rotates a 2d Game Object on it's z axis so its forward vector points at a 2d or 3d position. Optional clamp between angles")]
	public class LookAt2dClamped : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to rotate.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The 2d position to Look At.")]
		public FsmVector2 vector2Target;
		
		[Tooltip("The 3d position to Look At. If not set to none, will be added to the 2d target")]
		public FsmVector3 vector3Target;
		
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
		
		private Vector3 lookAtPos;
		
		
		
		public override void Reset()
		{
			gameObject = null;
			
			vector2Target = null;
			vector3Target = new FsmVector3() {UseVariable=true};

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
			
			if (go == null)
			{
				return;
			}
			
			Vector3 target = new Vector3(vector2Target.Value.x,vector2Target.Value.y,0f);
			if (!vector3Target.IsNone)
			{
				target += vector3Target.Value;
			}
			
			Vector3 diff = target - go.transform.position;
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
					float distance = (target-go.transform.position).magnitude;
					Vector3 RealPointingtip = go.transform.position + go.transform.TransformDirection(Vector3.right)*distance;
					Debug.DrawLine(go.transform.position, RealPointingtip, debugLineColor.Value);
					Debug.DrawLine(go.transform.position, target, debugLineColor.Value);
					Debug.DrawLine(RealPointingtip, target, debugLineColor.Value);
				}else{
					Debug.DrawLine(go.transform.position, target, debugLineColor.Value);
				}

			}
			
		}
		
	}
}