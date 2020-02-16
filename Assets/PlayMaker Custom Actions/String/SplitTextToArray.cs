// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Split a text asset or string into an array or strings|ints|floats.")]
	public class SplitTextToArray : FsmStateAction
	{
		
		public enum ArrayParseStringAs  
		{
			String,
			Int,
			Float
		};

		public enum SplitSpecialChars
		{
			NewLine,
			Tab,
			Space,
		}


		[ActionSection("Source")]
		[Tooltip("Text asset source")]
		public TextAsset textAsset;
		
		[Tooltip("Text Asset is ignored if this is set.")]
		public FsmString OrThisString;

		[Tooltip("From where to start parsing, leave to 0 to start from the beginning")]
		public FsmInt startIndex;
		
		[Tooltip("the range of parsing")]
		public FsmInt parseRange;


		[ActionSection("Split")]
		[Tooltip("Split")]
		public SplitSpecialChars split;
		
		[Tooltip("Split is ignored if this value is not empty. Each chars taken in account for split")]
		public FsmString OrThisChar;

		[ActionSection("Storage")]

		[Tooltip("Parse the line as a specific type")]
		public ArrayParseStringAs parseAsType;

		[Tooltip("The result.")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmArray result;


		public override void Reset()
		{
			startIndex = null;
			OrThisString = new FsmString() {UseVariable = true};
			parseRange = null;
			textAsset = null;	
			split = SplitSpecialChars.NewLine;
			parseAsType = ArrayParseStringAs.String;
		}
		
		
		public override void OnEnter()
		{
			splitText();
			Finish();
		}
		
		
		public void splitText()
		{

			string _text;
			
			if (OrThisString.Value.Length==0){
				if (textAsset== null) 
				{
					return;
				}else{
					_text = textAsset.text;
				}
			}else{
				_text = OrThisString.Value;
			}
			
			
			result.Reset();

			
			string[] rawlines;
			
			if (OrThisChar.Value.Length==0){
				char _split = '\n';
				
				switch(split){
				case SplitSpecialChars.Tab:
					_split = '\t';
					break;
				case SplitSpecialChars.Space:
					_split = ' ';
					break;
				}
				
				
				rawlines = _text.Split(_split);
				
			}else{
				rawlines = _text.Split(OrThisChar.Value.ToCharArray());
			}
			
			
			
			int start = startIndex.Value;
			
			int count = rawlines.Length;
			
			if (parseRange.Value>0)
			{
				count = Mathf.Min (count-start,parseRange.Value);
			}

			string[] lines = new string[count];

			int j = 0;
			
			for(int i=start;i<start+count;i++)
			{
				lines[j] = rawlines[i];
				j++;
			}

			result.Resize(count);


			if (parseAsType == ArrayParseStringAs.String)
			{
				result.Values = lines;
				
			}else if (parseAsType == ArrayParseStringAs.Int)
			{

				int i = 0;

				int _value_i = 0;

				foreach(String text in lines)
				{

					int.TryParse(text, out _value_i);
					result.Set(i,_value_i);
					++i;
					
				}

				result.SaveChanges();

				
			}else if (parseAsType == ArrayParseStringAs.Float)
			{

				int i = 0;

				float _result_float = 0f;
				foreach(String text in lines)
				{
					float.TryParse(text, out _result_float);
					result.Set(i,_result_float);
					++i;
					
				}

				result.SaveChanges();

			}
			
			
			
			
		}
	}
}