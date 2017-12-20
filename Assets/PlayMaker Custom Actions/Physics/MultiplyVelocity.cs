// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
/* original action by trentSterling on http://hutonggames.com/playmakerforum/index.php?topic=8416.msg32820 */

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Multiplies the Velocity of a Game Object. To leave any axis unchanged, set variable to 'None'. NOTE: Game object must have a rigidbody.")]
	public class MultiplyVelocity : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector;
		
		public FsmFloat x;
		public FsmFloat y;
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
			space = Space.World;
			everyFrame = true;
		}

		public override void OnPreprocess()
		{
			Fsm.HandleFixedUpdate = true;
		}		

		public override void OnFixedUpdate()
		{
			DoSetVelocity ();
			if (!everyFrame)
			{
				Finish();
			}
		}

		void DoSetVelocity ()
		{
			var go = Fsm.GetOwnerDefaultTarget (gameObject);
			if (go == null || go.rigidbody == null) {
				return;
			}
			
			Vector3 velocity;

			if (vector.IsNone) {
				velocity = space == Space.World ? go.rigidbody.velocity : go.transform.InverseTransformDirection (go.rigidbody.velocity);
			} else {
				velocity = vector.Value;
			}
			
			// override any axis

			if (!x.IsNone)
				velocity.x *= x.Value;
			if (!y.IsNone)
				velocity.y *= y.Value;
			if (!z.IsNone)
				velocity.z *= z.Value;

			// apply
			
			go.rigidbody.velocity = space == Space.World ? velocity : go.transform.TransformDirection (velocity);
		}
	}
}
