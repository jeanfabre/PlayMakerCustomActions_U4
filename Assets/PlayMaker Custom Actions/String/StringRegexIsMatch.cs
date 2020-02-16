// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// http://hutonggames.com/playmakerforum/index.php?topic=7005.msg34168#msg34168


using UnityEngine;
using System.Text.RegularExpressions;

namespace HutongGames.PlayMaker.Actions {

	[ActionCategory(ActionCategory.String)]
	[Tooltip("Execute a Regex on a string and return whether a match was found. Use StringRegexMatch if you want to store the result")]
	public class StringRegexIsMatch : FsmStateAction {

		[RequiredField]
		[Tooltip("The string to execute the regex on")]
		public FsmString stringVariable;

		[RequiredField]
		[Tooltip("The regex expression.")]
		public FsmString regex;

		[Tooltip("The regex expression options.")]
		public RegexOptions[] options;

		[UIHint(UIHint.Variable)]
		[Tooltip("True if the regex matched.")]
		public FsmBool storeIsMatch;

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
			storeIsMatch = null;
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
			if ( options.Length > 0 ) {
				RegexOptions optionsBit = 0;
				
				foreach ( RegexOptions option in options ) {
					optionsBit |= option;
				}
				
				storeIsMatch.Value = Regex.IsMatch( stringVariable.Value, regex.Value, optionsBit );
			} else {
				storeIsMatch.Value = Regex.IsMatch( stringVariable.Value, regex.Value );
			}


			if ( storeIsMatch.Value ) {
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
