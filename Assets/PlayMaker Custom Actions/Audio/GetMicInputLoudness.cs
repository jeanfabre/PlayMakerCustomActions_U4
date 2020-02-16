// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords : Microphone, volume, decibel, decibels
/*---
EcoMetaStart
{
"script dependancies":["Assets/PlayMaker Custom Scripts/Audio/MicInput.cs"]
}
EcoMetaEnd
---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Check the default Mic Input Volume. Requires an active MicInput Component in your scene")]
	public class GetMicInputLoudness : FsmStateAction
	{

		[UIHint(UIHint.Variable)]
		[Tooltip("True if the audioSource is playing")]
		public FsmFloat micLoudness;

		[UIHint(UIHint.Variable)]
		[Tooltip("True if the audioSource is playing")]
		public FsmFloat micLoudnessInDecibels;
	
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;

		public override void Reset()
		{ 
			micLoudness = null;
			everyFrame = true;
		}
		
		public override void OnEnter()
		{
			Execute ();

			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			Execute ();
		}

		void Execute()
		{
			micLoudness.Value = MicInput.MicLoudness;
			micLoudnessInDecibels.Value = MicInput.MicLoudnessInDecibels;
		}
	}
}