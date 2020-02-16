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

*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Clamps the Magnitude of Vector3 Variable.")]
	public class Vector3ClampMagnitudeAdvanced : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Variable;
		[RequiredField]
		public FsmFloat maxLength;
		public bool everyFrame;

		public PlayMakerActionsUtils.EveryFrameUpdateSelector updateType;

		public override void Reset()
		{
			vector3Variable = null;
			maxLength = null;
			everyFrame = false;
			updateType = PlayMakerActionsUtils.EveryFrameUpdateSelector.OnUpdate;
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
				DoVector3ClampMagnitude();
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
				DoVector3ClampMagnitude();
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
				DoVector3ClampMagnitude();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		void DoVector3ClampMagnitude()
		{
			vector3Variable.Value = Vector3.ClampMagnitude(vector3Variable.Value, maxLength.Value);
		}
	}
}

