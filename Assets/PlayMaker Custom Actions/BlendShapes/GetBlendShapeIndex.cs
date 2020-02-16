// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("BlendShapes")]
	[Tooltip("Returns the BlendShape index using its name.")]
	public class GetBlendShapeIndex : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject target. Requires a SkinnedMeshRenderer component ")]
		[CheckForComponent(typeof(SkinnedMeshRenderer))]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The BlendShape name")]
		public FsmString blendShapeName;
		
		[RequiredField]
		[Tooltip("The BlendShape index of blendShapeName")]
		[UIHint(UIHint.Variable)]
		public FsmInt blendShapeIndex;
		
		SkinnedMeshRenderer _skr;

		public override void Reset()
		{
			gameObject = null;
			blendShapeName = null;
			blendShapeIndex = null;
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
				Finish();
				return;
			}

			DoGetBlendShapeIndex();

			Finish();

		}

		void DoGetBlendShapeIndex()
		{
			if (_skr!=null)
			{
				blendShapeIndex.Value = _skr.sharedMesh.GetBlendShapeIndex(blendShapeName.Value);
			}
		}
	}
}

