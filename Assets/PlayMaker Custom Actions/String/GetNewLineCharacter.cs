// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System;
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Gets the newline character, prevents having to copy paste from text editor. Use it in StringSplit action for example")]
	public class GetNewLineCharacter : FsmStateAction
	{
		
		[Tooltip("The newline string.")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString newLine;

		
		
		public override void Reset()
		{
			newLine = null;
			
		}
		
		public override void OnEnter()
		{
			newLine.Value = Environment.NewLine;

			Finish();
		}
		

		
	}
}
