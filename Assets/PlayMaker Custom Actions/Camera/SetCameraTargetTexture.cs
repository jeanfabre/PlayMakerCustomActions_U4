// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Sets the TargetTexture of a Camera.")]
	public class SetCameraTargetTexture : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(Camera))]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The renderTexture to set as target for that Camera.")]
		public FsmTexture renderTexture;

		private Camera _camera;
		
		public override void Reset()
		{
			gameObject = null;
			renderTexture = null;

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
			
			
			_camera.targetTexture = renderTexture.Value as RenderTexture;

			Finish();
		}


		public override string ErrorCheck()
		{

			if (renderTexture.Value !=null && !(renderTexture.Value is RenderTexture))
			{
				return "Only RenderTexture can be used as Camera TargetTexture";

			}


			return "";
		}

	}
}
