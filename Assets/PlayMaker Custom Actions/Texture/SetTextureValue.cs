// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
 
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Texture")]
	[Tooltip("Sets the value of a Texture Variable.")]
	public class SetTextureValue : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmTexture textureVariable;
		[RequiredField]
		public FsmTexture textureValue;
		public bool everyFrame;

		public override void Reset()
		{
			textureVariable = null;
			textureValue = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetTextureValue();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoSetTextureValue();
		}
		
		void DoSetTextureValue()
		{
			if (textureVariable == null) return;
			if (textureValue == null) return;
			
			textureVariable.Value = textureValue.Value;
		}
		
	}
}
