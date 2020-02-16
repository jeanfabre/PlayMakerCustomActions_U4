// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Transform)]
    [Tooltip("Inverse transforms a Vector from a Game Object's world space to local space.")]
    public class InverseTransformVector : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        [RequiredField]
        public FsmVector3 direction;
        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmVector3 storeResult;
        public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            direction = null;
            storeResult = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            DoInverseTransformVector();

            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            DoInverseTransformVector();
        }

        void DoInverseTransformVector()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null) return;

            storeResult.Value = go.transform.InverseTransformVector(direction.Value);
        }
    }
}

