// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("BlendShapes")]
	[Tooltip("Sets weight of BlendShape on this renderer. You can use the blendshape index or name. Check Every Frame to update continously, e.g., if you're manipulating a variable that controls this blendshape weight.")]
	public class SetBlendShapeWeight : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject target. Requires a SkinnedMeshRenderer component ")]
		[CheckForComponent(typeof(SkinnedMeshRenderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("The BlendShape index, starting at 0. Out of range indexes will produce error.")]
		public FsmInt blendShapeIndex;

		[Tooltip("Or the BlendShape name. If set, blendShapeIndex will be ignored.")]
		public FsmString orBlendShapeName;

		[Tooltip("The BlendShape weight, ranging from 0 to 100. out of range values will have no effect.")]
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


			DoSetBlendShapeWeight();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetBlendShapeWeight();
		}
		
		void DoSetBlendShapeWeight()
		{
			if (_skr!=null)
			{
				_skr.SetBlendShapeWeight(_blendShapeIndex,weight.Value);
			}
		}
	}
}
