//Made by nightcorelv

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Add values to a FSM array.")]
    public class FsmArrayAddRange : BaseFsmVariableAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmArray)]
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