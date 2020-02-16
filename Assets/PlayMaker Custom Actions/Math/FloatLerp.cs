// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Linearly interpolates between 2 floats. It has an option to lerp against deltaTime for the amount, allowing framerate indepedant animations.")]
	public class FloatLerp : FsmStateAction
	{
		[RequiredField]
		[Tooltip("First Float")]
		public FsmFloat fromFloat;
		
		[RequiredField]
		[Tooltip("Second Float.")]
		public FsmFloat toFloat;
		
		[RequiredField]
		[Tooltip("Interpolate between FromFloat and ToFloat by this amount. Value is clamped to 0-1 range. 0 = FromFloat; 1 = ToFloat; 0.5 = half way in between.")]
		public FsmFloat amount;
		
		[Tooltip("Amount is multiplied by the deltatime")]
		public bool lerpAgainstDeltaTime;
		
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
			amount = 0.5f;
			lerpAgainstDeltaTime =false;
			storeResult = null;
			everyFrame = true;
		}

		public override void OnEnter()
		{
			DoFloatLerp();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoFloatLerp();
		}

		void DoFloatLerp()
		{
			float _amount = lerpAgainstDeltaTime?Time.deltaTime*amount.Value:amount.Value;
			
			storeResult.Value = Mathf.Lerp(fromFloat.Value, toFloat.Value, _amount);
		}
	}
}

