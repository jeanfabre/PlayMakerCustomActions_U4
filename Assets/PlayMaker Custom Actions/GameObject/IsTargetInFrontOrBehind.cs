// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Important code is from http://www.habrador.com/tutorials/linear-algebra/1-behind-or-in-front/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.GameObject)]
    [Tooltip("Checks to see if a target game object is in front or behind another game object. Stores results in bools and can fire events.")]
    public class IsTargetInFrontOrBehind : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The first game object")]
        public FsmGameObject baseObject;
        [Tooltip("The target game object.")]
        public FsmGameObject target;
        public FsmBool isFront;
        public FsmBool isBehind;
        public FsmEvent Behind;
        public FsmEvent Front;

   

        public override void Reset()
        {
            baseObject = null;
            isBehind = null;
            isFront = null;

            target = null;
        }

        public override void OnEnter()
        {
            DoInFront();
            Finish();
        }

        public override void OnUpdate()
        {
           
                DoInFront();

          

        }

        void DoInFront()
        {

            Vector3 youForward = baseObject.Value.transform.forward;
            Vector3 youToEnemy = target.Value.gameObject.transform.position - baseObject.Value.transform.position;
            float dotProduct = Vector3.Dot(youForward, youToEnemy);
    

            if (dotProduct >= 0f)
            {
                isFront = true;
                isBehind = false;
                Fsm.Event(Front);
            } 
            else
            {
                isFront = false;
                isBehind = true;
                Fsm.Event(Behind);
            }



        }

      



    }
}