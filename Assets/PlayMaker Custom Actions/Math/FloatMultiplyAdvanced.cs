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
	[Tooltip("Multiplies one Float by another. This advanced version lets you choose betweent the different types of updates")]
	public class FloatMultiplyAdvanced : FsmStateActionAdvanced
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The float variable to multiply.")]
		public FsmFloat floatVariable;

		[RequiredField]
        [Tooltip("Multiply the float variable by this value.")]
		public FsmFloat multiplyBy;


		public override void Reset()
		{
			base.Reset();
			
			floatVariable = null;
			multiplyBy = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			floatVariable.Value *= multiplyBy.Value;
			
			if (!everyFrame)
			{
			    Finish();
			}
		}

		public override void OnActionUpdate()
		{
			floatVariable.Value *= multiplyBy.Value;
		}
	}
}
