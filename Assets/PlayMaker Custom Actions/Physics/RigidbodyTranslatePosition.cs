// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Moves the position of a Rigidbody, calcs and smoothing are NOT included.")]
	public class RigidbodyTranslatePosition : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		[Tooltip("The GameObject to move.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("The vector input for the rigidbody. This is the distance in units that it will move from its current position per FixedUpdate.")]
		public FsmVector3 inputVector;

		private GameObject go;
		private Rigidbody rb;

		public override void Reset()
		{
			gameObject = null;
			inputVector = null;
		}

		public override void OnPreprocess()
		{
			Fsm.HandleFixedUpdate = true;
		}

		public override void OnEnter()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);
			rb = go.GetComponent <Rigidbody> ();
		}

		public override void OnFixedUpdate()
		{
			DoMovement();
		}

		void DoMovement()
		{
			rb.MovePosition (go.transform.position + inputVector.Value);
		}
	}
}
