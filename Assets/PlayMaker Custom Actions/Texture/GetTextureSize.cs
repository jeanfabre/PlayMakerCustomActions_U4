// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Texture")]
	[Tooltip("Gets a texture height and width in pixels.")]
	public class GetTextureSize : FsmStateAction
	{
 
		[Tooltip("The texture")]
		public FsmTexture texture;
		
		[Tooltip("The texture size")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 size;

		[Tooltip("The texture width")]
		[UIHint(UIHint.Variable)]
		public FsmInt width;

		[Tooltip("The texture height")]
		[UIHint(UIHint.Variable)]
		public FsmInt height;

		public override void Reset ()
		{
			texture = null;
			size = null;
			width = null;
			height = null;
		}

		public override void OnEnter()
		{
			if(texture.Value!=null)
			{
				if (!width.IsNone) {
					width.Value = texture.Value.width;
				}

				if (!height.IsNone) {
					height.Value = texture.Value.height;
				}

				if (!size.IsNone) {
					size.Value = new Vector2(texture.Value.width,texture.Value.height);
				}
			}

			Finish();
		}
	}
}