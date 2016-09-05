// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// Based on MaDDoX work http://hutonggames.com/playmakerforum/index.php?topic=159.msg591#msg591
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Material)]
    [Tooltip("Sets or toggle the visibility on a game object. Can optionnaly reset on exit and recurse children visibility")]
	public class SetVisibility2 : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;

        [Tooltip("Should the object visibility be toggled?\nHas priority over the 'visible' setting")]
        public FsmBool toggle;
		
        [Tooltip("Should the object be set to visible or invisible?")]
        public FsmBool visible;
		
        [Tooltip("Resets to the initial visibility once\nit leaves the state")]
        public bool resetOnExit;
		
		[Tooltip("Affects children if set to true")]
        public bool recursive;
		
        private bool initialVisibility;

		public override void Reset()
		{
			gameObject = null;
            toggle = false;
			visible = false;
            resetOnExit = true;
            initialVisibility = false;
		    recursive = true;
		}
		
		public override void OnEnter()
		{
            if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
                DoSetVisibility(Owner);
            else
                DoSetVisibility(gameObject.GameObject.Value);
            
            Finish();
		}

        void DoSetVisibility(GameObject go)
		{
			
			if (go == null) return;

            var renderers = go.GetComponentsInChildren<Renderer>();

            // 'memorizes' initial visibility
            bool isRootRenderer = (go.GetComponent<Renderer>() != null);

            if (isRootRenderer) 
                initialVisibility = go.renderer.isVisible;
            else
                initialVisibility = false;                

            // if 'toggle' is not set, simply sets visibility to new value
            if (toggle.Value == false) {
                if (isRootRenderer) 
                    go.renderer.enabled = visible.Value;
                
                if (!recursive) return;
                foreach (var renderer in renderers) {
                    renderer.enabled = visible.Value;
                }
                return;
            }
			
            // otherwise, toggles the visibility
            if (isRootRenderer)
                go.renderer.enabled = !go.renderer.isVisible;

            if (!recursive) return;
            foreach (var renderer in renderers) {
                renderer.enabled = visible.Value;
            }
            return;
        }

        public override void OnExit()
        {
            if (resetOnExit)
                ResetVisibility();
        }

        void ResetVisibility()
        {
            // uses the FSM to get the target object and resets its visibility
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go != null && go.renderer != null)
                go.renderer.enabled = initialVisibility;
        }

	}
}
