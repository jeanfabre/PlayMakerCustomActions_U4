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


		[UIHint(UIHint.Variable)]
        	[Tooltip("The sign as bool. False or True")]
        	public FsmBool signAsBool;
	
		public override void Reset()
		{
			base.Reset ();

			floatVariable = new FsmFloat() {UseVariable =true};
			sign = null;
			signAsInt = null;
			signAsBool = null;

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
			
			if (!signAsBool.IsNone) signAsBool.Value = Mathf.Sign(floatVariable.Value) == 1f ? true : false;
		}

	}
}
