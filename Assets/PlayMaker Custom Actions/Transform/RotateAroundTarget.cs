// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Transform)]
    [Tooltip("Rotates a Game Object around a target position. You can use this to do targeting like the Dark Souls games.")]
    public class RotateAroundTarget : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The game object to rotate. Usually your player character.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("Vector3 for the target object to rotate around. This is your target object you want to rotate around.")]
        [UIHint(UIHint.Variable)]
        public FsmVector3 targetPosition;

        [Tooltip("Movement on the X axis while targeting. Use the vector from a Get Axis action here.")]
        public FsmFloat rotationDirection;

        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            targetPosition = null;
            everyFrame = true;
            rotationDirection = null;
          
        }

        public override void OnEnter()
        {
            if (!everyFrame)
            {
                Finish();
            }

        }

        public override void OnUpdate()
        {
            if (everyFrame)
            {
                DoRotate();
            }
        }

        void DoRotate()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            // Rotate around target

            go.transform.RotateAround(targetPosition.Value, Vector3.up, rotationDirection.Value * Time.deltaTime);

         
        }

    }
}