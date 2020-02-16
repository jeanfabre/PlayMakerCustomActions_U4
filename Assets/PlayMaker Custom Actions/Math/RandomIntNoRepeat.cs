// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// 'inclusiveMax' option added by MaDDoX (@brenoazevedo)
// no repeat option added by DjayDino
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Sets an Integer Variable to a random value between Min/Max.")]
	public class RandomIntNoRepeat : FsmStateAction
	{
		[RequiredField]
		public FsmInt min;
		[RequiredField]
		public FsmInt max;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt storeResult;
        [Tooltip("Should the Max value be included in the possible results?")]
        public bool inclusiveMax;
		public FsmBool noRepeat;
		
		private int randomIndex;
		private int lastIndex = -1;
		
		public override void Reset()
		{
			min = 0;
			max = 100;
			storeResult = null;
			// make default false to not break old behavior.
		    inclusiveMax = false;
			noRepeat = false;
		}

		public override void OnEnter()
		{
			PickRandom();
            Finish();
		}

		void PickRandom()
		{
			if (noRepeat.Value)
			{
				do
				{
				randomIndex = (inclusiveMax) ? 
				Random.Range(min.Value, max.Value + 1) : 
				Random.Range(min.Value, max.Value);
				} while ( randomIndex == lastIndex);
				
				lastIndex = randomIndex;
				storeResult.Value = randomIndex;
				
			}else
			{
				randomIndex = (inclusiveMax) ? 
				Random.Range(min.Value, max.Value + 1) : 
				Random.Range(min.Value, max.Value);
				storeResult.Value = randomIndex;
			}
			Debug.Log("" +storeResult);
			Debug.Log("" +randomIndex);
		}
	}
}
