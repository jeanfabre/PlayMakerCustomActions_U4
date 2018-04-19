//Made by nightcorelv

using UnityEngine;
using System.Collections.Generic;
using System;

namespace HutongGames.PlayMaker.Actions
{
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Remove an item from a Fsm array")]
    public class FsmArrayRemove : BaseFsmVariableAction
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

        int index=(-1);


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

            value.UpdateValue();

            List<object> _new = new List<object>();

            int i = 0;
            foreach (object _obj in fsmArray.Values)
            {
                if (!_obj.Equals(value.GetValue()))
                {
                    _new.Add(_obj);
                }

                i++;
            }


            fsmArray.Values = _new.ToArray();


        }
    }
}