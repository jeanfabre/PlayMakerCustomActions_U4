// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Select a random int from an array of ints.")]
	public class SelectRandomInt : FsmStateAction
	{
		[CompoundArray("Ints", "Int", "Weight")]
		public FsmInt[] ints;
		[HasFloatSlider(0, 1)]
		public FsmFloat[] weights;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt storeInt;
		
		public override void Reset()
		{
			ints = new FsmInt[3];
			weights = new FsmFloat[] {1,1,1};
			storeInt = null;
		}

		public override void OnEnter()
		{
			DoSelectRandomString();
			Finish();
		}
		
		void DoSelectRandomString()
		{
			if (ints == null) return;
			if (ints.Length == 0) return;
			if (storeInt == null) return;

			int randomIndex = ActionHelpers.GetRandomWeightedIndex(weights);
			
			if (randomIndex != -1)
			{
				storeInt.Value = ints[randomIndex].Value;
			}
		}
	}
}