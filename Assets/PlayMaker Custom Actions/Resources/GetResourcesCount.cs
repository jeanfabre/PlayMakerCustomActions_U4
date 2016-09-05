// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// original action from Amaranth : http://hutonggames.com/playmakerforum/index.php?action=post;topic=3138.0;last_msg=52910
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Resources")]
	[Tooltip("Get the number of files in a resource directory.")]
	public class GetResourcesCount : FsmStateAction
	{

		[Tooltip("The name of the folder.")]
		public FsmString folderName;
		
		[Tooltip("The number of files in the folder")]
		[UIHint(UIHint.Variable)]
		public FsmInt fileCount;

		
		[Tooltip("Event sent if no resources found")]
		public FsmEvent noResourcesEvent;
		
		public override void Reset()
		{
			folderName = null;
			fileCount = 0;
			noResourcesEvent = null;
		}

		public override void OnEnter()
		{
			DoGetFileCount();
	
			Finish();
		}

		
		void DoGetFileCount()
		{
			Object[] resourceDir = Resources.LoadAll(folderName.Value);
			int _count =  resourceDir.Length;
			fileCount.Value = _count;
			
			if (_count==0)
			{
				Fsm.Event(noResourcesEvent);
			}
		}
		
	}
}
