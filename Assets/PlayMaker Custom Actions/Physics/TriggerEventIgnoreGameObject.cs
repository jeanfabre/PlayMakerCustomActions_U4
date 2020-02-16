// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("The same functionality as Trigger Event except the game object set as 'ignoreThis' will be ignored. ")]
    public class TriggerEventIgnoreGameObject : FsmStateAction
    {

        [Tooltip("The gameobject you want the trigger to ignore.")]
        public FsmOwnerDefault ignoreThis;

        public TriggerType trigger;
        [UIHint(UIHint.Tag)]
        public FsmString collideTag;
        public FsmEvent sendEvent;
        [UIHint(UIHint.Variable)]
        public FsmGameObject storeCollider;
        [Tooltip("The gameobject that activated the trigger. Use this to debug or instead of an additional get trigger info action.")]
        public FsmGameObject gameObjectHit;

        public override void Reset()
        {
            trigger = TriggerType.OnTriggerEnter;
            collideTag = "Untagged";
            sendEvent = null;
            storeCollider = null;
            ignoreThis = null;
            gameObjectHit = null;
        }

        public override void OnPreprocess()
        {
            switch (trigger)
            {
                case TriggerType.OnTriggerEnter:
                    Fsm.HandleTriggerEnter = true;
                    break;
                case TriggerType.OnTriggerStay:
                    Fsm.HandleTriggerStay = true;
                    break;
                case TriggerType.OnTriggerExit:
                    Fsm.HandleTriggerExit = true;
                    break;
            }
        }

        void StoreCollisionInfo(Collider collisionInfo)
        {
            storeCollider.Value = collisionInfo.gameObject;
            gameObjectHit.Value = Fsm.TriggerCollider.gameObject;
           // Debug.Log(ignoreThis.GameObject.Value.name);
        }

        public override void DoTriggerEnter(Collider other)
        {
            gameObjectHit.Value = Fsm.TriggerCollider.gameObject;
            
             // Debug.Log("true");
            {
                gameObjectHit.Value = Fsm.TriggerCollider.gameObject;
               if (other.gameObject.tag == collideTag.Value && gameObjectHit.Value != ignoreThis.GameObject.Value)
                {
                    StoreCollisionInfo(other);
                    Fsm.Event(sendEvent);
                   // Debug.Log(gameObjectHit.Value.name);
                }
            }
        }

        public override void DoTriggerStay(Collider other)
        {
            if (trigger == TriggerType.OnTriggerStay)
            {
                gameObjectHit.Value = Fsm.TriggerCollider.gameObject;
                if (other.gameObject.tag == collideTag.Value && gameObjectHit.Value != ignoreThis.GameObject.Value)
                {
                    StoreCollisionInfo(other);
                    Fsm.Event(sendEvent);
                }
            }
        }

        public override void DoTriggerExit(Collider other)
        {
            if (trigger == TriggerType.OnTriggerExit && gameObjectHit.Value != ignoreThis.GameObject.Value)
            {
                gameObjectHit.Value = Fsm.TriggerCollider.gameObject;
                if (other.gameObject.tag == collideTag.Value)
                {
                    StoreCollisionInfo(other);
                    Fsm.Event(sendEvent);
                }
            }
        }

        public override string ErrorCheck()
        {
            return ActionHelpers.CheckOwnerPhysicsSetup(Owner);
        }


    }
}