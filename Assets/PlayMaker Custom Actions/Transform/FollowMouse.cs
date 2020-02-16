// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// Original action by Amaranth from the PlayMaker forum
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Have an object follow the mouse.")]
	public class FollowMouse : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to Follow the mouse")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Show the object a specific distance on the X axis, away from the mouse.")]
		public FsmFloat offsetX;
		
		[Tooltip("Show the object a specific distance on the X axis, away from the mouse.")]
		public FsmFloat offsetY;
		
		[Tooltip("Select this if you don't want the object to move off screen with the mouse.")]
		public FsmBool keepOnScreen;
		
		[Tooltip("This determines how close to the edge of the screen the object can get IF Keep On Screen is used.")]
		public FsmFloat screenOffset;
		
		[Tooltip("The distance to the camera")]
		public FsmFloat distanceToCamera;
		

		private float screenHeight;
		private float screenWidth;

			
		public override void Reset()
		{
			gameObject = null;
			offsetX = null;
			offsetY = null;
			keepOnScreen = true;
			screenOffset = null;
			distanceToCamera = 4f;
			
		}

		public override void OnEnter()
		{

			GetScreenSize();
			DoSetPosition();		
		}

		public override void OnUpdate()
		{
			DoSetPosition();
		}
		
		void GetScreenSize()
		{
			screenHeight = Screen.height;
			screenWidth = Screen.width;
		}
		
		void DoSetPosition()
		{
			if (Camera.main == null)
			{
				LogError("No MainCamera defined!");
				Finish();
				return;
			}			
			
			
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}	
			
			// get screen position
			var position = Vector3.zero;
			position.x = Input.mousePosition.x + offsetX.Value;
			position.y = Input.mousePosition.y + offsetY.Value;	
			
			// if you want to keep the object on screen, this keeps the objects inside the screen
			if (keepOnScreen.Value)
			{	
				var xleft = 0f + screenOffset.Value;
				var xright = screenWidth - screenOffset.Value;
				var ytop = 0f + screenOffset.Value;
				var ybottom = screenHeight - screenOffset.Value;
				
				if (position.x < xleft) position.x = xleft;
				
				if (position.x > xright) position.x = xright;
	
				if (position.y < ytop) position.y = ytop;
				
				if (position.y > ybottom) position.y = ybottom;			
			}
			

			position.z = distanceToCamera.Value;

			// move game object to world position
			go.transform.position = Camera.main.ScreenToWorldPoint(position);
		}		

	}
	
	
	
}
