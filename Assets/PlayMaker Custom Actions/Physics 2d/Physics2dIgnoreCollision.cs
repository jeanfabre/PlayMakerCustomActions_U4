// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Physics 2d")]
	#if UNITY_4_3 || UNITY_4_4
	[Obsolete("This action is only available starting from Unity 4.5 onward")]
	#endif
	[Tooltip("Makes the 2d collision detection system ignore all collisions between collider1 and collider2.")]
	public class Physics2dIgnoreCollision : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The first collider")]
		[CheckForComponent(typeof(Collider2D))]
		public FsmOwnerDefault collider1;
		
		[RequiredField]
		[Tooltip("The second collider")]
		[CheckForComponent(typeof(Collider2D))]
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
			if (go1==null || go1.collider2D==null)
			{
				return;
			}
			
			GameObject go2 = collider2.Value;
			if (go2==null || go2.collider2D==null)
			{
				return;
			}

			if (go1.activeInHierarchy)
			{
				Debug.LogWarning(go1.name+" not active, ignoring collision failed");
				return;
			}
			if (go2.activeInHierarchy)
			{
				Debug.LogWarning(go2.name+" not active, ignoring collision failed");
				return;
			}

			#if UNITY_4_3 || UNITY_4_4
			#else
			Physics2D.IgnoreCollision(go1.collider2D, go2.collider2D,ignoreCollision.Value);
			#endif
		}
		
	}
}

