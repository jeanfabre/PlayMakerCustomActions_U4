﻿//Made by nightcorelv

using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Shuffle values in a FSM array. Optionally set a start index and range to shuffle only part of the array.")]
    public class FsmArrayShuffle : BaseFsmVariableAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmArray)]
        public FsmString variableName;

        [Tooltip("Optional start Index for the shuffling. Leave it to none or 0 for no effect")]
        public FsmInt startIndex;

        [Tooltip("Optional range for the shuffling, starting at the start index if greater than 0. Leave it to none or 0 for no effect, it will shuffle the whole array")]
        public FsmInt shufflingRange;


        public override void Reset()
        {
            gameObject = null;
            fsmName = "";
            startIndex = new FsmInt { UseVariable = true };
            shufflingRange = new FsmInt { UseVariable = true };
        }

        public override void OnEnter()
        {
            ggop();
            Finish();
        }

        private void ggop()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (!UpdateCache(go, fsmName.Value))
            {
                return;
            }

            var fsmArray = fsm.FsmVariables.GetFsmArray(variableName.Value);


            List<object> _list = new List<object>(fsmArray.Values);

            int start = 0;
            int end = _list.Count - 1;

            if (startIndex.Value > 0)
            {
                start = Mathf.Min(startIndex.Value, end);
            }

            if (shufflingRange.Value > 0)
            {
                end = Mathf.Min(_list.Count - 1, start + shufflingRange.Value);

            }

            for (int i = end; i > start; i--)
            {
                int swapWithPos = Random.Range(start, i + 1);

                object tmp = _list[i];
                _list[i] = _list[swapWithPos];
                _list[swapWithPos] = tmp;
            }

            fsmArray.Values = _list.ToArray();


        }
    }
}