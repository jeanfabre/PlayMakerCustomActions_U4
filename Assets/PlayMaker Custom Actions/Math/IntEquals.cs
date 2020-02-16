// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends Events based on the comparison of multiple Integers.")]
	public class IntEquals : FsmStateAction
	{
		[RequiredField]
		public FsmInt[] integers;
		
		[Tooltip("Event sent if all ints equal")]
		public FsmEvent equal;
		[Tooltip("Event sent if ints not equal")]
		public FsmEvent notEqual;
		
		public bool everyFrame;
		
		public override void Reset()
		{
			integers = new FsmInt[2];
			
			equal = null;
			notEqual = null;
			
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoIntCompare();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoIntCompare();
		}		

		void DoIntCompare()
		{
			if (integers.Length>1)
			{
				int _base = integers[0].Value;
				foreach(FsmInt _int in integers)
				{
					if (_base!= _int.Value)
					{
						Fsm.Event(notEqual);
						return;
					}
				}
				
			
			}
			
			Fsm.Event(equal);	
	
		}

		public override string ErrorCheck()
		{
			if (FsmEvent.IsNullOrEmpty(equal) &&
				FsmEvent.IsNullOrEmpty(notEqual))
				return "Action sends no events!";
			
			if (integers.Length <2)
			{
				return "Action needs more than 1 int to compare";
			}
			return "";
		}
	}
}
