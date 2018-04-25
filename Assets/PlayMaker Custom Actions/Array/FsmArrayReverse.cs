//Made by nightcorelv

using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Reverse the order of items in a FSM Array.")]
    public class FsmArrayReverse : BaseFsmVariableAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmArray)]
        public FsmString variableName;


        public override void Reset()
        {
            gameObject = null;
            fsmName = "";
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

            var _list = new List<object>(fsmArray.Values);
            _list.Reverse();
            fsmArray.Values = _list.ToArray();


        }
    }
}