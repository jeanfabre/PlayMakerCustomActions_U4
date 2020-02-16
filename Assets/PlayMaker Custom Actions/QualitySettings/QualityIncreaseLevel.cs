// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"package bundle":["Assets/PlayMaker/Ecosystem/Custom Packages/QualitySettings.unitypackage"]
}
EcoMetaEnd
---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("QualitySettings")]
	[Tooltip("Increase the graphics quality level.")]
	public class QualityIncreaseLevel : FsmStateAction
	{		
		
		[Tooltip("Set to True to force expensive changes to be applied. Only apply anti-aliasing if applyExpensiveChanges is true.")]		
		public FsmBool applyExpensiveChanges;
		
		[ActionSection("Result")]
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The index of the new quality level")]
		public FsmInt qualityIndex;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The name of the new quality level")]
		public FsmString qualityName;
		
		[Tooltip("Event fired when the level reached is the highest level available")]
		public FsmEvent LimitReachedEvent;
		
		public override void Reset()
		{
			applyExpensiveChanges = null;
			

			
			qualityIndex = new FsmInt() {UseVariable=true};
			qualityName = new FsmString() {UseVariable=true};
			applyExpensiveChanges = null;
			LimitReachedEvent = null;
		}

		public override void OnEnter()
		{
			QualitySettings.IncreaseLevel(applyExpensiveChanges.Value);
						
			if (qualityIndex.UseVariable)
			{
				qualityIndex.Value = QualitySettings.GetQualityLevel();
			}
			if (qualityName.UseVariable)
			{
				qualityName.Value = QualitySettings.names[QualitySettings.GetQualityLevel()];
			}
			
			if (QualitySettings.GetQualityLevel() == (QualitySettings.names.Length -1))
			{
				Fsm.Event(LimitReachedEvent);
			}

			Finish();
		}
	}
}
