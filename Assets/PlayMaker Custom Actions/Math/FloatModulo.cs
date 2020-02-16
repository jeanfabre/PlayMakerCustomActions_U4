// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Find the modulo between two floats dividend % diviser.")]
	public class FloatModulo : FsmStateAction
	{
		
		public FsmFloat dividend;
		
		public FsmFloat diviser;

		[UIHint(UIHint.Variable)]
		public FsmFloat result;
		
		public bool everyFrame;

		public override void Reset()
		{
			dividend = null;
			diviser = null;
			result = null;
			
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoModulo();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoModulo();
		}
		
		void DoModulo()
		{
			try{
				result.Value = dividend.Value % diviser.Value;
			}catch(Exception e)
			{
				Debug.LogWarning("Float Modulo error: "+e);
			}
		}
	}
}
