using HutongGames.PlayMaker;
using UnityEngine;

namespace Game.Scripts.CustomPlayMakerActions.EasyTweenActions
{
    [ActionCategory("EasyTween")]
    public class EasyTweenCheckState : FsmStateAction
    {
        [CheckForComponent(typeof(EasyTween))]
        [HutongGames.PlayMaker.Tooltip("The game object that has easy tween on")]
        public FsmOwnerDefault AnimationGo;

        [HutongGames.PlayMaker.Tooltip("Find the animation game object by tag")]
        public FsmBool FindAnimationGoByTag;
        [UIHint(UIHint.Tag)]
        public FsmString Tag;

        private EasyTween _anim;


        public FsmBool Result;

        public FsmEvent isOpened;
        public FsmEvent isNotOpened;
        
        
        public override void Reset()
        {
            AnimationGo = null;
            FindAnimationGoByTag = null;
            Tag = null;
            _anim = null;
            Result = null;

            isOpened = null;
            isNotOpened = null;
        }

        public override void OnEnter()
        {
            if (FindAnimationGoByTag.Value)
            {
                if (!string.IsNullOrEmpty(Tag.Value))
                {
                    AnimationGo.GameObject.Value = GameObject.FindGameObjectWithTag(Tag.Value);
                }
                else
                {
                    LogError("You have opted to automatically find anim go by tag but have NOT specified the tag!");
                }
            }
            else
            {
                if (AnimationGo.GameObject.Value == null)
                {
                    LogError("Animation game object is null");
                }
            }
            
            var targetObject = Fsm.GetOwnerDefaultTarget(AnimationGo);
			
            if (targetObject == null)
            {
                return;
            }
            
            _anim = targetObject.gameObject.GetComponent<EasyTween>();

            if (_anim.IsObjectOpened())
            {
                Result.Value = true;
                Fsm.Event(isOpened);
            }
            else
            {
                Fsm.Event(isNotOpened);
            }
            
            Finish();
        }
    }
}