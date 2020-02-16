// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UnityObject)]
	[Tooltip("Gets a Component attached to a GameObject or its children and stores it in an Object variable. NOTE: Set the Object variable's Object Type to get a component of that type. E.g., set Object Type to UnityEngine.AudioListener to get the AudioListener component on the camera.")]
	public class GetComponentInChildren : FsmStateAction
	{
        [Tooltip("The GameObject that owns the component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
        [Tooltip("Store the component in an Object variable.\nNOTE: Set theObject variable's Object Type to get a component of that type. E.g., set Object Type to UnityEngine.AudioListener to get the AudioListener component on the camera.")]
		public FsmObject storeComponent;
		
        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			storeComponent = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetComponent();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetComponent();
		}
		
		void DoGetComponent()
		{
			if (storeComponent == null)
			{
				return;
			}
			
			var targetObject = Fsm.GetOwnerDefaultTarget(gameObject);
			
			if (targetObject == null)
			{
				return;
			}
			
			storeComponent.Value = targetObject.GetComponentInChildren(storeComponent.ObjectType);
		}
	}
}
