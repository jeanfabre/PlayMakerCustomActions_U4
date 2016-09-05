// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Start Recording sound from a microphone device")]
	public class MicrophoneStartRecording : FsmStateAction
	{
		[RequiredField]
		[ObjectType(typeof(AudioSource))]
		[Tooltip("The audio source where the created audioClip will be stored.")]
		public FsmOwnerDefault audioSource;
		
		[Tooltip("The name of the device. Passing null or an empty string will pick the default device. Get device names using the action MicrophoneGetDeviceById for example")]
		public FsmString deviceName;
		
		[Tooltip("indicates whether the recording should continue recording if lengthSec is reached, and wrap around and record from the beginning of the AudioClip.")]
		public FsmBool loop;
		
		public FsmInt recordDuration;
		
		public FsmInt frequency;
		
		[ActionSection("Output")] 
		[RequiredField]
		[ObjectType(typeof(AudioClip))]
		[Tooltip("The audio clip where the record is saved.")]
		public FsmObject audioClip;
		
		[Tooltip("Event sent when the device effectivly start recording. WARNING, if Loop is on, event will not be fired")]
		public FsmEvent RecordStartedEvent;
		
		[Tooltip("Event sent at the end of the record duration. WARNING, if Loop is on, event will not be ignored")]
		public FsmEvent RecordDoneEvent;
		
		[Tooltip("Event sent when the record failed")]
		public FsmEvent RecordFailedEvent;
		
		public override void Reset()
		{
			deviceName = "";
			loop = false;
			recordDuration = 10;
			frequency = 44100;
			
			audioClip = null;
			
		}

		public override void OnEnter()
		{
			//	Application.RequestUserAuthorization(UserAuthorization.WebCam | UserAuthorization.Microphone);
			
			//	yield return 
	       // if (! Application.HasUserAuthorization(UserAuthorization.WebCam | UserAuthorization.Microphone)) {
		//		Fsm.Event(RecordFailedEvent);
		//		return;
	    //    }
			
			var go = Fsm.GetOwnerDefaultTarget(audioSource);
			if (go == null)
			{
				Fsm.Event(RecordFailedEvent);
				return;
			}
			
			
			if (Application.HasUserAuthorization(UserAuthorization.WebCam | UserAuthorization.Microphone) )
			{
				string deviceNameString = deviceName.Value;
				bool deviceExists = false;
				
				if (deviceNameString != "")
				{
					 foreach (string _deviceName in Microphone.devices) {
						if (string.Equals(deviceNameString,_deviceName))
						{
           	 				deviceExists = true;
							break;
      			  		}
					}
				}else{
					deviceExists = true;
				}
				
				if (deviceExists)
				{
					go.audio.clip = Microphone.Start(deviceNameString, loop.Value, recordDuration.Value, frequency.Value);
					if (go.audio.clip!=null)
					{
						audioClip.Value = go.audio.clip;
						if (RecordStartedEvent!=null)
						{
							Fsm.Event(RecordStartedEvent);
						}
						if (RecordDoneEvent!=null && !loop.Value)
						{
							Fsm.DelayedEvent(RecordDoneEvent,recordDuration.Value);
						}
						return;
					}
				}
				
				Fsm.Event(RecordFailedEvent);
			}
			
		}
	}
}
