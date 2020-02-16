// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Set the size of boxCollider

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Set the size of a Box Collider")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12040.0")]
    public class GetBoxColliderSize : ComponentAction<Rigidbody>
    {
        [RequiredField]
        [CheckForComponent(typeof(BoxCollider))]
        [Tooltip("The GameObject to apply the size to.")]
        public FsmOwnerDefault gameObject;

        public FsmVector3 size;


        [Tooltip("size along the X axis. To leave unchanged, set to 'None'.")]
        public FsmFloat x;

        [Tooltip("size along the Y axis. To leave unchanged, set to 'None'.")]
        public FsmFloat y;

        [Tooltip("size along the Z axis. To leave unchanged, set to 'None'.")]
        public FsmFloat z;

        [Tooltip("Repeat every frame while the state is active.")]
        public FsmBool everyFrame;

        public override void Reset()
        {
            gameObject = null;
        
            x = new FsmFloat { UseVariable = true };
            y = new FsmFloat { UseVariable = true };
            z = new FsmFloat { UseVariable = true };
            everyFrame = false;
        }


        public override void OnEnter()
        {
            DoChange();

            if (!everyFrame.Value)
            {
                Finish();
            }
        }

        public override void OnFixedUpdate()
        {
            DoChange();
        }

        void DoChange()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            BoxCollider collider = go.GetComponent<BoxCollider>();
            //Debug.Log(collider);
            //Debug.Log(collider.size);

            size.Value = collider.size;

            x.Value = collider.size.x;
            y.Value = collider.size.y;
            z.Value = collider.size.z;





        }
    }
}
