// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("Get System Language")]
	public class ApplicationGetSystemLanguage : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The application language")]
		public FsmString language;

		public override void Reset()
		{
			language = null;
		}

		public override void OnEnter()
		{
			language.Value = Application.systemLanguage.ToString();
			
			Finish();
		}

	}
}
