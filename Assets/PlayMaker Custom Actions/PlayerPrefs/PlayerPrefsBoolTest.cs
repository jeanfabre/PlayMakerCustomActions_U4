// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// @keyword: playerpref player pref preference

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("PlayerPrefs")]
	[Tooltip("Sends event based on the value corresponding to key in the preference file.")]
	public class PlayerPrefsBoolTest : FsmStateAction
	{

		[Tooltip("Case sensitive key.")]
		public FsmString key;
		
		[UIHint(UIHint.Variable)]
        [Tooltip("The bool value of this playerpref key.")]
		public FsmBool boolVariable;

        [Tooltip("Event to send if the PlayerPref Boolvalue is True.")]
		public FsmEvent isTrue;

        [Tooltip("Event to send if the PlayerPref Bool value is False.")]
		public FsmEvent isFalse;
		
		 [Tooltip("Event to send if the PlayerPref Bool key was not found.")]
		public FsmEvent keyNotFound;

		public override void Reset()
		{
			key = null;
			boolVariable = null;
			isTrue = null;
			isFalse = null;
			keyNotFound = null;
		}

		public override void OnEnter()
		{
			bool _value =false;
			if(!key.IsNone || !key.Value.Equals(""))
			{
				_value = PlayerPrefs.GetInt(key.Value) == 1;
				boolVariable.Value = _value;
				
				Fsm.Event(_value ? isTrue : isFalse);
			}else{
				Fsm.Event(keyNotFound);
			}
			
			Finish();
		}

	}
}
