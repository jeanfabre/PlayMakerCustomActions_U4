// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Just Another Array Action Added!!! Darkhitor Out!!!


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Swap two items at a specified indexes of a PlayMaker Array")]
	public class ArraySwapItems: FsmStateAction
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;
		
		[UIHint(UIHint.FsmInt)]
		[Tooltip("The first index to swap")]
		public FsmInt index1;
		
		[UIHint(UIHint.FsmInt)]
		[Tooltip("The second index to swap")]
		public FsmInt index2;
		
		[ActionSection("Result")]
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger if the removeAt throw errors")]
		public FsmEvent failureEvent;
		
		public override void Reset()
		{
			array = null;
			failureEvent = null;
			index1 = null;
			index2 = null;
		}
		
		
		public override void OnEnter()
		{
			doArrayListSwap();
			
			Finish();
		}
		
		
		public void doArrayListSwap()
		{
			if (array == null) 
				return;
			
			try
			{
				var _item2 = array.Values[index2.Value];
				
				array.Values[index2.Value] = array.Values[index1.Value];
				array.Values[index1.Value] = _item2;
				
			}catch(System.Exception e){
				Debug.LogError(e.Message);
				Fsm.Event(failureEvent);
			}
				
		}
	}
}
