// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__Internal/FsmStateActionAdvanced.cs"
					]
}
EcoMetaEnd
---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Clamps the value of Float Variable to a Min/Max. This advanced versrion let you choose between the different types of updates")]
	public class FloatClampAdvanced : FsmStateActionAdvanced
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("Float variable to clamp.")]
		public FsmFloat floatVariable;

		[RequiredField]
        [Tooltip("The minimum value.")]
		public FsmFloat minValue;

		[RequiredField]
        [Tooltip("The maximum value.")]
		public FsmFloat maxValue;


		public override void Reset()
		{
			base.Reset();
			floatVariable = null;
			minValue = null;
			maxValue = null;

		}

		public override void OnActionUpdate()
		{
			DoClamp();
		}
		
		void DoClamp()
		{
			floatVariable.Value = Mathf.Clamp(floatVariable.Value, minValue.Value, maxValue.Value);
		}
	}
}
