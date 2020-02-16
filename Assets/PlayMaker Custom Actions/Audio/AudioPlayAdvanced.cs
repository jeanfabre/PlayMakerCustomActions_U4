// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Made By : DjayDino

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
    [ActionTarget(typeof(AudioSource), "gameObject")]
    [ActionTarget(typeof(AudioClip), "oneShotClip")]
	[Tooltip("Plays the Audio Clip set with Set Audio Clip or in the Audio Source inspector on a Game Object. Optionally plays a one shot Audio Clip./n If you wnat to change values while playing Wait For End Of Clip must be turned on")]
	public class AudioPlayAdvanced : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		[Tooltip("The GameObject with an AudioSource component.")]
		public FsmOwnerDefault gameObject;
		
		[ObjectType(typeof(AudioClip))]
		[Tooltip("Optionally play a 'one shot' AudioClip.")]
		public FsmObject oneShotClip;
		
		[ActionSection("Settings")]
		
		[HasFloatSlider(0,255)]
        [Tooltip("Set the priority.")]
		public FsmInt priority;
		
		[HasFloatSlider(0,1)]
        [Tooltip("Set the volume.")]
		public FsmFloat volume;
		
		[HasFloatSlider(-3,3)]
        [Tooltip("Set the pitch.")]
		public FsmFloat pitch;

		#if UNITY_5_5_OR_NEWER
		[HasFloatSlider(-1,1)]
        [Tooltip("Set the Stereo Pan.")]
		public FsmFloat stereoPan;
		
		[HasFloatSlider(0,1)]
        [Tooltip("Set the Spatial Blend.")]
		public FsmFloat spatialBlend;
		
		[HasFloatSlider(0,1.1f)]
        [Tooltip("Set the reverb Zone Mix.")]
		public FsmFloat reverbZoneMix;

#endif
		[ActionSection("3D Sound Settings.")]
		
		[HasFloatSlider(0,5)]
        [Tooltip("Set the dopplerLevel.")]
		public FsmFloat dopplerLevel;
		
		[ActionSection("Finish Options")]
		
		public bool WaitForEndOfClip;
		
		[Tooltip("Event to send when start playing or when the AudioClip finishes playing.")]
		public FsmEvent finishedEvent;

		private AudioSource audio;
				
		public override void Reset()
		{
			gameObject = null;
			volume = new FsmFloat(){UseVariable=true};
			priority = new FsmInt(){UseVariable=true};
			#if UNITY_5_5_OR_NEWER
			stereoPan = new FsmFloat(){UseVariable=true};
			spatialBlend = new FsmFloat(){UseVariable=true};
			reverbZoneMix = new FsmFloat(){UseVariable=true};
			#endif
			dopplerLevel = new FsmFloat(){UseVariable=true};
			pitch = new FsmFloat(){UseVariable=true};
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
				if (!volume.IsNone && volume.Value != audio.volume) audio.volume = volume.Value;
				if (!priority.IsNone) audio.priority = priority.Value;
				#if UNITY_5_5_OR_NEWER
				if (!stereoPan.IsNone) audio.panStereo = stereoPan.Value;
				if (!spatialBlend.IsNone) audio.spatialBlend = spatialBlend.Value;
				if (!reverbZoneMix.IsNone) audio.reverbZoneMix = reverbZoneMix.Value;
				#endif
				if (!dopplerLevel.IsNone) audio.dopplerLevel = dopplerLevel.Value;
				if (!pitch.IsNone) audio.pitch = pitch.Value;
					
					
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
					if 	(!WaitForEndOfClip)
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
				if (!volume.IsNone && volume.Value != audio.volume) audio.volume = volume.Value;
				if (!priority.IsNone) audio.priority = priority.Value;
				#if UNITY_5_5_OR_NEWER
				if (!stereoPan.IsNone) audio.panStereo = stereoPan.Value;
				if (!spatialBlend.IsNone) audio.spatialBlend = spatialBlend.Value;
				if (!reverbZoneMix.IsNone) audio.reverbZoneMix = reverbZoneMix.Value;
				#endif
				if (!dopplerLevel.IsNone) audio.dopplerLevel = dopplerLevel.Value;
				if (!pitch.IsNone) audio.pitch = pitch.Value;
			}
		}
	}
}