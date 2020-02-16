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
	[Tooltip("Loops the value so that it's never larger than length and never smaller than 0.")]
	public class FloatRepeat : FsmStateActionAdvanced
	{
		[RequiredField]
        [Tooltip("Float variable to repeat.")]
		public FsmFloat floatVariable;

		[Tooltip("It True, FloatVariable is multiplied by Time.time for time based repetition")]
		public bool timeBased;

        [Tooltip("The length value.")]
		public FsmFloat length;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The result")]
		public FsmFloat result;

		float _input;

		public override void Reset()
		{
			base.Reset();

			floatVariable = null;
			timeBased = false;
			length = 10f;
			result = null;
			everyFrame = true;

		}

		public override void OnActionUpdate()
		{
			DoRepeat();

		}
		
		void DoRepeat()
		{
			_input = floatVariable.Value;
			if (timeBased) _input *= Time.time;
			
			result.Value = Mathf.Repeat(_input, length.Value);
		}
	}
}
