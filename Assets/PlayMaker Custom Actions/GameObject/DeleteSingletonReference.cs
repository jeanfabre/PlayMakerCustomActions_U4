// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// PLAYMAKER ECOSYSTEM DO NOT EDIT
/*---
EcoMetaStart
{
"script dependancies":["Assets/PlayMaker Custom Actions/GameObject/SingletonManager.cs"]
}
EcoMetaEnd
---*/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Delete a singleton reference added using SingletonManager action.")]
	public sealed class DeleteSingletonReference : FsmStateAction{
		

		public FsmString reference;
		
		public override void Reset(){
			reference = "My Singleton";
		}
		
		public override void OnEnter(){

			if (SingletonManager.SINGLETONS!=null)
			{
				SingletonManager.SINGLETONS.Remove(reference.Value);
			}

			Finish();		
		}
	}
}
