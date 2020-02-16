// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
/*--- keywords: animation curve  ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Evaluate a float using a curve. example: for a linear curve ranging from 0,0, to 1,1, evaluating 0.5f would return 0.5f as a result")]
	public class FloatEvaluateOnCurve : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Value")]
		public FsmFloat theFloat;

		[RequiredField]
		[Tooltip("The curve defining the mapping between the base and target ranges")]
		public FsmAnimationCurve animCurve;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in this float variable.")]
		public FsmFloat storeResult;

		[Tooltip("Repeat every frame. Useful if any of the values are changing.")]
		public bool everyFrame;

		public override void Reset()
		{
			theFloat = 0f;
			animCurve = new FsmAnimationCurve();
			storeResult = null;
			everyFrame = true;
		}

		public override void OnEnter()
		{
			DoFloatEvaluate();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoFloatEvaluate();
		}

		void DoFloatEvaluate()
		{
			storeResult.Value = animCurve.curve.Evaluate(theFloat.Value);
		}

		public override string ErrorCheck ()
		{
			if (animCurve.curve == null) {
				return  "Missing curve";
			}

			return string.Empty;
		}
	}
}