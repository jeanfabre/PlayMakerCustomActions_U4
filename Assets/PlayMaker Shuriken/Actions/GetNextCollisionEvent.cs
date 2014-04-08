// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Shuriken")]
	[Tooltip("Each time this action is called it gets the next Collision event for that frame. Use this on States triggered by the global event 'ON PARTICLE COLLISION'. This lets you quickly loop through all the collisions for a particular collision event. NOTE: PlayMakerShurikenProxy Required")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1142")]
	public class GetNextCollisionEvent : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(playMakerShurikenProxy))]
		[Tooltip("The GameObject you expect collisions to happen.")]
		public FsmOwnerDefault gameObject;

		
		[UIHint(UIHint.Variable)]
		[Tooltip("Store this index of  the collision. Use this to exit early to avoid processing too much particles.")]
		public FsmInt index;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Store this particular collision normal")]
		public FsmVector3 collisionNormal;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Store this particular collision velocity")]
		public FsmVector3 collisionVelocity;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Store this particular collision intersection")]
		public FsmVector3 collisionIntersection;
		
		
		[Tooltip("Event to send to get the next collision event.")]
		public FsmEvent loopEvent;

		[Tooltip("Event to send when there are no more collision events.")]
		public FsmEvent finishedEvent;

		public override void Reset()
		{
			gameObject = null;
			
			index = null;
			collisionNormal = null;
			collisionVelocity = null;
			collisionIntersection = null;
			
			loopEvent = null;
			finishedEvent = null;
		}

		// cache the gameObject so we no if it changes
		private ParticleSystem.CollisionEvent[] collisionEvents;

		// increment a index as we loop through Collision Events
		private int nextIndex = 0;

		public override void OnEnter()
		{
			
			if (nextIndex==0) // only check if index is 0
			{
				GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
				if(_go==null)
				{
					Finish();
				}
				
				playMakerShurikenProxy _proxy = _go.GetComponent<playMakerShurikenProxy>();
				
				if (_proxy==null)
				{
					Finish();
				}
				
				collisionEvents = _proxy.GetCollisionEvents();
			}
			
			DoGetNextCollisionEvent();

			Finish();
		}

		void DoGetNextCollisionEvent()
		{
			//Debug.Log("DoGetNextCollisionEvent "+collisionEvents.Length);
			if (collisionEvents == null)
			{
				return;
			}

			// no more collision events?
			// check first to avoid errors.

			if (nextIndex >= collisionEvents.Length)
			{
				nextIndex = 0;
				Fsm.Event(finishedEvent);
				return;
			}
		
			// get next Collision Event

			//storeNextChild.Value = parent.transform.GetChild(nextIndex).gameObject;
			
			collisionVelocity.Value = collisionEvents[nextIndex].velocity;
		 	collisionNormal.Value = 	collisionEvents[nextIndex].normal;
			collisionIntersection.Value = collisionEvents[nextIndex].intersection;

			// no more Collision Events?
			// check a second time to avoid process lock and possible infinite loop if the action is called again.
			// Practically, this enabled calling again this state and it will start again iterating from the first Collision Event.

			if (nextIndex >= collisionEvents.Length)
			{
				nextIndex = 0;
				index.Value = 0;
				Fsm.Event(finishedEvent);
				return;
			}

			// iterate the next Collision Event
			nextIndex++;
			index.Value = nextIndex;
			
			if (loopEvent != null)
			{
				Fsm.Event(loopEvent);
			}
		}
	}
}