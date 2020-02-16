// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Action version of Unity's Smooth Follow script. Does not look at the target so that it allows you to use a separate look at action.")]
	public class SmoothFollowActionNoLookAt : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The game object to control. E.g. The camera.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The GameObject to follow usually the Player.")]
        public FsmGameObject targetObject;

      //  [Tooltip("The position to look at.")]
      //  public FsmVector3 lookTarget;

		[RequiredField]
		[Tooltip("The distance in the x-z plane to the target.")]
		public FsmFloat distance;

		[RequiredField]
		[Tooltip("The height we want the camera to be above the target")]
		public FsmFloat height;

		[RequiredField]
		[Tooltip("How much to dampen height movement. More = tighter following.")]
		public FsmFloat heightDamping;

		[RequiredField]
		[Tooltip("How much to dampen rotation changes. More = less choppiness.")]
		public FsmFloat rotationDamping;

		// Cache for performance
		private GameObject cachedObect;
		private Transform myTransform;
		private Transform targetTransform;

		public override void Reset()
		{
			gameObject = null;
			targetObject = null;
			distance = 10f;
			height = 5f;
			heightDamping = 2f;
			rotationDamping = 3f;
          //  lookTarget = null;
		}

		public override void OnPreprocess()
		{
			#if PLAYMAKER_1_8_5_OR_NEWER
				Fsm.HandleLateUpdate = true;
			#endif
		}

		public override void OnLateUpdate()
		{
			if (targetObject.Value == null)
			{
				return;
			}

			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}
			
			if (cachedObect != go)
			{
				cachedObect = go;
				myTransform = go.transform;
				targetTransform = targetObject.Value.transform;
			}

            // Calculate the current rotation angles
            //var wantedRotationAngle = targetTransform.eulerAngles.y;
            var wantedHeight = targetTransform.position.y + height.Value;

            var currentRotationAngle = myTransform.eulerAngles.y;
            var currentHeight = myTransform.position.y;

            // Damp the rotation around the y-axis
         //   currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping.Value * Time.deltaTime);

            // Damp the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping.Value * Time.deltaTime);

            // Convert the angle into a rotation
            var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            //// Set the position of the camera on the x-z plane to:
            //// distance meters behind the target
            myTransform.position = targetTransform.position;
            myTransform.position -= currentRotation * Vector3.forward * distance.Value;

            // Set the height of the camera
            myTransform.position = new Vector3(myTransform.position.x, currentHeight, myTransform.position.z);

            //// Always look at the target
          //  myTransform.LookAt(lookTarget.Value);
        }

	}
}