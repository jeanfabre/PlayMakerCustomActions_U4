//Made by nightcorelv

using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Delete the item at an index. Index must be between 0 and the number of items -1. First item is index 0.")]
    public class FsmArrayDeleteAt : BaseFsmVariableAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmArray)]
        public FsmString variableName;

        public FsmInt index;

        [ActionSection("Result")]

        public FsmEvent indexOutOfRangeEvent;


        public override void Reset()
        {
            index = null;
            indexOutOfRangeEvent = null;
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

            if (index.Value >= 0 && index.Value < fsmArray.Length)
            {
                List<object> _list = new List<object>(fsmArray.Values);
                _list.RemoveAt(index.Value);
                fsmArray.Values = _list.ToArray();
            }
            else
            {
                Fsm.Event(indexOutOfRangeEvent);
            }

        }
    }
}