// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Drag a Rigid body with another GameObject.")]
	public class DragRigidBodyWithGo : FsmStateAction
	{
		
		[RequiredField]
		[Tooltip("The dragging target")]
		public FsmOwnerDefault draggingGO;
		
		[RequiredField]
		[Tooltip("The RigidBody to drag")]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmGameObject draggedRB;
		
		
		[Tooltip("the springness of the drag")]
		public FsmFloat spring;
		
		[Tooltip("the damping of the drag")]
		public FsmFloat  damper;
		
		[Tooltip("the drag during dragging")]
		public FsmFloat drag;
		
		[Tooltip("the angular drag during dragging")]
		public FsmFloat angularDrag;
		
		[Tooltip("The Distance between the dragging target and the RigidBody being dragged")]
		public FsmFloat distance;
		
		[Tooltip("Force the Distance between the dragging target and the RigidBody being dragged to be th eone when starting dragging")]
		public FsmBool UseStartDistance;
		
		[Tooltip("vector(0,0,0) would be the center of mass")]
		public FsmVector3 dragAnchor;
		
		[Tooltip("whether the go has to drag the rigidBody ")]
		public FsmBool isDragging;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("feedback of the current distance and the wanted distance")]
		public FsmFloat tension;
		
		private SpringJoint springJoint;
		private bool _isDragging;
		
		private float oldDrag;
		private float oldAngularDrag;
		
		
		
		private float dragDistance;
		
		
		private GameObject _draggingGO;
		private Rigidbody _draggedRB;
		
		
		public override void Reset()
		{
			spring = 50f;
			damper = 5f;
			drag = 10f;
			angularDrag = 5f;
			distance = 0.2f;
			UseStartDistance = true;
			dragAnchor = Vector3.zero;
			isDragging = true;
			
			tension = null;
		
		}
		
		public override void OnExit()
		{
			StopDragging();
		}
		
		
		public override void OnUpdate()
		{			
			
			if (isDragging.Value !=_isDragging)
			{
				_isDragging = isDragging.Value;
				
				if (_isDragging)
				{
					StartDragging();
				}else{
					StopDragging();
				}
				
			}
			
			if (_isDragging)
			{
				Drag();

			}
			
		}
		
		private void StartDragging()
		{
			_draggingGO = draggingGO.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : draggingGO.GameObject.Value;
			if (_draggingGO == null) return;
			
			GameObject go = draggedRB.Value;
			if (go == null) return;
			if (go.rigidbody == null) return;
			
			_draggedRB = go.rigidbody;
			
			
			if (!springJoint)
			{
				GameObject _RB_draggerGo = new GameObject("__Rigidbody dragger__");
				Rigidbody body = _RB_draggerGo.AddComponent<Rigidbody>();
				springJoint = _RB_draggerGo.AddComponent<SpringJoint>();
				body.isKinematic = true;
			}
			
		

			springJoint.anchor =  dragAnchor.Value;


			springJoint.transform.position =  _draggedRB.transform.position;
			
		
			if (UseStartDistance.Value)
			{
				Vector3 delta = _draggingGO.transform.position - _draggedRB.transform.position;
				distance.Value = delta.magnitude;
			}
			
			springJoint.spring = spring.Value;
			springJoint.damper = damper.Value;
			springJoint.maxDistance = Mathf.Abs(distance.Value);
			//springJoint.minDistance = springJoint.maxDistance;
			springJoint.connectedBody = _draggedRB;
			
			oldDrag = springJoint.connectedBody.drag;
			oldAngularDrag = springJoint.connectedBody.angularDrag;
			
			springJoint.connectedBody.drag = drag.Value;
			springJoint.connectedBody.angularDrag = angularDrag.Value;
			
			
			
		}
		
		private void Drag()
		{
			springJoint.transform.position =_draggingGO.transform.position;
				
			if (distance.Value != springJoint.maxDistance)
			{
				springJoint.maxDistance = Mathf.Abs(distance.Value);
				//springJoint.minDistance = springJoint.maxDistance;
			}	
			
			tension.Value = (_draggingGO.transform.position - _draggedRB.transform.position).magnitude -distance.Value ;

			
			/*
			if (springJoint.anchor !=  dragAnchor.Value)
			{
				springJoint.anchor =  dragAnchor.Value;
			}
			*/
			
			if (springJoint.damper != damper.Value)
			{
				springJoint.damper = damper.Value;
			}
			if (springJoint.spring != spring.Value)
			{
				springJoint.spring = spring.Value;
			}
			if (springJoint.connectedBody.drag != drag.Value)
			{
				springJoint.connectedBody.drag = drag.Value;
			}
			
			if (springJoint.connectedBody.angularDrag != angularDrag.Value)
			{
				springJoint.connectedBody.angularDrag = angularDrag.Value;
			}
			
		}
		
		
		private void StopDragging()
		{
			_isDragging = false;
			if (springJoint==null)
			{
				return;
			}
			if (springJoint.connectedBody)
			{
				springJoint.connectedBody.drag = oldDrag;
				springJoint.connectedBody.angularDrag = oldAngularDrag;
				springJoint.connectedBody = null;
			}
		}
	
	}
}
