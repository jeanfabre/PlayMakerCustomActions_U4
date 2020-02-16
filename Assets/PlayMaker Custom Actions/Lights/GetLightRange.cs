// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: light range get
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Lights)]
	[Tooltip("Gets the Range of a Light.")]
	public class GetLightRange : ComponentAction<Light>
	{
		[RequiredField]
		[CheckForComponent(typeof(Light))]
		public FsmOwnerDefault gameObject;
		public FsmFloat lightRange;
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			lightRange = 20f;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetLightRange();

		    if (!everyFrame)
		    {
		        Finish();
		    }
		}
		
		public override void OnUpdate()
		{
			DoGetLightRange();
		}
		
		void DoGetLightRange()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (UpdateCache(go))
		    {
				lightRange.Value = light.range;
		    }
		}
	}
}
