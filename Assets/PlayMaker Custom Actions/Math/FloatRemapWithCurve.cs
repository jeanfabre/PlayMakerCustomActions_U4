// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
/*--- keywords: mapper range cross multiplication rule of three ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Remap a float from one scale to the other using a curve. example: for a linear curve we have 2 existing between 1 and 3, remapping 2 between 0 and 10 would give 5")]
	public class FloatRemapWithCurve : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Value")]
		public FsmFloat theFloat;
		
		[RequiredField]
		[Tooltip("The base start reference")]
		public FsmFloat baseStart;
		
		[RequiredField]
		[Tooltip("The base end reference")]
		public FsmFloat baseEnd;
		
		[RequiredField]
		[Tooltip("The target start reference")]
		public FsmFloat targetStart;
		
		[RequiredField]
		[Tooltip("The target end reference")]
		public FsmFloat targetEnd;

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
			theFloat = 50f;
			baseStart = 0f;
			baseEnd = 100f;
			
			targetStart = 0;
			targetEnd = 1f;

			animCurve = new FsmAnimationCurve ();
			animCurve.curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
			storeResult = null;
			everyFrame = true;
		}

		public override void OnEnter()
		{
			DoFloatRemap();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoFloatRemap();
		}

		void DoFloatRemap()
		{
			float _unitTarget = map (theFloat.Value,baseStart.Value,baseEnd.Value,0f,1f);


			float _curveTarget = animCurve.curve.Evaluate (_unitTarget);

			storeResult.Value = map (_curveTarget,0f,1f,targetStart.Value,targetEnd.Value);
		}
		
		float map(float s, float a1, float a2, float b1, float b2)
		{
		    return b1 + (s-a1)*(b2-b1)/(a2-a1);
		}

		public override string ErrorCheck ()
		{
			if (animCurve.curve == null)
			{
				return  "Missing curve";
			}

			if (animCurve.curve.keys[0].time != 0f || animCurve.curve.keys[0].value != 0f ||
			    animCurve.curve.keys [animCurve.curve.keys.Length - 1].time != 1f || animCurve.curve.keys [animCurve.curve.keys.Length - 1].value != 1f) 
			{
				return "Curve must start at 0,0 and end at 1,1, use 'Target Start' and 'Target End' to define mapping range";
			}


			return string.Empty;
		}
		
	}
}

