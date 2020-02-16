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
	[Tooltip("Gets the index of a quality settings from its name.")]
	public class GetQualityNameFromIndex : FsmStateAction
	{		
		[RequiredField]
		[Tooltip("The index of the quality level to find")]
		public FsmInt qualityIndex;
		
		[ActionSection("Result")]
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The name of this quality level index.")]
		public FsmString qualityName;
		
		[Tooltip("Event sent if quality index was found in the quality settings list")]
		public FsmEvent qualityFoundEvent;
		
		[Tooltip("Event sent if quality index was not found in the quality settings list")]
		public FsmEvent qualityNotFoundEvent;
				
				
		public override void Reset()
		{
			qualityIndex = null;
			qualityName = null;
			qualityFoundEvent = null;
			qualityNotFoundEvent = null;
		}

		public override void OnEnter()
		{
			
			
			int _index = qualityIndex.Value;
			
			if (_index<0 || _index>= QualitySettings.names.Length )
			{
				Fsm.Event(qualityNotFoundEvent);
				Finish();
			}
			
			qualityName.Value = QualitySettings.names[_index];
			Fsm.Event(qualityFoundEvent);
			Finish();
		}
	}
}
