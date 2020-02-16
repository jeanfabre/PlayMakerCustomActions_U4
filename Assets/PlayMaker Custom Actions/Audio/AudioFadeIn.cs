// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Fades in the Volume of the Audio Clip played by the AudioSource component on a Game Object.")]
	public class AudioFadeIn : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[HasFloatSlider(0,1)]
		[Tooltip("The volume to reach")]
		public FsmFloat inVolume;
		
		[RequiredField]
		[HasFloatSlider(0,10)]
		[Tooltip("Fade in time in seconds.")]
		public FsmFloat time;
		
		[Tooltip("Event to send when finished.")]
		public FsmEvent finishEvent;
		
		[Tooltip("Ignore TimeScale. Useful if the game is paused.")]
		public bool realTime;
		
		private float startTime;
		private float currentTime;
		
		private AudioSource _audioSource;
		
		// for the linear equation;
		private float m;
		
		public override void Reset()
		{
			inVolume = 1f;
			gameObject = null;
			time = 1.0f;
			finishEvent = null;
		}
		
		public override void OnEnter()
		{
			
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				_audioSource = go.audio;
			}
			
			startTime = FsmTime.RealtimeSinceStartup;
			currentTime = 0f;
			
			
			if (time.Value>0f)
			{
				m = inVolume.Value/time.Value;
			}else{
				m = 0f;
			}
		}
		
		public override void OnUpdate()
		{
			if (realTime)
			{
				currentTime = FsmTime.RealtimeSinceStartup - startTime;
			}
			else
			{
				currentTime += Time.deltaTime;
			}
			
			if (_audioSource != null)
			{
				_audioSource.volume = m*(currentTime);
			}
			
			if (currentTime > time.Value)
			{
				if (finishEvent != null)
				{
					Fsm.Event(finishEvent);
				}
				
				Finish();
			}
		}



	}
}
