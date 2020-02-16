// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
/* Keywords : round up */

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Returns the smallest integer greater to or equal to f.")]
	public class FloatCeiling : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The Float variable to process.")]
		public FsmFloat floatVariable;

		[UIHint(UIHint.Variable)]
		[Tooltip("The float result.")]
		public FsmFloat floatResult;

		[UIHint(UIHint.Variable)]
		[Tooltip("The int result.")]
		public FsmInt intResult;

        [Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		public override void Reset()
		{
			floatVariable = null;
			floatResult = null;
			intResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoMath();
			
			if (!everyFrame)
			{
			    Finish();
			}
		}

		public override void OnUpdate()
		{
			DoMath();
		}
		
		void DoMath()
		{
			if (!floatResult.IsNone)
			{
				floatResult.Value = (Mathf.Ceil(floatVariable.Value));
			}

			if (!intResult.IsNone)
			{
				intResult.Value = (Mathf.CeilToInt(floatVariable.Value));
			}
		}
	}
}





