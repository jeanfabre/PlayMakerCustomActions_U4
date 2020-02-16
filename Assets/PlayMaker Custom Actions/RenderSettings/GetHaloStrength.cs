// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Get the Size of the Light halos.")]
	public class GetHaloStrength : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The size of the Light halos.")]
		public FsmFloat haloStrength;
		
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;
		
		
		public override void Reset()
		{
			haloStrength =null;
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
			haloStrength.Value = RenderSettings.haloStrength;
		}
	}
}
