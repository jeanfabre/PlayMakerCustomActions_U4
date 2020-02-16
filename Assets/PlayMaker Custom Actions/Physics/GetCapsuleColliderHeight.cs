// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Gets the height of a Game Object's capsule collider.")]
    public class GetCapsuleColliderHeight : ComponentAction<Collider>
    {
        [RequiredField]
        [CheckForComponent(typeof(Collider))]
        [Tooltip("The GameObject that owns the Collider")]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the height in a float variable.")]
        public FsmFloat storeResult;

        public FsmBool everyFrame;



        public override void Reset()
        {
            gameObject = null;
            storeResult = null;
        }

        public override void OnEnter()
        {

            if (!everyFrame.Value)
            {
                Finish();
            }
        }
            public override void OnUpdate()
        {
            if (everyFrame.Value)
            {
                DoGetHeight();
            }

        }

        void DoGetHeight()
        {
            
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            var collider = go.GetComponent<CapsuleCollider>();
            if (UpdateCache(go))
            {
                storeResult.Value = ((CapsuleCollider)collider).height;
            }
        }
    }
}