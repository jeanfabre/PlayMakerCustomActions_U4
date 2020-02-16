// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//by MDS
// Used Audio Play action + a little code to make this

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Audio)]
    [ActionTarget(typeof(AudioSource), "gameObject")]
    [Tooltip("Plays a random audioClip from an array. Can also randomize clip pitch. Useful for footsteps. Array var type must be Object-->UnityEngine-->AudioClip")]
    public class PlayRandomAudioFromArray : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(AudioSource))]
        [Tooltip("The GameObject with an AudioSource component.")]
        public FsmOwnerDefault gameObject;

        [HasFloatSlider(0, 1)]
        [Tooltip("Set the volume.")]
        public FsmFloat volume;

        [Tooltip("Set the lowest the pitch can go. Leave at 1 for no random pitch.")]
        public FsmFloat randomPitchLow;
        [Tooltip("Set the highest the pitch can go. Leave at 1 for no random pitch.")]
        public FsmFloat randomPitchHigh;

        [Tooltip("The array of AudioClips.")]
        [UIHint(UIHint.Variable)]
        public FsmArray audioArray;

        [Tooltip("If true the Finished event will fire before audio is done playing.")]
        public FsmBool waitForFinish;

        [Tooltip("Use this variable to avoid repeats. Store it and repeats won't happen.")]
        public FsmInt lastIndex;


        [Tooltip("Event to send when the AudioClip finishes playing.")]
        public FsmEvent finishedEvent;

        private AudioSource audio;
        private int index;
 
        public override void Reset()
        {
            gameObject = null;
            volume = 1f;
            randomPitchLow = 0.8f;
            randomPitchHigh = 1.2f;      
            finishedEvent = null;
            waitForFinish = true;
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
                    index = Random.Range(0, audioArray.Length);
              
                    while (index == lastIndex.Value)
                    {
                        index = Random.Range(0, audioArray.Length); 
                       
                    }
                    

                    lastIndex.Value = index;
                    

                    AudioClip audioClip = (AudioClip)audioArray.Get(index);
                    audio.clip = audioClip;
                    audio.volume = volume.Value;
                    audio.pitch = Random.Range(randomPitchLow.Value, randomPitchHigh.Value);
                    audio.Play();                  
                }                
            }
            else
            {
                // Finish if failed to play sound	
                Finish();

            }
        
        }

        public override void OnUpdate()
        {
            if (audio == null)
            {
                Finish();
            } 
            else
            {
                if (audio.isPlaying == false | waitForFinish.Value == false)
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
