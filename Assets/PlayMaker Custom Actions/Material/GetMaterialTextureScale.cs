// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Gets the Scale of a named texture in a Game Object's Material. Useful for special effects.")]
	public class GetMaterialTextureScale : ComponentAction<Renderer>
	{
		[RequiredField]
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;

		[UIHint(UIHint.NamedTexture)]
		[Tooltip("The named texture parameter in the shader.")]
		public FsmString namedTexture;

		[Tooltip("The scale of the texture")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 scale;

		[Tooltip("The scale x value of the texture")]
		[UIHint(UIHint.Variable)]
		public FsmFloat scaleX;

		[Tooltip("The scale y value of the texture")]
		[UIHint(UIHint.Variable)]
		public FsmFloat scaleY;

		public bool everyFrame;

		Vector2 _scale;
		public override void Reset()
		{
			gameObject = null;
			materialIndex = 0;
			namedTexture = "_MainTex";
			scaleX = null;
			scaleY = null;
			scale = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoGetTextureScale();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoGetTextureScale();
		}
		
		void DoGetTextureScale()
		{
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
				_scale = renderer.material.GetTextureScale(namedTexture.Value);
			}
			else if (renderer.materials.Length > materialIndex.Value)
			{
				_scale =  renderer.materials[materialIndex.Value].GetTextureScale(namedTexture.Value);
			}	

			if (!this.scale.IsNone) {
				this.scale.Value = _scale;
			}

			if (!this.scaleX.IsNone) {
				this.scaleX.Value = _scale.x;
			}

			if (!this.scaleY.IsNone) {
				this.scaleY.Value = _scale.y;
			}
		}
	}
}