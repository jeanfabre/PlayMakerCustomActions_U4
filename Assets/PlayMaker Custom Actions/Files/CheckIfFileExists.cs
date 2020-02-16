// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.IO;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Files")]
	[Tooltip("Check if a File exists")]
	public class CheckIfFileExists : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The file path")]
		public FsmString filePath;
		
		[ActionSection("Result")]
		[Tooltip("True if the file exists, false if it wasn't found")]
		[UIHint(UIHint.Variable)]
		public FsmBool fileExists;
		
		public FsmEvent FileExistsEvent;
		public FsmEvent FileNotFoundEvent;
		
		
		public override void Reset()
		{
			filePath = null;
			fileExists = null;
			FileExistsEvent = null;
			FileNotFoundEvent = null;
		}
		
		
		public override void OnEnter()
		{
			if (DoesFileExists())
			{
				Fsm.Event(FileExistsEvent);
			}else{
				Fsm.Event(FileNotFoundEvent);
			}
			
			Finish ();
		}

		
		private bool DoesFileExists()
		{
			bool _exists = File.Exists(filePath.Value);
			fileExists.Value = _exists;
		
			return _exists;
		}

	}
}

