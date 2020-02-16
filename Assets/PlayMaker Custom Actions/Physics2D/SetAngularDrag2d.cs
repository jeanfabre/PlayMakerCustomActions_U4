// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// original action by dudeBxl
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Sets the angular drag that applies to rotational movement and is set up separately from the linear drag that affects positional movement.")]
	public class SetAngularDrag2d : ComponentAction<Rigidbody2D>
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[HasFloatSlider(0.0f,10f)]
		public FsmFloat drag;
		
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			drag = 1;
			everyFrame = false;
		}

		public override void OnPreprocess()
		{
			Fsm.HandleFixedUpdate = true;
		}

		public override void OnEnter()
		{
			DoSetDrag();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnFixedUpdate()
		{
			DoSetDrag();
            
            		if (!everyFrame)
			{
				Finish();
			}
		}

		void DoSetDrag()
		{
			if (!UpdateCache (Fsm.GetOwnerDefaultTarget (gameObject))) {
				return;
			}

			rigidbody2d.angularDrag = drag.Value;
		}
	}
}
