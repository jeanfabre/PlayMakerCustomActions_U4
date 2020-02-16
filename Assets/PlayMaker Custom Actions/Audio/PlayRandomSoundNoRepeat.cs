// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Plays a Random Audio Clip at a position defined by a Game Object or a Vector3. If a position is defined, it takes priority over the game object. You can set the relative weight of the clips to control how often they are selected.")]
	public class PlayRandomSoundNoRepeat : FsmStateAction
	{
		public FsmOwnerDefault gameObject;
		public FsmVector3 position;
		[CompoundArray("Audio Clips", "Audio Clip", "Weight")]
		public AudioClip[] audioClips;
		[HasFloatSlider(0, 1)]
		public FsmFloat[] weights;
		[HasFloatSlider(0, 1)]
		public FsmFloat volume = 1f;
		[Tooltip("Can hit the same number twice in a row")]
		public FsmBool Repeat;
		
		private int randomIndex;
		private int lastIndex = -1;
		
		public override void Reset()
		{
			gameObject = null;
			position = new FsmVector3 { UseVariable = true };
			audioClips = new AudioClip[3];
			weights = new FsmFloat[] {1,1,1};
			volume = 1;
			Repeat = true;
		}

		public override void OnEnter()
		{
			DoPlayRandomClip();
			
			Finish();
		}

		void DoPlayRandomClip()
		{
			if (audioClips.Length == 0) 
			{
			return;
			}
			
			if (Repeat.Value)
			{
				randomIndex = ActionHelpers.GetRandomWeightedIndex(weights);

	
			}
			else
			{
				do
				{
					randomIndex = ActionHelpers.GetRandomWeightedIndex(weights);
				} while ( randomIndex == lastIndex);
				
				lastIndex = randomIndex;

			}
			
			if (randomIndex != -1)
			{
				AudioClip clip = audioClips[randomIndex];
				if (clip != null)
				{
					if (!position.IsNone)
					{
						AudioSource.PlayClipAtPoint(clip, position.Value, volume.Value);
					}
					else
					{
						GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
						if (go == null) return;
						
						AudioSource.PlayClipAtPoint(clip, go.transform.position, volume.Value);
					}				
				}
			}
		}
	}
}
