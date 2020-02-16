// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("BlendShapes")]
	[Tooltip("Returns BlendShapes count on this mesh.")]
	public class GetBlendShapesCount : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject target. Requires a SkinnedMeshRenderer component ")]
		[CheckForComponent(typeof(SkinnedMeshRenderer))]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The number of BlendShapes on this mesh")]
		[UIHint(UIHint.Variable)]
		public FsmInt blendShapesCount;
		

		SkinnedMeshRenderer _skr;
		
		public override void Reset()
		{
			gameObject = null;
			blendShapesCount = null;
		}
		
		public override void OnEnter()
		{
			
			GameObject go =	gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value;

			if (go!=null)
			{
				_skr = go.GetComponent<SkinnedMeshRenderer>();
			}
			
			if (_skr == null)
			{
				LogWarning("Missing component SkinnedMeshRenderer");
				return;
			}
			
			
			DoGetBlendShapesCount();
			
			Finish();
		}

		void DoGetBlendShapesCount()
		{
			if (_skr!=null)
			{
				blendShapesCount.Value = _skr.sharedMesh.blendShapeCount;
			}
		}
	}
}
