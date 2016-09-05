// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Resources")]
	[Tooltip("UnLoads unused Resources. Be careful to use it only once at a time as it crates hickups especially on mobile.")]
	public class ResourcesUnLoadUnusedAssets : FsmStateAction
	{

		public FsmEvent UnloadDoneEvent;
		
		AsyncOperation _op;
		
		public override void Reset()
		{
			UnloadDoneEvent = null;
		}
		
		public override void OnEnter()
		{
		  _op	= Resources.UnloadUnusedAssets();
		}
		
		public override void OnUpdate()
		{
		  if (_op!=null)
			{
				if (_op.isDone)
				{
					Fsm.Event(UnloadDoneEvent);
					Finish();
				}
			}
		}
		
	}
}

