// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System;
using System.Globalization;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Gets various useful Time measurements between two dates. Use Totalx very likely, query for Minutes return the actual 'displayed' minutes of the time, not the total amount of the time span.")]
	public class GetTimeSpanBetweenDates : FsmStateAction
	{
		public enum TimeSpanInfo
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
		
		[Tooltip("")]
		public TimeSpanInfo getSpanInfo;
		
		
		[RequiredField]
		[Tooltip("The start date")]
		[UIHint(UIHint.FsmString)]
		public FsmString startDate;

		[RequiredField]
		[Tooltip("The end date")]
		[UIHint(UIHint.FsmString)]
		public FsmString endDate;
		
		[RequiredField]
		[Tooltip("The date format startDate and endDate are expressed with")]
		[UIHint(UIHint.FsmString)]
		public FsmString dateFormat;
		
		[RequiredField]
		[Tooltip("The result")]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeValue;
		
		public bool everyFrame;

		public override void Reset()
		{
			startDate = null;
			endDate = null;
			dateFormat = "MM/dd/yyyy HH:mm";
			getSpanInfo = TimeSpanInfo.Days;
			storeValue = null;
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
			CultureInfo provider = CultureInfo.InvariantCulture;
			DateTime _startDate = DateTime.ParseExact(startDate.Value,dateFormat.Value,provider);
			DateTime _endDate = DateTime.ParseExact(endDate.Value,dateFormat.Value,provider);

			TimeSpan elapsed = _endDate.Subtract(_startDate);
			
			switch (getSpanInfo) 
			{
			
			case TimeSpanInfo.Milliseconds:
				storeValue.Value = elapsed.Milliseconds;
				break;
			case TimeSpanInfo.TotalMilliseconds:
				storeValue.Value = (float)elapsed.TotalMilliseconds;
				break;
			case TimeSpanInfo.Seconds:
				storeValue.Value = elapsed.Seconds;
				break;
			case TimeSpanInfo.TotalSeconds:
				storeValue.Value = (float)elapsed.TotalSeconds;
				break;
			case TimeSpanInfo.Hours:
				storeValue.Value = elapsed.Hours;
				break;
			case TimeSpanInfo.TotalHours:
				storeValue.Value = (float)elapsed.TotalHours;
				break;
			case TimeSpanInfo.Minutes:
				storeValue.Value = elapsed.Minutes;
				break;
			case TimeSpanInfo.totalMinutes:
				storeValue.Value = (float)elapsed.TotalMinutes;
				break;
			case TimeSpanInfo.Days:
				storeValue.Value = elapsed.Days;
				break;
			case TimeSpanInfo.TotalDays:
				storeValue.Value = (float)elapsed.TotalDays;
				break;
				
			default:
				storeValue.Value = 0f;
				break;
			}
		}
	}
}
