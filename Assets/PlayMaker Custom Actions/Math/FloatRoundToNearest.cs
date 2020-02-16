// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Round a Float to the specified nearest value. Optional Int store value.")]
	public class FloatRoundToNearest : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat floatVariable;
		
		public FsmFloat nearest;

		[UIHint(UIHint.Variable)]
		public FsmInt resultAsInt;

		[UIHint(UIHint.Variable)]
		public FsmFloat resultAsFloat;
		
		public bool everyFrame;

		public override void Reset()
		{
			floatVariable = null;
			nearest = 10;
			resultAsInt = null;
			resultAsFloat = null;
			
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoFloatRound();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoFloatRound();
		}
		
		void DoFloatRound()
		{
			resultAsFloat.Value = Mathf.Round (floatVariable.Value / nearest.Value) * nearest.Value;

			if (resultAsInt != null)
			{
				resultAsInt.Value = Mathf.RoundToInt(resultAsFloat.Value);
			}

		}
	}
}
