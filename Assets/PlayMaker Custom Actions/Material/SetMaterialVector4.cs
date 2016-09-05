// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets a named Vector4 in a game object's material.")]
	public class SetMaterialVector4 : FsmStateAction
	{
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;

		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;

		[RequiredField]
		[Tooltip("A named vector4 parameter in the shader.")]
		public FsmString namedVector4;
		
		public FsmFloat xValue;
		public FsmFloat yValue;
		public FsmFloat zValue;
		public FsmFloat wValue;
		
		
		[Tooltip("Repeat every frame. Useful if the value is animated.")]
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			materialIndex = 0;
			material = null;
			namedVector4 = "";
			xValue = null;
			yValue = null;
			zValue = null;
			wValue = null;
			
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetMaterialProperty();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate ()
		{
			DoSetMaterialProperty();
		}
		
		void DoSetMaterialProperty()
		{
			
			Vector4 _vector4 = new Vector4(xValue.Value,yValue.Value,zValue.Value,wValue.Value);
			
			//Debug.Log(_vector4);
			if (material.Value != null)
			{
				material.Value.SetVector(namedVector4.Value, _vector4);
				return;
			}
			
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;

			if (go.renderer == null)
			{
				LogError("Missing Renderer!");
				return;
			}
			
			if (go.renderer.material == null)
			{
				LogError("Missing Material!");
				return;
			}
			
			if (materialIndex.Value == 0)
			{
				go.renderer.material.SetVector(namedVector4.Value, _vector4);
			}
			else if (go.renderer.materials.Length > materialIndex.Value)
			{
				var materials = go.renderer.materials;
				materials[materialIndex.Value].SetVector(namedVector4.Value, _vector4);
				go.renderer.materials = materials;			
			}	
		}
	}
}
