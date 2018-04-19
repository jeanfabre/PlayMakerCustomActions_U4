﻿//Made by nightcorelv
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Each time this action is called it gets the next item from a FSM Array. This version has a reset flag \n" +
             "This lets you quickly loop through all the items of an array to perform actions on them." +
             "")]
    public class FsmArrayGetNext : BaseFsmVariableAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmArray)]
        public FsmString variableName;

        public FsmInt startIndex;

        public FsmInt endIndex;

        public FsmEvent loopEvent;

        public FsmEvent finishedEvent;

        [Tooltip("If you want to reset the iteration, raise this flag to true when you enter the state, it will indicate you want to start from the beginning again")]
        public FsmBool resetFlag;

        [ActionSection("Result")]

        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmVar result;

        [UIHint(UIHint.Variable)]
        public FsmInt currentIndex;

        private int nextItemIndex = 0;



        public override void Reset()
        {
            startIndex = null;
            endIndex = null;

            currentIndex = null;

            loopEvent = null;
            finishedEvent = null;

            resetFlag = null;

            result = null;
        }

        public override void OnEnter()
        {
            if (resetFlag.Value)
            {
                resetFlag.Value = false;
                nextItemIndex = 0;
            }

            if (nextItemIndex == 0)
            {
                if (startIndex.Value > 0)
                {
                    nextItemIndex = startIndex.Value;
                }
            }

            ggop();

            Finish();
        }






        void ggop()
        {

            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (!UpdateCache(go, fsmName.Value))
            {
                return;
            }

            var fsmArray = fsm.FsmVariables.GetFsmArray(variableName.Value);



            if (nextItemIndex >= fsmArray.Length)
            {
                nextItemIndex = 0;
                currentIndex.Value = fsmArray.Length - 1;
                Fsm.Event(finishedEvent);
                return;
            }


            result.SetValue(fsmArray.Get(nextItemIndex));

            if (nextItemIndex >= fsmArray.Length)
            {
                nextItemIndex = 0;
                currentIndex.Value = fsmArray.Length - 1;
                Fsm.Event(finishedEvent);
                return;
            }

            if (endIndex.Value > 0 && nextItemIndex >= endIndex.Value)
            {
                nextItemIndex = 0;
                currentIndex.Value = endIndex.Value;
                Fsm.Event(finishedEvent);
                return;
            }

            nextItemIndex++;

            currentIndex.Value = nextItemIndex - 1;

            if (loopEvent != null)
            {
                Fsm.Event(loopEvent);
            }
        }
    }
}