// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"package bundle":["Assets/PlayMaker/Ecosystem/Custom Packages/QualitySettings.unitypackage"]
}
EcoMetaEnd
---*/

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("QualitySettings")]
	[Tooltip("Sets a new graphics quality level.")]
	public class SetQualityLevel : FsmStateAction
	{		
		
		[RequiredField]
		[Tooltip("The name of the quality level so to say. leave empty if using quality index")]
		public FsmString qualityName;
		
		[RequiredField]
		[Tooltip("The index of the quality level so to say. 0 is the first quality level in the list. Leave to 'none' when using 'quality name'.")]
		public FsmInt qualityIndex;
		
		[Tooltip("Set to True to force expensive changes to be applied when changing quality level")]		
		public FsmBool applyExpensiveChanges;

		public override void Reset()
		{
			qualityName = "Fastest";
			qualityIndex = new FsmInt() {UseVariable=true};
			applyExpensiveChanges = false;
		}

		public override void OnEnter()
		{
			
			UnityEngine.QualitySettings.SetQualityLevel (qualityIndex.Value, applyExpensiveChanges.Value);

			Finish();
		}
	}
}
