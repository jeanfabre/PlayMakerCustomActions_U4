// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Manager Singletons. If the reference given exists already, then this gameobject is destroyed")]
	public sealed class SingletonManager : FsmStateAction{

		public static List<string> SINGLETONS;

		public FsmString reference;

		public override void Reset(){
			reference = "My Singleton";
		}
		
		public override void OnEnter(){
			if (SINGLETONS==null)
			{
				SINGLETONS = new List<string>();
				SINGLETONS.Add(reference.Value);
			}else{

				if (SINGLETONS.Contains(reference.Value))
				{
					Object.Destroy(Owner);
					return;
				}else{
					SINGLETONS.Add(reference.Value);
				}

			}

			Finish();		
		}
	}
}
