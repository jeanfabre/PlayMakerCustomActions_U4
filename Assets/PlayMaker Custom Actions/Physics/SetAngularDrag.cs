// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Sets the Angular drag of a Game Object Rigidbody. NOTE: Game object must have a rigidbody.")]
	public class SetAngularDrag : ComponentAction<Rigidbody> 
	{
		[RequiredField]
		[Tooltip("The Rigidbody GameObject with to set Angular Velocity")]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The Angular drag. Must be non negative!")]
		public FsmFloat angularDrag;
		
		public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
			angularDrag = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetAngularVelocity();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}
		
		public override void OnUpdate()
		{
			DoSetAngularVelocity();
			
			if (!everyFrame)
				Finish();
		}
		
		void DoSetAngularVelocity()
		{
			if (!UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
			{
				return;
			}

			if (angularDrag.Value<0)
			{
				LogError("The angular damping must be nonnegative!");
			}else{

				rigidbody.angularDrag = angularDrag.Value;
			}
		}
		
	}
}