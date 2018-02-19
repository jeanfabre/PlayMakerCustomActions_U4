// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
 

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UnityObject)]
	[Tooltip("activate a Component of an Object. WARNING! COLLIDERS DO NOT WORK WITH THIS ACTION and Some Components Might not work!")]
	public class ActivateComponent : FsmStateAction
	{
        [RequiredField]
        [Tooltip("Place Component Object here n/WARNING COLLIDERS DO NOT WORK!!")]
        public FsmObject component;
        [Tooltip("Activate / Deactivate the Component")]
        public FsmBool Activate;

		public override void Reset()
		{
			component = null;
            Activate = false;

        }

		public override void OnEnter()
		{

            DoActivateComponent();

            Finish();
		}

		
		void DoActivateComponent()
		{

            if (component.Value == null)
			{
				LogError("No Component Selected");
                Debug.Log("No Component");
			}
			else
			{
                Behaviour be = component.Value as Behaviour;

                if (be != null)
                {
                    be.enabled = Activate.Value;
                }
                else
                {
                    Renderer rend = component.Value as Renderer;
                    if (rend != null)
                        rend.enabled = Activate.Value;
                }
            }
		}
	}
}