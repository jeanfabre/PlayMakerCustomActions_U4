// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: All Recursive Childs

using UnityEngine;
using System.Linq;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Each time this action is called it gets the next recursived child of a GameObject within all its hierarchy. This lets you quickly loop through all the recursive children of an object to perform actions on them. NOTE: To find a specific child use Find Child.")]
	public class GetNextChildRecursive : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The parent GameObject. Note, if GameObject changes, this action will reset and start again at the first child.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("if true, will list inactive gameobjects")]
		public FsmBool includeInactive;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the next child in a GameObject variable.")]
		public FsmGameObject storeNextChild;
		
		[Tooltip("Set to true to force iterating from the first item. This variable will be set to false as it carries on iterating, force it back to true if you want to renter this action back to the first item.")]
		[UIHint(UIHint.Variable)]
		public FsmBool reset;
		[Tooltip("Event to send to get the next child.")]
		
		public FsmEvent loopEvent;

		[Tooltip("Event to send when there are no more children.")]
		public FsmEvent finishedEvent;

		public override void Reset()
		{
			gameObject = null;
			storeNextChild = null;
			loopEvent = null;
			finishedEvent = null;
			reset = null;
			includeInactive = null;
		}

		// cache the gameObject so we no if it changes
		private GameObject go;

		// increment a child index as we loop through children
		private int nextChildIndex;

		Transform[] _result;

		public override void OnEnter()
		{
			if (reset.Value)
			{
				reset.Value =  false;
				nextChildIndex = 0;
			}


			DoGetNextChild(Fsm.GetOwnerDefaultTarget(gameObject));

			Finish();
		}

		void DoGetNextChild(GameObject parent)
		{
			if (parent == null)
			{
				return;
			}

			// reset?

			if (go != parent)
			{
				go = parent;
				nextChildIndex = 0;
			}

			if (nextChildIndex == 0)
			{
				_result = parent.GetComponentsInChildren<Transform>(includeInactive.Value);
				_result = _result.Where((source, index) => index != 0).ToArray();


			}



			// no more children?
			// check first to avoid errors.

			if (nextChildIndex >= _result.Length)
			{
				nextChildIndex = 0;
				Fsm.Event(finishedEvent);
				return;
			}

			// get next child

			storeNextChild.Value = _result[nextChildIndex].gameObject;


			// no more children?
			// check a second time to avoid process lock and possible infinite loop if the action is called again.
			// Practically, this enabled calling again this state and it will start again iterating from the first child.

			if (nextChildIndex >= _result.Length)
			{
				nextChildIndex = 0;
				Fsm.Event(finishedEvent);
				return;
			}

			// iterate the next child
			nextChildIndex++;

			if (loopEvent != null)
			{
				Fsm.Event(loopEvent);
			}
		}
	}
}
