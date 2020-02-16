// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System.Linq;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Performs a rayCast hit and Each time this action is called it gets the next hit. This lets you quickly loop through all the hits to perform actions on them.")]
	public class GetNextRayCastAllHitFromScreen : FsmStateAction
	{
		
		[ActionSection("Raycast Settings")] 
		
		[Tooltip("The Camera GameObject. Leave to none to use the main camera")]
		[CheckForComponent(typeof(Camera))]
		public FsmOwnerDefault camera;
		
		[Tooltip("Start ray at a vector3 screen position. Screenspace is defined in pixels. The bottom-left of the screen is (0,0); the right-top is (pixelWidth,pixelHeight). ")]
		public FsmVector3 fromScreenPosition;

		[Tooltip("The length of the ray. Set to -1 for infinity.")]
		public FsmFloat distance;

		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;
		
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;
		
		[Tooltip("If set to true, will reset the loop and perform a new raycast, useful whne exiting the loop before the end")]
		public FsmBool resetAction;
		
		[ActionSection("Hit")] 
		
		[RequiredField]
		[Tooltip("Event to send to get the next child.")]
		public FsmEvent loopEvent;

		[Tooltip("Event to send if there is no hit at all")]
		public FsmEvent noHitEvent;
		
		[RequiredField]
		[Tooltip("Event to send when there are no more hits to loop.")]
		public FsmEvent finishedEvent;
		
		private Camera _cam;
		
		public override void Reset()
		{
			
			camera =  new FsmOwnerDefault() { OwnerOption = OwnerDefaultOption.SpecifyGameObject};
			camera.GameObject.UseVariable = true;
			
			fromScreenPosition = new FsmVector3 { UseVariable = true };
			distance = 100;
			
			resetAction = false;
			
			layerMask = new FsmInt[0];
			invertMask = false;
					
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
			
			Debug.Log("getting index"+nextHitIndex );
			Fsm.RaycastHitInfo = hits[nextHitIndex];
			
			nextHitIndex++;
			
			Fsm.Event(loopEvent);
			
			Finish();
		}
		
		
		void DoRaycastAll()
		{
			Debug.Log("DoRaycastAll");
			
			if (distance.Value == 0)
			{
				return;
			}

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
			
			var screenPos = fromScreenPosition.Value;
			
			var rayLength = Mathf.Infinity;
			if (distance.Value > 0 )
			{
				rayLength = distance.Value;
			}

			
			Ray ray  = _cam.ScreenPointToRay(screenPos);
			hits = Physics.RaycastAll(ray, rayLength, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value)).OrderBy(h=>h.distance).ToArray();
			
			if (hits.Length==0)
			{
				Fsm.Event(noHitEvent);
			}
			
		}
		
		
	}
}
