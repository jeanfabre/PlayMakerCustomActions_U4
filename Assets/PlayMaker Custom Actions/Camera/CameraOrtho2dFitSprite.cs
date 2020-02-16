// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Fit a sprite inside the screen assuming the camera is in a 2d setup. No Cropping will occur, use CameraOrthoFillSprite to crop and fill screen")]
	public class CameraOrtho2dFitSprite : FsmStateAction
	{

		[CheckForComponent(typeof(Camera))]
		[Tooltip("The Camera to control. Leave to none to use the MainCamera")]
		public FsmOwnerDefault camera;
		
		[Tooltip("The sprite to fit")]
		[RequiredField]
		[ObjectType(typeof(SpriteRenderer))]
		public FsmGameObject targetSprite;

		
		public bool everyFrame;


		GameObject _go;
		Camera _camera;

		GameObject _targetGo;
		SpriteRenderer _spriteRenderer;

		
		public override void Reset()
		{
			camera = new FsmOwnerDefault();
			camera.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			camera.GameObject = new FsmGameObject(){UseVariable=true};

			targetSprite = null;
			
			everyFrame = false;
		}
		
		public override void OnEnter()
		{

			DoFit();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoFit();
		}
		
		void DoFit()
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
			
			_targetGo = targetSprite.Value;
			
			if (_targetGo==null)
			{
				LogError("Missing Target GameObject!");
				return;
			}

			_spriteRenderer = _targetGo.GetComponent<SpriteRenderer>();

			if (_spriteRenderer==null)
			{
				LogError("Missing SpriteRenderer on targetSprite!");
				return;
			}


			float targetAspect = _spriteRenderer.bounds.size.x / _spriteRenderer.bounds.size.y;
			
			float windowAspect = (float)Screen.width / (float)Screen.height;
			
			// greater, we fit the height, if it's less we fit the width;
			float heightToBeSeen = windowAspect>targetAspect?_spriteRenderer.bounds.size.y:_spriteRenderer.bounds.size.x/windowAspect;
			
			//	UnityEngine.Debug.Log("windowAspect: "+windowAspect+" targetAspect : "+targetAspect+" HeightTobeSeen"+heightToBeSeen);
			
			_camera.orthographicSize = 	heightToBeSeen * 0.5f;
		}
	}
}
