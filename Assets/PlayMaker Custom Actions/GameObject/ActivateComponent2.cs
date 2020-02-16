// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Destroys a Component of an Object.")]
	public class ActivateComponent2 : FsmStateAction
	{
		[RequiredField]
        [Tooltip("The GameObject that owns the Component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.ScriptComponent)]
        [Tooltip("The name of the Component to destroy.")]
		public FsmString component;

		[Tooltip("Activate / Deactivate the Component")]
		public FsmBool activate;

		[Tooltip("Repeat Everyframe")]
		public bool everyFrame;


		Component aComponent;

		Behaviour _behaviour;

		Renderer rend;

		public override void Reset()
		{
			gameObject = null;
			component = null;
			activate = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoDestroyComponent(gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value);

			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			DoDestroyComponent(gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value);

		}

		
		void DoDestroyComponent(GameObject go)
		{
			aComponent = go.GetComponent(ReflectionUtils.GetGlobalType(component.Value));
			if (aComponent == null)
			{
				LogError("No such component: " + component.Value);
			}
			else
			{
				_behaviour = aComponent as Behaviour;
				
				if (_behaviour != null)
				{
					if( _behaviour.enabled!= activate.Value)
					{
						_behaviour.enabled = activate.Value;
					}
				}
				else
				{
					Renderer rend = aComponent as Renderer;
					if (rend != null)
					{
						if( _behaviour.enabled!= activate.Value)
						{
							rend.enabled = activate.Value;
						}
					}
				}
			}
		}
	}
}
