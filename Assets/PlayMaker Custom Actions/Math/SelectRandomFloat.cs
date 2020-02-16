// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Select a random float from an array of floats.")]
	public class SelectRandomFloat : FsmStateAction
	{
		[CompoundArray("floats", "float", "Weight")]
		public FsmFloat[] floats;
		[HasFloatSlider(0, 1)]
		public FsmFloat[] weights;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeFloat;
		
		public override void Reset()
		{
			floats = new FsmFloat[3];
			weights = new FsmFloat[] {1,1,1};
			storeFloat = null;
		}

		public override void OnEnter()
		{
			DoSelectRandomString();
			Finish();
		}
		
		void DoSelectRandomString()
		{
			if (floats == null) return;
			if (floats.Length == 0) return;
			if (storeFloat == null) return;

			int randomIndex = ActionHelpers.GetRandomWeightedIndex(weights);
			
			if (randomIndex != -1)
			{
				storeFloat.Value = floats[randomIndex].Value;
			}
		}
	}
}