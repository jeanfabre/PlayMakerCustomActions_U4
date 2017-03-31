using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Renderer)]
    public class enableMeshRenderer : FsmStateAction
    {
        public FsmOwnerDefault owner;
        public FsmBool enable;
        MeshRenderer theMesh;

        public override void Reset()
        {
            enable = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(owner);
            theMesh = go.GetComponent<MeshRenderer>();
            theMesh.enabled = enable.Value;
            Finish();
        }
    }




}