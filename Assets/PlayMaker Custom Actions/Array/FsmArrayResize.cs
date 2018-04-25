//Made by nightcorelv

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Resize an array.")]
    public class FsmArrayResize : BaseFsmVariableAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmArray)]
        public FsmString variableName;

        public FsmInt newSize;

        public FsmEvent sizeOutOfRangeEvent;


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



            if (newSize.Value >= 0)
            {
                fsmArray.Resize(newSize.Value);
            }
            else
            {
                LogError("Size out of range: " + newSize.Value);
                Fsm.Event(sizeOutOfRangeEvent);
            }


        }
    }
}