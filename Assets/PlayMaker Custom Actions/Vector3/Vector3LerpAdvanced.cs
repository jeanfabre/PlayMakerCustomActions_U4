// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ 
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__Internal/PlayMakerActionsUtils.cs"
					],
"version":"1.1.0"
}
EcoMetaEnd
---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Linearly interpolates between 2 vectors.\n Advanced features allows selection of update type and lerp against deltaTime for the amount, allowing framerate indepedant animations.")]
	public class Vector3LerpAdvanced : FsmStateAction
	{
		[RequiredField]
		[Tooltip("First Vector.")]
		public FsmVector3 fromVector;
		
		[RequiredField]
		[Tooltip("Second Vector.")]
		public FsmVector3 toVector;
		
		[RequiredField]
		[Tooltip("Interpolate between From Vector and ToVector by this amount. Value is clamped to 0-1 range. 0 = From Vector; 1 = To Vector; 0.5 = half way between.")]
		public FsmFloat amount;
		
		[Tooltip("Amount is multiplied by the deltatime")]
		public bool lerpAgainstDeltaTime;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in this vector variable.")]
		public FsmVector3 storeResult;
		
		public PlayMakerActionsUtils.EveryFrameUpdateSelector updateType;
		
		[Tooltip("Repeat every frame. Useful if any of the values are changing.")]
		public bool everyFrame;
		

		public override void Reset()
		{
			fromVector = new FsmVector3 { UseVariable = true };
			toVector = new FsmVector3 { UseVariable = true };
			amount = null;
			lerpAgainstDeltaTime = false;
			storeResult = null;
			updateType = PlayMakerActionsUtils.EveryFrameUpdateSelector.OnUpdate;
			everyFrame = true;
		}
		
		public override void OnPreprocess()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnFixedUpdate)
			{
				Fsm.HandleFixedUpdate = true;
			}
			
			#if PLAYMAKER_1_8_5_OR_NEWER
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnLateUpdate)
			{
				Fsm.HandleLateUpdate = true;
			}
			#endif
		}

		public override void OnUpdate()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnUpdate)
			{
				DoVector3Lerp();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnLateUpdate()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnLateUpdate)
			{
				DoVector3Lerp();
			}

			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnFixedUpdate)
			{
				DoVector3Lerp();
			}

			if (!everyFrame)
			{
				Finish();
			}
		}

		void DoVector3Lerp()
		{
			float _amount = lerpAgainstDeltaTime?Time.deltaTime*amount.Value:amount.Value;
			
			storeResult.Value = Vector3.Lerp(fromVector.Value, toVector.Value, _amount);
		}
	}
}

