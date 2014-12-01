// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Automatically types into a string.")]
	public class StringTypewriter : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The string with the entire message to type out.")]
		public FsmString baseString;

		[RequiredField]
		[UIHint(UIHint.Variable)] 
		[Tooltip("The target result string (can be the same as the base string).")]
		public FsmString resultString;

		[Tooltip("The time between letters appearing.")]
		public FsmFloat pause;

		[Tooltip("True is realtime: continues typing while game is paused. False will subject time variable to the game's timeScale.")]
		public FsmBool realtime;
		
		[Tooltip("Send this event when finished typing.")]
		public FsmEvent finishEvent;



		[UIHint(UIHint.Description)]
		public string d1 = "     Optional Sounds Section:";

		[Tooltip("Check this to play sounds while typing.")]
		public bool useSounds;

		[Tooltip("Do not play a sound when it is a spacebar character.")]
		public bool noSoundOnSpaces;

		[ObjectType(typeof(AudioClip))]
		[Tooltip("The sound to play for each letter typed.")]
		public FsmObject typingSound;

		[Tooltip("The GameObject with an AudioSource component.")]
		public FsmOwnerDefault audioSourceObj;

		int index = 0;
		int length;
		float startTime;
		float timer = 0.0f;
		string message;
		private AudioSource audioSource;
		private AudioClip sound;

		public override void Reset()
		{
			baseString = null;
			resultString = null;
			pause = 0.05f;
			realtime = false;
			finishEvent = null;

			useSounds = false;
			noSoundOnSpaces = true;
			typingSound = null;
			audioSourceObj = null;
		}

		public override void OnEnter()
		{
			// sort out the sounds
			if (useSounds)
			{
				// find the audio source
				var go = Fsm.GetOwnerDefaultTarget(audioSourceObj);
				if (go != null)
				{
					audioSource = go.GetComponent<AudioSource>();
					if (audioSource == null)
					{
						Debug.Log ("AudioSource component not found.");
						useSounds = false;
					}

					sound = typingSound.Value as AudioClip;
					if (sound == null)
					{
						Debug.Log ("AudioClip not found.");
						useSounds = false;
					}
				}

				else 
				{
					Debug.Log ("AudioSource Object not found.");
					useSounds = false;
				}
			}

			// clone the base string.
			message = baseString.Value;

			// get the length of the message.
			length = message.Length;

			// clear the target string.
			resultString.Value = "";

			// get the actual time since the game started.
			startTime = Time.realtimeSinceStartup;
		}

		public override void OnUpdate()
		{
			if (realtime.Value)
			{	
				// check the current time minus the previous Typing event time.
				// if that's more than the pause gap, then its time for another character.
				if (Time.realtimeSinceStartup - startTime >= pause.Value)
				{
					DoTyping();
				}
			}

			if (!realtime.Value)
			{
				// add delta time until its more than the pause gap.
				timer += Time.deltaTime;
				if (timer >= pause.Value)
				{
					DoTyping();
				}
			}
		}

		public void DoTyping()
		{
			if (useSounds)
			{
				if (noSoundOnSpaces && message[index] != ' ')
				{
					audioSource.PlayOneShot (sound);
				}

				if (!noSoundOnSpaces)
				{
					audioSource.PlayOneShot (sound);
				}
			}

			// add one character
			resultString.Value += message[index];
			index++;
			timer = 0.0f;

			// if we're done, then finish out
			if (index >= length)
			{
				DoFinish();
			}
			
			// otherwise, get the current time again and wait for the next OnUpdate
			startTime = Time.realtimeSinceStartup;
		}

		public void DoFinish()
		{
			Finish();
			if (finishEvent != null)
			{
				Fsm.Event(finishEvent);
			}
		}

		public override void OnExit()
		{
			// if the state exits before finishing the string
			// then it needs to be auto-completed.
			resultString.Value = message;
		}
	}
}










