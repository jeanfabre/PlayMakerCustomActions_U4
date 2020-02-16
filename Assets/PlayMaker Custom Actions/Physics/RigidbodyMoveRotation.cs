// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Rotates a RigidBody to a particular rotation. It's more efficient then accessing the transform of the gameObject and interacts properly with colliders and physics materials")]
	public class RigidBodyMoveRotation : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		[Tooltip("The rigid body to rotate.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("A rotation quaternion.")]
		[UIHint(UIHint.Variable)]
		public FsmQuaternion rotation;
		
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
		private Rigidbody rigidBody;
		
		public override void Reset()
		{
			gameObject = null;
			rotation = null;
			everyFrame = true;
		}

        public override void OnPreprocess()
        {
            Fsm.HandleFixedUpdate = true;
        }
		
		public override void OnEnter()
		{

			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				LogError("Missing gameObject target");
				return;
			}
			
			rigidBody = go.GetComponent<Rigidbody>();
			if (rigidBody == null)
			{
				LogError("Missing Rigidbody component");
				return;
			}
				
		}

        public override void OnFixedUpdate()
        {
            DoRotate();
           
            if (!everyFrame)
            {
                Finish();
            }
        }

		void DoRotate()
		{
			if (rigidBody != null)
			{
				rigidBody.MoveRotation(rotation.Value);
			}
		}

	}
}
