// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts a color to a string. Format is flexible, [r] [g] [b] and [a] are placeholders")]
	public class ConvertColorToString : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The value itself")]
		public FsmColor color;
		
		[Tooltip("32bit colors ranges from 0 to 255 for their values, else, it ranges from 0 to 1")]
		public FsmBool use32BitColor;
		
		[Tooltip("The string pattern to express the color value.")]
		public FsmString format;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The result")]
		public FsmString storeResult;
		
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		public override void Reset()
		{
			color = null;
			format = "Color([r],[g],[b],[a])";
			use32BitColor = true;
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

			string _result = format.Value;
			
			if (use32BitColor.Value)
			{
				Color32 _val = color.Value;
			
				_result = _result.Replace("[r]" , _val.r.ToString());
				_result = _result.Replace("[g]" , _val.g.ToString());
				_result = _result.Replace("[b]" , _val.b.ToString());
				_result = _result.Replace("[a]" , _val.a.ToString());
			}else{
				Color _val = color.Value;
			
				_result = _result.Replace("[r]" , _val.r.ToString());
				_result = _result.Replace("[g]" , _val.g.ToString());
				_result = _result.Replace("[b]" , _val.b.ToString());
				_result = _result.Replace("[a]" , _val.a.ToString());
			}
			
			
			 storeResult.Value = _result;
		}
		
	}
}
