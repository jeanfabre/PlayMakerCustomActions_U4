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
	[Tooltip("Gets the name of a quality settings from an index.")]
	public class GetQualityIndexFromName : FsmStateAction
	{		
		[RequiredField]
		[Tooltip("The name of the quality level to find")]
		public FsmString qualityName;
		
		[ActionSection("Result")]
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The index of this quality level name.")]
		public FsmInt qualityIndex;
		
		[Tooltip("Event sent if quality name was found in the quality settings list")]
		public FsmEvent qualityFoundEvent;
		
		[Tooltip("Event sent if quality name was not found in the quality settings list")]
		public FsmEvent qualityNotFoundEvent;
				
				
		public override void Reset()
		{
			qualityIndex = 0;
			qualityName = null;
			qualityFoundEvent = null;
			qualityNotFoundEvent = null;
		}

		public override void OnEnter()
		{
			string _value = qualityName.Value;
			
			int i = 0;
			
			foreach(string name in QualitySettings.names )
			{
				if (string.Equals(name,_value))
				{
					qualityIndex.Value = i;
					Fsm.Event(qualityFoundEvent);
					Finish();
					return;
				}
				
				i++;
			}
			
			Fsm.Event(qualityNotFoundEvent);
			Finish();
			
		}
	}
}
