// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
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
		[Tooltip("Set the file path, for example : Assets/myfile.txt")]
		public FsmString filePath;
		
		[RequiredField]
        [UIHint(UIHint.TextArea)]
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
            if (System.IO.File.Exists(filePath.Value))
            {
                // Create an instance of StreamWriter to write text to a file.
                StreamWriter sw = new StreamWriter(filePath.Value, true);
                sw.Write(text.Value);
                sw.Close();
                Fsm.Event(successEvent);
                Finish();
            }
            else
            {
                Debug.Log("File does not exist. Did you enter the correct filename and file extension?");
                Fsm.Event(failureEvent);
            }
        }
	}
}