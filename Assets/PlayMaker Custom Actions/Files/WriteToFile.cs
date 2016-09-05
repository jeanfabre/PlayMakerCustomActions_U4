// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.IO;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Files")]
	[Tooltip("Write string to a File")]
	public class WriteToFile : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The file name")]
		public FsmString filePath;
		
		[RequiredField]
		[Tooltip("The text")]
		public FsmString text;
		
		public FsmEvent successEvent;
		public FsmEvent failureEvent;
		
		
		public override void Reset()
		{
			filePath = null;
			text = null;
			
		}
		
		
		public override void OnEnter()
		{
			if (Write())
			{
				Fsm.Event(successEvent);
			}else{
				Fsm.Event(failureEvent);
			}
			
			Finish ();
		}

		
		private bool Write()
		{
			if ( string.IsNullOrEmpty(filePath.Value) )
			{
				return false;
			}
			
			
	   		// Create an instance of StreamWriter to write text to a file.
		   	StreamWriter sw = new StreamWriter(filePath.Value,true);
			sw.Write(text.Value);
		
		    sw.Close();
			
			return true;
		}

	}
}

