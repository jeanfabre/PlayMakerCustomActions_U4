// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System;
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Gets the tab character, prevents having to copy paste from text editor. Use it in StringSplit action for example")]
	public class GetTabCharacter : FsmStateAction
	{
		
		[Tooltip("The tab string.")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString tab;

		
		public override void Reset()
		{
			tab = null;
			
		}
		
		public override void OnEnter()
		{
			tab.Value = "\t";

			Finish();
		}
		

		
	}
}
