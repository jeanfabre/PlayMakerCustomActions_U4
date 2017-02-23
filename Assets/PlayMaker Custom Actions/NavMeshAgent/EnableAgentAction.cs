

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.NavMeshAgent)]
    [Tooltip("Enables/Disables Navmesh agent component. ")]
    public class EnableAgentAction : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(NavMeshAgent))]
        public FsmOwnerDefault gameObject;
        public FsmBool enable;   
        public FsmBool everyFrame;
        NavMeshAgent nm;
        public override void Reset()
        {
            gameObject = null;
            enable = null;
            everyFrame = true;

        }

        public override void OnEnter()
        {
            if (!everyFrame.Value)
            {
                DoEnableAgent();
                Finish();
            }

        }

        public override void OnUpdate()
        {
            if (everyFrame.Value)
            {
                DoEnableAgent();
            }
        }

        void DoEnableAgent()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            if(go.GetComponent<NavMeshAgent>() == null)
            {
                return;
            }

            nm = go.GetComponent<NavMeshAgent>();

            nm.enabled = enable.Value;


        }

    }
}
