// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// based on http://hutonggames.com/playmakerforum/index.php?topic=2493.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Sets the Angular Velocity of a Game Object. To leave any axis unchanged, set variable to 'None'. NOTE: Game object must have a rigidbody.")]
	public class SetAngularVelocity : ComponentAction<Rigidbody> 
	{
		[RequiredField]
		[Tooltip("The Rigidbody GameObject with to set Angular Velocity")]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The Angular Velocity")]
		public FsmVector3 vector;

		[Tooltip("The x axis Angular Velocity")]
		public FsmFloat x;

		[Tooltip("The y axis Angular Velocity")]
		public FsmFloat y;

		[Tooltip("The z axis Angular Velocity")]
		public FsmFloat z;
		
		public Space space;
		
		public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
			vector = null;
			// default axis to variable dropdown with None selected.
			x = new FsmFloat { UseVariable = true };
			y = new FsmFloat { UseVariable = true };
			z = new FsmFloat { UseVariable = true };
			space = Space.Self;
			everyFrame = false;
		}

		public override void OnPreprocess()
		{
			Fsm.HandleFixedUpdate = true;
		}

		public override void OnEnter()
		{
			DoSetAngularVelocity();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}
		
		public override void OnFixedUpdate()
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
			
			// init position
			
			Vector3 angularVelocity;
			
			if (vector.IsNone)
			{
				angularVelocity = space == Space.World ?
					rigidbody.angularVelocity : 
					rigidbody.transform.InverseTransformDirection(rigidbody.angularVelocity);
			}
			else
			{
				angularVelocity = vector.Value;
			}
			
			// override any axis
			
			if (!x.IsNone) angularVelocity.x = x.Value;
			if (!y.IsNone) angularVelocity.y = y.Value;
			if (!z.IsNone) angularVelocity.z = z.Value;
			
			// apply
			
			rigidbody.angularVelocity = 
				space == Space.World ? 
					angularVelocity : 
					rigidbody.transform.TransformDirection(angularVelocity);
		}
		
	}
}