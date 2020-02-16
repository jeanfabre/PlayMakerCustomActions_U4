// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts an String value to an Int value.")]
	public class ConvertFormatedStringToInt : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The String variable to convert to an integer.")]
		public FsmString stringVariable;
		
		public System.Globalization.NumberStyles numberStyle;
		
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the result in an Int variable.")]
		public FsmInt intVariable;

        [Tooltip("Repeat every frame. Useful if the String variable is changing.")]
		public bool everyFrame;

		public override void Reset()
		{
			intVariable = null;
			stringVariable = null;
			numberStyle = System.Globalization.NumberStyles.Any;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoConvertStringToInt();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoConvertStringToInt();
		}
		
		void DoConvertStringToInt()
		{
			intVariable.Value = int.Parse(stringVariable.Value,numberStyle);
		}
	}
}
