// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// based on http://hutonggames.com/playmakerforum/index.php?topic=2493.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets the Angular Velocity of a Game Object and stores it in a Vector3 Variable or each Axis in a Float Variable. NOTE: The Game Object must have a Rigid Body.")]
	public class GetAngularVelocity : ComponentAction<Rigidbody> {
		
		[RequiredField]
		[Tooltip("The Rigidbody GameObject with to get Angular Velocity from")]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;

		[Tooltip("The Angular Velocity")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector;

		[Tooltip("The Angular Velocity x axis value")]
		[UIHint(UIHint.Variable)]
		public FsmFloat x;

		[Tooltip("The Angular Velocity y axis value")]
		[UIHint(UIHint.Variable)]
		public FsmFloat y;

		[Tooltip("The Angular Velocity z axis value")]
		[UIHint(UIHint.Variable)]
		public FsmFloat z;

		[Tooltip("The Angular Velocity value referencing")]
		public Space space;

		[Tooltip("Repeat every frame. Useful to watch value evolve over time")]
		public bool everyFrame;

		Vector3 _angularVelocity;

		public override void Reset()
		{
			gameObject = null;
			vector = null;
			x = null;
			y = null;
			z = null;
			space = Space.World;
			everyFrame = false;
		}

		public override void OnPreprocess()
		{
			Fsm.HandleFixedUpdate = true;
		}

		public override void OnEnter()
		{
			DoGetAngularVelocity();
			
			if (!everyFrame)
				Finish();		
		}
		
		public override void OnFixedUpdate()
		{
			DoGetAngularVelocity();
		}
		
		void DoGetAngularVelocity()
		{
			if (!UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
			{
				return;
			}

			_angularVelocity = rigidbody.angularVelocity;
			
			if (space == Space.Self)
			{
				_angularVelocity = rigidbody.transform.InverseTransformDirection(_angularVelocity);
			}

			if (!vector.IsNone)
			{
				vector.Value = _angularVelocity;
			}

			if (!x.IsNone)
			{
				x.Value = _angularVelocity.x;
			}

			if (!y.IsNone)
			{
				y.Value = _angularVelocity.y;
			}

			if (!z.IsNone)
			{
				z.Value = _angularVelocity.z;
			}
		}
	}
}