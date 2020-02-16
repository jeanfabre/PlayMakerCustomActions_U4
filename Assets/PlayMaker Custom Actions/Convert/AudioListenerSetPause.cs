// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Paused state of the audio. If set to True, the listener will not generate sound. Similar to setting the volume to 0.0.")]
	public class AudioListenerSetPause : FsmStateAction
	{
		
		public FsmBool pause;
		
		public bool everyFrame;
		
		public override void Reset()
		{
			pause = null;
		}
		
		public override void OnEnter()
		{
			SetPause();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		public override void OnUpdate()
		{
			SetPause();
		}
		
		void SetPause()
		{
			if (AudioListener.pause != pause.Value)
			{
				AudioListener.pause = pause.Value;
			}
		}
		
	}
}
