// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Advanced interpolation between 2 floats.")]
	public class FloatLerpAdvanced : FsmStateAction
	{
		public enum LerpInterpolationType {Linear,Quadratic,EaseIn,EaseOut,Smoothstep,Smootherstep,DeltaTime};

		[RequiredField]
		[Tooltip("First Vector.")]
		public FsmFloat fromFloat;
		
		[RequiredField]
		[Tooltip("Second Vector.")]
		public FsmFloat toFloat;
		
		[RequiredField]
		[HasFloatSlider(-1f, 1f)]
		[Tooltip("Interpolate between From Vector and ToVector by this amount. Value is clamped to -1 1 range. interpolation can be choosen below")]
		public FsmFloat amount;

		public LerpInterpolationType interpolation;

		[RequiredField]
		[UIHint(UIHint.Variable)]

		[Tooltip("Store the result in this float variable.")]
		public FsmFloat storeResult;
		
		[Tooltip("Repeat every frame. Useful if any of the values are changing.")]
		public bool everyFrame;


		public override void Reset()
		{
			fromFloat = new FsmFloat { UseVariable = true };
			toFloat = new FsmFloat { UseVariable = true };

			interpolation = LerpInterpolationType.Linear;

			storeResult = null;
			everyFrame = true;
		}


		public override void OnEnter()
		{
			DoLerp();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoLerp();
		}
	
		void DoLerp()
		{
			float t = amount.Value;

			float mult = t<0?-1:1;


			t = GetInterpolation(Mathf.Abs(t),interpolation);

			storeResult.Value = mult * Mathf.Lerp(fromFloat.Value, toFloat.Value, t);
		}

		float GetInterpolation(float t,LerpInterpolationType type)
		{
			switch(type)
			{
			case LerpInterpolationType.Quadratic:
				return t*t;
			case LerpInterpolationType.EaseIn:
				return 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
			case LerpInterpolationType.EaseOut:
				return Mathf.Sin(t * Mathf.PI * 0.5f);
			case LerpInterpolationType.Smoothstep:
				return t*t * (3f - 2f*t);
			case LerpInterpolationType.Smootherstep:
				return t*t*t * (t * (6f*t - 15f) + 10f);
			case LerpInterpolationType.DeltaTime:
				return Time.deltaTime*t;
			}

			return t;
		}




	}
}

