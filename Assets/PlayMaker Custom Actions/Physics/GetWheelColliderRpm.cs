// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets the wheelCollider rpm")]
	public class GetWheelColliderRpm : ComponentAction<WheelCollider> 
	{
		
		[RequiredField]
		[Tooltip("The wheelCollider target")]
		[CheckForComponent(typeof(WheelCollider))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The wheel Collider rpm")]
		[UIHint(UIHint.Variable)]
		public FsmFloat rpm;

		[Tooltip("Repeats every frame, usefull if value changes overtime")]
		public bool everyFrame;

		WheelCollider _wc;

		public override void Reset()
		{
			rpm = null;

			everyFrame = false;
		}

		public override void OnPreprocess()
		{
			Fsm.HandleFixedUpdate = true;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
	
			_wc = go.GetComponent<WheelCollider>();

			ExecuteAction();
			
			if(!everyFrame)
			{
				Finish();
			}
		}
		public override void OnFixedUpdate()
		{
			ExecuteAction();
		}
		
		public void ExecuteAction()
		{
			if (!UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
			{
				return;
			}

			_wc = (WheelCollider)this.cachedComponent;

			if (_wc == null)
			{
				return;
			}
			rpm.Value = _wc.rpm;
			
		}
		
	}
}
