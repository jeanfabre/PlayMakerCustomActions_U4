// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Sets the wheelCollider BrakeTorque")]
	public class SetWheelColliderBrakeTorque : ComponentAction<WheelCollider> 
	{
		
		[RequiredField]
		[Tooltip("The wheelCollider target")]
		[CheckForComponent(typeof(WheelCollider))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The Brake Torque")]
		public FsmFloat brakeTorque;

		[Tooltip("Repeats every frame, usefull if value changes overtime")]
		public bool everyFrame;

		WheelCollider _wc;

		public override void Reset()
		{
			brakeTorque = null;
			
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

			_wc.brakeTorque = brakeTorque.Value;
			
		}
		
	}
}
