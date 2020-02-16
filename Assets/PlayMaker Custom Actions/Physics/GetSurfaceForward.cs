// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
// 
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Gets the surface forward from a cast.")]
    public class GetSurfaceForward : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Game Object that peforms the cast")]
        public FsmGameObject CasterGameObject;
        [Tooltip("World direction of caster object 0,1,0 would mean its Y axis is pointing up")]
        public FsmVector3 casterDirection;

        [UIHint(UIHint.Variable)]
        [Tooltip("Input the hit normal from a cast")]
        [Title("input hit normal")]
        public FsmVector3 hitNormal;

        private Vector3 source;
        private Vector3 forLookR;
        private Vector3 FromtoPiexe;
        private Quaternion startRot;
        private Quaternion Change;

        [UIHint(UIHint.Variable)]
        [Tooltip("Input the hit point from a cast")]
        [Title("input hit point")]
        public FsmVector3 hitPoint;

        public FsmVector3 surfaceForward;

        public bool everyFrame;


        public override void Reset()
        {
            hitNormal = null;
            hitPoint = null;
            CasterGameObject = null;
            casterDirection = null;
            source = Vector3.zero;
            forLookR = Vector3.zero;
            FromtoPiexe = Vector3.zero;
            startRot = new Quaternion(0, 0, 0, 0);
            Change = new Quaternion(0, 0, 0, 0);


        }

        void GetSurfaceFWD()
        {
            
            source = CasterGameObject.Value.transform.position;
            forLookR = hitPoint.Value - source;
            startRot = Quaternion.LookRotation(hitNormal.Value, forLookR);
            FromtoPiexe = startRot * casterDirection.Value;
            Change = Quaternion.FromToRotation(casterDirection.Value, FromtoPiexe);
            surfaceForward.Value = Change * casterDirection.Value;

        }

        public override void OnEnter()
        {
            GetSurfaceFWD();

            if (!everyFrame)
                Finish();


        }
        public override void OnUpdate()
        {
            GetSurfaceFWD();
        }

    }
}
