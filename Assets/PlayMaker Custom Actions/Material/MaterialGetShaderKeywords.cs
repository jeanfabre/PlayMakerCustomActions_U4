// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
/*--- Keywords: Shader Keyword parameter --- */

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Gets the shaderKeywords property of a material.")]
	public class MaterialGetShaderKeywords : FsmStateAction
	{
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;

		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;

		[RequiredField]
		[Tooltip("shader keywords.")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.String)]
		public FsmArray shaderKeywords;

		[Tooltip("Event sent if material was not found")]
		public FsmEvent fail;

		public override void Reset()
		{
			gameObject = null;
			materialIndex = 0;
			material = null;
			shaderKeywords = null;

			fail = null;
		}

		public override void OnEnter()
		{
			bool ok = DoSetMaterialProperty();

			if (!ok) Fsm.Event(fail);

			Finish();
		}

		bool DoSetMaterialProperty()
		{
			
		
			//Debug.Log(_vector4);
			if (material.Value != null)
			{
				shaderKeywords.stringValues = material.Value.shaderKeywords;
				return true;
			}
			
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return false;

			if (go.renderer == null)
			{
				LogError("Missing Renderer!");
				return false;
			}
			
			if (go.renderer.material == null)
			{
				LogError("Missing Material!");
				return false;
			}
			
			if (materialIndex.Value == 0)
			{
				shaderKeywords.stringValues = go.renderer.material.shaderKeywords;
				return true;
			}
			else if (go.renderer.materials.Length > materialIndex.Value)
			{
				var materials = go.renderer.materials;
				shaderKeywords.stringValues =  materials[materialIndex.Value].shaderKeywords;
				go.renderer.materials = materials;	
				return true;
			}

			return false;
		}
	}
}
