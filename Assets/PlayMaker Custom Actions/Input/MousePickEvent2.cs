// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Sends Events based on mouse interactions with a Game Object: MouseOver, MouseDown, MouseUp, MouseOff. Use Ray Distance to set how close the camera must be to pick the object. Button id can be set as well: 0 for left click ( default), 1 for right click, 2 for middle click")]
	public class MousePickEvent2 : FsmStateAction
	{
		[CheckForComponent(typeof(Collider))]
		public FsmOwnerDefault GameObject;
		
		[Tooltip("Length of the ray to cast from the camera.")]
		public FsmFloat rayDistance;
		
		[Tooltip("The button id. 0 by default, 1 for right click, 2 for middle click")]
		public FsmInt buttonId;
		
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

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			GameObject = null;
			rayDistance = 100f;
			buttonId = 0;
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

			Fsm.RaycastHitInfo = ActionHelpers.mousePickInfo;
			
			// Send events based on the raycast and mouse buttons

			if (isMouseOver)
			{
				if (mouseDown != null && Input.GetMouseButtonDown(buttonId.Value))
				{
					Fsm.Event(mouseDown);
				}

				if (mouseOver != null)
				{
					Fsm.Event(mouseOver);
				}

				if (mouseUp != null &&Input.GetMouseButtonUp(buttonId.Value))
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
			var testObject = GameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : GameObject.GameObject.Value;
			
			// ActionHelpers uses a cache to try and minimize Raycasts

			return ActionHelpers.IsMouseOver(testObject, rayDistance.Value, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value));
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
