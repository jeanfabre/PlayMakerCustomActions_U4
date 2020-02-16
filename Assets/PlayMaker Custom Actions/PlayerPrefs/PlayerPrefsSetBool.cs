// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// @keyword: playerpref player pref preference

using UnityEngine;
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("PlayerPrefs")]
	[Tooltip("Sets the value of the preference identified by key. Uses ints to save bool value, as playerprefs doesn't have bool type built in.")]
	public class PlayerPrefsSetBool : FsmStateAction
	{
		[CompoundArray("Count", "Key", "Value")]
		[Tooltip("Case sensitive key.")]
		public FsmString[] keys;
		public FsmBool[] values;

		public override void Reset()
		{
			keys = new FsmString[1];
			values = new FsmBool[1];
		}

		public override void OnEnter()
		{
			for(int i = 0; i<keys.Length;i++){
				int _value = values[i].IsNone ? 0 : values[i].Value?1:0;
				if(!keys[i].IsNone || !keys[i].Value.Equals("")) PlayerPrefs.SetInt(keys[i].Value,_value);
			}
			Finish();
		}

	}
}
