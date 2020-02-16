// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// __ECO__ __PLAYMAKER__ __ACTION__

using UnityEngine;
using System.Collections;
using System.IO;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Files")]
	[Tooltip("Get the number of files in a directory.")]
	public class GetFileCount : FsmStateAction
	{

		[Tooltip("The path of the folder.")]
		public FsmString folderPath;
		
		[Tooltip("The search pattern. Use * for all")]
		public FsmString searchPattern;
		
		[Tooltip("The search options. let you search only in the folder or in all sub folders too")]
		public SearchOption searchOption;
		
		[Tooltip("The number of files in the folder")]
		[UIHint(UIHint.Variable)]
		public FsmInt fileCount;
		
		[Tooltip("Event sent if no file found")]
		public FsmEvent noFilesEvent;
		

		
		public override void Reset()
		{
			folderPath = null;
			searchPattern ="*";
			searchOption = SearchOption.TopDirectoryOnly;
			fileCount = 0;
			noFilesEvent = null;
			
		}

		public override void OnEnter()
		{
			DoGetFileCount();
	
			Finish();
		}

		
		void DoGetFileCount()
		{
			 int _count = Directory.GetFiles(folderPath.Value, searchPattern.Value, searchOption).Length;
			
			fileCount.Value = _count;
			if (_count==0)
			{
				Fsm.Event(noFilesEvent);
			}
		}
		
	}
}