// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Create a new RenderTexture")]
	public class CreateRenderTexture : FsmStateAction
	{
		[Tooltip("The width of the renderTexture")]
		public FsmInt width;

		[Tooltip("The height of the renderTexture")]
		public FsmInt height;

		[Tooltip("The depth renderTexture, depth can be 0, 16 or 24")]
		public FsmInt depth;

		[Tooltip("The texture format")]
		[ObjectType(typeof(RenderTextureFormat))]
		public FsmEnum TextureFormat;

		[Tooltip("The readwrite option")]
		[ObjectType(typeof(RenderTextureReadWrite))]
		public FsmEnum TextureReadWrite;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The new renderTexture")]
		public FsmTexture renderTexture;

		public override void Reset()
		{
			width = null;
			height = null;
			depth = 24;
			TextureFormat = null;
			TextureReadWrite = null;
			renderTexture = null;
		}
		
		public override void OnEnter()
		{
		
			renderTexture.Value = new RenderTexture (width.Value, height.Value, depth.Value,
			                                         (RenderTextureFormat)TextureFormat.Value,
			                                         (RenderTextureReadWrite)TextureReadWrite.Value);

			Finish();
		}


		public override string ErrorCheck()
		{
			if (depth.Value != 0 && depth.Value != 16 && depth.Value != 24) {
				return "depth can only be 0, 16 or 24";
			}

			return "";
		}

	}
}
