// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Gets a named color value from a game object's material.")]
	public class GetMaterialColor : FsmStateAction
	{
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;
		
		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;
		
		[UIHint(UIHint.NamedColor)]
		[Tooltip("The named color parameter in the shader.")]
		public FsmString namedColor;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the parameter value.")]
		public FsmColor color;

		[Tooltip("Event sent if material was not found")]
		public FsmEvent fail;
		
		
		public override void Reset()
		{
			gameObject = null;
			materialIndex = 0;
			material = null;
			namedColor = "_Color";
			color = null;
			fail = null;
		}
		
		public override void OnEnter()
		{
			DoGetMaterialColor();
			
			Finish();
		}
		
		void DoGetMaterialColor()
		{
			if (color.IsNone)
			{
				return;
			}
			
			var colorName = namedColor.Value;
			if (colorName == "") colorName = "_Color";
			
			if (material.Value != null)
			{
				if ( ! material.Value.HasProperty(colorName)) 
				{
					Fsm.Event(fail);
					return;
				}
				
				color.Value = material.Value.GetColor(colorName);
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
				if ( ! go.renderer.material.HasProperty(colorName)) 
				{
					Fsm.Event(fail);
					return;
				}
				color.Value = go.renderer.material.GetColor(colorName);
			}
			else if (go.renderer.materials.Length > materialIndex.Value)
			{
				var materials = go.renderer.materials;
				
				if ( ! materials[materialIndex.Value].HasProperty(colorName)) 
				{
					Fsm.Event(fail);
					return;
				}
				
				color.Value = materials[materialIndex.Value].GetColor(colorName);			
			}		
		}
	}
}
