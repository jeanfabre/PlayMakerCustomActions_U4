// Reference and Credit information
//
// Action compiled by Lane Fox
//
// Original code Link:
// http://forum.unity3d.com/threads/3848-Getting-AI-to-lead-with-its-shots
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Compute the intercept point for firing a projectile at a moving target.")]
	public class LeadTarget : FsmStateAction
	{
			[ActionSection("    Primary variables")] 

		[RequiredField]
		[Tooltip("The object firing the projectile.")]
		public FsmOwnerDefault theOrigin;

		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		[Tooltip("The target object to lead.")]
		public FsmGameObject theTarget;

		[RequiredField]
		[Tooltip("The resulting intercept position.")]
		public FsmVector3 interceptPosition;

			[ActionSection("    Options")]

		[Tooltip("The speed of the projectile being fired.")]
		public FsmFloat weaponSpeed;

		[Tooltip("A magic mulitplier number you probably don't need to change.")]
		public FsmFloat magicNumber;

		[Tooltip("How often to process. 0 = do once; 1 = every frame; 2 = every other frame. \nDo not change at runtime.")]
		public FsmInt repeatInterval;

		int repeat;

		public override void Reset()
		{
			theOrigin = null;
			theTarget = null;
			interceptPosition = null;
			weaponSpeed = 500;
			magicNumber = 1.15f;
			repeatInterval = 1;
		}

		public override void OnEnter()
		{
			DoLeadTarget();
			
			if (repeatInterval.Value == 0)
			{
				Finish();
			}
		}

		public override void OnUpdate ()
		{
			repeat--;
			
			if (repeat == 0)
			{
				DoLeadTarget();
			}
		}

		void DoLeadTarget ()
		{
			repeat = repeatInterval.Value;

			var Origin = Fsm.GetOwnerDefaultTarget(theOrigin);
			var Target = theTarget.Value;
			var projectileSpeed = weaponSpeed.Value;
			float mult = magicNumber.Value;

			if (Target)
			{
		
				// actual distance to target -- OKAY
				var distTarget = Vector3.Distance(Target.transform.position, Origin.transform.position);

				// first calculation, using actual distance
				var velocityPosition1 = Target.transform.position + ((Target.rigidbody.velocity*mult) * (distTarget/projectileSpeed));
				var velocityDist1 = Vector3.Distance(velocityPosition1, Origin.transform.position);
		
				// second calc., using distance from first calc.
				var velocityPosition2 = Target.transform.position + ((Target.rigidbody.velocity*mult) * (velocityDist1/projectileSpeed));
				var velocityDist2 = Vector3.Distance(velocityPosition2, Origin.transform.position);
			
				// third calc., using distance from second calc.
				var TargetInterceptPosition = Target.transform.position + ((Target.rigidbody.velocity*mult) * (velocityDist2/projectileSpeed));

				// store the result upwards so we can use it at will.
				interceptPosition.Value = TargetInterceptPosition;
			}
		}
	}
}
