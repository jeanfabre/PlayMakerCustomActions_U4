// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Sets the Rect of a Camera.")]
	public class SetCameraRect : FsmStateAction
	{

		public enum RectCoordinates {Pixel,Screen,Viewport};


		[RequiredField]
		[CheckForComponent(typeof(Camera))]
		public FsmOwnerDefault gameObject;
		[RequiredField]
		public FsmRect rect;

		[Tooltip("Rect coordinates: Screen is relative and 0,0 is the top left of the screen. Viewport is relative and 0,0 is bottom left of the screen. Otherwise coordinates are in pixels.")]
		public RectCoordinates coordinates;

		public bool everyFrame;

		private Camera _camera;
		private Rect _rect;

		public override void Reset()
		{
			gameObject = null;
			rect = new FsmRect();
			rect.Value = new Rect(0f,0f,0.5f,0.5f);

			coordinates = RectCoordinates.Viewport;

			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			GameObject go = gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value;
			if (go == null)
			{
				LogError("gameObject is missing!");
				return;
			}

			
			_camera = go.camera;
			
			if (_camera == null)
			{
				LogError("Missing Camera Component!");
				return;
			}


			DoSetCameraRect();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoSetCameraRect();
		}
		
		void DoSetCameraRect()
		{
			if (_camera != null)
			{
				_rect = rect.Value;

				if (coordinates == RectCoordinates.Screen )
				{
					_rect.y = 1 - _rect.y;
				}else if (coordinates == RectCoordinates.Viewport )
				{
					_rect.x /=  Screen.width;
					_rect.y /=  Screen.height;
					_rect.width /=  Screen.width;
					_rect.height /=  Screen.height;

					_rect.y = 1 - _rect.y - _rect.height;
				}


				_camera.rect = _rect;
			}
		}
	}
}
