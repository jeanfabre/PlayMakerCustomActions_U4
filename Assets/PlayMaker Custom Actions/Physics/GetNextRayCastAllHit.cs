// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System.Linq;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Performs a rayCast hit and Each time this action is called it gets the next hit. This lets you quickly loop through all the hits to perform actions on them.")]
	public class GetNextRayCastAllHit : FsmStateAction
	{
		
		[ActionSection("Raycast Settings")] 
		
		[Tooltip("Start ray at game object position. \nOr use From Position parameter.")]
		public FsmOwnerDefault fromGameObject;

		[Tooltip("Start ray at a vector3 world position. \nOr use Game Object parameter.")]
		public FsmVector3 fromPosition;

		[Tooltip("A vector3 direction vector")]
		public FsmVector3 direction;

		[Tooltip("Cast the ray in world or local space. Note if no Game Object is specfied, the direction is in world space.")]
		public Space space;

		[Tooltip("The length of the ray. Set to -1 for infinity.")]
		public FsmFloat distance;

		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;
		
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;
		
		[Tooltip("If set to true, will reset the loop and perform a new raycast, useful whne exiting the loop before the end")]
		public FsmBool resetAction;
		

		[ActionSection("RayCast Debug")] 
		
		[Tooltip("The color to use for the debug line.")]
		public FsmColor debugColor;

		[Tooltip("Draw a debug line. Note: Check Gizmos in the Game View to see it in game.")]
		public FsmBool debug;
		
		[ActionSection("Hit")] 
		
		
		[RequiredField]
		[Tooltip("Event to send to get the next child.")]
		public FsmEvent loopEvent;

		[Tooltip("Event to send if there is no hit at all")]
		public FsmEvent noHitEvent;
		
		[RequiredField]
		[Tooltip("Event to send when there are no more hits to loop.")]
		public FsmEvent finishedEvent;

		public override void Reset()
		{
			
			fromGameObject = null;
			fromPosition = new FsmVector3 { UseVariable = true };
			direction = new FsmVector3 { UseVariable = true };
			space = Space.Self;
			distance = 100;
			
			resetAction = false;
			
			layerMask = new FsmInt[0];
			invertMask = false;
			debugColor = Color.yellow;
			debug = false;
			
		
			
			loopEvent = null;
			finishedEvent = null;
			noHitEvent = null;
		}
		
		// cache the hits
		private RaycastHit[] hits;
		
		// increment a hit index as we loop through the hits
		private int nextHitIndex;
		
		
		public override void OnEnter()
		{
			if (resetAction.Value)
			{
				nextHitIndex = 0;
				resetAction.Value = false;
			}
			
			if (nextHitIndex==0)
			{
				DoRaycastAll();
			}
			
			if (hits.Length==0)
			{
				nextHitIndex = 0;
				Fsm.Event(noHitEvent);
				Fsm.Event(finishedEvent);
				Finish();
				return;
			}
			
			if (nextHitIndex>=hits.Length)
			{
				nextHitIndex = 0;
				Fsm.Event(finishedEvent);
				Finish();
				return;
			}
			
		//	Debug.Log("getting index"+nextHitIndex );
			Fsm.RaycastHitInfo = hits[nextHitIndex];
			
			nextHitIndex++;
			
			Fsm.Event(loopEvent);
			
			Finish();
		}
		
		
		void DoRaycastAll()
		{
			//Debug.Log("DoRaycastAll");
			
			if (distance.Value == 0)
			{
				return;
			}

			var go = Fsm.GetOwnerDefaultTarget(fromGameObject);
			
			var originPos = go != null ? go.transform.position : fromPosition.Value;
			
			var rayLength = Mathf.Infinity;
			if (distance.Value > 0 )
			{
				rayLength = distance.Value;
			}

			var dirVector = direction.Value;
			if(go != null && space == Space.Self)
			{
				dirVector = go.transform.TransformDirection(direction.Value);
			}
			
			
			hits = Physics.RaycastAll(originPos, dirVector, rayLength, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value)).OrderBy(h=>h.distance).ToArray();
			
			if (debug.Value)
			{
				var debugRayLength = Mathf.Min(rayLength, 1000);
				Debug.DrawLine(originPos, originPos + dirVector * debugRayLength, debugColor.Value);
			}
			
			if (hits.Length==0)
			{
				Fsm.Event(noHitEvent);
			}
			
		}
		
		
	}
}
