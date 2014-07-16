// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
//
// 1.8.0b12 BETA ACTION
//
// Requires FsmArrays in Playmaker 1.8.0+ and therefore the minimum Unity version for Playmaker.
// LF

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Performs a sphereCast hit")]
	public class SphereCastAll : FsmStateAction
	{
					[ActionSection("Spherecast Settings")] 
		
		[Tooltip("The center of the sphere at the start of the sweep. \nOr use From Position parameter.")]
		public FsmOwnerDefault fromGameObject;

		[Tooltip("The center of the sphere at the start of the sweep. \nOr use Game Object parameter.")]
		public FsmVector3 fromPosition;
		
		[Tooltip("The radius of the sphere.")]
		public FsmFloat radius;

		[Tooltip("The direction into which to sweep the sphere.")]
		public FsmVector3 direction;

		[Tooltip("Cast the sphere in world or local space. Note if no Game Object is specified, the direction is in world space.")]
		public Space space;

		[Tooltip("The length of the ray. Set to -1 for infinity.")]
		public FsmFloat distance;

		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;
		
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;

		[Tooltip("The max array size of objects hit at one time.")]
		public FsmInt clampSize;

					[ActionSection("RayCast Debug")] 
		
		[Tooltip("The color to use for the debug line.")]
		public FsmColor debugColor;

		[Tooltip("Draw a debug line. Note: Check Gizmos in the Game View to see it in game.")]
		public FsmBool debug;
		
					[ActionSection("Hit")] 
		
		[Tooltip("Set a bool variable to true if hit something, otherwise false.")]
		[UIHint(UIHint.Variable)]
		public FsmBool storeDidHit;

		[Tooltip("Store the game object hit in a variable.")]
		[UIHint(UIHint.Variable)]
		public FsmArray storeHitObject;
			
		[Tooltip("Event to send when there is a hit.")]
		public FsmEvent hitEvent;

		[Tooltip("Event to send if there is no hit at all")]
		public FsmEvent noHitEvent;

		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		private bool didHit;
		private int[] mask;

		public override void Reset()
		{
			
			fromGameObject = null;
			fromPosition = new FsmVector3 { UseVariable = true };
			direction = Vector3.forward;
			radius = 1f;
			space = Space.Self;
			distance = 100;
			storeHitObject = null;
			everyFrame = false;
			
			
			layerMask = new FsmInt[0];
			invertMask = false;
			debugColor = Color.yellow;
			debug = false;
			
			
			hitEvent = null;
			noHitEvent = null;
		}

		/* TODO layermask setup OnEnter
		public override void OnEnter()
		{
			// convert the layer mask, this used to be in the cast directly but 
			// makes more sense here because it doesn't need to be refigured each frame
			// although I can't seem to get it to work.
			//
			// mask = (ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value));
		}
		*/

		public override void OnUpdate()
		{
			DoSphereCast();

			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public void DoSphereCast()
		{
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
			if (go != null && space == Space.Self)
			{
				dirVector = go.transform.TransformDirection(direction.Value);

			}

			// I think this debug is more useful that the previous
			/* TODO
			 * set the debug length and position to reflect the end of the actual ray
			 * instead of passing through objects.
			 * 
			 */
			if (debug.Value)
			{
				var debugRayLength = Mathf.Min(rayLength, 1000);
				var endPos = (originPos + dirVector * debugRayLength);

				// debug line start to finish
				Debug.DrawLine(originPos, endPos, debugColor.Value);

				//debug lines indicating spherecast radius
				Debug.DrawLine
					(endPos + ((go.transform.rotation * Vector3.up) * radius.Value),
					 endPos + ((go.transform.rotation * Vector3.down) * radius.Value),
					 debugColor.Value);

				Debug.DrawLine
					(endPos + ((go.transform.rotation * Vector3.left) * radius.Value),
					 endPos + ((go.transform.rotation * Vector3.right) * radius.Value),
					 debugColor.Value);
			}

			// do the actual raycast
			RaycastHit[] hitInfo = Physics.SphereCastAll (originPos, radius.Value, dirVector, rayLength);

			// I've got two bools maintained here for the "Did Hit Something" status, an internal and public user var.
			// wasn't sure if the user choosing not to use it would screw with the public variable's value (null maybe?).
			if (hitInfo.Length != 0)
			{
				didHit = true;
				storeDidHit = true;

			}

			if (hitInfo.Length == 0)
			{
				didHit = false;
				storeDidHit.Value = false;

				// since we don't go into the if (didHit) below, we should
				// reset the array size to clean up.
				storeHitObject.Resize (0);
				Fsm.Event(noHitEvent);
			}

			if (didHit)
			{
				// resize the user's array to match the ray hit number
				storeHitObject.Resize (hitInfo.Length);

				// iterate through the hits, clone to the FsmArray
				for (int i = 0; i < hitInfo.Length; i++)
				{
					storeHitObject.Set(i, hitInfo[i].collider.gameObject);
				}

				// send the hit event (if there is one)
				Fsm.Event(hitEvent);
			}
		}
	}
}