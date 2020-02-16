// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// http://hutonggames.com/playmakerforum/index.php?topic=7006.msg34170#msg34170


using UnityEngine;
using System.Text.RegularExpressions;

namespace HutongGames.PlayMaker.Actions {

	[ActionCategory(ActionCategory.String)]
	[Tooltip("Execute a Regex on a string and return whether a match was found. ")]
	public class StringRegexMatch : FsmStateAction {

		[RequiredField]
		[Tooltip("The string to execute the regex on")]
		public FsmString stringVariable;

		[Tooltip("The regex expression.")]
		public FsmString regex;

		[Tooltip("The regex expression options.")]
		public RegexOptions[] options;

		[UIHint(UIHint.Variable)]
		[Tooltip("The regex Match Value. Check storeIsSuccess to make sure the regex did match.")]
		public FsmString storeMatch;

		[UIHint(UIHint.Variable)]
		[Tooltip("True if the regex matched.")]
		public FsmBool storeIsSuccess;

		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent if regex matches.")]
		public FsmEvent trueEvent;

		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent if regex do not matches.")]
		public FsmEvent falseEvent;

		[Tooltip("Executes every frame.")]
		public bool everyFrame;
		
		public override void Reset() {

			stringVariable = new FsmString() {UseVariable=true};

			regex = new FsmString { Value = "" };

			options = new RegexOptions[0];

			storeMatch = null;
			storeIsSuccess = null;
			trueEvent = null;
			falseEvent = null;
			everyFrame = false;
		}
		
		public override void OnEnter() {

			DoAction();
			
			if ( ! everyFrame ) {
				Finish();
			}
		}
		
		public override void OnUpdate() {
			DoAction();
		}
		
		void DoAction() {
			Match match = null;
			
			if ( options.Length > 0 ) {
				RegexOptions optionsBit = 0;
				
				foreach ( RegexOptions option in options ) {
					optionsBit |= option;
				}
				
				match = Regex.Match( stringVariable.Value, regex.Value, optionsBit );
			} else {
				match = Regex.Match( stringVariable.Value, regex.Value );
			}
			
			storeMatch.Value = match.Value;

			storeIsSuccess.Value =  match.Success;

			if ( match.Success ) {
				if ( trueEvent != null ) {
					Fsm.Event( trueEvent );
				}
			} else {
				if ( falseEvent != null ) {
					Fsm.Event( falseEvent );
				}
			}
		}
	}
}
