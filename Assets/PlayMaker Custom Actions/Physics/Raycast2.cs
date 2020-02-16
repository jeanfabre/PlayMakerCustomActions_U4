// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Casts a Ray against all Colliders in the scene. Use either a Game Object or Vector3 world position as the origin of the ray. Use GetRaycastInfo to get more detailed info.")]
	public class Raycast2 : FsmStateAction
	{
		[Tooltip("Start ray at game object position. \nOr use From Position parameter.")]
		public FsmOwnerDefault fromGameObject;

		[Tooltip("Start ray at a vector3 world position. \nOr use Game Object parameter.")]
		public FsmVector3 fromPosition;

		[Tooltip("A vector3 direction vector")]
		public FsmVector3 direction;

		[Tooltip("A Target Object for the direction. Overrides Direction if used.")]
		public FsmGameObject endObject;

		[Tooltip("Cast the ray in world or local space. Note if no Game Object is specfied, the direction is in world space.")]
		public Space space;

		[Tooltip("The length of the ray. Set to -1 for infinity.")]
		public FsmFloat distance;

        [ActionSection("Result")] 

		[Tooltip("Event to send if the ray hits an object.")]
		[UIHint(UIHint.Variable)]
		public FsmEvent hitEvent;

		[Tooltip("Event to send if the ray does not hit any object.")]
		[UIHint(UIHint.Variable)]
		public FsmEvent noHitEvent;

		[Tooltip("Set a bool variable to true if hit something, otherwise false.")]
		[UIHint(UIHint.Variable)]
		public FsmBool storeDidHit;

		[Tooltip("Store the game object hit in a variable.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeHitObject;

        [UIHint(UIHint.Variable)]
        [Tooltip("Get the world position of the ray hit point and store it in a variable.")]
        public FsmVector3 storeHitPoint;

        [UIHint(UIHint.Variable)]
        [Tooltip("Get the normal at the hit point and store it in a variable.")]
        public FsmVector3 storeHitNormal;

        [UIHint(UIHint.Variable)]
        [Tooltip("Get the distance along the ray to the hit point and store it in a variable.")]
        public FsmFloat storeHitDistance;

		[Tooltip("If true the script will store hitDistance regardless of whether or not the ray hit something.")]
		public FsmBool storeDistanceOnMiss;

		[Tooltip("If the ray doesn't hit anything, update hitPoint correctly.")]
		public FsmBool storeHitPointOnMiss;

        [ActionSection("Filter")] 

        [Tooltip("Set how often to cast a ray. 0 = once, don't repeat; 1 = everyFrame; 2 = every other frame... \nSince raycasts can get expensive use the highest repeat interval you can get away with.")]
        public FsmInt repeatInterval;

        [UIHint(UIHint.Layer)]
        [Tooltip("Pick only from these layers.")]
        public FsmInt[] layerMask;

        [Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
        public FsmBool invertMask;

		[ActionSection("Debug")] 
		
		[Tooltip("The color to use for the debug line.")]
		public FsmColor debugColor;

		[Tooltip("Draw a debug line. Note: Check Gizmos in the Game View to see it in game.")]
		public FsmBool debug;
		
		int repeat;
		
		public override void Reset()
		{
			fromGameObject = null;
			fromPosition = new FsmVector3 { UseVariable = true };
			direction = new FsmVector3 { UseVariable = true };
			endObject = new FsmGameObject { UseVariable = true };
			space = Space.Self;
			distance = 100;
			hitEvent = null;
			noHitEvent = null;
			storeDidHit = null;
			storeHitObject = null;
		    storeHitPoint = null;
		    storeHitNormal = null;
		    storeHitDistance = null;
			storeDistanceOnMiss = true;
			storeHitPointOnMiss = true;
			repeatInterval = 1;
			layerMask = new FsmInt[0];
			invertMask = false;
			debugColor = Color.yellow;
			debug = false;

		}

		public override void OnEnter()
		{
			DoRaycast();
			
			if (repeatInterval.Value == 0)
			{
				Finish();
			}		
		}

		public override void OnUpdate()
		{
			repeat--;
			
			if (repeat == 0)
			{
				DoRaycast();
			}
		}
		
		void DoRaycast()
		{
			repeat = repeatInterval.Value;

			if (distance.Value == 0)
			{
				return;
			}

			var goFr = Fsm.GetOwnerDefaultTarget(fromGameObject);
			var originPos = goFr != null ? goFr.transform.position : fromPosition.Value;
			var rayLength = Mathf.Infinity;
			var goTo = endObject.Value;
			var dirVector = direction.Value;
			var next = true;
			
			if (distance.Value > 0 )
			{
				rayLength = distance.Value;
			}

			if (goTo != null)
			{
				dirVector = goTo.transform.position - originPos;
				next = false;
			}

			if (goFr != null && space == Space.Self && next)
			{
				dirVector = goFr.transform.TransformDirection(direction.Value);
			}

			RaycastHit hitInfo;
			Physics.Raycast(originPos, dirVector, out hitInfo, rayLength, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value));
			
			Fsm.RaycastHitInfo = hitInfo;
			var didHit = hitInfo.collider != null;
			storeDidHit.Value = didHit;

			if (didHit)
			{
				storeHitObject.Value = hitInfo.collider.GetComponent<Collider>().gameObject;
                storeHitPoint.Value = Fsm.RaycastHitInfo.point;
				storeHitNormal.Value = Fsm.RaycastHitInfo.normal;
                storeHitDistance.Value = Fsm.RaycastHitInfo.distance;
				Fsm.Event(hitEvent);
			}

			if (storeHitPointOnMiss.Value && !didHit)
			{
				storeHitPoint.Value = originPos + dirVector * rayLength;
			}

			if (!didHit)
			{

				Fsm.Event(noHitEvent);
			}

			if (!didHit & storeDistanceOnMiss.Value)
			{
				storeHitDistance.Value = rayLength;
			}
			
			if (debug.Value && next)
			{
				var debugRayLength = Mathf.Min(rayLength, 1000);
				Debug.DrawLine(originPos, originPos + dirVector * debugRayLength, debugColor.Value);
			}

			if (debug.Value && !next)
			{
				var debugRayLength = Mathf.Min(rayLength, 1000);
				var d = (goTo.transform.position - originPos).normalized * debugRayLength + originPos;
				Debug.DrawLine(originPos, d, debugColor.Value);
			}
		}
	}
}

