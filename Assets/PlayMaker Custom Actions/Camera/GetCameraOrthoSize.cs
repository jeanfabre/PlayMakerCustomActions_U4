// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Gets the ortho size of the Camera.")]
	public class GetCameraOrthoSize : FsmStateAction
	{

		[CheckForComponent(typeof(Camera))]
		[Tooltip("The GameObject with a Camera Component. If not defined will use the Main Camera")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("The ortho size.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat orthoSize;

		Camera _camera;

		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			orthoSize = null;
			
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoGetCameraOrthoSize();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoGetCameraOrthoSize();
		}
		
		void DoGetCameraOrthoSize()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				_camera  = go.GetComponent<Camera>();
			}
			
			if (_camera == null)
			{
				_camera = Camera.main;
			}
			
			
			if (_camera == null)
			{
				LogError("Missing Camera Component!");
				return;
			}

			orthoSize.Value = _camera.orthographicSize;
		}
	}
}
