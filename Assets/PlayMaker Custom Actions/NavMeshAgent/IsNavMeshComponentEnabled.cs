// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.NavMeshAgent)]
    [Tooltip("Checks to see if Navmesh agent component is enabled. ")]
    public class IsNavMeshComponentEnabled : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(NavMeshAgent))]
        public FsmOwnerDefault gameObject;
        public FsmBool enabled;
        public FsmBool everyFrame;
        NavMeshAgent nm;
        public override void Reset()
        {
            gameObject = null;
            enabled = null;
            everyFrame = true;

        }

        public override void OnEnter()
        {
          


            if (!everyFrame.Value)
            {
                DoIsAgentEnabled();
                Finish();
            }

        }

        public override void OnUpdate()
        {
            if (everyFrame.Value)
            {
                DoIsAgentEnabled();
            }
        }

        void DoIsAgentEnabled()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

           

            nm = go.GetComponent<NavMeshAgent>();

         
            enabled.Value = nm.enabled;


        }

    }
}
