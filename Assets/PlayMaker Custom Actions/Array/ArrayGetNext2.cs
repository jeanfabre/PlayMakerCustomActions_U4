// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// __ECO__ __PLAYMAKER__ __ACTION__

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Each time this action is called it gets the next item from a Array. This version has a reset flag \n" +
	         "This lets you quickly loop through all the items of an array to perform actions on them." +
	         "")]
	public class ArrayGetNext2 : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;

		[Tooltip("From where to start iteration, leave as 0 or none to start from the beginning")]
		public FsmInt startIndex;
		
		[Tooltip("When to end iteration, leave to none to iterate until the end")]
		public FsmInt endIndex;
		
		[Tooltip("Event to send to get the next item.")]
		public FsmEvent loopEvent;
		
		[Tooltip("Event to send when there are no more items.")]
		public FsmEvent finishedEvent;

		[Tooltip("If you want to reset the iteration, raise this flag to true when you enter the state, it will indicate you want to start from the beginning again")]
		public FsmBool resetFlag;

		[ActionSection("Result")]

		[MatchElementType("array")]
		[UIHint(UIHint.Variable)]
		public FsmVar result;

		[UIHint(UIHint.Variable)]
		public FsmInt currentIndex;
	
		// increment that index as we loop through item
		private int nextItemIndex = 0;		
		
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
                startIndex.Value = 0;
            }
            if (endIndex.IsNone)
            {
                endIndex.Value = 0;
            }

            if (resetFlag.Value)
            {
                nextItemIndex = startIndex.Value;
                resetFlag.Value = false;
            }
            if (nextItemIndex == 0)
            {
                if (startIndex.Value > 0)
                {
                    nextItemIndex = startIndex.Value;
                }
            }

            DoGetNextItem();

            Finish();
        }


        void DoGetNextItem()
        {
            // no more children?
            // check first to avoid errors.

            if (nextItemIndex >= array.Length)
            {
                nextItemIndex = 0;
                currentIndex.Value = array.Length - 1;
                Fsm.Event(finishedEvent);
                return;
            }

            // get next item

            result.SetValue(array.Get(nextItemIndex));

            // no more items?
            // check a second time to avoid process lock and possible infinite loop if the action is called again.
            // Practically, this enabled calling again this state and it will start again iterating from the first child.

            if (nextItemIndex >= array.Length)
            {
                nextItemIndex = 0;
                currentIndex.Value = array.Length - 1;
                Fsm.Event(finishedEvent);
                return;
            }

            if (endIndex.Value > 0 && nextItemIndex >= endIndex.Value)
            {
                nextItemIndex = 0;
                currentIndex.Value = endIndex.Value;
                Fsm.Event(finishedEvent);
                return;
            }

            // iterate the next child
            nextItemIndex++;

            currentIndex.Value = nextItemIndex - 1;

            if (loopEvent != null)
            {
                Fsm.Event(loopEvent);
            }
        }
    }
}
