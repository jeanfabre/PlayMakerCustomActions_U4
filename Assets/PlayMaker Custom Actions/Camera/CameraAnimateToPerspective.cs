// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Smooth animation from ortho ( or perspective) to Perspective. Use GameObject reference instead of camera reference")]
	public class CameraAnimateToPerspective : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The camera to animate. If null or not defined, uses the MainCamera")]
		[CheckForComponent(typeof(Camera))]
		public FsmOwnerDefault camera;
		
		[Tooltip("The Field of View to reach")]
		public FsmFloat fov;
		
		[Tooltip("The near plane distance to reach. Leave to none for no effect")]
		public FsmFloat near;
		
		[Tooltip("The near plane distance to reach. Leave to none for no effect")]
		public FsmFloat far;
		
		[Tooltip("The duration of the transition animation")]
		public FsmFloat duration;
		
		[Tooltip("Event sent when transition is done")]
		public FsmEvent transitionDoneEvent;
		
		Camera _camera;
		Matrix4x4 _perspMatrix;
		
		float _startTime;
		float _duration;
		

		public override void Reset()
		{
			camera = null;
			
			fov = 60f;
			near = new FsmFloat() {UseVariable=true};
			far = new FsmFloat() {UseVariable=true};
			
			duration = 1f;
			
			transitionDoneEvent = null;
		}

		public override void OnEnter()
		{
			
			_startTime = Time.time;
			_duration = duration.Value;
			
			AnimateToOrtho();
		}

		public override void OnUpdate()
		{
			
			if (_camera!=null)
			{
				
				if (Time.time - _startTime < _duration)
				{
				 	_camera.projectionMatrix = MatrixLerp(_camera.projectionMatrix,_perspMatrix,(Time.time - _startTime) / _duration);
				}else{
					Fsm.Event(transitionDoneEvent);
					Finish();
				}
			}
		}

		void AnimateToOrtho()
		{
			GameObject _go = Fsm.GetOwnerDefaultTarget(camera);
			
			if (_go == null && _go.camera != null)
			{
				_camera = _go.camera ;
			}else{
				_camera = Camera.main;
			}
			
			float _near = near.IsNone?_camera.nearClipPlane:near.Value;
			float _far = far.IsNone?_camera.farClipPlane:far.Value;

			float aspect = (float) Screen.width / (float) Screen.height;

       		 _perspMatrix =	Matrix4x4.Perspective(fov.Value, aspect, _near, _far);
			
			if (_duration==0f)
			{
				_camera.projectionMatrix = _perspMatrix;
				Fsm.Event(transitionDoneEvent);
				Finish();
			}
		}
		
		Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float time)
	    {
	
	        Matrix4x4 ret = new Matrix4x4();
	
	        for (int i = 0; i < 16; i++)
	
	            ret[i] = Mathf.Lerp(from[i], to[i], time);
	
	        return ret;
	
	    }
	}
}
