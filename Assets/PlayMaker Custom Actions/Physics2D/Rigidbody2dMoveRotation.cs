// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Rotates a RigidBody2d to a particular rotation. It's more efficient then accessing the transform of the gameObject and interacts properly with colliders and physics materials")]
	public class RigidBody2dMoveRotation : ComponentAction<Rigidbody2D>
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The rigid body to rotate.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("A angle rotation.")]
		public FsmFloat rotation;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		private Rigidbody rigidBody;
		
		public override void Reset ()
		{
			gameObject = null;
			rotation = null;
			everyFrame = true;
		}

		public override void OnPreprocess ()
		{
			Fsm.HandleFixedUpdate = true;
		}
		
		public override void OnEnter ()
		{
			DoRotate ();
			
			if (!everyFrame) {
				Finish ();
			}
				
		}

		public override void OnFixedUpdate ()
		{
			DoRotate ();
           
		}

		void DoRotate ()
		{
			if (!UpdateCache (Fsm.GetOwnerDefaultTarget (gameObject))) {
				return;
			}

			rigidbody2d.MoveRotation(rotation.Value);
		}

	}
}
