// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __ACTION__ ---*/
// Made By : DjayDino

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
    [ActionTarget(typeof(AudioSource), "gameObject")]
    [ActionTarget(typeof(AudioClip), "oneShotClip")]
	[Tooltip("Plays the Audio Clip set with Set Audio Clip or in the Audio Source inspector on a Game Object. Optionally plays a one shot Audio Clip.")]
	public class AudioPlaySimple : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		[Tooltip("The GameObject with an AudioSource component.")]
		public FsmOwnerDefault gameObject;
		
		[HasFloatSlider(0,1)]
        [Tooltip("Set the volume.")]
		public FsmFloat volume;
		
		[ObjectType(typeof(AudioClip))]
		[Tooltip("Optionally play a 'one shot' AudioClip.")]
		public FsmObject oneShotClip;
		
		public FsmBool WaitForEndOfClip;
		
		[Tooltip("Event to send when the AudioClip finishes playing.")]
		public FsmEvent finishedEvent;

		private AudioSource audio;
				
		public override void Reset()
		{
			gameObject = null;
			volume = 1f;
			oneShotClip = null;
		    finishedEvent = null;
			WaitForEndOfClip = false;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				// cache the AudioSource component

			    audio = go.GetComponent<AudioSource>();
				if (audio != null)
				{
					var audioClip = oneShotClip.Value as AudioClip;

					if (audioClip == null)
					{
						audio.Play();
						
						if (!volume.IsNone)
						{
							audio.volume = volume.Value;
						}
						
						return;
					}
					
					if (!volume.IsNone)
					{
						audio.PlayOneShot(audioClip, volume.Value);
					}
					else
					{
						audio.PlayOneShot(audioClip);
					}
					if 	(WaitForEndOfClip.Value == false)
					{
						Fsm.Event(finishedEvent);
						Finish();
					}
					
					return;
				}
			}
			
			// Finish if failed to play sound	
		
			Finish();
		}
		
		public override void OnUpdate ()
		{
			if (audio == null)
			{
				Finish();
			}
			else
			{
				if (!audio.isPlaying)
				{
					Fsm.Event(finishedEvent);
					Finish();
				}
                else if (!volume.IsNone && volume.Value != audio.volume)
				{
					audio.volume = volume.Value;
				}
			}
		}
	}
}