// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// Action made by DjayDino
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// @keyword: playerpref player pref preference

using UnityEngine;
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("PlayerPrefs")]
	[Tooltip("Adds a value to a playerprefs int identified by key. WARNING!! use PlayerPrefs only at key moments")]
	public class PlayerPrefsAddInt : FsmStateAction
	{
		[Tooltip("Case sensitive key.")]
		public FsmString key;
		public FsmInt add;
		private int variables;
		
		public override void Reset()
		{
			key = "";
			add = null;
		}

		public override void OnEnter()
		{
				if(!key.IsNone || !key.Value.Equals(""))  
				variables = PlayerPrefs.GetInt(key.Value, 0);

				variables += add.Value;
				
				PlayerPrefs.SetInt(key.Value,(variables));

			Finish();
		}

	}
}
