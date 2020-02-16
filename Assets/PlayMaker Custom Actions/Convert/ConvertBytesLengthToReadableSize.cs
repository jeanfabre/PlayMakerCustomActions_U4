// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// http://stackoverflow.com/questions/281640/how-do-i-get-a-human-readable-file-size-in-bytes-abbreviation-using-net#
// __ECO__ __PLAYMAKER__ __ACTION__

using UnityEngine;
using System.Collections;
using System.IO;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Convert bytes lenght into a human readable format like 13KB or 1.4GB.")]
	public class ConvertBytesLengthToReadableSize : FsmStateAction
	{

		[Tooltip("The byte lenght to convert.")]
		public FsmInt byteLength;
		
		[Tooltip("The human readable pattern. 0 being the size and 1 being the size suffix.\n For example \"{0:0.#}{1}\" wouldshow a single decimal place, and no space")]
 		public FsmString format;
		
		[Tooltip("The search options. let you search only in the folder or in all sub folders too")]
		public FsmString[] sizes;
		
		[ActionSection("Result")]
		
		[Tooltip("The result")]
			[UIHint(UIHint.Variable)]
		public FsmString result;
		
		public bool everyFrame;
		
		
		public override void Reset()
		{
			byteLength = null;
			format ="{0:0.##} {1}";

			
			sizes = new FsmString[4];
			sizes[0] = "B";
			sizes[1] = "KB";
			sizes[2] = "MB";
			sizes[3] = "GB";
			
			result = null;
			
			everyFrame = false;
			
		}

		public override void OnEnter()
		{
			Convert();
	
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			Convert();
		}
		
		void Convert()
		{
			double len = byteLength.Value;
			int order = 0;
			while (len >= 1024 && order + 1 < sizes.Length) {
			    order++;
			    len = len/1024;
			}
			
			result.Value = string.Format(format.Value, len, sizes[order].Value);
		}
		
	}
}