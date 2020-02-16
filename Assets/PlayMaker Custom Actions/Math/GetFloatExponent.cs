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
	[Tooltip("Store a float exponent")]
	public class GetFloatExponent : FsmStateActionAdvanced
	{
		[RequiredField]
		[Tooltip("The power float variable.")]
		public FsmFloat power;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The exponent")]
		public FsmFloat exponent;


		public override void Reset()
		{
			base.Reset ();

			power = new FsmFloat() {UseVariable =true};
			exponent = null;

		}
		
		public override void OnEnter()
		{
			OnActionUpdate ();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnActionUpdate()
		{

			if (!exponent.IsNone) exponent.Value = Mathf.Exp(power.Value);

		}

	}
}