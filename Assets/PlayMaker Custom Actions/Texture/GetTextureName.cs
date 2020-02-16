// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Texture")]
	[Tooltip("Get a texture's name ")]
	public class GetTextureName : FsmStateAction
	{

		[Tooltip("The texture")]
		public FsmTexture texture;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The texture name.")]
		public FsmString textureName;

		public override void Reset ()
		{
			texture = null;
			textureName = null;

		}

		public override void OnEnter ()
		{
			DoGetMaterialTextureName ();

			Finish ();
		}

		void DoGetMaterialTextureName ()
		{
			if (texture.Value != null) {			
				textureName.Value = texture.Value.name;
			}else{
				textureName.Value = string.Empty;
			}
		}
	}
}
