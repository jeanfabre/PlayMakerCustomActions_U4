// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// http://hutonggames.com/playmakerforum/index.php?topic=8665.msg41731#msg41731

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Pick a random weighted Int picked from an array of Ints.")]
	public class RandomWeightedInt : FsmStateAction
	{
		[CompoundArray("Ints", "Int", "Weight")]
		public FsmInt[] ints;
		[HasFloatSlider(0, 1)]
		public FsmFloat[] weights;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt result;
		[Tooltip("Can hit the same number twice in a row")]
		public FsmBool Repeat;
		
		private int randomIndex;
		private int lastIndex = -1;
		
		public override void Reset()
		{
			ints = new FsmInt[3];
			ints[0] = 1;
			ints[1] = 2;
			ints[2] = 3;
			weights = new FsmFloat[] {1,1,1};
			result = null;
			Repeat = false;
		}
		public override void OnEnter()
		{
			
			PickRandom();
			Finish ();						
		}	
		
		void PickRandom()
		{
			if (ints.Length ==  0)
			{
				return;
			}
			
			if (Repeat.Value)
			{
				randomIndex = ActionHelpers.GetRandomWeightedIndex(weights);
				result.Value = ints[randomIndex].Value;
	
			}else
			{
				do
				{
					randomIndex = ActionHelpers.GetRandomWeightedIndex(weights);
				} while ( randomIndex == lastIndex);
				
				lastIndex = randomIndex;
				result.Value = ints[randomIndex].Value;
			}
		}
	}
}
