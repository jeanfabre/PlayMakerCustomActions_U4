// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Activates/deactivates children of a gameObject. Can optionnaly define depth of recursion to only affects so many levels of children")]
	public class ActivateChildren : FsmStateAction
	{
		[RequiredField]
        [Tooltip("The GameObject to activate/deactivate.")]
        public FsmOwnerDefault gameObject;
		
		[RequiredField]
        [Tooltip("Check to activate, uncheck to deactivate Game Object.")]
        public FsmBool activate;

        [Tooltip("Recursively activate/deactivate all children given their depth. 0 direct children, 1 means grand children, -1 means all")]
		public FsmInt recursiveDepth; 
		
        [Tooltip("Reset the game objects when exiting this state. Useful if you want an object to be active only while this state is active.\nNote: Only applies to the last Game Object activated/deactivated (won't work if Game Object changes).")]
		public bool resetOnExit;

		
		// store the game object parent
		// so we can de-activate it on exit.
		GameObject parent;


		public override void Reset()
		{
			gameObject = null;
			activate = true;
			recursiveDepth = 0;
			resetOnExit = false;
		}

		public override void OnEnter()
		{
			parent =  Fsm.GetOwnerDefaultTarget(gameObject);

			if (parent!=null)
			{
				foreach (Transform child in parent.transform)
				{
					SetActiveRecursively(child.gameObject, activate.Value,0);
				}
			}
			Finish();

		}

		public override void OnExit()
		{
			if (parent == null)
			{
				return;
			}

			if (resetOnExit)
			{
				foreach (Transform child in parent.transform)
				{
					SetActiveRecursively(child.gameObject, !activate.Value,0);
				}
			}
		}


        public void SetActiveRecursively(GameObject go, bool state,int currentDepth)
        {

			go.SetActive(state);

			if (recursiveDepth.Value>=0 && currentDepth>=recursiveDepth.Value)
			{
				return;
			}
           
			int _nextDepth = currentDepth+1;

            foreach (Transform child in go.transform)
            {
				SetActiveRecursively(child.gameObject, state,_nextDepth);
            }
        } 

    }
}
