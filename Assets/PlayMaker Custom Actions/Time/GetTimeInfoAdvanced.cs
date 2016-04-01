// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__internal/FsmStateActionAdvanced.cs"
					]
}
EcoMetaEnd
---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Gets various useful Time measurements.")]
	public class GetTimeInfoAdvanced : FsmStateActionAdvanced
	{
		public enum TimeInfo
		{
			DeltaTime,
			TimeScale,
			SmoothDeltaTime,
			TimeInCurrentState,
			TimeSinceStartup,
			TimeSinceLevelLoad,
			RealTimeSinceStartup,
			RealTimeInCurrentState
		}

		[Tooltip("Time measurements properties.")]
		public TimeInfo getInfo;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Time Value for the choosen Time property.")]
		public FsmFloat storeValue;


		public override void Reset()
		{
			base.Reset ();
			getInfo = TimeInfo.TimeSinceLevelLoad;
			storeValue = null;
	
		}

		public override void OnEnter()
		{
			OnActionUpdate ();

			if (!everyFrame)
			{
				Finish();
			}
		}



		public override void OnActionUpdate()
		{
			switch (getInfo) 
			{

			case TimeInfo.DeltaTime:
				storeValue.Value = Time.deltaTime;
				break;

			case TimeInfo.TimeScale:
				storeValue.Value = Time.timeScale;
				break;

			case TimeInfo.SmoothDeltaTime:
				storeValue.Value = Time.smoothDeltaTime;
				break;

			case TimeInfo.TimeInCurrentState:
				storeValue.Value = State.StateTime;
				break;

			case TimeInfo.TimeSinceStartup:
				storeValue.Value = Time.time;
				break;

			case TimeInfo.TimeSinceLevelLoad:
				storeValue.Value = Time.timeSinceLevelLoad;
				break;

			case TimeInfo.RealTimeSinceStartup:
				storeValue.Value = FsmTime.RealtimeSinceStartup;
				break;

			case TimeInfo.RealTimeInCurrentState:
				storeValue.Value = FsmTime.RealtimeSinceStartup - State.RealStartTime;
				break;

			default:
				storeValue.Value = 0f;
				break;
			}
		}
	}
}