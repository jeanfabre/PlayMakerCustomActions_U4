// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Made by nightcorelv

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [Tooltip("Swap two items at a specified indexes of a FSM Array")]
    public class FsmArraySwapItems : BaseFsmVariableAction
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

        [UIHint(UIHint.FsmInt)]
        [Tooltip("The first index to swap")]
        public FsmInt index1;

        [UIHint(UIHint.FsmInt)]
        [Tooltip("The second index to swap")]
        public FsmInt index2;

        [UIHint(UIHint.FsmEvent)]
        [Tooltip("The event to trigger if the removeAt throw errors")]
        public FsmEvent failureEvent;

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

            if (fsmArray == null)
                return;

            try
            {
                var _item2 = fsmArray.Values[index2.Value];

                fsmArray.Values[index2.Value] = fsmArray.Values[index1.Value];
                fsmArray.Values[index1.Value] = _item2;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
                Fsm.Event(failureEvent);
            }
        }
    }
}