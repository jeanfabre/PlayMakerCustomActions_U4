// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Tests if a wheel Collider is grounded.")]
	public class GetWheelColliderIsGrounded : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(WheelCollider))]
		public FsmOwnerDefault gameObject;
		
		public FsmEvent isGroundedEvent;
		
		public FsmEvent isNotGroundedEvent;
		
		[UIHint(UIHint.Variable)]
		public FsmBool store;
		
		public bool everyFrame;
		public bool sendEventOnlyIfChange;
		
		
		
		WheelCollider _wheel;
		bool isGrounded;
		
		public override void Reset()
		{
			gameObject = null;
			isGroundedEvent = null;
			isNotGroundedEvent = null;
			store = null;
			everyFrame = false;
			sendEventOnlyIfChange = false;
			
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);

			if (go != null)
			{
				_wheel = go.GetComponent<WheelCollider>();
			}
			
				
			DoIsGrounded();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoIsGrounded();
		}

		void DoIsGrounded()
		{
			
			if (_wheel==null)
			{
				return;
			}
			bool _newIsGrounded = _wheel.isGrounded;
			
			store.Value = _newIsGrounded;
			
			bool fire = false;
			
			if (!sendEventOnlyIfChange)
			{
				fire = true;
			}else if (_newIsGrounded != isGrounded )
			{
				isGrounded = _newIsGrounded;
				fire = true;
				
			}
	
			if (fire) Fsm.Event(isGrounded ? isGroundedEvent : isNotGroundedEvent);
		}
	}
}

