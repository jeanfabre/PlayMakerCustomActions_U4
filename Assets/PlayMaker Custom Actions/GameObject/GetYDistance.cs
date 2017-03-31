// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.GameObject)]
    [Tooltip("Measures the Y Distance betweens 2 Game Objects and stores the result in a Float Variable.")]
    public class GetYDistance : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Measure distance from this GameObject.")]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [Tooltip("Target position.")]
        public FsmVector3 target;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the distance in a float variable.")]
        public FsmFloat distance;

        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            target = null;
            distance = null;
            everyFrame = true;
        }

        public override void OnEnter()
        {
           

            DoGetYDistance();

            if (!everyFrame)
            {
                Finish();
            }
        }
        public override void OnUpdate()
        {
            DoGetYDistance();
        }

        void DoGetYDistance()
        {
            

            float firstYPos = gameObject.GameObject.Value.transform.position.y;
            float secondYPos = target.Value.y;

            distance.Value = firstYPos = secondYPos;

           
        }

    }
}