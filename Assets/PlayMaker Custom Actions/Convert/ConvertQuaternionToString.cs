// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts a Quaternion to a string. Format is flexible, [x] [y] [z] and [w] are placeholders")]
	public class ConvertQuaternionToString : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The value itself")]
		public FsmQuaternion quaternion;
		
		[Tooltip("The string pattern to express the Quaternion value.")]
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
			quaternion = null;
			format = "Quaternion([x],[y],[z],[w])";
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
			
			Quaternion _val = quaternion.Value;
			
			_result = _result.Replace("[x]" , _val.x.ToString(_fFormat));
			_result = _result.Replace("[y]" , _val.y.ToString(_fFormat));
			_result = _result.Replace("[z]" , _val.z.ToString(_fFormat));
			_result = _result.Replace("[w]" , _val.w.ToString(_fFormat));
			
			 storeResult.Value = _result;
		}
		
	}
}
