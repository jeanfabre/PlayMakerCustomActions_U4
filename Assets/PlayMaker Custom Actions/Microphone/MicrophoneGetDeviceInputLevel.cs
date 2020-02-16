// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Get a microphone device current input volume level")]
	public class MicrophoneGetDeviceInputLevel : FsmStateAction
	{
		
		[Tooltip("The name of the device. Passing null or an empty string will pick the default device. Get device names using the action MicrophoneGetDeviceById for example")]
		public FsmString deviceName;
		
		[RequiredField]
		[ObjectType(typeof(AudioClip))]
		[Tooltip("The audio clip where the record is saved.")]
		public FsmObject audioClip;
		
		[Tooltip("The Device input volume level")]
		public FsmFloat level;
		
		public FsmFloat maxLevel;
		
		public FsmFloat multiplier;
		
		
		int dec = 128;
		float[] waveData;
	//	AudioClip clipRecord;
		public override void Reset()
		{
			deviceName = "";
			multiplier = 1000;
			level = 0f;
			
		}
		
		public override void OnEnter()
		{
			waveData = new float[dec];
			
		}
		
		public override void OnUpdate()
		{
			int micPosition = Microphone.GetPosition(deviceName.Value)-(dec+1); // null means the first microphone
			
			AudioClip clipRecord = audioClip.Value as AudioClip;
			if (clipRecord==null)
			{
				return;
			}
			
			if (micPosition<0)
			{
					return;
			}
			
			clipRecord.GetData(waveData, micPosition);
			
			// Getting a peak on the last 128 samples
			
			float levelMax = 0;
			
			for (int i = 0; i < dec; i++) {
			
			    float wavePeak = waveData[i] * waveData[i];
			    if (levelMax < wavePeak) {
			
			        levelMax = wavePeak;
				
			    }
			
			}
			
			// levelMax equals to the highest normalized value power 2, a small number because < 1
			level.Value = Mathf.Sqrt(levelMax) * multiplier.Value;
			if (level.Value>maxLevel.Value)
			{
				maxLevel.Value = level.Value;
			}
		}
	}
}
