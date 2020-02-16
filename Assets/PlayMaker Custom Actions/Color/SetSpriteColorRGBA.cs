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
	public class SetSpriteColorRGBA : FsmStateAction
	{
		

		public FsmOwnerDefault gameObject;
		
	//	[UIHint(UIHint.Variable)]
		[Tooltip("Set the Color of the UI component")]
		public FsmColor color;

		[HasFloatSlider(0,1)]
		[Tooltip("Set the red channel")]
		public FsmFloat red;
		
		[HasFloatSlider(0,1)]
		[Tooltip("Set the green channel")]
		public FsmFloat green;
		
		[HasFloatSlider(0,1)]
		[Tooltip("Set the blue channel")]
		public FsmFloat blue;
		
		[HasFloatSlider(0,1)]
		[Tooltip("Set the alpha channel")]
		public FsmFloat alpha;
	
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
			everyFrame =false;
		
		}
		
		public override void OnEnter()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);
			sRenderer = go.GetComponent<SpriteRenderer>();

			SpriteColo();
			
			if (!everyFrame.Value)
				Finish();	

		}
		
		public override void OnUpdate()
		{
			SpriteColo();
		}
		
		void SpriteColo()
		{
			if (sRenderer == null) return;
			
			var newColor = sRenderer.color;
			
			if (!color.IsNone)
			{
				newColor = color.Value;
			}

			if (!red.IsNone)
			{
				newColor.r = red.Value;
			}

			if (!green.IsNone)
			{
				newColor.g = green.Value;
			}
			
			if (!blue.IsNone)
			{
				newColor.b = blue.Value;
			}
			
			if (!alpha.IsNone)
			{
				newColor.a = alpha.Value;
			}
			sRenderer.color = newColor;

		}
		
	
		
	}
}
