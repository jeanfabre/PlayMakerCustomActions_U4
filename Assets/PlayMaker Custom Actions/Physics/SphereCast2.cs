// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Performs a sphereCast hit")]
	public class SphereCast2 : FsmStateAction
	{		
		[Tooltip("The center of the sphere at the start of the sweep. \nOr use From Position parameter.")]
		public FsmOwnerDefault fromGameObject;

		[Tooltip("The center of the sphere at the start of the sweep. \nOr use Game Object parameter.")]
		public FsmVector3 fromPosition;

        [Tooltip("The radius of the shpere.")]
        public FsmFloat radius;

        [Tooltip("The direction into which to sweep the sphere.")]
        public FsmVector3 direction;

        [Tooltip("Cast the sphere in world or local space. Note if no Game Object is specified, the direction is in world space.")]
        public Space space;

		[Tooltip("The length of the ray. Set to -1 for infinity.")]
		public FsmFloat maxDistance;


        [ActionSection("Result")]

        [Tooltip("Event to send when there is a hit.")]
        public FsmEvent hitEvent;

        [Tooltip("Event to send if there is no hit at all")]
        public FsmEvent noHitEvent;

        [Tooltip("Set a bool variable to true if hit something, otherwise false.")]
        [UIHint(UIHint.Variable)]
        public FsmBool storeDidHit;

        [Tooltip("Store the game object hit in a variable.")]
        [UIHint(UIHint.Variable)]
        public FsmGameObject storeHitObject;

        [UIHint(UIHint.Variable)]
        [Tooltip("Get the world position of the ray hit point and store it in a variable.")]
        public FsmVector3 storeHitPoint;

        [UIHint(UIHint.Variable)]
        [Tooltip("Get the normal at the hit point and store it in a variable.")]
        public FsmVector3 storeHitNormal;

        [UIHint(UIHint.Variable)]
        [Tooltip("Get the distance along the ray to the hit point and store it in a variable.")]
        public FsmFloat storeHitDistance;


        [Tooltip("this can be used with the wiresphere tester")]
        public FsmVector3 sphereCastCenter;



        [ActionSection("Filter")]

        [Tooltip("Set how often to cast a ray. 0 = once, don't repeat; 1 = everyFrame; 2 = every other frame... \nSince raycasts can get expensive use the highest repeat interval you can get away with.")]
        public FsmInt repeatInterval;

        [UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;
		
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;

        public QueryTriggerInteraction triggerInteraction;

        [ActionSection("Debug")] 
		
		[Tooltip("The color to use for the debug line.")]
		public FsmColor debugMaxDistanceColor;

        [Tooltip("The color to use for the debug line.")]
        public FsmColor debugCurrentDistanceColor;

        [Tooltip("Draw a debug line. Note: Check Gizmos in the Game View to see it in game.")]
		public FsmBool debug;

        private int repeat;
        private Vector3 originPos;

        public override void Reset()
		{
			
			fromGameObject = null;
			fromPosition = new FsmVector3 { UseVariable = true };
			direction = Vector3.forward;
			space = Space.Self;
			maxDistance = 100;
            radius = 1f;
            hitEvent = null;
            noHitEvent = null;
            storeHitObject = null;
            storeHitPoint = null;
            storeHitNormal = null;
            storeHitDistance = null;
            repeatInterval = 1;
            layerMask = new FsmInt[0];
			invertMask = false;
            triggerInteraction = QueryTriggerInteraction.UseGlobal;
            debugMaxDistanceColor = Color.yellow;
            debugCurrentDistanceColor = Color.red;
            debug = false;
            sphereCastCenter = null;


        }

		public override void OnEnter()
		{
            DoSphereCast();

            if (repeatInterval.Value == 0)
            {
                
                Finish();
            }
		}

        public override void OnUpdate()
        {
            
            repeat--;

            if (repeat == 0)
            {
                DoSphereCast();
            }
        }

        void DoSphereCast()
		{
            repeat = repeatInterval.Value;

            if (maxDistance.Value == 0)
			{
				return;
			}

			var go = Fsm.GetOwnerDefaultTarget(fromGameObject);
			
			originPos = go != null ? go.transform.position : fromPosition.Value;
			
			var rayLength = Mathf.Infinity;
			if (maxDistance.Value > 0 )
			{
				rayLength = maxDistance.Value;
			}

			var dirVector = direction.Value;
			if(go != null && space == Space.Self)
			{
				dirVector = go.transform.TransformDirection(direction.Value);
			}
			
			
			RaycastHit hitInfo;
			Physics.SphereCast(originPos,radius.Value, dirVector,out hitInfo, rayLength, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value), triggerInteraction);	

			Fsm.RaycastHitInfo = hitInfo;
			
			var didHit = hitInfo.collider != null;
			
			storeDidHit.Value = didHit;
            
            if (didHit)
            {
                storeHitObject.Value = hitInfo.transform.gameObject;
                storeHitPoint.Value = Fsm.RaycastHitInfo.point;
                storeHitNormal.Value = Fsm.RaycastHitInfo.normal;
                storeHitDistance.Value = Fsm.RaycastHitInfo.distance;
                Fsm.Event(hitEvent);
            }
            if (!didHit)
            {
                storeHitDistance.Value = maxDistance.Value;
                storeHitObject.Value = null;
                Fsm.Event(noHitEvent);
            }
            sphereCastCenter.Value = originPos + (direction.Value.normalized * storeHitDistance.Value);
            if (debug.Value)
            {
                Debug.DrawLine(originPos, originPos + (direction.Value.normalized * maxDistance.Value), debugMaxDistanceColor.Value);
                Debug.DrawLine(originPos, originPos + (direction.Value.normalized * storeHitDistance.Value), debugCurrentDistanceColor.Value);
            }
        }
	}
}