// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
// __ECO__ __PLAYMAKER__ __ACTION__ 
// http://hutonggames.com/playmakerforum/index.php?topic=12545.msg58593#msg58593

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets a named HDR color value in a game object's material.")]
	public class SetMaterialHDRColor : ComponentAction<Renderer>
	{
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;

		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;

		[UIHint(UIHint.NamedColor)]
		[Tooltip("A named color parameter in the shader.")]
		public FsmString namedColor;
		
		[RequiredField]
		[Tooltip("Set the parameter value.")]
		public FsmColor color;
		
		[RequiredField]
		[Tooltip("Set the parameter value.")]
		public FsmFloat brightness;

		[Tooltip("Repeat every frame. Useful if the value is animated.")]
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			materialIndex = 0;
			brightness = 1;
			material = null;
			namedColor = "_EmissionColor";
			color = Color.black;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetMaterialColor();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoSetMaterialColor();
		}

		void DoSetMaterialColor()
		{
			if (color.IsNone)
			{
				return;
			}
            var brightnessFactor = brightness.Value;
			var colorName = namedColor.Value;
			if (colorName == "") colorName = "_EmissionColor";

			if (material.Value != null)
			{
				material.Value.SetColor(colorName, color.Value * brightnessFactor);
				return;
			}
			
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (!UpdateCache(go))
		    {
		        return;
		    }
			
			if (renderer.material == null)
			{
				LogError("Missing Material!");
				return;
			}		
			
			if (materialIndex.Value == 0)
			{
				renderer.material.SetColor(colorName, color.Value * brightnessFactor);
			}
			else if (renderer.materials.Length > materialIndex.Value)
			{
				var materials = renderer.materials;
				materials[materialIndex.Value].SetColor(colorName, color.Value * brightnessFactor);
				renderer.materials = materials;			
			}		
		}
	}
}