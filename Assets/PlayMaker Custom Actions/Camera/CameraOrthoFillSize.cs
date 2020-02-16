// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Fill an Aspect Ratio inside the screen. Cropping may occur, use CameraOrthoFitSize if you don't want any cropping")]
	public class CameraOrthoFillSize : FsmStateAction
	{
		[Tooltip("The Camera to control. Leave to none to use the MainCamera")]
		[CheckForComponent(typeof(Camera))]
		public FsmOwnerDefault camera;
		
		[Tooltip("The width to fill")]
		public FsmFloat targetWidth;

		[Tooltip("The height to fill")]
		public FsmFloat targetHeight;

		public bool everyFrame;

		GameObject _go;
		Camera _camera;

		public override void Reset()
		{
			camera = new FsmOwnerDefault();
			camera.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			camera.GameObject = new FsmGameObject(){UseVariable=true};

			targetWidth = null;
			targetHeight = null;

			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoFill();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoFill();
		}
		
		void DoFill()
		{
			_go = Fsm.GetOwnerDefaultTarget(camera);

			if (_go!=null)
			{
				_camera = _go.GetComponent<Camera>();
			}

			if (_camera == null)
			{
				_camera = Camera.main;
			}

			float targetAspect = targetWidth.Value / targetHeight.Value;

			float windowAspect = (float)Screen.width / (float)Screen.height;

			float heightToBeSeen = windowAspect<targetAspect?targetHeight.Value:targetWidth.Value/windowAspect;

		//	UnityEngine.Debug.Log("windowAspect: "+windowAspect+" targetAspect : "+targetAspect+" HeightTobeSeen"+heightToBeSeen);

			_camera.orthographicSize = 	heightToBeSeen * 0.5f;
		}
	}
}
