// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Text.RegularExpressions;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts a string into a color variable. Format is flexible, [r] [g] [b] and [a] are placeholders, escape special chars like \\ for parenthesis for example ")]
	public class ConvertStringToColor : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The string representing the color value.")]
		public FsmString source;
		
		[RequiredField]
		[Tooltip("The expected string pattern for the color value.")]
		public FsmString format;
		
		[Tooltip("32bit colors ranges from 0 to 255 for their values, else, it ranges from 0 to 1")]
		public FsmBool use32BitColor;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The result")]
		public FsmColor storeResult;
		
		[Tooltip("Repeats every frame. Warning, performance will be affected")]
		public bool everyFrame;
		
		public override void Reset()
		{
			source = null;
			format = "Color\\([r],[g],[b],[a]\\)";
			use32BitColor = true;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoParsing();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoParsing();
		}
		
		void DoParsing()
		{
			if (source == null) return;
			if (storeResult == null) return;
			
			string floatregex = "[-+]?[0-9]*\\.?[0-9]+([eE][-+]?[0-9]+)?";
			
			
			string fullregex = format.Value;
			// could be improved with some more regex to match x y z and inject in the replace... but let's not go there...
			fullregex = fullregex.Replace("[r]","(?<r>" + floatregex + ")");
			fullregex = fullregex.Replace("[g]","(?<g>" + floatregex + ")");
			fullregex = fullregex.Replace("[b]","(?<b>" + floatregex + ")");
			fullregex = fullregex.Replace("[a]","(?<a>" + floatregex + ")");
			
			fullregex = "^\\s*" + fullregex;
			
			Regex r = new Regex(fullregex);
   			Match m = r.Match(source.Value);
			
			if ( m.Groups["r"].Value!="" && m.Groups["g"].Value!="" && m.Groups["b"].Value!="" ){
				
				if(use32BitColor.Value)
				{
					Color32 _col = new Color32(byte.Parse(m.Groups["r"].Value),byte.Parse(m.Groups["g"].Value),byte.Parse(m.Groups["b"].Value),255);
					if (m.Groups["a"].Value!="")
					{
						_col.a = byte.Parse(m.Groups["a"].Value);
							
					}
					storeResult.Value = _col;
					
				}else{
					Color _col = new Color(float.Parse(m.Groups["r"].Value),float.Parse(m.Groups["g"].Value),float.Parse(m.Groups["b"].Value));
					if (m.Groups["a"].Value!="")
					{
						_col.a = float.Parse(m.Groups["a"].Value);
							
					}
					storeResult.Value = _col;
				}
				
						
				
			}

		}
		
	}
}
