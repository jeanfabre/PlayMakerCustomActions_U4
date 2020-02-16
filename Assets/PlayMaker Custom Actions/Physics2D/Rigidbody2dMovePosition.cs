// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Moves a RigidBody2d to a particular position. It's more efficient then accessing the transform of the gameObject and interacts properly with colliders and physics materials")]
	public class RigidBody2dMovePosition : ComponentAction<Rigidbody2D>
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The rigid body to move.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The position to move to.")]
		public FsmVector2 position;

		[Tooltip("The position to move to as vector3.")]
		public FsmVector3 orVector3position;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
		public override void Reset ()
		{
			gameObject = null;
			position = null;
			orVector3position = new FsmVector3(){UseVariable=true};
			everyFrame = true;
		}

		public override void OnPreprocess ()
		{
			Fsm.HandleFixedUpdate = true;
		}

		public override void OnEnter()
		{
			DoPosition();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}

		public override void OnFixedUpdate ()
		{
			DoPosition ();
            
			if (!everyFrame) {
				Finish ();
			}
		}

		void DoPosition ()
		{

			if (!UpdateCache (Fsm.GetOwnerDefaultTarget (gameObject))) {
				return;
			}

			Vector2 _pos = position.Value;
			if (!orVector3position.IsNone)
			{
				_pos.x = orVector3position.Value.x;
				_pos.y = orVector3position.Value.y;
			}

			rigidbody2d.MovePosition (position.Value);
		}

	}
}
