// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Get the ambient light color")]
	public class GetAmbientLight : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The ambient light color")]
		public FsmColor ambientLight;
		
		[Tooltip("Repeat every frame. Useful if the setting is changing.")]
		public bool everyFrame;
		
		
		public override void Reset()
		{
			ambientLight =null;
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
			ambientLight.Value = RenderSettings.ambientLight;
		}
	}
}
