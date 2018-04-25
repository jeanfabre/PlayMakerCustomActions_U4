//Made by nightcorelv

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Add an item to the end of a FSM Array.")]
    public class FsmArrayAdd : BaseFsmVariableAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmArray)]
        public FsmString variableName;

        [RequiredField]
        public FsmVar value;


        public override void Reset()
        {
            gameObject = null;
            fsmName = "";
            value = null;
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

            fsmArray.Resize(fsmArray.Length + 1);
            value.UpdateValue();
            fsmArray.Set(fsmArray.Length - 1, value.GetValue());

        }
    }
}