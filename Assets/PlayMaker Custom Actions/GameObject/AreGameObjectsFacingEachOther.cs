// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: facing angle look game object align

using UnityEngine;
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.GameObject)]
    [Tooltip("Checks to see if two game objects are facing each other with a tolerance. A tolerance of 0 means they must be perfectly aligned to return true.")]
    public class AreGameObjectsFacingEachOther : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        [RequiredField]
        public FsmGameObject target;
        public FsmFloat threshold;
        public FsmBool isFacing;
        public FsmEvent Facing;
        public FsmEvent NotFacing;
        public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            target = null;
            threshold = 15f;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            DoFacingCheck();

            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            DoFacingCheck();
        }

        void DoFacingCheck()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null) return;

            float dot = (Mathf.Abs(Vector3.Angle(go.transform.forward, target.Value.gameObject.transform.forward) - 180));
        
            if (dot <= threshold.Value)
            {
                isFacing.Value = true;
                Fsm.Event(Facing);              
            }
            else
            {
                isFacing.Value = false;
                Fsm.Event(NotFacing);              
            }


        }
    }
}

