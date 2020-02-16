// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Texture")]
	[Tooltip("The texture must have the Is Readable flag set in the import settings, otherwise this function will fail. ")]
	public class GetPixelColor   : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The texture needs to have the Is readable flag set to true in the import settings")]
		public FsmTexture texture;

		[RequiredField]
		public FsmInt x;

		[RequiredField]
		public FsmInt y;

		[Tooltip("The color at the position defined")]
		[UIHint(UIHint.Variable)]
		public FsmColor color;
	  
		public override void OnEnter ()
		{
			var texture1 = texture.Value as Texture2D;
			color.Value = texture1.GetPixel (x.Value, y.Value);
			Finish ();
		} 
	}
}