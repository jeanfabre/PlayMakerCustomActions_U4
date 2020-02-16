// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Camera)]
    [Tooltip("Gets the ortho size of the Camera.")]
    public class GetCameraClippingPlane : ComponentAction<Camera>
    {

        [CheckForComponent(typeof(Camera))]
        [Tooltip("The GameObject with a Camera Component. If not defined will use the Main Camera")]
        public FsmOwnerDefault gameObject;

        [Tooltip("The near Clipping Plane.")]
        [UIHint(UIHint.Variable)]
        public FsmFloat nearClippingPlane;


        [Tooltip("The far Clipping Plane.")]
        [UIHint(UIHint.Variable)]
        public FsmFloat farClippingPlane;

        public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            nearClippingPlane = null;
            farClippingPlane = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            DoGetCameraClippingPlane();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoGetCameraClippingPlane();
        }

        void DoGetCameraClippingPlane()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                if (nearClippingPlane != null) nearClippingPlane.Value = camera.nearClipPlane;
                if(farClippingPlane != null) farClippingPlane.Value = camera.farClipPlane;
            }
        }
    }
}
