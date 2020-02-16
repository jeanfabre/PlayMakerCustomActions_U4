// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Sets the ortho size of the Camera.")]
	public class SetCameraOrthoSize : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Camera))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The ortho size.")]
		public FsmFloat orthoSize;
		
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			orthoSize = 4.5f;
			
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoSetCameraOrthoSize();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetCameraOrthoSize();
		}
		
		void DoSetCameraOrthoSize()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			
			Camera _camera = go.camera;
			if (_camera == null)
			{
				LogError("Missing Camera Component!");
				return;
			}

			_camera.orthographicSize = orthoSize.Value;
		}
	}
}
