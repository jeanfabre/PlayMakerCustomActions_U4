// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Sends Events based on mouse interactions with a Game Object: MouseOver, MouseDown, MouseUp, MouseOff. Use Ray Distance to set how close the camera must be to pick the object.")]
	public class MousePickEventCustom : FsmStateAction
	{
		[Tooltip("The Camera from which to check. LEave empty to use main camera")]
		[CheckForComponent(typeof(Camera))]
		public FsmGameObject camera;
			
		[CheckForComponent(typeof(Collider))]
		public FsmOwnerDefault GameObject;
		
		[Tooltip("Length of the ray to cast from the camera.")]
		public FsmFloat rayDistance = 100f;

		[Tooltip("Event to send when the mouse is over the GameObject.")]
		public FsmEvent mouseOver;

		[Tooltip("Event to send when the mouse is pressed while over the GameObject.")]
		public FsmEvent mouseDown;

		[Tooltip("Event to send when the mouse is released while over the GameObject.")]
		public FsmEvent mouseUp;
		
		[Tooltip("Event to send when the mouse moves off the GameObject.")]
		public FsmEvent mouseOff;
		
		[Tooltip("Pick only from these layers.")]
		[UIHint(UIHint.Layer)]
		public FsmInt[] layerMask;
		
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;

		public bool debugLine;
		public FsmColor debugColor;
		public bool everyFrame;
		
		private RaycastHit hit;
		private Camera cameraComp;

		public override void Reset()
		{
			GameObject = null;
			rayDistance = 100f;
			mouseOver = null;
			mouseDown = null;
			mouseUp = null;
			mouseOff = null;
			layerMask = new FsmInt[0];
			invertMask = false;
			everyFrame = true;
		}
		
		public override void OnEnter()
		{
			DoMousePickEvent();

			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoMousePickEvent();
		}

		void DoMousePickEvent()
		{
			// Do the raycast

			bool isMouseOver = DoRaycast();

			// Store mouse pick info so it can be seen by Get Raycast Hit Info action

			Fsm.RaycastHitInfo = hit;
			
			// Send events based on the raycast and mouse buttons

			if (isMouseOver)
			{
				if (mouseDown != null && Input.GetMouseButtonDown(0))
				{
					Fsm.Event(mouseDown);
				}

				if (mouseOver != null)
				{
					Fsm.Event(mouseOver);
				}

				if (mouseUp != null &&Input.GetMouseButtonUp(0))
				{
					Fsm.Event(mouseUp);
				}
			}
			else
			{
				if (mouseOff != null)
				{
					Fsm.Event(mouseOff);
				}
			}
		}

		bool DoRaycast()
		{

			if (camera.Value != null) {
				cameraComp =  camera.Value.GetComponent<Camera>();
			}
			else 
			{
				cameraComp = Camera.main;
				if (cameraComp == null) 
				{
					return false;
				}
			}
			
			var testObject = GameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : GameObject.GameObject.Value;
			
			if (cameraComp.gameObject.activeSelf == false)
			{
				return false;
			}
			// find out if mouse is outside camera rect. This is both a bug and performance thing.
			if (!cameraComp.pixelRect.Contains(Input.mousePosition)) {
				return false;
			}
			Ray ray = cameraComp.ScreenPointToRay(Input.mousePosition);
			Physics.Raycast(ray,out hit,rayDistance.Value, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value));
			
			if(debugLine) 
			{
			Debug.DrawRay(ray.origin, ray.direction * rayDistance.Value, debugColor.Value);
			}
			
			if(hit.collider.gameObject == testObject){return true;}
			else{
				return false;
			}
		}

		public override string ErrorCheck()
		{
			string errorString = "";

			errorString += ActionHelpers.CheckRayDistance(rayDistance.Value);
			errorString += ActionHelpers.CheckPhysicsSetup(GameObject);

			return errorString;
		}
	}
}
