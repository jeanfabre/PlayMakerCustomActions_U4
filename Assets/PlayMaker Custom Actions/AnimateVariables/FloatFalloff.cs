// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

// based on https://twitter.com/runevision/status/798580573606395904 https://www.youtube.com/watch?v=5eoDoMj4E8E
// f(x) = x / ((1 / b - 2) * (1 - x) + 1)


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.AnimateVariables)]
	[Tooltip("Follow a float value and lerp if outside the defined margin. Typically use for camera movement to follow player when getting close to the screen sides")]
	public class FloatFalloff : FsmStateAction
	{

		[RequiredField]
		[Tooltip("The Input value")]
		public FsmFloat value;

		[RequiredField]
		[Tooltip("The fallOff Factor. Ranges from 0 to 1")]
		[HasFloatSlider(0,1)]
		public FsmFloat fallOffFactor;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The result")]
		public FsmFloat result;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		public override void Reset()
		{
			value = null;
			fallOffFactor = 1f;
			result = null;
		}

		public override void OnEnter()
		{
			DoFallOff();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoFallOff();
		}

		void DoFallOff()
		{
			//result.Value = value.Value / ( (Mathf.Pow(2,fallOffFactor.Value) -1) * (1-value.Value) + 1f );
			result.Value = value.Value / ( (1f / (fallOffFactor.Value - 2f)) * (1f - value.Value) + 1f );
		}
		

	}
}