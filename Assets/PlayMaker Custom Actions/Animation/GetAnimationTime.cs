// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
// __ECO__ __PLAYMAKER__ __ACTION__ 

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Get the current Time of an Animation, Normalize time means 0 (start) to 1 (end); useful if you don't need the exact time. Check Every Frame to update the time continuosly.")]
	public class GetAnimationTime : FsmStateAction
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
		public FsmFloat time;

		[Tooltip("express the time using normalized (0 to 1)")]
		public bool normalized;

		[Tooltip("Repeat every frame. Useful to watch the time value change")]
		public bool everyFrame;

		Animation _anim;
		AnimationState _animState;

		public override void Reset()
		{
			gameObject = null;
			animName = null;
			time = null;
			normalized = false;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetAnimationTime(gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value);


			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoGetAnimationTime(gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value);
		}

		void DoGetAnimationTime(GameObject go)
		{
			if (go == null) return;
			
			if (go.animation == null)
			{
				LogWarning("Missing animation component: " + go.name);
				return;
			}
			
			
			_anim = go.GetComponent<Animation>();
			
			
			if (_anim == null)
			{
				LogWarning("Missing animation on : " + go.name);
				return;
			}

			_animState = go.animation[animName.Value];

			if (_animState == null)
			{
				LogWarning("Missing animation State: " + animName.Value);
				return;
			}

			if (normalized)
			{
				time.Value = _animState.normalizedTime ;
			}
			else
			{
				time.Value = _animState.time;
			}

		}

	}
}