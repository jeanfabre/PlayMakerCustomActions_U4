// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Check if user granted access to microphone")]
	public class MicrophoneIsAccessGranted : FsmStateAction
	{
		
		[ObjectType(typeof(AudioClip))]
		[Tooltip("Is microphone accessible")]
		public FsmBool isAccessGranted;
		
		[RequiredField]
		[Tooltip("Event sent when microphone accessible")]
		public FsmEvent AccessGrantedEvent;
		
		[RequiredField]
		[Tooltip("Event sent when microphone not accessible")]
		public FsmEvent AccessDeniedEvent;
		
		public override void Reset()
		{
			isAccessGranted = false;
			AccessGrantedEvent = null;
			AccessDeniedEvent = null;
			
		}

		public override void OnEnter()
		{
			isAccessGranted.Value = Application.HasUserAuthorization(UserAuthorization.WebCam | UserAuthorization.Microphone);
			if (isAccessGranted.Value)
			{
				Fsm.Event(AccessGrantedEvent);	
			}else{
				Fsm.Event(AccessDeniedEvent);
			}
			
			Finish();
		}
	}
}
