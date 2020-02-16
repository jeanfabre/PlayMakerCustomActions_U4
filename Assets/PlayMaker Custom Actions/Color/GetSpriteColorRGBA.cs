// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// made by DjayDino
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Color)]
	[Tooltip("Sets the color RGBA of sprite renderer")]
	public class GetSpriteColorRGBA : FsmStateAction
	{
		

		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("Get The Color of the UI component")]
		public FsmColor color;
		
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the red channel in a float variable.")]
		public FsmFloat red;
		
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the green channel in a float variable.")]
		public FsmFloat green;
		
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the blue channel in a float variable.")]
		public FsmFloat blue;
		
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the alpha channel in a float variable.")]
		public FsmFloat alpha;

        [Tooltip("Repeat every frame. Useful if the color variable is changing.")]		
		public FsmBool everyFrame;


		private SpriteRenderer sRenderer;
		private GameObject go;
		
		public override void Reset()
		{
			gameObject = null;
			red = new FsmFloat(){UseVariable=true};
			green = new FsmFloat(){UseVariable=true};
			blue = new FsmFloat(){UseVariable=true};
			alpha = new FsmFloat(){UseVariable=true};
			everyFrame = false;
		
		}
		
		public override void OnEnter()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);
			sRenderer = go.GetComponent<SpriteRenderer>();

			GetSpriteColo();
			
			if (!everyFrame.Value)
				Finish();	

		}
		
		public override void OnUpdate()
		{
			GetSpriteColo();
		}
		
		void GetSpriteColo()
		{
			if (sRenderer == null) return;
			
			var spriteColor = sRenderer.color;
			
			color.Value = spriteColor;
			red.Value = spriteColor.r;
			green.Value = spriteColor.g;
			blue.Value = spriteColor.b;
			alpha.Value = spriteColor.a;

		}
		
	
		
	}
}
