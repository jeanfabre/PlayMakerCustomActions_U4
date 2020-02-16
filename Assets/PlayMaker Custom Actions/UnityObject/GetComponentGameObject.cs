// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// source: http://hutonggames.com/playmakerforum/index.php?action=post;topic=19298.0;last_msg=84017

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory(ActionCategory.UnityObject)]
	[Tooltip("Gets the owner gameobject of a component")]
	public class GetComponentOwner : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Object referencing your component")]
		public FsmObject componentToUse;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The gameobject of that component")]
		public FsmGameObject storeGameObject;
		
		public override void Reset()
		{
			componentToUse = null;
			storeGameObject = null;
		}
		
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			DoGetComponentOwner();
			
			Finish();
		}
		
		public void DoGetComponentOwner()
		{
			Component _mb = componentToUse.Value as Component;
			if (_mb != null) {
				storeGameObject.Value = _mb.gameObject;
			} else {
				storeGameObject.Value = null;
			}
		}
		
		
	}
	
}