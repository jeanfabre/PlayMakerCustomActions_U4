// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
// by MDS
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Applies knock to a game object using a Rigidbody, NavmeshAgent or Character Controller")]
    public class KnockbackAction : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The object to knockback")]
        public FsmOwnerDefault knockThisBack;
        [Tooltip("Select type- selected component must be present on object or it won't work")]
        public enum objectType {NavMeshAgent,RigidBody,CharacterController }
        public objectType type;
        private Vector3 npcPos;
        [Tooltip("The hitPoint from a cast")]
        public FsmVector3 hitPoint;
        [Tooltip("How far to knock the object back")]
        public FsmFloat knockBackAmount;
        Rigidbody rb;
        NavMeshAgent nav;
        CharacterController cc;

        public bool everyFrame;

        public override void Reset()
        {
            everyFrame = false;
        }

        public override void OnEnter()
        {


            DoKnockBackAction();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoKnockBackAction();
        }

        void DoKnockBackAction()
        {
            var go = Fsm.GetOwnerDefaultTarget(knockThisBack);
            if (go == null)
            {
                return;
            }

            int eValue = (int)type;

            switch(eValue)
            {
                case 0:
                    // navmesh
                    nav = go.GetComponent<NavMeshAgent>();
                    npcPos = go.transform.position;
                    Vector3 direction1 = (npcPos - hitPoint.Value).normalized;
                    direction1 = direction1 * knockBackAmount.Value / 2;
                    direction1 = new Vector3(direction1.x, 0f, direction1.z);
                    nav.Move(direction1);
                    

                    break; 
                case 1:
                    // rb
                    rb = go.GetComponent<Rigidbody>();
                    npcPos = go.transform.position;
                    Vector3 direction = (npcPos - hitPoint.Value).normalized;
                    direction = direction * knockBackAmount.Value;
                    direction = new Vector3(direction.x, 0f, direction.z);
                    rb.AddForce(direction, ForceMode.VelocityChange);

                    break;
                case 2:
                    // cc
                    cc = go.GetComponent<CharacterController>();
                    npcPos = go.transform.position;
                    Vector3 direction2 = (npcPos - hitPoint.Value).normalized;
                    direction2 = direction2 * knockBackAmount.Value * 10f;
                    direction2 = new Vector3(direction2.x, 0f, direction2.z);
                    cc.Move(direction2 * Time.deltaTime);

                    break;

            }

           


          


        }


    }
}