// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Performs a sphereCast hit")]
	public class SphereCast : FsmStateAction
	{
		
		[ActionSection("Spherecast Settings")] 
		
		[Tooltip("The center of the sphere at the start of the sweep. \nOr use From Position parameter.")]
		public FsmOwnerDefault fromGameObject;

		[Tooltip("The center of the sphere at the start of the sweep. \nOr use Game Object parameter.")]
		public FsmVector3 fromPosition;
		
		[Tooltip("The radius of the shpere.")]
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

		[ActionSection("RayCast Debug")] 
		
		[Tooltip("The color to use for the debug line.")]
		public FsmColor debugColor;

		[Tooltip("Draw a debug line. Note: Check Gizmos in the Game View to see it in game.")]
		public FsmBool debug;
		
		[ActionSection("Hit")] 
		
		[Tooltip("Set a bool variable to true if hit something, otherwise false.")]
		[UIHint(UIHint.Variable)]
		public FsmBool storeDidHit;
			
		[Tooltip("Event to send when there is a hit.")]
		public FsmEvent hitEvent;

		[Tooltip("Event to send if there is no hit at all")]
		public FsmEvent noHitEvent;
		

		public override void Reset()
		{
			
			fromGameObject = null;
			fromPosition = new FsmVector3 { UseVariable = true };
			direction = Vector3.forward;
			radius = 1f;
			space = Space.Self;
			distance = 100;
			
			
			layerMask = new FsmInt[0];
			invertMask = false;
			debugColor = Color.yellow;
			debug = false;
			
			
			hitEvent = null;
			noHitEvent = null;
		}

		public override void OnEnter()
		{
		
			if (! DoSphereCast() )
			{
				Fsm.Event(noHitEvent);
			}else{
				Fsm.Event(hitEvent);
			}
			
			Finish();
		}
		
		
		bool DoSphereCast()
		{
			if (distance.Value == 0)
			{
				return false;
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
			
			if (debug.Value)
			{
				var debugRayLength = Mathf.Min(rayLength, 1000);
				Debug.DrawLine(originPos, originPos + dirVector * debugRayLength, debugColor.Value);
			}
			
			RaycastHit hitInfo;
			Physics.SphereCast(originPos,radius.Value, dirVector,out hitInfo, rayLength, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value));	
			Fsm.RaycastHitInfo = hitInfo;
			
			var didHit = hitInfo.collider != null;
			
			storeDidHit.Value = didHit;
			
			return didHit;
		}
		
		
	}
}
