// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
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

				[HasFloatSlider(0,1)]
		public FsmFloat red;
		
		[HasFloatSlider(0,1)]
		public FsmFloat green;
		
		[HasFloatSlider(0,1)]
		public FsmFloat blue;
		
		[HasFloatSlider(0,1)]
		public FsmFloat alpha;
	
		public FsmBool everyFrame;


		private SpriteRenderer sRenderer;
		private GameObject go;
		
		public override void Reset()
		{
			gameObject = null;
			red = 0;
			green = 0;
			blue = 0;
			alpha = 1;
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
