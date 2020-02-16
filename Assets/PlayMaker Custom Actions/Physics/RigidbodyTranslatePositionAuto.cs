// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Moves the position of a Rigidbody, calcs and smoothing are internal.")]
	public class RigidbodyTranslatePositionAuto : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		[Tooltip("The GameObject to move.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The horizontal axis name as defined in the input manager.")]
		public FsmString horizontalAxis;
		
		[Tooltip("The vertical axis name as defined in the input manager.")]
		public FsmString verticalAxis;

		[Tooltip("The speed of the rigidbody.")]
		public FsmFloat speed;

		[UIHint(UIHint.Variable)]
		public FsmVector3 storeVector;

		private GameObject go;
		private Rigidbody rb;
		private Vector3 movement;

		public override void Reset()
		{
			gameObject = null;
			horizontalAxis = "Horizontal";
			verticalAxis = "Vertical";
			speed = 5.0f;
			storeVector = null;
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
			float h = Input.GetAxisRaw (horizontalAxis.Value);
			float v = Input.GetAxisRaw (verticalAxis.Value);

			DoMovement(h, v);
		}

		void DoMovement(float h, float v)
		{
			movement.Set (h, 0f, v);
			movement = movement.normalized * speed.Value * Time.deltaTime;
			rb.MovePosition (go.transform.position + movement);

			storeVector.Value = movement;

		}
	}
}
