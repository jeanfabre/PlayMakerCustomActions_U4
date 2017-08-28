// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Detect collisions between the Owner of this FSM and other Game Objects that have RigidBody components.\nNOTE: The system events, COLLISION ENTER, COLLISION STAY, and COLLISION EXIT are sent automatically on collisions with any object. Use this action to filter collisions by Tag.")]
    public class CollisionEventPlus : FsmStateAction
    {
        public enum Options
        {
            ColliderOnly,
            LayerOnly,
            ColliderOrLayer,
            ColliderAndLayer
        }

        [Tooltip("The type of collision to detect.")]
        public CollisionType collision;


        [Tooltip("The math operation to perform on the floats.")]
        public Options options = Options.ColliderOnly;

        [UIHint(UIHint.Tag)]
        [Tooltip("Filter by Tag.")]
        public FsmString collideTag;

        [UIHint(UIHint.Layer)]
        [Tooltip("Filter by Layer.")]
        public FsmInt collideLayer;

        [Tooltip("Event to send if a collision is detected.")]
        public FsmEvent sendEvent;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the GameObject that collided with the Owner of this FSM.")]
        public FsmGameObject storeCollideObject;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the force of the collision. NOTE: Use Get Collision Info to get more info about the collision.")]
        public FsmFloat storeForce;

        public override void Reset()
        {
            collision = CollisionType.OnCollisionEnter;
            collideTag = new FsmString() { UseVariable = true };
            options = Options.ColliderOnly;
            sendEvent = null;
            collideLayer =  new FsmInt() { UseVariable = true };
            storeCollideObject = null;
            storeForce = null;
        }

        public override void OnPreprocess()
        {
            switch (collision)
            {
                case CollisionType.OnCollisionEnter:
                    Fsm.HandleCollisionEnter = true;
                    break;
                case CollisionType.OnCollisionStay:
                    Fsm.HandleCollisionStay = true;
                    break;
                case CollisionType.OnCollisionExit:
                    Fsm.HandleCollisionExit = true;
                    break;
                case CollisionType.OnControllerColliderHit:
                    Fsm.HandleControllerColliderHit = true;
                    break;
                case CollisionType.OnParticleCollision:
                    Fsm.HandleParticleCollision = true;
                    break;

            }

        }

        void StoreCollisionInfo(Collision collisionInfo)
        {
            storeCollideObject.Value = collisionInfo.gameObject;
            if(storeForce != null) storeForce.Value = collisionInfo.relativeVelocity.magnitude;

        }

        public override void DoCollisionEnter(Collision collisionInfo)
        {
            switch (options)
            {
                case Options.ColliderOnly:
                    //set
                    if (collision == CollisionType.OnCollisionEnter)
                    {
                        if (collisionInfo.collider.gameObject.tag == collideTag.Value)
                        {
                            StoreCollisionInfo(collisionInfo);
                            Fsm.Event(sendEvent);
                        }
                    }
                    break;
                case Options.LayerOnly:
                    //set
                    if (collision == CollisionType.OnCollisionEnter)
                    {
                        if (collisionInfo.collider.gameObject.layer == collideLayer.Value)
                        {
                            StoreCollisionInfo(collisionInfo);
                            Fsm.Event(sendEvent);
                        }
                    }
                    break;
                case Options.ColliderOrLayer:
                    //set
                    if (collision == CollisionType.OnCollisionEnter)
                    {
                        if (collisionInfo.collider.gameObject.tag == collideTag.Value || collisionInfo.collider.gameObject.layer == collideLayer.Value)
                        {
                            StoreCollisionInfo(collisionInfo);
                            Fsm.Event(sendEvent);
                        }
                    }
                    break;
                case Options.ColliderAndLayer:
                    //set
                    if (collision == CollisionType.OnCollisionEnter)
                    {
                        if (collisionInfo.collider.gameObject.tag == collideTag.Value && collisionInfo.collider.gameObject.layer == collideLayer.Value)
                        {
                            StoreCollisionInfo(collisionInfo);
                            Fsm.Event(sendEvent);
                        }
                    }
                    break;
            }
            
        }

        public override void DoCollisionStay(Collision collisionInfo)
        {
            switch (options)
            {
                case Options.ColliderOnly:
                    //set
                    if (collision == CollisionType.OnCollisionStay)
                    {
                        if (collisionInfo.collider.gameObject.tag == collideTag.Value)
                        {
                            StoreCollisionInfo(collisionInfo);
                            Fsm.Event(sendEvent);
                        }
                    }
                    break;
                case Options.LayerOnly:
                    //set
                    if (collision == CollisionType.OnCollisionStay)
                    {
                        if (collisionInfo.collider.gameObject.layer == collideLayer.Value)
                        {
                            StoreCollisionInfo(collisionInfo);
                            Fsm.Event(sendEvent);
                        }
                    }
                    break;
                case Options.ColliderOrLayer:
                    //set
                    if (collision == CollisionType.OnCollisionStay)
                    {
                        if (collisionInfo.collider.gameObject.tag == collideTag.Value || collisionInfo.collider.gameObject.layer == collideLayer.Value)
                        {
                            StoreCollisionInfo(collisionInfo);
                            Fsm.Event(sendEvent);
                        }
                    }
                    break;
                case Options.ColliderAndLayer:
                    //set
                    if (collision == CollisionType.OnCollisionStay)
                    {
                        if (collisionInfo.collider.gameObject.tag == collideTag.Value && collisionInfo.collider.gameObject.layer == collideLayer.Value)
                        {
                            StoreCollisionInfo(collisionInfo);
                            Fsm.Event(sendEvent);
                        }
                    }
                    break;
            }
        }

        public override void DoCollisionExit(Collision collisionInfo)
        {
            switch (options)
            {
                case Options.ColliderOnly:
                    //set
                    if (collision == CollisionType.OnCollisionExit)
                    {
                        if (collisionInfo.collider.gameObject.tag == collideTag.Value)
                        {
                            StoreCollisionInfo(collisionInfo);
                            Fsm.Event(sendEvent);
                        }
                    }
                    break;
                case Options.LayerOnly:
                    //set
                    if (collision == CollisionType.OnCollisionExit)
                    {
                        if (collisionInfo.collider.gameObject.layer == collideLayer.Value)
                        {
                            StoreCollisionInfo(collisionInfo);
                            Fsm.Event(sendEvent);
                        }
                    }
                    break;
                case Options.ColliderOrLayer:
                    //set
                    if (collision == CollisionType.OnCollisionExit)
                    {
                        if (collisionInfo.collider.gameObject.tag == collideTag.Value || collisionInfo.collider.gameObject.layer == collideLayer.Value)
                        {
                            StoreCollisionInfo(collisionInfo);
                            Fsm.Event(sendEvent);
                        }
                    }
                    break;
                case Options.ColliderAndLayer:
                    //set
                    if (collision == CollisionType.OnCollisionExit)
                    {
                        if (collisionInfo.collider.gameObject.tag == collideTag.Value && collisionInfo.collider.gameObject.layer == collideLayer.Value)
                        {
                            StoreCollisionInfo(collisionInfo);
                            Fsm.Event(sendEvent);
                        }
                    }
                    break;
            }
        }

        public override void DoControllerColliderHit(ControllerColliderHit collisionInfo)
        {
            if (collision == CollisionType.OnControllerColliderHit)
            {
                switch (options)
                {
                    case Options.ColliderOnly:
                        //set
                        if (collisionInfo.collider.gameObject.tag == collideTag.Value)
                        {
                            if (storeCollideObject != null)
                                storeCollideObject.Value = collisionInfo.gameObject;

                            storeForce.Value = 0f; //TODO: impact force?
                            Fsm.Event(sendEvent);
                        }
                        break;
                    case Options.LayerOnly:
                        //set
                        if (collisionInfo.collider.gameObject.layer == collideLayer.Value)
                        {
                            if (storeCollideObject != null)
                                storeCollideObject.Value = collisionInfo.gameObject;

                            storeForce.Value = 0f; //TODO: impact force?
                            Fsm.Event(sendEvent);
                        }
                        break;
                    case Options.ColliderOrLayer:
                        //set
                        if (collisionInfo.collider.gameObject.tag == collideTag.Value || collisionInfo.collider.gameObject.layer == collideLayer.Value)
                        {
                            if (storeCollideObject != null)
                                storeCollideObject.Value = collisionInfo.gameObject;

                            storeForce.Value = 0f; //TODO: impact force?
                            Fsm.Event(sendEvent);
                        }
                        break;
                    case Options.ColliderAndLayer:
                        //set
                        if (collisionInfo.collider.gameObject.tag == collideTag.Value && collisionInfo.collider.gameObject.layer == collideLayer.Value)
                        {
                            if (storeCollideObject != null)
                                storeCollideObject.Value = collisionInfo.gameObject;

                            storeForce.Value = 0f; //TODO: impact force?
                            Fsm.Event(sendEvent);
                        }
                        break;
                }
                
            }
        }

        public override void DoParticleCollision(GameObject other)
        {
            if (collision == CollisionType.OnParticleCollision)
            {
                switch (options)
                {
                    case Options.ColliderOnly:
                        //set
                        if (other.collider.gameObject.tag == collideTag.Value)
                        {
                            if (other.tag == collideTag.Value)
                            {
                                if (storeCollideObject != null)
                                    storeCollideObject.Value = other;

                                storeForce.Value = 0f; //TODO: impact force?
                                Fsm.Event(sendEvent);
                            }
                        }
                        break;
                    case Options.LayerOnly:
                        //set
                        if (other.collider.gameObject.layer == collideLayer.Value)
                        {
                            if (other.tag == collideTag.Value)
                            {
                                if (storeCollideObject != null)
                                    storeCollideObject.Value = other;

                                storeForce.Value = 0f; //TODO: impact force?
                                Fsm.Event(sendEvent);
                            }
                        }
                        break;
                    case Options.ColliderOrLayer:
                        //set
                        if (other.collider.gameObject.tag == collideTag.Value || other.collider.gameObject.layer == collideLayer.Value)
                        {
                            if (other.tag == collideTag.Value)
                            {
                                if (storeCollideObject != null)
                                    storeCollideObject.Value = other;

                                storeForce.Value = 0f; //TODO: impact force?
                                Fsm.Event(sendEvent);
                            }
                        }
                        break;
                    case Options.ColliderAndLayer:
                        //set
                        if (other.collider.gameObject.tag == collideTag.Value && other.collider.gameObject.layer == collideLayer.Value)
                        {
                            if (other.tag == collideTag.Value)
                            {
                                if (storeCollideObject != null)
                                    storeCollideObject.Value = other;

                                storeForce.Value = 0f; //TODO: impact force?
                                Fsm.Event(sendEvent);
                            }
                        }
                        break;
                }
            }
        }

        public override string ErrorCheck()
        {
            return ActionHelpers.CheckOwnerPhysicsSetup(Owner);
        }
    }
}