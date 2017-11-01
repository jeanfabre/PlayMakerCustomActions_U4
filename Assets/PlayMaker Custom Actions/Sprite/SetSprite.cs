// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("sprite")]
	[Tooltip("Sets a Sprite on any GameObject. Object must have a Sprite Renderer.")]
	public class SetSprite : FsmStateAction
	{
		
		[RequiredField]
		[CheckForComponent(typeof(SpriteRenderer))]
		[Tooltip("The GameObject with a Sprite Renderer.")]
		public FsmOwnerDefault gameObject;

		[ObjectType(typeof(Sprite))]
		[Tooltip("The Sprite to set")]
		public FsmObject sprite;

		private SpriteRenderer spriteRenderer;

		public override void Reset()
		{
			gameObject = null;
			sprite = null;
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

			DoSetSprite();
			
			Finish();
		}

		void DoSetSprite()
		{
			spriteRenderer.sprite = sprite.Value as Sprite;
		}


		
	}
}
