// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets the material  on the last Raycast and store in variables.")]
	public class GetRaycastHitInfoMaterial : FsmStateAction
	{

		[UIHint(UIHint.Variable)]
		[Tooltip("Get the material of the triangle that was hit.")]
		public FsmMaterial material;

		public FsmOwnerDefault meshRenderer;

		
        [Tooltip("Repeat every frame.")]
	    public bool everyFrame;

		MeshCollider  _meshCollider;
		MeshRenderer _meshRenderer;
		SkinnedMeshRenderer _skinnedMeshRenderer;

		public override void Reset()
		{
			material = null;
			
		    everyFrame = false;
		}

		void GetRaycastInfo()
		{
			if (Fsm.RaycastHitInfo.collider != null)
			{
				if (!material.IsNone)
				{
					_meshCollider = Fsm.RaycastHitInfo.collider as MeshCollider;

					var go = Fsm.GetOwnerDefaultTarget(meshRenderer);
					if (go !=null)
					{
						_meshRenderer = go.GetComponent<MeshRenderer>();
						_skinnedMeshRenderer =  go.GetComponent<SkinnedMeshRenderer>();
					}

					// There are 3 indices stored per triangle
					int limit = Fsm.RaycastHitInfo.triangleIndex * 3;
					int submesh;
					for(submesh = 0; submesh < _meshCollider.sharedMesh.subMeshCount; submesh++)
					{
						int numIndices = _meshCollider.sharedMesh.GetTriangles(submesh).Length;
						if(numIndices > limit)
							break;
						
						limit -= numIndices;   
					}

					if (_meshRenderer!=null)
					{
						material.Value =  _meshRenderer.sharedMaterials[submesh];
					}else if (_skinnedMeshRenderer)
					{
						material.Value =  _skinnedMeshRenderer.sharedMaterials[submesh];
					}


				}
			}
		}

		public override void OnEnter()
		{
			GetRaycastInfo();
			
            if (!everyFrame)
            {
                Finish();
            }
		}

        public override void OnUpdate()
        {
            GetRaycastInfo();
        }

		public override string ErrorCheck()
		{
			var go = Fsm.GetOwnerDefaultTarget (meshRenderer);
			if (go != null) {
				_meshRenderer = go.GetComponent<MeshRenderer> ();
				_skinnedMeshRenderer = go.GetComponent<SkinnedMeshRenderer> ();
			}

			if (_meshRenderer == null && _skinnedMeshRenderer == null) {
				return "meshRenderer must have a MeshRenderer Component or a SkinnedMeshRenderer component attached";
			}

			return string.Empty;
		}
	}
}
