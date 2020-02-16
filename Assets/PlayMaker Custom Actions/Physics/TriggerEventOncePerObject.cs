// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: trigger once event

using System;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("The same functionality as Trigger Event except a specific game object will only register as a trigger event once. ")]
    public class TriggerEventOncePerObject : FsmStateAction
    {
        // Might add ignore functionality from trigger event ignore action later
        //[Tooltip("The gameobject you want the trigger to ignore.")]
        //public FsmOwnerDefault ignoreThis;
        //public FsmBool ignoreGameObject;

        public TriggerType trigger;
        [UIHint(UIHint.Tag)]
        public FsmString collideTag;
        public FsmEvent sendEvent;
        [UIHint(UIHint.Variable)]
        public FsmGameObject storeCollider;

        [Tooltip("If you want to reset the iteration, raise this flag to true when you enter the state, it will indicate you want to start from the beginning again")]
        public FsmBool resetFlag;

        [Tooltip("The gameobject that activated the trigger. Use this to debug or instead of an additional get trigger info action.")]
        public FsmGameObject gameObjectHit;

        private List<GameObject> alreadyHit;


        public override void Reset()
        {
            trigger = TriggerType.OnTriggerEnter;
            collideTag = "Untagged";
            sendEvent = null;
            storeCollider = null;
            resetFlag = null;

            gameObjectHit = null;
            alreadyHit = null;
          
        }

        public override void OnPreprocess()
        {
            alreadyHit.Clear();
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

        }

        public override void DoTriggerEnter(Collider other)
        {
            if (resetFlag.Value)
            {
                alreadyHit.Clear();
                resetFlag.Value = false;
            }
            gameObjectHit.Value = Fsm.TriggerCollider.gameObject;


            {
                gameObjectHit.Value = Fsm.TriggerCollider.gameObject;

                if (other.gameObject.tag == collideTag.Value & !(alreadyHit.Contains(gameObjectHit.Value)))
                {
                    StoreCollisionInfo(other);
                    Fsm.Event(sendEvent);
                    alreadyHit.Add(gameObjectHit.Value);
                }

            }
        }

        public override void DoTriggerStay(Collider other)
        {
            if (trigger == TriggerType.OnTriggerStay)
            {
                if (resetFlag.Value)
                {
                    alreadyHit.Clear();
                    resetFlag.Value = false;
                }
                gameObjectHit.Value = Fsm.TriggerCollider.gameObject;

                if (other.gameObject.tag == collideTag.Value & !(alreadyHit.Contains(gameObjectHit.Value)))
                {
                    StoreCollisionInfo(other);
                    Fsm.Event(sendEvent);
                    alreadyHit.Add(gameObjectHit.Value);
                }

            }
        }

        public override void DoTriggerExit(Collider other)
        {
            if (trigger == TriggerType.OnTriggerExit)
            {
                if (resetFlag.Value)
                {
                    alreadyHit.Clear();
                    resetFlag.Value = false;
                }
                gameObjectHit.Value = Fsm.TriggerCollider.gameObject;

                if (other.gameObject.tag == collideTag.Value & !(alreadyHit.Contains(gameObjectHit.Value)))
                {
                    StoreCollisionInfo(other);
                    Fsm.Event(sendEvent);
                    alreadyHit.Add(gameObjectHit.Value);
                }

            }
        }

        public override string ErrorCheck()
        {
            return ActionHelpers.CheckOwnerPhysicsSetup(Owner);
        }


    }
}
