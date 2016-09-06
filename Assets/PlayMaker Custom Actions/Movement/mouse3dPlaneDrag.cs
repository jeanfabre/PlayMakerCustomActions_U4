// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

/// <summary>
/// 25 of may 2011 
/// contact@fabrejean.net
/// http://www.fabrejean.net
/// 
/// This action compute the drag and worldposition of the mouse (or any position on screen)
/// It assumes the following: 
/// -- Enter this action ( State ) when the drag is supposed to start.
/// -- quit this action when the drag is suppose to finish.
/// 
/// currently. It accomodates groundplane point changes to have a consistent drag computation if the camera is moving while you drag.
/// NOTE: this is not the best way. the plance normal changes is not taken in consideration ( and so is the camera rotation ). 
/// IMPROVEMENTS: ideally, the plane should be defines as a transform, and the drag expressed in that transform reference to allow full flexibility
/// if camera or plane is rotated during the drag. GEt in touch with me if you need that improvement and did not succeed in trying yourself to implement it.
/// 
/// If camera is not defined, it will use the camera it is attached to or as a last resort the main camera.
/// </summary> 


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Movement)]
	[Tooltip("drag around on a 3d plane.")]
	public class mouse3dPlaneDrag : FsmStateAction
	{
		[Tooltip("The camera, leave empty if you want to use the camera this fsm is attached to or if you want to use the main camera")]
		public Camera camera;
		
		[Tooltip("The ground position")]
		[UIHint(UIHint.FsmVector3)]
		public FsmVector3 groundPoint;
		
		[Tooltip("The ground normal")]
		[UIHint(UIHint.FsmVector3)]
		public FsmVector3 groundNormal;

		
		[Tooltip("The screen point")]
		[UIHint(UIHint.FsmVector3)]
		public FsmVector3 screenPoint;
		
		[Tooltip("The resulting world point")]
		[UIHint(UIHint.FsmVector3)]
		public FsmVector3 worldPointResult;

		[Tooltip("The resulting drag in world space")]
		[UIHint(UIHint.FsmVector3)]
		public FsmVector3 deltaDragResult;
		
		[Tooltip("The resulting delta movement in world space")]
		[UIHint(UIHint.FsmVector3)]
		public FsmVector3 deltaPositionResult;
		
		private Plane groundPlane;
		private bool dragging;
		private Vector3 startPosition;
		private Vector3 dragVector;
		
		private RaycastHit _hit_;
		private float _hitDistance_;
		private Ray _ray_;

		private Vector3 _lastPos;
		
		public override void Reset()
		{
			camera = null;
			groundNormal = null;
			groundPoint = null;
			worldPointResult = null;
			deltaDragResult = null;
			deltaPositionResult = null;

			
		}// Reset


		
		public override void OnEnter()
		{
			// simply check for the camera;
			if (camera== null){
				if (Fsm.GameObject.camera != null){
					camera = Fsm.GameObject.camera;
				}else{
					camera = Camera.main;
				}
			}

			groundPlane = new Plane(groundNormal.Value, groundPoint.Value);
			
			_lastPos = computeWorldPosition();
			startPosition = _lastPos - groundPoint.Value;
			
			deltaPositionResult.Value = Vector3.zero;
			deltaDragResult.Value = Vector3.zero;
			
		}// OnEnter


		
		public override void OnUpdate()
		{
			groundPlane.SetNormalAndPosition(groundNormal.Value, groundPoint.Value);
			
			Vector3 _newPos = computeWorldPosition();
			deltaPositionResult.Value = _newPos - _lastPos;
			_lastPos = _newPos;
			worldPointResult.Value = _newPos;
			deltaDragResult.Value = (worldPointResult.Value -  groundPoint.Value) - startPosition;
			

			
		}// OnUpdate
		
		public override void OnExit(){
			worldPointResult.Value = Vector3.zero;
			deltaDragResult.Value = Vector3.zero;
			
		}
		
		private Vector3 computeWorldPosition(){
			_ray_ = camera.ScreenPointToRay(screenPoint.Value);
			groundPlane.Raycast(_ray_, out _hitDistance_);

			return _ray_.GetPoint(_hitDistance_);
		}// computeWorldPosition

		
	}// class
}// namespace
