// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Made by DjayDino

using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("sprite")]
	[Tooltip("Replaces a Random Sprite on any GameObject. Object must have a Sprite Renderer.")]
	public class SwapSpriteRandom : FsmStateAction
	{
		
		[RequiredField]
		[CheckForComponent(typeof(SpriteRenderer))]
		public FsmOwnerDefault gameObject;
		[CompoundArray("Sprites", "Sprite", "Weight")]		
		[ObjectType(typeof(Sprite))]
		public Sprite[] sprites;
		[HasFloatSlider(0, 1)]
		public FsmFloat[] weights;

		public FsmBool resetOnExit;
		private SpriteRenderer spriteRenderer;
		public FsmBool Repeat;
		private int randomIndex;
		private int lastIndex = -1;
		
		Sprite _orig;

		public override void Reset()
		{
			gameObject = null;
			sprites = new Sprite[3];
			weights = new FsmFloat[] {1,1,1};
			resetOnExit = false;
			Repeat = false;
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			
			spriteRenderer = go.GetComponent<SpriteRenderer>();
			
			if (spriteRenderer == null)
			{
				LogError("SwapSingleSprite: Missing SpriteRenderer!");
				return;
			}

			_orig = spriteRenderer.sprite;

			SwapSprites();
			
			Finish();
		}

		public override void OnExit()
		{
			if (resetOnExit.Value)
			{
				spriteRenderer.sprite = _orig;
			}
		}
		void SwapSprites()
		{
			if (sprites == null) return;
			if (sprites.Length == 0) return;

			if (Repeat.Value)
			{
				randomIndex = ActionHelpers.GetRandomWeightedIndex(weights);
			}
			else
			{
				do
				{
					randomIndex = ActionHelpers.GetRandomWeightedIndex(weights);
				} while ( randomIndex == lastIndex);
				
				lastIndex = randomIndex;

			}
			
			if (randomIndex != -1)
			{
				spriteRenderer.sprite = sprites[randomIndex] as Sprite;
			}

		}


		
	}
}
