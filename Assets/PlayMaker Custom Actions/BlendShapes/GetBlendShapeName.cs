// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("BlendShapes")]
	[Tooltip("Returns the BlendShape name using its index.")]
	public class GetBlendShapeName : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject target. Requires a SkinnedMeshRenderer component ")]
		[CheckForComponent(typeof(SkinnedMeshRenderer))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("The BlendShape index")]
		public FsmInt blendShapeIndex;
		
		[RequiredField]
		[Tooltip("The BlendShape name of blendShapeIndex")]
		[UIHint(UIHint.Variable)]
		public FsmString blendShapeName;
		
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
			
			DoGetBlendShapeName();
			
			Finish();
			
		}
		
		void DoGetBlendShapeName()
		{
			if (_skr!=null)
			{
				blendShapeName.Value = _skr.sharedMesh.GetBlendShapeName(blendShapeIndex.Value);
			}
		}
	}
}
