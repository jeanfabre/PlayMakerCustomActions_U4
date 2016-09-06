// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Detect collisions with objects that have RigidBody components. \nNOTE: The system events, TRIGGER ENTER, TRIGGER STAY, and TRIGGER EXIT are sent when any object collides with the trigger. Use this action to filter collisions by Tag.")]
	public class TriggerEvent2 : FsmStateAction
	{
		public TriggerType trigger;
		[UIHint(UIHint.Tag)]
		public FsmString collideTag;
		public FsmEvent sendEvent;
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeCollider;

		public override void Reset()
		{
			trigger = TriggerType.OnTriggerEnter;
			collideTag = new FsmString() {UseVariable=true};
			sendEvent = null;
			storeCollider = null;
		}

		public override void Awake()
		{
			switch (trigger)
			{
				case TriggerType.OnTriggerEnter:
					Fsm.HandleTriggerEnter = true;
					break;
				case TriggerType.OnTriggerStay:
					Fsm.HandleTriggerStay = true;
					break;
				case TriggerType.OnTriggerExit:
					Fsm.HandleTriggerExit = true;
					break;
			}
		}

		void StoreCollisionInfo(Collider collisionInfo)
		{
			storeCollider.Value = collisionInfo.gameObject;
		}

		public override void DoTriggerEnter(Collider other)
		{
			if (trigger == TriggerType.OnTriggerEnter)
			{
				if (other.gameObject.tag == collideTag.Value || collideTag.IsNone)
				{
					StoreCollisionInfo(other);
					Fsm.Event(sendEvent);
				}
			}
		}

		public override void DoTriggerStay(Collider other)
		{
			if (trigger == TriggerType.OnTriggerStay)
			{
				if (other.gameObject.tag == collideTag.Value || collideTag.IsNone)
				{
					StoreCollisionInfo(other);
					Fsm.Event(sendEvent);
				}
			}
		}

		public override void DoTriggerExit(Collider other)
		{
			if (trigger == TriggerType.OnTriggerExit)
			{
				if (other.gameObject.tag == collideTag.Value|| collideTag.IsNone )
				{
					StoreCollisionInfo(other);
					Fsm.Event(sendEvent);
				}
			}
		}

		public override string ErrorCheck()
		{
			return ActionHelpers.CheckOwnerPhysicsSetup(Owner);
		}


	}
}
