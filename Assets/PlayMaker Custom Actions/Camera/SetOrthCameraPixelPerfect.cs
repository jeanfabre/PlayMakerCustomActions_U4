// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// This Action requires ArrayMaker: https://hutonggames.fogbugz.com/default.asp?W715
// original action http://hutonggames.com/playmakerforum/index.php?topic=10261.msg48444#msg48444

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Set the Orthographic size of the Camera to be pixel perfect to sprite")]
	public class SetOrthCameraPixelPerfect : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Camera))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Height of Sprite Square pixel (32x32, 64x64, etc)")]
		[UIHint(UIHint.FsmInt)]
		public FsmInt spriteSize;
		[Tooltip("Scale the pixel - 1 Sprite unit pixel = 1x1, 2x2 or 3x3, etc, screen unit pixels. 0 = disable")]
		[TitleAttribute("Pixel Unit Scale")]
		public FsmInt pixelScale;


		[ActionSection("Result feedback")]
		
		[UIHint(UIHint.Description)]
		public string CameraSize;
		public FsmFloat sizeResult;
		private float temp;
		[ActionSection("")]
		[TitleAttribute("See Formula Debug")]
		public bool seeFormula;

		public override void Reset()
		{
			gameObject = null;
			pixelScale= 0;
			spriteSize=0;
			seeFormula = false;

		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			
			Camera _camera = go.camera;
			if (_camera == null)
			{
				LogError("Missing Camera Component!");
				return;
			}

			if (!_camera.isOrthoGraphic)
			{
				LogError("Wrong Camera Component - Please set camera to Orthographic!");
				return;
			}

			DoSetCameraOrthoSize();

			Finish();
		}

		
		void DoSetCameraOrthoSize()
		{
			var gos = Fsm.GetOwnerDefaultTarget(gameObject);
			Camera _camera = gos.camera;


			if (pixelScale.Value !=0)
				temp = ((Screen.height/spriteSize.Value)/2f/pixelScale.Value);
			else
				temp = ((Screen.height/spriteSize.Value)/2f);

			_camera.orthographicSize = (sizeResult.Value = temp);

			if (seeFormula)
				CameraSize = sizeResult.Value.ToString()+"= (("+Screen.height.ToString()+"/"+spriteSize.Value.ToString()+")/2f/"+pixelScale.Value.ToString()+")";

			Finish();
		}

	}
}