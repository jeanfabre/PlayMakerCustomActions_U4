// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Sends Events based on keyboard stroke and current mouse position interactions with a Game Object. Use Ray Distance to set how close the camera must be to pick the object.")]
	public class KeyboardPickEvent : FsmStateAction
	{
		[CheckForComponent(typeof(Collider))]
		public FsmOwnerDefault GameObject;
		
		[Tooltip("Length of the ray to cast from the camera.")]
		public FsmFloat rayDistance;
		
		[RequiredField]
		[Tooltip("The key to test.")]
		public KeyCode key;
		
		[Tooltip("Event to send when the mouse is over the GameObject.")]
		public FsmEvent mouseOver;

		[Tooltip("Event to send when the key is pressed while mouse over the GameObject.")]
		public FsmEvent keyDown;

		[Tooltip("Event to send when the key is released while over the GameObject.")]
		public FsmEvent keyUp;
		
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
			key = KeyCode.Space;
			mouseOver = null;
			keyDown = null;
			keyUp = null;
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
				if (keyDown != null && Input.GetKeyDown(key))
				{
					Fsm.Event(keyDown);
				}

				if (mouseOver != null)
				{
					Fsm.Event(mouseOver);
				}

				if (keyUp != null &&Input.GetKeyUp(key))
				{
					Fsm.Event(keyUp);
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
