// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Find nearest power of two for a given integer value")]
	public class GetNearestPowerOfTwo : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The value")]
		public FsmInt number;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The nearest power of two for that value")]
		public FsmInt nearestPowerOfTwo;
		
		[Tooltip("Repeat everyFrame")]
		public bool everyFrame;
		
		public override void Reset()
		{
			number = null;
			nearestPowerOfTwo = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoNearest();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoNearest();
		}
		
		void DoNearest()
		{
			int x = Math.Abs(number.Value);
			    --x;
			    x |= x >> 1;
			    x |= x >> 2;
			    x |= x >> 4;
			    x |= x >> 8;
			    x |= x >> 16;
			
			nearestPowerOfTwo.Value = ++x;
		}
	}
}
