//Made by nightcorelv
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Get a FSM Random item from an Array.")]
    public class FsmArrayGetRandomIndex : BaseFsmVariableAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmArray)]
        public FsmString variableName;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmVar storeValue;

        [UIHint(UIHint.Variable)]
        public FsmInt index;


        private int randomIndex;
        private int lastIndex = -1;

        public override void Reset()
        {
            storeValue = null;
            index = null;
        }

        public override void OnEnter()
        {
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



            randomIndex = Random.Range(0, fsmArray.Length);
            index.Value = randomIndex;
            storeValue.SetValue(fsmArray.Get(index.Value));

        }


    }
}

