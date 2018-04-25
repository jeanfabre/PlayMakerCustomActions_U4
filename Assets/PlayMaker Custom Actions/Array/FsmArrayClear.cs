//Made by nightcorelv

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Sets all items in a FSM Array to their default value: 0, empty string, false, or null depending on their type. Optionally defines a reset value to use.")]
    public class FsmArrayClear : BaseFsmVariableAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmArray)]
        public FsmString variableName;

        [Tooltip("Optional reset value. Leave as None for default value.")]
        public FsmVar resetValue;

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

            int count = fsmArray.Length;

            fsmArray.Reset();
            fsmArray.Resize(count);

            if (!resetValue.IsNone)
            {
                resetValue.UpdateValue();
                object _val = resetValue.GetValue();
                for (int i = 0; i < count; i++)
                {
                    fsmArray.Set(i, _val);
                }
            }
            Finish();

        }
    }
}