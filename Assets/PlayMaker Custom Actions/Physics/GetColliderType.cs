// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Gets the type of a Game Object's collider and stores it as a String var.")]
    public class GetColliderType : ComponentAction<Collider>
    {
        [RequiredField]
        [CheckForComponent(typeof(Collider))]
        [Tooltip("The GameObject that owns the Collider")]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the type in a string variable.")]
        public FsmString storeResult;

       

        public FsmEvent BoxCollider;
        public FsmEvent MeshCollider;
        public FsmEvent CapsuleCollider;
        public FsmEvent SphereCollider;
        public FsmEvent TerrainCollider;
        public FsmEvent WheelCollider;
        public FsmEvent OtherCollider;

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
                DoGetType();
                Finish();
            }
        }
        public override void OnUpdate()
        {
            if (everyFrame.Value)
            {
                DoGetType();
            }

        }

        void DoGetType()
        {

            var go = Fsm.GetOwnerDefaultTarget(gameObject);
         
            var collider = go.GetComponent<Collider>();
           
            if (UpdateCache(go))
            {
                storeResult.Value = collider.GetType().ToString();
            }


            if (storeResult.Value == "UnityEngine.BoxCollider")
            {
                Fsm.Event(BoxCollider);
            }

            if (storeResult.Value == "UnityEngine.SphereCollider")
            {
                Fsm.Event(SphereCollider);
            }

            if (storeResult.Value == "UnityEngine.CapsuleCollider")
            {
                Fsm.Event(CapsuleCollider);
            }

            if (storeResult.Value == "UnityEngine.MeshCollider")
            {
                Fsm.Event(MeshCollider);
            }

            if (storeResult.Value == "UnityEngine.WheelCollider")
            {
                Fsm.Event(WheelCollider);
            }

            if (storeResult.Value == "UnityEngine.TerrainCollider")
            {
                Fsm.Event(TerrainCollider);
            }
            
            else
            {

                Fsm.Event(OtherCollider);
            }

            

        }
    }
}