// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Made by nightcorelv
// forumlink : http://hutonggames.com/playmakerforum/index.php?topic=18563.0

using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    [ActionCategory(ActionCategory.StateMachine)]
    [Tooltip("Delete the item at an index. Index must be between 0 and the number of items -1. First item is index 0.")]
    public class FsmArrayDeleteAt : BaseFsmVariableAction
    {
        [RequiredField]
        [Tooltip("The GameObject that owns the FSM.")]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        [Tooltip("Optional name of FSM on Game Object")]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmArray)]
        [Tooltip("The name of the FSM variable.")]
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