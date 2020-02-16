// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: light intensity get
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Lights)]
	[Tooltip("Gets the Intensity of a Light.")]
	public class GetLightIntensity : ComponentAction<Light>
	{
		[RequiredField]
		[CheckForComponent(typeof(Light))]
		public FsmOwnerDefault gameObject;
		public FsmFloat lightIntensity;
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			lightIntensity = 1f;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetLightIntensity();

		    if (!everyFrame)
		    {
		        Finish();
		    }
		}
		
		public override void OnUpdate()
		{
			DoGetLightIntensity();
		}
		
		void DoGetLightIntensity()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (UpdateCache(go))
		    {
				lightIntensity.Value = light.intensity;
		    }
		}
	}
}
