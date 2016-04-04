// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ 
EcoMetaStart
{
	"script dependancies":[
	   "Assets/PlayMaker Custom Actions/__internal/FsmStateActionAdvanced.cs"
	   ]
}
EcoMetaEnd
---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Store sign of a float as -1 or 1.")]
	public class GetFloatSign : FsmStateActionAdvanced
	{
		[RequiredField]
		[Tooltip("The float variable.")]
		public FsmFloat floatVariable;


		[UIHint(UIHint.Variable)]
		[Tooltip("The sign as float. -1f or 1f")]
		public FsmFloat sign;


		[UIHint(UIHint.Variable)]
		[Tooltip("The sign as int. -1 or 1")]
		public FsmInt signAsInt;


		public override void Reset()
		{
			base.Reset ();

			floatVariable = new FsmFloat() {UseVariable =true};
			sign = null;
			signAsInt = null;

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



			if (!sign.IsNone) sign.Value = Mathf.Sign(floatVariable.Value);

			if (!signAsInt.IsNone) signAsInt.Value = floatVariable.Value == 0f ?1:System.Math.Sign(floatVariable.Value);
		}

	}
}