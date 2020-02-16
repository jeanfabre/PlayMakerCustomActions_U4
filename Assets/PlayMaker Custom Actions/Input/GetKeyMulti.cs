// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Created by DjayDino

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Input)]
    [Tooltip("Gets the pressed state of a Key.")]
    public class GetKeyMulti : FsmStateAction
    {
        [CompoundArray("Count", "KeyCode", "StoreValue")]
        [RequiredField]
        [Tooltip("The key to test.")]
        public KeyCode[] key;
        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Store if the key is down (True) or up (False).")]
        public FsmBool[] values;





        [Tooltip("Repeat every frame. Useful if you're waiting for a key press/release.")]
        public bool everyFrame;

        public override void Reset()
        {
            key = new KeyCode[1];
            values = new FsmBool[1];
            everyFrame = false;
        }

        public override void OnEnter()
        {
            DoGetKey();

            if (!everyFrame)
            {
                Finish();
            }
        }


        public override void OnUpdate()
        {
            DoGetKey();
        }

        void DoGetKey()
        {
            for (int i = 0; i < key.Length; i++)
            {
                values[i].Value = Input.GetKey(key[i]);
            }
        }

    }
}