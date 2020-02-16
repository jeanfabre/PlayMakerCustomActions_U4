// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Gets the iPhone generation.")]
	public class GetIphoneGeneration : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The iPhone generation.")]
		public FsmString iphoneGeneration;
		
		
		public override void Reset()
		{
			iphoneGeneration = null;
		}
		
		public override void OnEnter()
		{
			DoGetIphoneGeneration();
			Finish();
		}
		
		void DoGetIphoneGeneration()
		{
			#if UNITY_IPHONE
			iphoneGeneration.Value = iPhone.generation.ToString();
			#endif
		}
		
	}
}
