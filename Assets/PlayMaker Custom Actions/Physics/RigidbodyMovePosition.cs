// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Moves a RigidBody to a particular position. It's more efficient then accessing the transform of the gameObject and interacts properly with colliders and physics materials")]
	public class RigidBodyMovePosition : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		[Tooltip("The rigid body to move.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The position to move to.")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 position;
		
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
		private Rigidbody rigidBody;
		
		public override void Reset()
		{
			gameObject = null;
			position = null;
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
            DoPosition();
            
            if (!everyFrame)
            {
                Finish();
            }
        }

		void DoPosition()
		{
			if (rigidBody != null)
			{
				rigidBody.MovePosition(position.Value);
			}
		}

	}
}
