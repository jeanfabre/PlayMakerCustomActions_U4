// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts a vector2 to a string. Format is flexible, [x] and [y] are placeholders")]
	public class ConvertVector2ToString : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The value itself")]
		public FsmVector2 vector2;
		
		[Tooltip("The string pattern to express the Vector2 value.")]
		public FsmString format;
		
		[Tooltip("The c# ToString() format to express each vector float components as strings. Useful if you want to optimized the generated string.")]
		public FsmString floatConversion;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The result")]
		public FsmString storeResult;
		
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		public override void Reset()
		{
			vector2 = null;
			format = "Vector2([x],[y])";
			floatConversion = "G";
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoToString();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoToString();
		}
		
		void DoToString()
		{
			string _fFormat = string.IsNullOrEmpty(floatConversion.Value)?"G":floatConversion.Value;
			
			string _result = format.Value;
			
			Vector2 _val = vector2.Value;
			
			_result = _result.Replace("[x]" , _val.x.ToString(_fFormat));
			_result = _result.Replace("[y]" , _val.y.ToString(_fFormat));
			
			 storeResult.Value = _result;
		}
		
	}
}
