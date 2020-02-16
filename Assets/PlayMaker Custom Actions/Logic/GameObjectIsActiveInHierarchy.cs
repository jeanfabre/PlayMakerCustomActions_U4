// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if a GameObject Variable is Active or not.")]
	public class GameObjectIsActiveInHierarchy : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The GameObject variable to test.")]
		public FsmGameObject gameObject;

        [Tooltip("Event to send if the GamObject is Active.")]
		public FsmEvent isActive;

        [Tooltip("Event to send if the GamObject is NOT Active.")]
		public FsmEvent isNotActive;

		[UIHint(UIHint.Variable)]
        [Tooltip("Store the result in a bool variable.")]
		public FsmBool storeResult;

		[Tooltip("Debug console warning if null object.")]
		public bool debugging;

        [Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			isActive = null;
			isNotActive = null;
			storeResult = null;
			debugging = false;
			everyFrame = false;
		}

		public override void OnUpdate()
		{
			if (debugging && gameObject.Value == null)
			{
				Debug.Log("GameObject is null!");
				
			}else{

				DoIsGameObjectActive();
			}

			if (!everyFrame)
			{
				Finish();
			}
		}
		
		void DoIsGameObjectActive()
		{
			var goIsActive = gameObject.Value.activeInHierarchy;
			storeResult.Value = goIsActive;

			Fsm.Event(goIsActive ? isActive : isNotActive);
		}
	}
}
