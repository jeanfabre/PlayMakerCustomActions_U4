// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Automatically types into a string.")]
	public class StringTypewriter : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.TextArea)]
		[Tooltip("The string with the entire message to type out.")]
		public FsmString baseString;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The target result string (can be the same as the base string).")]
		public FsmString resultString;
		
		[Tooltip("The time between letters appearing.")]
		public FsmFloat pause;
		
		[Tooltip("When punctuation is encountered then pause is multiplied by this.\n(period, exclamation, question, comma, semicolon, colon and ellipsis).\nIt also handles repeating characters and pauses only one time at their end.")]
		public FsmFloat punctuationMultiplier;
		
		[Tooltip("True is realtime: continues typing while game is paused. False will subject time variable to the game's timeScale.")]
		public FsmBool realtime;
		
		[Tooltip("Support <color><b><i><size> use. \n LIMITATION: Cannot stack formats together yet! eg <b><i>Text</i></b> won't work. \n Pause: <p=0.9> for a 0.9 second pause (mid sentance pause). \n Speed: <s=1.5> changes Pause to 1.5 (time between characters).")]
		public bool richText;
		
		[Tooltip("Send this event when finished typing.")]
		public FsmEvent finishEvent;
		
		[UIHint(UIHint.Description)]
		public string d1 = "     Optional Sounds Section:";
		
		[Tooltip("Check this to play sounds while typing.")]
		public bool useSounds;
		
		[Tooltip("Check this to not play a sound when it is a spacebar character.")]
		public bool noSoundOnSpaces;
		
		[ObjectType(typeof(AudioClip))]
		[Tooltip("The sound to play for each letter typed.")]
		public FsmObject typingSound;
		
		[Tooltip("The GameObject with an AudioSource component.")]
		public FsmOwnerDefault audioSourceObj;
		
		// Time data
		float p = 0.0f;
		float startTime;
		float timer = 0.0f;
		
		// Character data
		char[] punctuation = {'.', '!', '?', ',', ';', ':'};
		string message = "";
		int index = 0;
		char lastChar;
		char nextChar;
		
		// Rich Text formatting
		private string block;
		private string suffix;
		private bool fBold = false;
		private bool fItal = false;
		private bool fSize = false;
		private bool fColor = false;
		private float forcedPause;

		// Audio
		private AudioSource audioSource;
		private AudioClip sound;
		
		public override void Reset()
		{
			// --- Basic --- 
			baseString = null;
			resultString = null;
			pause = 0.05f;
			punctuationMultiplier = 10.0f;
			realtime = false;
			richText = true;
			finishEvent = null;
			
			// --- Sounds --- 
			useSounds = false;
			noSoundOnSpaces = true;
			typingSound = null;
			audioSourceObj = null;
		}
		
		public override void OnEnter()
		{
			// sort out the sound stuff
			if (useSounds){
				var go = Fsm.GetOwnerDefaultTarget(audioSourceObj);
				if (go != null){
					audioSource = go.GetComponent<AudioSource>();
					if (audioSource == null){
						Debug.LogError ("String Typewriter Action reports: The <color=#ffa500ff>AudioSource component</color> was not found! Does the target object have an Audio Source component?");
						useSounds = false;
					}
					
					sound = typingSound.Value as AudioClip;
					if (sound == null){
						Debug.LogError ("String Typewriter Action reports: The <color=#ffa500ff>AudioClip</color> was not found!");
						useSounds = false;
					}
				}
				
				else {
					Debug.LogError ("String Typewriter Action reports: The <color=#ffa500ff>target Game Object</color> for the audio source was not found!");
					useSounds = false;
				}
			}
			
			index = 0;
			message = baseString.Value; // clone the base string.
			resultString.Value = ""; // clear the target string.
			startTime = Time.realtimeSinceStartup; // get the actual time since the game started.
		}
		
		// Here in OnUpdate we handle...
		// 1) Pausing between letters
		// 2) Checking for punctuation marks
		// 3) Identifying Rich Text Formatting
		public override void OnUpdate()
		{
			// Check if the string is complete
			if (message == resultString.Value)
			{
				DoFinish();
			}
			
			// If the string is not complete, continue work
			else
			{
				p = pause.Value; // clone the pause variable in OnUpdate in case it is changed by the user at runtime.
				
				nextChar = message[index];
				
				// fetch/compare the characters to see if they exist in the punctuation array or not
				int _iLast = Array.IndexOf (punctuation, lastChar); // get last index
				int _iNext = Array.IndexOf (punctuation, nextChar); // get next index
				
				// compare the result
				bool _lastIsMark = _iLast != -1; // if index is not -1, there is a punctuation mark.
				bool _nextIsMark = _iNext != -1; // if index is not -1, there is a punctuation mark.
				
				if (_lastIsMark)
				{
					// if the next char is a punctuation mark then we should not pause.
					if (!_nextIsMark)
					{
						pause.Value = (p * punctuationMultiplier.Value);
					}
				}
				
				// if we run into a format opener, we should process the block!
				if (richText && message[index] == '<')
				{
					DoRichText();
				}

				if (realtime.Value)
				{	
					// check the current time minus the previous Typing event time.
					// if that's more than the pause gap, then its time for another character.
					if (Time.realtimeSinceStartup - startTime >= ((forcedPause != 0) ? forcedPause : pause.Value))
					{
						DoTyping();
					}
				}
				
				if (!realtime.Value)
				{
					// add delta time until its equal or greater than the pause gap.
					timer += Time.deltaTime;
					if (timer >= ((forcedPause != 0) ? forcedPause : pause.Value))
					{
						DoTyping();
					}
				}

				// done with pausing, so revert the pause in case it was changed for punctuation.
				// this also catches the speed change from the <s=[float]> format
				// for the <p=[float]> format this 
				pause.Value = p; 
			}
		}
		
		// Here in DoTyping we handle...
		// 1) Playing sound
		// 2) Adding text to the message
		// 3) Iterating the character index value
		// 4) Resetting the timer and getting time
		public void DoTyping()
		{
			// play the sound if enabled
			if (useSounds)
			{
				if (noSoundOnSpaces && message[index] != ' ')
				{
					audioSource.PlayOneShot (sound);
				}
				
				else
				{
					audioSource.PlayOneShot (sound);
				}
			}
			
			// build the display string
			if (richText)
			{
				// TODO //
				// This needs to have support for organzizing the openers with the closers.
				// If the openers are <b><i><color>... then the closers must be organized as </color><i><b> instead of the fixed arrangement below.
				suffix = (fBold ? "</b>" : "") + (fItal ? "</i>" : "") + (fSize ? "</size>" : "") + (fColor ? "</color>" : "");

				// add one character to the string, and the suffix.
				resultString.Value = message.Substring(0, index) + (message[index] + suffix); 
			}
			
			if (!richText)
			{
				resultString.Value += message[index]; // add one character to the string
			}
			
			lastChar = message[index]; // store the index that we just typed
			index++; // iterate the index
			
			timer = 0.0f; // reset timer
			startTime = Time.realtimeSinceStartup; // update realtime
		}

		public void DoRichText()
		{
			//reset the forced pause here because it should only be used for one character.
			forcedPause = 0.0f;

			block = "";
			int blockStartPoint = index;

			// Construct the <block>
			while (index < message.Length)
			{
				block += message[index];
				index++;
				
				if (message[index] == '>')
				{
					block += message[index];
					index = index+1;
					break;
				}
			}

			block = block.ToLower();
			
			if (block.Contains("/")) // block is an closer, disable the flag for the suffix builder since it isn't necessary anymore.
			{
				if (block.Contains ("</c")){
					fColor = false; return;}
				if (block.Contains ("</s")){
					fSize = false; return;}
				if (block.Contains ("</i")){
					fItal = false; return;}
				if (block.Contains ("</b")){
					fBold = false; return;}
			}
			
			if (!block.Contains("/")) // block is an opener (or special), tell the suffix to make a closer for it OR catch the speed/pause change.
			{
				if (block.Contains ("<color")){
					fColor = true; return;}
				if (block.Contains ("<size")){
					fSize = true; return;}
				if (block.Contains ("<i")){
					fItal = true; return;}
				if (block.Contains ("<b")){
					fBold = true; return;}
				if (block.Contains ("<s="))
				{
					int i = 3;
					string speed = "";
					while (i < block.Length)
					{
						speed += block[i];
						i++;
						
						if (block[i] == '>')
						{
							p = float.Parse(speed);
							pause.Value = p;

							string front = message.Substring(0, blockStartPoint);
							string back = message.Substring(blockStartPoint+block.Length, (message.Length - front.Length - block.Length));

							index = blockStartPoint;
							message = front+back;
							
							break;
						}
					}
				}

				if (block.Contains ("<p="))
				{
					int i = 3;
					string pause = "";
					while (i < block.Length)
					{
						pause += block[i];
						i++;
						
						if (block[i] == '>')
						{
							float pa = float.Parse(pause);
							forcedPause += pa;

							string front = message.Substring(0, blockStartPoint);
							string back = message.Substring(blockStartPoint+block.Length, (message.Length - front.Length - block.Length));
							
							index = blockStartPoint;
							message = front+back;

							break;
						}
					}
				}
			}
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
