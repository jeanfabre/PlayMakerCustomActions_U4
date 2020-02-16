// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---
EcoMetaStart
{
"script dependancies":["Assets/PlayMaker Custom Actions/Camera/Editor/ScreenTo3dPlanePointActionEditor.cs"]
}
EcoMetaEnd
 */

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Project a screen point on a 3d plane.")]
	public class ScreenTo3dPlanePoint : FsmStateAction
	{
		[Tooltip("The camera, leave empty if you want to use the camera this fsm is attached to or if you want to use the main camera")]
		[CheckForComponent(typeof(Camera))]
		public FsmGameObject camera;
		
		[ActionSection("Ground")]
		public bool groundAsTransform;
		
		[Tooltip("The ground transform, Y axis will be the ground normal.")]
		public FsmGameObject groundTransform;
		
		[Tooltip("The ground position,")]
		[UIHint(UIHint.FsmVector3)]
		public FsmVector3 groundPoint;
		
		[Tooltip("The ground normal,")]
		[UIHint(UIHint.FsmVector3)]
		public FsmVector3 groundNormal;
		
		[ActionSection("Screen Point")]
		[Tooltip("The screen point")]
		[UIHint(UIHint.FsmVector3)]
		public FsmVector3 screenPoint;
		
		[ActionSection("Result")]
		[Tooltip("The resulting world point")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 worldPointResult;
		
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
		
		
		private Plane groundPlane;
		
		
		private RaycastHit _hit_;
		private float _hitDistance_;
		private Ray _ray_;
		
		
		private Transform _groundTransform;
		
		private Camera _camera;
		
		public override void Reset()
		{
			camera = null;
			groundAsTransform = false;
			
			groundTransform = new FsmGameObject() {UseVariable=true};
			
			groundNormal = null;
			groundPoint = new Vector3(0,1,0);
			worldPointResult = null;
			
			
		}// Reset
		
		/*
		public override string ErrorCheck()
		{

			if (_groundTransformIsNone && !groundTransform.IsNone)
			{
				if (!groundNormal.IsNone) groundNormal.IsNone = true;
				if (!groundNormal.IsNone) groundNormal.IsNone = true;
			}


			return "";
		}
		*/
		
		public override void OnEnter()
		{
			// simply check for the camera;
			if (camera.Value == null || camera.Value.GetComponent<Camera>() == null ){
				if (Fsm.GameObject.GetComponent<Camera>() != null){
					_camera = Fsm.GameObject.GetComponent<Camera>();
				}else{
					_camera = Camera.main;
				}
			}else{
				_camera = camera.Value.GetComponent<Camera>();
			}
			
			if (groundTransform.Value!=null)
			{
				_groundTransform = groundTransform.Value.transform;
				groundPlane = new Plane(_groundTransform.position, _groundTransform.up);
			}else{
				groundPlane = new Plane(groundNormal.Value, groundPoint.Value);
			}
			
			worldPointResult.Value = computeWorldPosition();
			
			
			if (!everyFrame)
			{
				Finish();
			}
		}// OnEnter
		
		public override void OnUpdate()
		{
			if (_groundTransform!=null)
			{
				groundPlane.SetNormalAndPosition(_groundTransform.position, _groundTransform.up);
				
			}else{
				groundPlane.SetNormalAndPosition(groundNormal.Value, groundPoint.Value);
			}
			
			worldPointResult.Value = computeWorldPosition();
			
		}// OnUpdate
		
		private Vector3 computeWorldPosition(){
			_ray_ = _camera.ScreenPointToRay(screenPoint.Value);
			groundPlane.Raycast(_ray_, out _hitDistance_);
			
			return _ray_.GetPoint(_hitDistance_);
		}// computeWorldPosition
		
		
	}// class
}// namespace
