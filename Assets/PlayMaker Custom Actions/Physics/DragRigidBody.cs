// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Drag a Rigid body with the mouse. If draggingPlaneTransform is defined, it will use the UP axis of this gameObject as the dragging plane normal \n" +
		"That is select the ground Plane, if you want to drag object on the ground instead of from the camera point of view.")]
	public class DragRigidBody : FsmStateAction
	{
		
		
		[Tooltip("the springness of the drag")]
		public FsmFloat spring;
		
		[Tooltip("the damping of the drag")]
		public FsmFloat  damper;
		
		[Tooltip("the drag during dragging")]
		public FsmFloat drag;
		
		[Tooltip("the angular drag during dragging")]
		public FsmFloat angularDrag;
		
		[Tooltip("The Max Distance between the dragging target and the RigidBody being dragged")]
		public FsmFloat distance;
		
		[Tooltip("If TRUE, dragging will have close to no effect on the Rigidbody rotation ( except if it hits other bodies as you drag it)")]
		public FsmBool attachToCenterOfMass;
		
		[Tooltip("If Defined. Use this transform Up axis as the dragging plane normal. Typically, set it to the ground plane if you want to drag objects around on the floor..")]
		public FsmOwnerDefault draggingPlaneTransform;
		
		[Tooltip("If set, will only drag rigidBodies from one of the defined layer, tagging (if set) is taken in consideration as well.")]
		[UIHint(UIHint.Layer)]
		public FsmInt[] layerMask;
		[Tooltip("Invert the mask, so you drag from all layers except those defined above.")]
		public FsmBool invertMask;
		
		[UIHint(UIHint.Tag)]
		[Tooltip("If set, will only drag rigidBodies with one of the defined tag, masking (if set) is taken in consideration as well.")]
		public FsmString[] dragTaggedOnly;
		
		private SpringJoint springJoint;
		
		private bool isDragging;
		
		private float oldDrag;
		private float oldAngularDrag;
		
		private Camera _cam;
		
		private GameObject _goPlane;
		
		private Vector3 _dragStartPos;
		
		private float dragDistance;
		
		public override void Reset()
		{
			spring = 50f;
			damper = 5f;
			drag = 10f;
			angularDrag = 5f;
			distance = 0.2f;
			attachToCenterOfMass = false;
			draggingPlaneTransform = null;
			dragTaggedOnly = null;
			
			layerMask = null;
			invertMask = false;
			
		
		}
		
		public override void OnEnter()
		{
			 _cam = Camera.main;
			 _goPlane = Fsm.GetOwnerDefaultTarget(draggingPlaneTransform);
			
			
		}
		public override void OnUpdate()
		{

			if (!isDragging && Input.GetMouseButtonDown (0))
			{
				
		
				// We need to actually hit an object
				RaycastHit hit;
				if (!Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out hit, 100))
				{		
					return;
				}
				
				// We need to hit a rigidbody that is not kinematic
				if (!hit.rigidbody || hit.rigidbody.isKinematic)
				{
					return;
				}
				
				bool isAllowed = false;
				
				// check against tags
				if (dragTaggedOnly.Length!=0)
				{
					for (int i = 0; i < dragTaggedOnly.Length; i++) 
					{
						if (hit.rigidbody.tag == dragTaggedOnly[i].Value)
						{
							isAllowed = true;
							break;
						}
					}
				}else{
					isAllowed = true;
				}
				
				// check against mask
				if (isAllowed && layerMask.Length!=0)
				{
					int mask = ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value);
			
					isAllowed =  ((1 << hit.rigidbody.gameObject.layer) & mask) > 0;
					
				}
				
				if (isAllowed)
				{
					StartDragging(hit);
				}
			}
		
			if (isDragging)
			{
				Drag();
			}
			
			
		}
		
		private void StartDragging(RaycastHit hit)
		{
			isDragging = true;
			
			if (!springJoint)
			{
				GameObject go = new GameObject("__Rigidbody dragger__");
				Rigidbody body = go.AddComponent<Rigidbody>();
				springJoint = go.AddComponent<SpringJoint>();
				body.isKinematic = true;
			}
			
			springJoint.transform.position = hit.point;
			if (attachToCenterOfMass.Value)
			{
				Vector3 anchor = _cam.transform.TransformDirection(hit.rigidbody.centerOfMass) + hit.rigidbody.transform.position;
				anchor = springJoint.transform.InverseTransformPoint(anchor);
				springJoint.anchor = anchor;
			}
			else
			{
				springJoint.anchor = Vector3.zero;
			}
			
			_dragStartPos = hit.point;
			
			
			springJoint.spring = spring.Value;
			springJoint.damper = damper.Value;
			springJoint.maxDistance = distance.Value;
			springJoint.connectedBody = hit.rigidbody;
			
			oldDrag = springJoint.connectedBody.drag;
			oldAngularDrag = springJoint.connectedBody.angularDrag;
			
			springJoint.connectedBody.drag = drag.Value;
			springJoint.connectedBody.angularDrag = angularDrag.Value;
			
			dragDistance = hit.distance;
			
		}
		
		private void Drag()
		{
			
			if (!Input.GetMouseButton (0))
			{
				StopDragging();
				return;
			}
			
				Ray ray = _cam.ScreenPointToRay (Input.mousePosition);
			if (_goPlane!=null)
			{
				Plane _plane = new Plane(_goPlane.transform.up,_dragStartPos);
				float enter;
				
				if (_plane.Raycast(ray,out enter))
				{
					springJoint.transform.position =ray.GetPoint(enter);
				}
				
			}else{
				
			springJoint.transform.position = ray.GetPoint(dragDistance);
			}
			
			

		}
		
		private void StopDragging()
		{
			isDragging = false;
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
		
		public override void OnExit()
		{
			StopDragging();
		}
	}
}
