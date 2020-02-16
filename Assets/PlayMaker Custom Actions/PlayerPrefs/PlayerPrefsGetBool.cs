// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// @keyword: playerpref player pref preference

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("PlayerPrefs")]
	[Tooltip("Returns the value corresponding to key in the preference file if it exists. Uses ints to save bool value, as playerprefs doesn't have bool type built in.")]
	public class PlayerPrefsGetBool : FsmStateAction
	{
		[CompoundArray("Count", "Key", "Variable")]
		[Tooltip("Case sensitive key.")]
		public FsmString[] keys;
		[UIHint(UIHint.Variable)]
		public FsmBool[] variables;

		public override void Reset()
		{
			keys = new FsmString[1];
			variables = new FsmBool[1];
		}

		public override void OnEnter()
		{
			for(int i = 0; i<keys.Length;i++){
				int _value = variables[i].Value?1:0;
				if(!keys[i].IsNone || !keys[i].Value.Equals(""))  variables[i].Value = PlayerPrefs.GetInt(keys[i].Value,_value) == 1;
			}
			Finish();
		}

	}
}
