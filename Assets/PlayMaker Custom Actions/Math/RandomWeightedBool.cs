// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Sets a Bool Variable to True or False randomly.")]
	public class RandomWeightedBool : FsmStateAction
	{
		[HasFloatSlider(0, 1)]
		[Tooltip("The higher the value, the more the chance to have a true value as result")]
		public FsmFloat weights;
		[UIHint(UIHint.Variable)]
		public FsmBool storeResult;

		
		public override void Reset()
		{
			storeResult = null;
			weights = 0.5f;
		}

		public override void OnEnter()
		{
			storeResult.Value = Random.Range(0f, 1f) < weights.Value;
			
			Finish();
		}
	}
}
