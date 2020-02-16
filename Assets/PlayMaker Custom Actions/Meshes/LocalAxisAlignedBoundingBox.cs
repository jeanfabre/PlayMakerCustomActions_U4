// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Gets the Bounds of a Target Mesh using a local Transform. Typically, this allows you to know the Bounding size in arbitrary directions.")]
	public class LocalAxisAlignedBoundingBox : FsmStateAction
	{
		[RequiredField]
        [Tooltip("The transform to represent the bounds")]
        public FsmOwnerDefault axis;

        [Tooltip("The Mesh to use to compute bounds from")]
		public FsmOwnerDefault meshTarget;

		[UIHint(UIHint.Variable)]
		[RequiredField]
		[Tooltip("Store the result in a Vector3 variable.")]
        public FsmVector3 BoundsSize;

        [Tooltip("Repeat every frame. Typically this would be set to True.")]
		public bool everyFrame;
		
		private Transform _targetTransform;
		private Transform _transform;
		private Mesh _mesh;
		
		private Bounds _bounds;
		
		public override void Reset()
		{
			axis = null; 
			meshTarget = null;
			BoundsSize = null;
			everyFrame = true;
		}

		public override void OnEnter()
		{
			
			GameObject root = Fsm.GetOwnerDefaultTarget(axis);
			if (root == null)
			{
				Finish();
				return;
			}
			
			GameObject target = Fsm.GetOwnerDefaultTarget(meshTarget);
			if (target == null)
			{
				Finish();
				return;
			}
			
			_targetTransform = target.transform;
			_transform = root.transform;
			_mesh = target.GetComponent<MeshFilter>().mesh;

			DoGetBounds();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetBounds();
		}

		void DoGetBounds()
		{
			_bounds = new Bounds();
			
			Vector3 _localVertex;
			foreach(Vector3 _vertex in _mesh.vertices)
			{
		    	_localVertex =	_transform.InverseTransformPoint(_targetTransform.TransformPoint(_vertex));	
				_bounds.Encapsulate(_localVertex);
			}
			BoundsSize.Value = _bounds.size;
		}
	}
}

