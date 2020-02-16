// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Vector2")]
	[Tooltip("Linearly interpolates between 2 vectors. " +
		"Allows selection of update type and lerp against deltaTime for the amount, allowing framerate indepedant animations.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1016")]
	public class Vector2LerpDelta : FsmStateAction
	{
		[RequiredField]
		[Tooltip("First Vector.")]
		public FsmVector2 fromVector;
		
		[RequiredField]
		[Tooltip("Second Vector.")]
		public FsmVector2 toVector;
		
		[RequiredField]
		[Tooltip("Interpolate between From Vector and ToVector by this amount. Value is clamped to 0-1 range. 0 = From Vector; 1 = To Vector; 0.5 = half way between.")]
		public FsmFloat amount;
		
		[Tooltip("Amount is multiplied by the deltatime")]
		public bool lerpAgainstDeltaTime;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in this vector variable.")]
		public FsmVector2 storeResult;

		[Tooltip("Repeat every frame. Useful if any of the values are changing.")]
		public bool everyFrame;

		public override void Reset()
		{
			fromVector = new FsmVector2 { UseVariable = true };
			toVector = new FsmVector2 { UseVariable = true };
			amount = null;
			lerpAgainstDeltaTime = false;
			storeResult = null;
			everyFrame = true;
		}

		public override void OnEnter()
		{
			DoVector2Lerp();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoVector2Lerp();
		}

		void DoVector2Lerp()
		{
			float _amount = lerpAgainstDeltaTime?Time.deltaTime*amount.Value:amount.Value;
			storeResult.Value = Vector2.Lerp(fromVector.Value, toVector.Value, _amount);
		}
	}
}

