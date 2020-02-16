// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Knocks back a rigidbody using the hit point of the projectile")]
    public class AddKnockBackForce : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault npc;
        private Vector3 npcPos;
        public FsmVector3 hitPoint;
        public FsmFloat knockBackAmount;
        public FsmBool useRelativeForce;
        Rigidbody rb;

        public bool everyFrame;

        public override void Reset()
        {
          
        
            everyFrame = false;
        }

        public override void OnEnter()
        {
            

            DoknockBackForce();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoknockBackForce();
        }

        void DoknockBackForce()
        {
            var go = Fsm.GetOwnerDefaultTarget(npc);
            if (go == null)
            {
                return;
            }

            rb = go.GetComponent<Rigidbody>();

            npcPos = go.transform.position;
            Vector3 direction = (npcPos - hitPoint.Value).normalized;
            direction = direction * knockBackAmount.Value;
            direction = new Vector3(direction.x, 0f, direction.z);

            if(useRelativeForce.Value == true)
            {
                rb.AddRelativeForce(direction, ForceMode.VelocityChange);
            }
            else
            {
                rb.AddForce(direction, ForceMode.VelocityChange);
            }

     
       
        }


    }
}