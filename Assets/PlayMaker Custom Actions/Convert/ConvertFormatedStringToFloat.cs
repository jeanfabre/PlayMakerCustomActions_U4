// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts an String value to a Float value.")]
	public class ConvertFormatedStringToFloat : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The String variable to convert to an Float.")]
		public FsmString stringVariable;
		
		public System.Globalization.NumberStyles numberStyle;
		
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the result in an Float variable.")]
		public FsmFloat floatVariable;

        [Tooltip("Repeat every frame. Useful if the String variable is changing.")]
		public bool everyFrame;

		public override void Reset()
		{
			floatVariable = null;
			stringVariable = null;
			numberStyle = System.Globalization.NumberStyles.Any;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoConvertStringToFloat();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoConvertStringToFloat();
		}
		
		void DoConvertStringToFloat()
		{
			float result = 0f;
			
			float.TryParse(stringVariable.Value,numberStyle,null,out result);
			
			floatVariable.Value = result;
		}
	}
}
