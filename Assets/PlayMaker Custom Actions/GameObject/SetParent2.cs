// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Sets the Parent of a Game Object. Same as official action but lt you use 'owner' for the parent for convenience.")]
	public class SetParent2 : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to parent.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The new parent for the Game Object.")]
		public FsmOwnerDefault parent;

		[Tooltip("Set the local position to 0,0,0 after parenting.")]
		public FsmBool resetLocalPosition;

		[Tooltip("Set the local rotation to 0,0,0 after parenting.")]
		public FsmBool resetLocalRotation;

		public override void Reset()
		{
			gameObject = new FsmOwnerDefault();
			gameObject.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			
			parent = null;
			resetLocalPosition = null;
			resetLocalRotation = null;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			var _parent = Fsm.GetOwnerDefaultTarget(parent);
			
			if ( _parent == go)
			{
				LogError("Can not parent to self");
			}
			
			if (go != null)
			{
				go.transform.parent = _parent == null ? null : _parent.transform;

				if (resetLocalPosition.Value)
				{
					go.transform.localPosition = Vector3.zero;
				}

				if (resetLocalRotation.Value)
				{
					go.transform.localRotation = Quaternion.identity;
				}
			}
			
			Finish();
		}
		
		public override string ErrorCheck()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			var _parent = Fsm.GetOwnerDefaultTarget(parent);
			
			if (_parent!=null && _parent == go)
			{
				return "Can not parent to self"; 
			}
			
			return "";
		}
	}
}