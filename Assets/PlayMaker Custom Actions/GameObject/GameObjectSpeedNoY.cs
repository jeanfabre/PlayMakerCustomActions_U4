// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.GameObject)]
    [Tooltip("Get the speed of a gameObject ignoring its Y velocity.")]
    public class GameObjectSpeedNoY : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [Tooltip("The current speed")]
        [UIHint(UIHint.Variable)]
        public FsmFloat speed;

        [Tooltip("The current speed vector")]
        [UIHint(UIHint.Variable)]
        public FsmVector3 speedVector;

        [Tooltip("Use local or world space for the Speed vector.")]
        public Space space;

        private GameObject go;

        private Vector3 lastPosition;

        public override void Reset()
        {
            gameObject = null;
            speed = null;
            speedVector = null;
            space = Space.World;
        }

        public override void OnEnter()
        {
            go = Fsm.GetOwnerDefaultTarget(gameObject);
            lastPosition = new Vector3(go.transform.position.x, 0f, go.transform.position.z);

        }

        public override void OnUpdate()
        {

            doComputeSpeed();

        }


        void doComputeSpeed()
        {

            if (go == null)
            {
                return;
            }

            Vector3 currentPosition = new Vector3( go.transform.position.x, 0f, go.transform.position.z);

            Vector3 delta = currentPosition - lastPosition;
            if (!speed.IsNone)
            {
                speed.Value = delta.magnitude / Time.deltaTime;
            }

            if (space == Space.Self)
            {
                delta = go.transform.InverseTransformPoint( new Vector3(go.transform.position.x, 0f, go.transform.position.z) + delta);
            }

            speedVector.Value = delta;


            lastPosition = currentPosition;
        }

    }
}
