// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// __ECO__ __PLAYMAKER__ __ACTION__
// Keywords: iterate


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Each time this action is called it gets the previous item from a Array. This version has a reset flag \n" +
	         "This lets you quickly loop through all the items of an array to perform actions on them." +
	         "")]
	public class ArrayGetPrevious : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;

		[Tooltip("From where to start iteration, leave as 0 or none to start from the end")]
		public FsmInt startIndex;
		
		[Tooltip("When to end iteration, leave to none to iterate until the beginning")]
		public FsmInt endIndex;
		
		[Tooltip("Event to send to get the previous item.")]
		public FsmEvent loopEvent;
		
		[Tooltip("Event to send when there are no more items.")]
		public FsmEvent finishedEvent;

		[Tooltip("If you want to reset the iteration, raise this flag to true when you enter the state, it will indicate you want to start from the end again")]
		public FsmBool resetFlag;

		[ActionSection("Result")]

		[MatchElementType("array")]
		[UIHint(UIHint.Variable)]
		public FsmVar result;

		[UIHint(UIHint.Variable)]
		public FsmInt currentIndex;
	
		// increment that index as we loop through item
		private int index = -2;		
		
		public override void Reset()
		{		
			array = null;
			startIndex = new FsmInt(){UseVariable=true};
			endIndex = new FsmInt(){UseVariable=true};

			currentIndex = null;

			loopEvent = null;
			finishedEvent = null;

			resetFlag = null;

			result = null;
		}
		
		public override void OnEnter()
		{
			if (startIndex.IsNone)
			{
				startIndex.Value = array.Length -1;
			}

			if (endIndex.IsNone)
			{
				endIndex.Value = 0;
			}

			if (index < -1 || resetFlag.Value)
			{
				resetFlag.Value = false;

				if (!startIndex.IsNone )
				{
					index = startIndex.Value;
				}else{
					index = array.Length -1;
				}
			}

			
			DoGetPreviousItem();
			
			Finish();
		}
		
		
		void DoGetPreviousItem()
		{
			// no more children?
			// check first to avoid errors.
			if (index < 0)
			{
				Fsm.Event(finishedEvent);
				return;
			}

			if (!endIndex.IsNone && index < endIndex.Value  )
			{
				Fsm.Event(finishedEvent);
				return;
			}

			// get item
			result.SetValue(array.Get(index));

			currentIndex.Value = index;

			// iterate the previous child
			index--;

			if (loopEvent != null)
			{
				Fsm.Event(loopEvent);
			}
		}
	}
}