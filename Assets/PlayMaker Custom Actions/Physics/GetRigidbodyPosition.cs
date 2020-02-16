// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets the Position of a Physics Game Object and stores it in a Vector3 Variable or each Axis in a Float Variable. It's more efficient than getting the rotation of the gameobject")]
	public class GetRigidbodyPosition : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.Variable)]
		public FsmVector3 position;
		
		[UIHint(UIHint.Variable)]
		public FsmFloat x;
		[UIHint(UIHint.Variable)]
		public FsmFloat y;
		[UIHint(UIHint.Variable)]
		public FsmFloat z;
		
		public bool everyFrame;
		
		private Rigidbody rigidBody;
		
		public override void Reset()
		{
			gameObject = null;
			position = null;
			x = null;
			y = null;
			z = null;

			everyFrame = false;
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
			DoGetPosition();
			
			if (!everyFrame)
			{
				Finish();
			}	
		}

		void DoGetPosition()
		{	
			if (rigidBody == null)
			{
				return;
			}
			
			
			position.Value = rigidBody.position;
			
			if (!x.IsNone || !y.IsNone || !z.IsNone)
			{
				x.Value = rigidBody.position.x;
				y.Value = rigidBody.position.y;
				z.Value = rigidBody.position.z;
			}
		}


	}
}
