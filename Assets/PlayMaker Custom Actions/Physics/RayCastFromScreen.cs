// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Casts a Ray against all Colliders in the scene from the screenSpace. Use a Vector3 screen position as the origin of the ray. Use GetRaycastInfo to get more detailed info.")]
	public class RaycastFromScreen : FsmStateAction
	{
		[Tooltip("The Camera GameObject. Leave to none to use the main camera")]
		[CheckForComponent(typeof(Camera))]
		public FsmOwnerDefault camera;
		
		[Tooltip("Start ray at a vector3 screen position. Screenspace is defined in pixels. The bottom-left of the screen is (0,0); the right-top is (pixelWidth,pixelHeight). ")]
		public FsmVector3 fromScreenPosition;

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

		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;
		
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;
		
		int repeat;
		
		private Camera _cam;
		
		public override void Reset()
		{
		
			camera =  new FsmOwnerDefault() { OwnerOption = OwnerDefaultOption.SpecifyGameObject};
			camera.GameObject.UseVariable = true;
			
			fromScreenPosition = new FsmVector3 { UseVariable = true };
			distance = 100;
			hitEvent = null;
			storeDidHit = null;
			storeHitObject = null;
			repeatInterval = 1;
			layerMask = new FsmInt[0];
			invertMask = false;
		}

		public override void OnEnter()
		{
			
			
			var go = Fsm.GetOwnerDefaultTarget(camera);
			if (go == null)
			{
				_cam = Camera.main;
			}else{

				Camera _camera = go.camera;
				if (_camera == null)
				{
					LogError("Missing Camera Component!");
					Finish();
					return;
				}else{
					_cam = _camera;
				}
			}
			
			DoRaycastFromScreen();
			
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
				DoRaycastFromScreen();
			}
		}
		
		void DoRaycastFromScreen()
		{
			repeat = repeatInterval.Value;

			if (distance.Value == 0)
			{
				return;
			}


			var screenPos = fromScreenPosition.Value;
			
			var rayLength = Mathf.Infinity;
			if (distance.Value > 0 )
			{
				rayLength = distance.Value;
			}

			
			RaycastHit hitInfo;
		
			Ray ray  = _cam.ScreenPointToRay(screenPos);
			Physics.Raycast(ray,out hitInfo, rayLength, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value));
			
			Fsm.RaycastHitInfo = hitInfo;
			
			bool didHit = hitInfo.collider != null;
			
			storeDidHit.Value = didHit;
			
			if (didHit)
			{
				storeHitObject.Value = hitInfo.collider.gameObject;
				Fsm.Event(hitEvent);
			}
		}
	}
}

