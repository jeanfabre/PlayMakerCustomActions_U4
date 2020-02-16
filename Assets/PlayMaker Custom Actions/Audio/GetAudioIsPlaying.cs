// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Check if an AudioSource is playing.")]
	public class GetAudioIsPlaying : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("True if the audioSource is playing")]
		public FsmBool isPlaying;
		
		[Tooltip("Event to send if the audioSource is playing")]
		public FsmEvent isPlayingEvent;
		
		[Tooltip("Event to send if the audioSource is not playing")]
		public FsmEvent isNotPlayingEvent;
		
		AudioSource _comp;
		
		public override void Reset()
		{ 
			gameObject = null;
			isPlaying =null;
			isPlayingEvent = null;
			isNotPlayingEvent = null;
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			
			_comp = go.GetComponent<AudioSource>();
			if (_comp == null)
			{
				LogError("GetAudioIsPlaying: Missing AudioSource!");
				return;
			}

			bool _isPlaying = _comp.isPlaying;
			isPlaying.Value = _isPlaying;

			Fsm.Event(_isPlaying ? isPlayingEvent : isNotPlayingEvent);

			Finish();
		}
	}
}