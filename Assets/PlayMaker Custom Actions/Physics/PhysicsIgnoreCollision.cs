// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Makes the collision detection system ignore all collisions between collider1 and collider2.")]
	public class PhysicsIgnoreCollision : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The first collider")]
		[CheckForComponent(typeof(Collider))]
		public FsmOwnerDefault collider1;
		
		[RequiredField]
		[Tooltip("The second collider")]
		[CheckForComponent(typeof(Collider))]
		public FsmGameObject collider2;
		
		[Tooltip("Wether to ignore or not the collisions between collider 1 and 2.")]
		public FsmBool ignoreCollision;
		
		public override void Reset()
		{
			collider1 = null;
			collider2 = null;
			ignoreCollision = true;
		}

		public override void OnEnter()
		{
			doIgnoreCollision();
			
			Finish();
		}
		
		public void doIgnoreCollision()
		{
			GameObject go1 = Fsm.GetOwnerDefaultTarget(collider1);
			if (go1==null || go1.collider==null)
			{
				return;
			}

			GameObject go2 = collider2.Value;
			if (go2==null || go2.collider==null)
			{
				return;
			}

			Physics.IgnoreCollision(go1.collider, go2.collider,ignoreCollision.Value);
		}

	}
}

