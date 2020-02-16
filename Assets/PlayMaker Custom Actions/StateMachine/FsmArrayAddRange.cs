// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Made by nightcorelv
// forumlink : http://hutonggames.com/playmakerforum/index.php?topic=18563.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    [ActionCategory(ActionCategory.StateMachine)]
    [Tooltip("Add values to a FSM array.")]
    public class FsmArrayAddRange : BaseFsmVariableAction
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

        [RequiredField]
        //[MatchElementType("array")]
        public FsmVar[] variables;

        public override void Reset()
        {
            gameObject = null;
            fsmName = "";
            variables = new FsmVar[2];
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
            
            int count = variables.Length;

            if (count > 0)
            {
                fsmArray.Resize(fsmArray.Length + count);

                foreach (FsmVar _var in variables)
                {
                    _var.UpdateValue();
                    fsmArray.Set(fsmArray.Length - count, _var.GetValue());
                    count--;
                }
            }
        }
    }
}