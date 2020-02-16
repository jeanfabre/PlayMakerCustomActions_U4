// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Set the size of a Circle Collider 2D")]
    [HelpUrl("")]
    public class SetCircleCollider2DRadius : ComponentAction<Rigidbody>
    {
        [RequiredField]
        [CheckForComponent(typeof(CircleCollider2D))]
        [Tooltip("The GameObject to apply the size to.")]
        public FsmOwnerDefault gameObject;
	
		[Tooltip("Circle radius")]
        public FsmFloat radius;

        [Tooltip("Repeat every frame while the state is active.")]
        public FsmBool everyFrame;

        public override void Reset()
        {
            gameObject = null;
			radius = null;
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
            CircleCollider2D collider = go.GetComponent<CircleCollider2D>();
			collider.radius = radius.Value;
                   

        }
    }
}
