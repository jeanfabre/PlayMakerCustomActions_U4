// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Check if a microphone device is currently recording")]
	public class MicrophoneIsDeviceRecording : FsmStateAction
	{
		
		[Tooltip("The name of the device. Passing null or an empty string will pick the default device. Get device names using the action MicrophoneGetDeviceById for example")]
		public FsmString deviceName;
		
	
		[Tooltip("Is the device recording or not?")]
		public FsmBool isRecording;
		
		[Tooltip("Event sent when the device is recording. ")]
		public FsmEvent DeviceIsRecordingEvent;
		
		[Tooltip("Event sent when the device is not recording.")]
		public FsmEvent DeviceIsNotRecordingEvent;
		
		public override void Reset()
		{
			deviceName = "";
			isRecording = false;
			DeviceIsRecordingEvent = null;
			DeviceIsNotRecordingEvent = null;
			
		}

		public override void OnEnter()
		{
			isRecording.Value = Microphone.IsRecording(deviceName.Value);
			if (isRecording.Value)
			{
				Fsm.Event(DeviceIsRecordingEvent);	
			}else{
				Fsm.Event(DeviceIsNotRecordingEvent);
			}
			
			Finish();
		}
	}
}
