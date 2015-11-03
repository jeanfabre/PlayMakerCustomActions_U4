// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"https://github.com/jeanfabre/PlayMaker--Utils/raw/master/PlayMakerUtils_FsmVar.cs?assetFilePath=Assets/PlayMaker Utils/PlayMakerUtils_FsmVar.cs"
					]
}
EcoMetaEnd
---*/

using UnityEngine;
using System;
using System.Globalization;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Gets various useful Time measurements for a timeSpane. Use Totalx very likely, query for Minutes return the actual 'displayed' minutes of the time, not the total amount of the time span.")]
	public class GetTimeSpanInfo : FsmStateAction
	{
		public enum TimeSpanProperties
		{
			Milliseconds,
			TotalMilliseconds,
			Seconds,
			TotalSeconds,
			Minutes,
			totalMinutes,
			Hours,
			TotalHours,
			Days,
			TotalDays
		}

		public enum TimeSpanCreate
		{
			TryParse,
			FromDays,
			FromHours,
			FromMinutes,
			FromSeconds,
			FromMilliseconds
		}


		[Tooltip("The timespan creation type")]
		public TimeSpanCreate timeSpanCreate;

		[RequiredField]
		[Tooltip("The TimeSpan. Make sure you set the timeSpanCreate propery to reflect the content of this string")]
		[UIHint(UIHint.FsmString)]
		public FsmString timeSpan;


		
		[CompoundArray("Infos","Type","Value")]
		[RequiredField]
		public TimeSpanProperties[] properties;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVar[] values;
		
		public bool everyFrame;

		public override void Reset()
		{
			timeSpanCreate = TimeSpanCreate.TryParse;
			timeSpan = "00:00:01.000";
			properties = new TimeSpanProperties[0];
			values = new FsmVar[0];
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetSpanTimeInfo();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetSpanTimeInfo();
		}
		
		void DoGetSpanTimeInfo()
		{


			TimeSpan span = new TimeSpan();

			if (timeSpanCreate == TimeSpanCreate.TryParse)
			{
				/*bool _success = */TimeSpan.TryParse(timeSpan.Value,out span);
			}else if (timeSpanCreate == TimeSpanCreate.FromSeconds)
			{
				span = TimeSpan.FromSeconds(Convert.ToDouble(timeSpan.Value));
			}


			int i =0;
			foreach(TimeSpanProperties _prop in properties)
			{
				FsmVar _var = values[i];

				switch (_prop) 
				{
				
				case TimeSpanProperties.Milliseconds:
					PlayMakerUtils.ApplyValueToFsmVar(this.Fsm,_var,span.Milliseconds);
					break;
				case TimeSpanProperties.TotalMilliseconds:
					PlayMakerUtils.ApplyValueToFsmVar(this.Fsm,_var,span.TotalMilliseconds);
					break;
				case TimeSpanProperties.Seconds:
					PlayMakerUtils.ApplyValueToFsmVar(this.Fsm,_var,span.Seconds);
					break;
				case TimeSpanProperties.TotalSeconds:
					PlayMakerUtils.ApplyValueToFsmVar(this.Fsm,_var,span.TotalSeconds);
					break;
				case TimeSpanProperties.Hours:
					PlayMakerUtils.ApplyValueToFsmVar(this.Fsm,_var,span.Hours);
					break;
				case TimeSpanProperties.TotalHours:
					PlayMakerUtils.ApplyValueToFsmVar(this.Fsm,_var,span.TotalHours);
					break;
				case TimeSpanProperties.Minutes:
					PlayMakerUtils.ApplyValueToFsmVar(this.Fsm,_var,span.Minutes);
					break;
				case TimeSpanProperties.totalMinutes:
					PlayMakerUtils.ApplyValueToFsmVar(this.Fsm,_var,span.TotalMinutes);
					break;
				case TimeSpanProperties.Days:
					PlayMakerUtils.ApplyValueToFsmVar(this.Fsm,_var,span.Days);
					break;
				case TimeSpanProperties.TotalDays:
					PlayMakerUtils.ApplyValueToFsmVar(this.Fsm,_var,span.TotalDays);
					break;
					
				default:
					PlayMakerUtils.ApplyValueToFsmVar(this.Fsm,_var,0);
					break;
				}

				i++;
			}

		}
	}
}
