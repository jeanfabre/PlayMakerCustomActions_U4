// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
//--- __ECO__ __ACTION__

using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Plays a series of sprites at a given framerate on a GameObject with a Sprite Renderer.")]
	public class PlaySpritesAnimation : FsmStateAction
	{
		
		[RequiredField]
		[CheckForComponent(typeof(SpriteRenderer))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The framerate to animate the sprites")]
		public FsmFloat framesPerSecond;
		
		[Tooltip("-1 or 0 for infinite loop, animation done will never be called. More than 0 to define the number of times to play.")]
		public FsmInt loop;
		
		public FsmEvent animationDoneEvent;
		
		public Sprite[] sprites;
		
		private SpriteRenderer spriteRenderer;
		
		private int lastSpriteIndex;
		
		private int loopCounter;
		
		public override void Reset()
		{
			gameObject = null;
			loop = -1;
			animationDoneEvent = null;
			
			framesPerSecond = 12f;
			sprites = new Sprite[3];
			
			loopCounter = 0;
		}
		
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			
			
			spriteRenderer = go.GetComponent<SpriteRenderer>();
			
			if (spriteRenderer == null)
			{
				LogError("PlaySpritesAnimation: Missing SpriteRenderer!");
				return;
			}
			
			lastSpriteIndex = 0;
			loopCounter = 0;
			
		}
		
		// Update is called once per frame
		public override void OnUpdate()
		{
			if (framesPerSecond.Value>0)
			{
				int index = (int)(Time.timeSinceLevelLoad * framesPerSecond.Value);
				int spriteIndex = index % sprites.Length;
				
				
				spriteRenderer.sprite = sprites[ spriteIndex ];
				
				if (spriteIndex!=lastSpriteIndex && spriteIndex==0 )
				{
					loopCounter++;
					
					if (loop.Value>0 && loopCounter>=loop.Value)
					{
						Fsm.Event(animationDoneEvent);
						Finish ();
					}
				}
				
				lastSpriteIndex = spriteIndex;
				
			}
		}
		
	}
}