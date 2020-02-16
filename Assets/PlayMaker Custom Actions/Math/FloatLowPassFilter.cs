// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Use a low pass filter to reduce the influence of sudden changes in a float Variable.")]
	public class FloatLowPassFilter : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Variable to filter. Should generally come from some constantly updated input")]
		public FsmFloat floatVariable;
		
		[Tooltip("Determines how much influence new changes have. E.g., 0.1 keeps 10 percent of the unfiltered vector and 90 percent of the previously filtered value")]
		public FsmFloat filteringFactor;		
		
		float filteredFloat;
		
		public override void Reset()
		{
			floatVariable = null;
			filteringFactor = 0.1f;
		}

		public override void OnEnter()
		{
			filteredFloat = floatVariable.Value;
		}

		public override void OnUpdate()
		{
			filteredFloat = (floatVariable.Value * filteringFactor.Value) + (filteredFloat * (1.0f - filteringFactor.Value));
			
			floatVariable.Value = filteredFloat;
		}
	}
}

