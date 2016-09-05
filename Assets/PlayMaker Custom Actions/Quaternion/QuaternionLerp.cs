// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/Quaternion/Editor/QuaternionLerpCustomEditor.cs",
						"Assets/PlayMaker Custom Actions/Quaternion/Editor/_internal/QuaternionCustomEditorBase.cs",
						"Assets/PlayMaker Custom Actions/Quaternion/_internal/QuaternionBaseAction.cs",
					]
}
EcoMetaEnd
---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Quaternion")]
	[Tooltip("Interpolates between from and to by t and normalizes the result afterwards. Can lerp against deltaTime for the amount, allowing framerate indepedant animations.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1092")]
	public class QuaternionLerp : QuaternionBaseAction
	{

		[RequiredField]
		[Tooltip("From Quaternion.")]
		public FsmQuaternion fromQuaternion;
		
		[RequiredField]
		[Tooltip("To Quaternion.")]
		public FsmQuaternion toQuaternion;
		
		[RequiredField]
		[Tooltip("Interpolate between fromQuaternion and toQuaternion by this amount. Value is clamped to 0-1 range. 0 = fromQuaternion; 1 = toQuaternion; 0.5 = half way between.")]
		[HasFloatSlider(0f, 1f)]
		public FsmFloat amount;
		
		[Tooltip("Amount is multiplied by the deltatime")]
		public bool lerpAgainstDeltaTime;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in this quaternion variable.")]
		public FsmQuaternion storeResult;


		public override void Reset()
		{
			fromQuaternion = new FsmQuaternion { UseVariable = true };
			toQuaternion = new FsmQuaternion { UseVariable = true };
			amount = 0.5f;
			lerpAgainstDeltaTime = false;
			storeResult = null;
			everyFrame = true;
			everyFrameOption = everyFrameOptions.Update;
		}

		
		public override void OnEnter()
		{
			DoQuatLerp();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if (everyFrameOption == everyFrameOptions.Update )
			{
				DoQuatLerp();
			}
		}
		public override void OnLateUpdate()
		{
			if (everyFrameOption == everyFrameOptions.LateUpdate )
			{
				DoQuatLerp();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (everyFrameOption == everyFrameOptions.FixedUpdate )
			{
				DoQuatLerp();
			}
		}

		void DoQuatLerp()
		{
			float _amount = lerpAgainstDeltaTime?Time.deltaTime*amount.Value:amount.Value;
			storeResult.Value = Quaternion.Lerp(fromQuaternion.Value, toQuaternion.Value, _amount);
		}
	}
}
