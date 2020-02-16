// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Gets the aspect of the Camera (width/height).")]
	public class GetCameraAspect : FsmStateAction
	{
		[CheckForComponent(typeof(Camera))]
		[Tooltip("The GameObject with a Camera Component. If not defined will use the Main Camera")]
		public FsmOwnerDefault gameObject;
		

		[Tooltip("The aspect (width/height)")]
		[UIHint(UIHint.Variable)]
		public FsmFloat aspect;

		[Tooltip("Event sent if aspect is more wide than tall")]
		public FsmEvent isWideEvent;

		[Tooltip("Event sent if aspect is tall wide than wide")]
		public FsmEvent isTallEvent;

		[Tooltip("Event sent if aspect is as wide as tall")]
		public FsmEvent isSquareEvent;


		Camera _camera;

		public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
			aspect = null;

			isWideEvent = null;
			isTallEvent = null;
			isSquareEvent = null;

			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoGetCameraAspect();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoGetCameraAspect();
		}
		
		void DoGetCameraAspect()
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
			
			aspect.Value = _camera.aspect;

			if (_camera.aspect > 1f && isWideEvent!=null)
			{
				Fsm.Event(isWideEvent);
			}

			if (_camera.aspect < 1f && isTallEvent!=null)
			{
				Fsm.Event(isTallEvent);
			}

			if (_camera.aspect == 1f && isSquareEvent!=null)
			{
				Fsm.Event(isSquareEvent);
			}
		}
	}
}
