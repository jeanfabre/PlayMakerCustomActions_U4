// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Casts a Ray from a GameObject against all Colliders in the scene. Use GetRaycastInfo to get more detailed info.")]
	public class RaycastFromGameObject : FsmStateAction
	{
		
		public enum Direction
		{
			Forward,
			Backward,
			Up,
			Down,
			Left,
			Right
		}
		
		[Tooltip("Start ray at game object position")]
		public FsmOwnerDefault fromGameObject;
		
		[Tooltip("A direction: Forward (z), Backward (-z), up (y), Down (-y), Left (-x), Right (x) ")]
		public Direction direction;
		
		[Tooltip("The length of the ray. Set to -1 for infinity.")]
		public FsmFloat distance;
		
		[Tooltip("Event to send if the ray hits an object.")]
		[UIHint(UIHint.Variable)]
		public FsmEvent hitEvent;
		
		[Tooltip("Set a bool variable to true if hit something, otherwise false.")]
		[UIHint(UIHint.Variable)]
		public FsmBool storeDidHit;
		
		[Tooltip("Store the game object hit in a variable.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeHitObject;
		
		[Tooltip("Set how often to cast a ray. 0 = once, don't repeat; 1 = everyFrame; 2 = every other frame... \nSince raycasts can get expensive use the highest repeat interval you can get away with.")]
		public FsmInt repeatInterval;
		
		[Tooltip("Pick only from these layers.")]
		[UIHint(UIHint.Layer)]
		public FsmInt[] layerMask;
		
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;
		
		public FsmBool debug;
		
		private GameObject go;
		
		int repeat;
		
		public override void Reset()
		{
			fromGameObject = null;
			direction = Direction.Forward;
			distance = 100;
			hitEvent = null;
			storeDidHit = null;
			storeHitObject = null;
			repeatInterval = 5;
			layerMask = new FsmInt[0];
			invertMask = false;		
			debug = false;
		}

		public override void OnEnter()
		{
			DoRaycast();
			
			if (repeatInterval.Value == 0)
				Finish();		
		}

		public override void OnUpdate()
		{
			repeat--;
			
			if (repeat == 0)
				DoRaycast();
		}
		
		void DoRaycast()
		{
			repeat = repeatInterval.Value;

			if (distance.Value == 0)
				return;
			
			Vector3 originPos;
			
			go = Fsm.GetOwnerDefaultTarget(fromGameObject);
			
			originPos = go.transform.position;

			
			float rayLength = Mathf.Infinity;
			if (distance.Value > 0 )
				rayLength = distance.Value;
			
			RaycastHit hitInfo;
			
			Vector3 dir = GetDirectionVector();
			
			Physics.Raycast(originPos, dir, out hitInfo, rayLength, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value)); //TODO LayerMask support

			Fsm.RaycastHitInfo = hitInfo;
			
			bool didHit = hitInfo.collider != null;
			
			storeDidHit.Value = didHit;
			
			if (didHit)
			{
				Fsm.Event(hitEvent);
				storeHitObject.Value = hitInfo.collider.collider.gameObject;
			}
			
			if (debug.Value)
			{
				float debugRayLength = Mathf.Min(rayLength, 1000);
				Debug.DrawLine(originPos, originPos + dir * debugRayLength, Fsm.DebugRaycastColor);
			}
		}
		
		
		Vector3 GetDirectionVector()
		{
			Vector3 dir = go.transform.forward; // by default
				
			switch(direction){
			case Direction.Backward:
				dir = - go.transform.forward;
				break;
			case Direction.Up:
				dir = go.transform.up;
				break;
			case Direction.Down:
				dir = -go.transform.up;
				break;
			case Direction.Left:
				dir = - go.transform.right;
				break;
			case Direction.Right:
				dir = go.transform.right;
				break;
			}
			
			return dir;
		}
	}
}

