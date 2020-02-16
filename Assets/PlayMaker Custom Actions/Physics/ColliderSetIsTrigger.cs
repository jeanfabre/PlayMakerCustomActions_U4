// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Controls a collider isTrigger Flag")]
	public class ColliderSetIsTrigger : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Collider))]
		[Tooltip("The GameObject with collider to control")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("Is the collider a trigger or not")]
		public FsmBool isTrigger;
		
		[Tooltip("Reset the collider trigger flag on exit")]
		public FsmBool resetOnExit;
		
		private Collider _collider;
		bool _originalValue;
		
		public override void Reset()
		{
			gameObject = null;
			isTrigger = false;
			resetOnExit = false;
		}

		public override void OnEnter()
		{
			DoSetIsTrigger();
			
			Finish();
		}

		void DoSetIsTrigger()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			
			_collider = go.GetComponent<Collider>();
			
			if (_collider == null) return;
			
			if (resetOnExit.Value)
			{
				_originalValue = _collider.isTrigger;
			}
			
			_collider.isTrigger = isTrigger.Value;
		}
		
		public override void OnExit()
		{
			if (_collider==null)
			{
				return;
			}
			
			if (resetOnExit.Value)
			{
				_collider.isTrigger = _originalValue;
			}
		}
	}
}
