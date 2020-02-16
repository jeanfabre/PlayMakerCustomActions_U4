// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//FrankenCopyCoded by MDS
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: knockback vector3 cast

using UnityEngine;


namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Vector3)]
    [Tooltip("Gets the position to knock an object back to on impact. Input a hitPosition and hitNormal from a cast on the projectile that hits the object. The Y axis is ignored. You can easily add it back.")]
    public class KnockBackPositionFinder : FsmStateAction
    {
      
      
        public FsmVector3 hitPoint;
        public FsmVector3 hitNormal;
        public FsmFloat KnockBackDistance;
        public FsmVector3 knockBackPosition;
        private Vector3 preKnock;
        private Ray Ayray;
                
        public bool everyFrame;

        public override void Reset()
        {
            hitNormal = null;
            hitPoint = null;
            knockBackPosition = null;
            KnockBackDistance = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            Ayray = new Ray(hitPoint.Value - hitNormal.Value * 1, hitNormal.Value * -1);
            preKnock = Ayray.GetPoint(KnockBackDistance.Value);
            knockBackPosition.Value = new Vector3(preKnock.x, 0, preKnock.z);
        

            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            Ayray = new Ray(hitPoint.Value - hitNormal.Value * 1, hitNormal.Value * -1);
            preKnock = Ayray.GetPoint(KnockBackDistance.Value);
            knockBackPosition.Value = new Vector3(preKnock.x, 0, preKnock.z);
        }
    }
}

