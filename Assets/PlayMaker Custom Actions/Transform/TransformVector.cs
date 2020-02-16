// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Transform)]
    [Tooltip("Transforms a Vector from a Game Object's local space to world space.")]
    public class TransformVector : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        [RequiredField]
        public FsmVector3 localDirection;
        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmVector3 storeResult;
        public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            localDirection = null;
            storeResult = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            DoTransformVector();

            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            DoTransformVector();
        }

        void DoTransformVector()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null) return;

            storeResult.Value = go.transform.TransformVector(localDirection.Value);
        }
    }
}

