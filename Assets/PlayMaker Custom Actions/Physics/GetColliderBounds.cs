// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

// drawing box based on http://answers.unity3d.com/questions/461588/drawing-a-bounding-box-similar-to-box-collider.html

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Get the bound values of a gameObject collider. Can optionaly include children")]
	public class GetColliderBounds : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject with to get bounds from")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("Check to activate, uncheck to deactivate Game Object. Warning this may impact performances if ran everyframe")]
		public FsmBool includeChildren;

		[ActionSection("Result")]
		[Tooltip("The center of the bounding box.")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 center;

		[Tooltip("The extents of the box. This is always half of the size.")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 extents;

		[Tooltip("The total size of the box. This is always twice as large as the extents.")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 size;

		[Tooltip("The minimal point of the box. This is always equal to center-extents.")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 min;

		[Tooltip("The maximal point of the box. This is always equal to center+extents.")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 max;

		[Title("Draw Debug Bounding Box")]
		[Tooltip("Draw a debug box in the Scene View.")]
		public FsmBool debug;
		
		[Tooltip("Color to use for the debug Box.")] 
		public FsmColor debugColor;

		[Tooltip("Repeat every frame. Warning this may impact performances if includeChildren is enabled")]
		public bool everyFrame;

	
		GameObject _go;
		Collider[] _colliders;
		Bounds _bounds;

		public override void Reset()
		{
			gameObject = null;
			includeChildren = false;

			center = null;
			extents = null;
			size = null;
			min = null;
			max = null;

			debug = false;
			debugColor = Color.yellow;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			GetBound();
			if (!everyFrame)
			{
				Finish();
			}
			
		}

		public override void OnUpdate()
		{
			GetBound();
		}

		
		public void GetBound()
		{
			_go =  Fsm.GetOwnerDefaultTarget(gameObject);


			if (_go==null)
			{
				return;
			}


			if (includeChildren.Value)
			{
				_colliders = _go.GetComponentsInChildren<Collider>();
			}else{
				_colliders = _go.GetComponents<Collider>();
			}

			if (_colliders.Length>0)
			{
				_bounds = _colliders[0].bounds;
			}

			for(int i=1;i<_colliders.Length;i++)
			{
				_bounds.Encapsulate(_colliders[i].bounds);
			}

			if (!center.IsNone) center.Value = _bounds.center;
			if (!extents.IsNone) extents.Value = _bounds.extents;
			if (!min.IsNone) min.Value = _bounds.min;
			if (!max.IsNone) max.Value = _bounds.max;
			if (!size.IsNone) size.Value = _bounds.size;


			if (debug.Value && Application.isEditor)
			{
				CalcPositons();
				DrawBox();
			}
		} 



		private Vector3 v3FrontTopLeft;
		private Vector3 v3FrontTopRight;
		private Vector3 v3FrontBottomLeft;
		private Vector3 v3FrontBottomRight;
		private Vector3 v3BackTopLeft;
		private Vector3 v3BackTopRight;
		private Vector3 v3BackBottomLeft;
		private Vector3 v3BackBottomRight; 
	

		void CalcPositons(){



			Vector3 v3Center = _bounds.center;
			Vector3 v3Extents = _bounds.extents;
			
			v3FrontTopLeft     = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);  // Front top left corner
			v3FrontTopRight    = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);  // Front top right corner
			v3FrontBottomLeft  = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z);  // Front bottom left corner
			v3FrontBottomRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z);  // Front bottom right corner
			v3BackTopLeft      = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);  // Back top left corner
			v3BackTopRight     = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);  // Back top right corner
			v3BackBottomLeft   = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);  // Back bottom left corner
			v3BackBottomRight  = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);  // Back bottom right corner

			/* 
			 _t = _go.GetComponent<Transform>();
			v3FrontTopLeft     = _t.TransformPoint(v3FrontTopLeft);
			v3FrontTopRight    = _t.TransformPoint(v3FrontTopRight);
			v3FrontBottomLeft  = _t.TransformPoint(v3FrontBottomLeft);
			v3FrontBottomRight = _t.TransformPoint(v3FrontBottomRight);
			v3BackTopLeft      = _t.TransformPoint(v3BackTopLeft);
			v3BackTopRight     = _t.TransformPoint(v3BackTopRight);
			v3BackBottomLeft   = _t.TransformPoint(v3BackBottomLeft);
			v3BackBottomRight  = _t.TransformPoint(v3BackBottomRight);   
			*/
		}


		void DrawBox() {

			Debug.DrawLine (v3FrontTopLeft, v3FrontTopRight, debugColor.Value);
			Debug.DrawLine (v3FrontTopRight, v3FrontBottomRight, debugColor.Value);
			Debug.DrawLine (v3FrontBottomRight, v3FrontBottomLeft, debugColor.Value);
			Debug.DrawLine (v3FrontBottomLeft, v3FrontTopLeft, debugColor.Value);
			
			Debug.DrawLine (v3BackTopLeft, v3BackTopRight, debugColor.Value);
			Debug.DrawLine (v3BackTopRight, v3BackBottomRight, debugColor.Value);
			Debug.DrawLine (v3BackBottomRight, v3BackBottomLeft, debugColor.Value);
			Debug.DrawLine (v3BackBottomLeft, v3BackTopLeft, debugColor.Value);
			
			Debug.DrawLine (v3FrontTopLeft, v3BackTopLeft, debugColor.Value);
			Debug.DrawLine (v3FrontTopRight, v3BackTopRight, debugColor.Value);
			Debug.DrawLine (v3FrontBottomRight, v3BackBottomRight, debugColor.Value);
			Debug.DrawLine (v3FrontBottomLeft, v3BackBottomLeft, debugColor.Value);


			Debug.DrawLine (v3FrontTopLeft, v3BackBottomRight, debugColor.Value);
			Debug.DrawLine (v3FrontBottomRight, v3BackTopLeft, debugColor.Value);
		}
		
	}
}
