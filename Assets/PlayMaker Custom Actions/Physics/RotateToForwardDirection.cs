// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Rotates a game object so that its forward(z) axis faces the direction.")]
    public class RotateToForwardDirection : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
        [Tooltip("Input direction")]
        [Title("Direction")]
        public FsmVector3 direction;

        [UIHint(UIHint.Variable)]
        [Tooltip("Output Euler Angles")]
        public FsmVector3 angles;


        public bool everyFrame;

        public override void Reset()
        {
            direction = null;
            angles = null;
        }

        void translate()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            go.transform.forward = direction.Value;
            angles.Value = go.transform.eulerAngles;
        }

        public override void OnEnter()
        {
            translate();

            if (!everyFrame)
                Finish();


        }
        public override void OnUpdate()
        {
            translate();
        }

    }
}
