// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Set a Rigidbody rotation. It's more efficient then accessing the transform of the gameObject.")]
	public class SetRigidbodyRotation : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		[Tooltip("The rigid body to rotate.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The rotation.")]
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
            DoRotation();
           
            if (!everyFrame)
            {
                Finish();
            }
        }

		void DoRotation()
		{
			if (rigidBody != null)
			{
				rigidBody.rotation = rotation.Value;
			}
		}

	}
}
