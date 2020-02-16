// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// http://hutonggames.com/playmakerforum/index.php?topic=7004.0

using UnityEngine;
using System.Text.RegularExpressions;

namespace HutongGames.PlayMaker.Actions {

	[ActionCategory(ActionCategory.String)]
	[Tooltip("Execute a Regex on a string and to replace match.")]
	public class StringRegexReplace : FsmStateAction {

		[RequiredField]
		[Tooltip("The string to execute the regex on")]
		public FsmString stringVariable;

		[Tooltip("The regex expression.")]
		public FsmString regex;

		[Tooltip("The string to replace matches with.")]
		public FsmString with;

		[Tooltip("The regex expression options.")]
		public RegexOptions[] options;

		[RequiredField, UIHint(UIHint.Variable)]
		[Tooltip("The new string with Match content replaced.")]
		public FsmString storeResult;

		public bool everyFrame;
		
		public override void Reset() {
			stringVariable = new FsmString() {UseVariable=true};
			regex = new FsmString { Value = "" };
			with = new FsmString { Value = "" };
			options = new RegexOptions[0];
			storeResult = null;
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
				
				storeResult.Value = Regex.Replace( stringVariable.Value, regex.Value, with.Value, optionsBit );
			} else {
				storeResult.Value = Regex.Replace( stringVariable.Value, regex.Value, with.Value );
			}
		}
	}
}
