// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Get the start distance of the fog")]
	public class GetFogStartDistance : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The fog start distance")]
		public FsmFloat startDistance;
		
		[Tooltip("Repeat every frame. Useful if the Fog setting is changing.")]
		public bool everyFrame;
		
		
		public override void Reset()
		{
			startDistance =null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			_getValue();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			_getValue();
		}
		
		void _getValue()
		{
			startDistance.Value = RenderSettings.fogStartDistance;
		}
	}
}
