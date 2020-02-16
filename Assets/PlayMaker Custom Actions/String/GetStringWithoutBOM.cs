// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// http://hutonggames.com/playmakerforum/index.php?action=post;topic=10645.0;last_msg=50293
// http://answers.unity3d.com/questions/10904/xmlexception-text-node-canot-appear-in-this-state.html

using UnityEngine;
using System.IO;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Gets a string without the Byte Order Mark. This can cause error for parsers like xml for example.")]
	public class GetStringWithoutBOM : FsmStateAction
	{
		
		[Tooltip("The string.")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString source;
		
		[Tooltip("The string without the BOM")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString result;
		
		
		public override void Reset()
		{
			source = null;
			result = null;
			
		}
		
		public override void OnEnter()
		{
			DoGetStringWithoutBOM();
			
			Finish();
		}
		
		
		void DoGetStringWithoutBOM()
		{
			if (source.IsNone) 
			{
				Debug.LogWarning("Source Variable not defined");
				return;
			}

			if (result.IsNone) 
			{
				Debug.LogWarning("Result Variable not defined");
				return;
			}

			MemoryStream memoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(source.Value));
			StreamReader streamReader = new StreamReader(memoryStream, true);
			
			string _result = streamReader.ReadToEnd();
			
			streamReader.Close();
			memoryStream.Close();
			
			result.Value = _result;
		}
		
	}
}
