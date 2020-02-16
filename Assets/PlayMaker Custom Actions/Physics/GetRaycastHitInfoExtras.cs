// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets extras info on the last Raycast and store in variables.")]
	public class GetRaycastHitInfoExtras : FsmStateAction
	{
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the barycentric coordinate of the triangle we hit.")]
		public FsmVector3 barycentricCoordinate;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the index of the triangle that was hit.")]
		public FsmInt triangleIndex;

		[UIHint(UIHint.Variable)]
		[Tooltip("Get the material of the triangle that was hit.")]
		public FsmMaterial TriangleMaterial;

		[UIHint(UIHint.Variable)]
		[Tooltip("Get the uv texture coordinate at the impact point.")]
		public FsmVector2 textureCoord;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the secondary uv texture coordinate at the impact point.")]
		public FsmVector2 textureCoord2;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the uv lightmap coordinate at the impact point.")]
		public FsmVector2 lightmapCoord;
		
        [Tooltip("Repeat every frame.")]
	    public bool everyFrame;

		public override void Reset()
		{
			triangleIndex = null;
			textureCoord = null;
			textureCoord2 = null;
			lightmapCoord = null;
			
		    everyFrame = false;
		}

		void GetRaycastInfo()
		{
			if (Fsm.RaycastHitInfo.collider != null)
			{
				triangleIndex.Value	= Fsm.RaycastHitInfo.triangleIndex;
				textureCoord.Value	= Fsm.RaycastHitInfo.textureCoord;
				textureCoord2.Value	= Fsm.RaycastHitInfo.textureCoord2;
				lightmapCoord.Value	= Fsm.RaycastHitInfo.lightmapCoord;

				if (!TriangleMaterial.IsNone)
				{
					TriangleMaterial.Value = GetTriangleIndexMaterial(Fsm.RaycastHitInfo.triangleIndex,Fsm.RaycastHitInfo.collider as MeshCollider);
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


		Material GetTriangleIndexMaterial(int triangleIndex, MeshCollider meshCollider)
		{

			// There are 3 indices stored per triangle
			int limit = triangleIndex * 3;
			int submesh;
			for(submesh = 0; submesh < meshCollider.sharedMesh.subMeshCount; submesh++)
			{
				int numIndices = meshCollider.sharedMesh.GetTriangles(submesh).Length;
				if(numIndices > limit)
					break;
				
				limit -= numIndices;   
			}
			
			return  meshCollider.GetComponent<MeshRenderer>().sharedMaterials[submesh];
		}
	}
}
