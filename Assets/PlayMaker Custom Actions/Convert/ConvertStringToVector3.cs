// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Text.RegularExpressions;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts a string into a vector3 variable. Format is flexible, [x] [y] and [z] are placeholders, escape special chars like \\ for parenthesis for example ")]
	public class ConvertStringToVector3 : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The string representing the Vector3 value.")]
		public FsmString source;
		
		[Tooltip("The expected format of the source representing the Vector3 value.")]
		public FsmString format;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The result")]
		public FsmVector3 storeResult;
		
		[Tooltip("Repeats every frame. Warning, performance will be affected")]
		public bool everyFrame;
		
		public override void Reset()
		{
			source = null;
			format = "Vector3\\([x],[y],[z]\\)";
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
			fullregex = fullregex.Replace("[x]","(?<x>" + floatregex + ")");
			fullregex = fullregex.Replace("[y]","(?<y>" + floatregex + ")");
			fullregex = fullregex.Replace("[z]","(?<z>" + floatregex + ")");
			
			fullregex = "^\\s*" + fullregex;
			
			Regex r = new Regex(fullregex);
   			Match m = r.Match(source.Value);
			
			if ( m.Groups["x"].Value!="" && m.Groups["y"].Value!="" && m.Groups["z"].Value!="" ){
				storeResult.Value = new Vector3(float.Parse(m.Groups["x"].Value),float.Parse(m.Groups["y"].Value),float.Parse(m.Groups["z"].Value));
			}

		}
		
	}
}
