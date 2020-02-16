// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Find the modulo between two ints dividend % diviser.")]
	public class IntModulo : FsmStateAction
	{
		
		public FsmInt dividend;
		
		public FsmInt diviser;

		[UIHint(UIHint.Variable)]
		public FsmFloat result;
		
		[UIHint(UIHint.Variable)]
		public FsmInt resultAsInt;
		
		public bool everyFrame;

		public override void Reset()
		{
			dividend = null;
			diviser = null;
			result = null;
			resultAsInt = null;
			
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
				int _mod = dividend.Value % diviser.Value;
				result.Value = (float)_mod;
				resultAsInt.Value = _mod;
				
			}catch(Exception e)
			{
				Debug.LogWarning("Int Modulo error: "+e);
			}
		}
	}
}
