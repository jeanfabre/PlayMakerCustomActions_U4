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
	[Tooltip("Sets a new graphics quality level.")]
	public class GetQualityLevel : FsmStateAction
	{		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The index of the current quality level")]
		public FsmInt qualityIndex;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The name of the current quality level")]
		public FsmString qualityName;

		public override void Reset()
		{
			qualityIndex = null;
			qualityName = null;
		}

		public override void OnEnter()
		{
			qualityIndex.Value = QualitySettings.GetQualityLevel();
			qualityName.Value = QualitySettings.names[QualitySettings.GetQualityLevel()];

			Finish();
		}
	}
}
