// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("BlendShapes")]
	[Tooltip("Returns weight of BlendShape on this renderer. You can use the blendshape index or name. Check Every Frame to update continously.")]
	public class GetBlendShapeWeight : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject target. Requires a SkinnedMeshRenderer component ")]
		[CheckForComponent(typeof(SkinnedMeshRenderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("The BlendShape index, starting at 0. Out of range indexes will produce error.")]
		public FsmInt blendShapeIndex;

		[Tooltip("Or the BlendShape name. If set, blendShapeIndex will be ignored.")]
		public FsmString orBlendShapeName;

		[RequiredField]
		[Tooltip("The BlendShape weight, ranging from 0 to 100.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat weight;
		
		[Tooltip("Repeat every frame, while the state is active")]
		public bool everyFrame;
		
		SkinnedMeshRenderer _skr;

		int _blendShapeIndex;

		public override void Reset()
		{
			gameObject = null;
			blendShapeIndex = null;
			orBlendShapeName = new FsmString(){UseVariable=true};
			weight = null;
			everyFrame = false;
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

			if (! orBlendShapeName.IsNone)
			{
				_blendShapeIndex = _skr.sharedMesh.GetBlendShapeIndex(orBlendShapeName.Value);
			}else{
				_blendShapeIndex = blendShapeIndex.Value;
			}
			
			
			DoGetBlendShapeWeight();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoGetBlendShapeWeight();
		}

		void DoGetBlendShapeWeight()
		{
			if (_skr!=null)
			{
				weight.Value = _skr.GetBlendShapeWeight(_blendShapeIndex);
			}
		}
	}
}
