// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Perform a Mouse Pick on the scene and stores the results. Use Ray Distance to set how close the camera must be to pick the object.")]
	public class CustomMousePick : FsmStateAction
	{
		
		[RequiredField]
		[Tooltip("The screen Position, same reference system as Input.MousePosition: The bottom-left of the screen or window is at (0, 0). The top-right of the screen or window is at (Screen.width, Screen.height).")]
		public FsmVector3 screenPosition;
		
		[RequiredField]
		public FsmFloat rayDistance = 100f;
		
		[Tooltip("The Camera from which to check. LEave empty to use main camera")]
		[CheckForComponent(typeof(Camera))]
		public FsmGameObject camera;
		
		

		[UIHint(UIHint.Variable)]
		public FsmBool storeDidPickObject;
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeGameObject;
		[UIHint(UIHint.Variable)]
		public FsmVector3 storePoint;
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeNormal;
		[UIHint(UIHint.Variable)]
		public FsmFloat storeDistance;
		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
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
			screenPosition = null;
			rayDistance = 100f;
			storeDidPickObject = null;
			storeGameObject = null;
			storePoint = null;
			storeNormal = null;
			storeDistance = null;
			layerMask = new FsmInt[0];
			invertMask = false;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoMousePick();
			
			if (!everyFrame)
				Finish();
		}
		
		public void DoMousePick() 
		{
			if (camera.Value != null) {
				cameraComp =  camera.Value.GetComponent<Camera>();
			}
			else 
			{
				cameraComp = Camera.main;
				if (cameraComp == null) 
				{
					return;
				}
			}
			
			
			if (cameraComp.gameObject.activeSelf == false)
			{
				return;
			}
			
			// find out if mouse is outside camera rect. This is both a bug and performance thing.
			if (!cameraComp.pixelRect.Contains(screenPosition.Value)) {
				// TODO: not sure if this is the right strategy...
				storeGameObject.Value = null;
				storeDistance.Value = Mathf.Infinity;
				storePoint.Value = Vector3.zero;
				storeNormal.Value = Vector3.zero;
				return;
			}
			
			Ray ray = cameraComp.ScreenPointToRay(Input.mousePosition);
			Physics.Raycast(ray,out hit,rayDistance.Value, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value));
			
			if(debugLine) 
			{
			Debug.DrawRay(ray.origin, ray.direction * rayDistance.Value, debugColor.Value);
			}
			
			bool didPick = hit.collider != null;
			storeDidPickObject.Value = didPick;
			
			if (didPick)
			{
				storeGameObject.Value = hit.collider.gameObject;
				storeDistance.Value = hit.distance;
				storePoint.Value = hit.point;
				storeNormal.Value = hit.normal;
			}
			else
			{
				// TODO: not sure if this is the right strategy...
				storeGameObject.Value = null;
				storeDistance.Value = Mathf.Infinity;
				storePoint.Value = Vector3.zero;
				storeNormal.Value = Vector3.zero;
			}
		}
		public override void OnUpdate()
		{
			DoMousePick();
		}
	}
}
		
		
		
		
