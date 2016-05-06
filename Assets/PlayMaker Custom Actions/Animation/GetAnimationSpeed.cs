// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
// __ECO__ __PLAYMAKER__ __ACTION__ 

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Get the current Speed of an Animation, Normalize is generaly used during blending. Check Every Frame to update the time continuously.")]
	public class GetAnimationSpeed : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		[Tooltip("The GameObject with Animation Component")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Animation)]
		[Tooltip("The name of the animation")]
		public FsmString animName;

		[UIHint(UIHint.Variable)]
		[Tooltip("The time of the animation")]
		public FsmFloat speed;

		[Tooltip("express the time using normalized (0 to 1)")]
		public bool normalized;

		[Tooltip("Repeat every frame. Useful to watch the time value change")]
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			animName = null;
			speed = null;
			normalized = false;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetAnimationSpeed(gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value);
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetAnimationSpeed(gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value);
		}

		void DoGetAnimationSpeed(GameObject go)
		{
			if (go == null) return;

			if (go.animation == null)
			{
				LogWarning("Missing animation component: " + go.name);
				return;
			}


			AnimationState anim = go.animation[animName.Value];

			if (anim == null)
			{
				LogWarning("Missing animation: " + animName.Value);
				return;
			}

			if (normalized)
			{
				speed.Value = anim.normalizedSpeed ;
			}
			else
			{
				speed.Value = anim.speed;
			}

		}

	}
}