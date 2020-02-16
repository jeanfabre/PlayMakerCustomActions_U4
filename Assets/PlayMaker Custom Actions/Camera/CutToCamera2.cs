// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Activates a Camera in the scene. Use GameObject reference instead of camera reference")]
	public class CutToCamera2 : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Camera))]
		public FsmOwnerDefault camera;
		
		public bool makeMainCamera;
		public bool cutBackOnExit;
		
		GameObject _go;
		Camera oldCamera;

		public override void Reset()
		{
			camera = null;
			makeMainCamera = true;
			cutBackOnExit = false;
		}

		public override void OnEnter()
		{
			_go = Fsm.GetOwnerDefaultTarget(camera);
			
			if (_go == null)
			{
				LogError("Missing gameObject!");
				return;
			}
			
			if (_go.camera == null)
			{
				LogError("Missing camera!");
				return;
			}

			oldCamera = Camera.main;

			SwitchCamera(Camera.main, _go.camera);

			if (makeMainCamera)
				_go.camera.tag = "MainCamera";
			
			Finish();
		}

		public override void OnExit()
		{
			if (cutBackOnExit)
			{
				SwitchCamera(_go.camera, oldCamera);
			}
		}

		static void SwitchCamera(Camera camera1, Camera camera2)
		{
			if (camera1 != null)
			{
				camera1.enabled = false;
			}

			if (camera2 != null)
			{
				camera2.enabled = true;
			}
		}
	}
}
