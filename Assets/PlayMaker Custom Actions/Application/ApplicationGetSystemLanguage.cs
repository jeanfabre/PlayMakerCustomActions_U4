// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
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
