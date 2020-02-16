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
	[Tooltip("Sets the magnitude ( length ) a Vector3. \n Advanced features allows selection of update type.")]
	public class Vector3SetMagnitude : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Vector3 variable.")]
		public FsmVector3 vector;

		[RequiredField]
		[Tooltip("The new magnitude or length of that vector3")]
		public FsmFloat magnitude;
		
		[Tooltip("Event sent if the vector is 0,0,0 which makes it impossible to set magnitude because the direction information is lost")]
		public FsmEvent failureEvent;
		
		
		public PlayMakerActionsUtils.EveryFrameUpdateSelector updateType;
		
		[Tooltip("Repeat every frame. Useful if any of the values are changing.")]
		public bool everyFrame;
		

		public override void Reset()
		{
			vector = null;
			magnitude = 1;
			failureEvent = null;
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
				DoVector3SetMagnitude();
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
				DoVector3SetMagnitude();
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
				DoVector3SetMagnitude();
			}

			if (!everyFrame)
			{
				Finish();
			}
		}

		void DoVector3SetMagnitude()
		{
			if (vector.Value.sqrMagnitude == 0f)
			{
				Fsm.Event(failureEvent);
				return;
			}
			
			vector.Value = vector.Value.normalized * magnitude.Value;
		}
	}
}

