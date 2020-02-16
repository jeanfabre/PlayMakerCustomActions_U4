// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Performs a OverlapSphere Each time this action is called it gets the next hit. This lets you quickly loop through all the hits to perform actions on them.")]
	public class GetNextPhysicsOverlapSphere : FsmStateAction
	{
		
		[ActionSection("Settings")] 
		
		[Tooltip("The center of the sphere at the start of the sweep. \nOr use From Position parameter.")]
		public FsmOwnerDefault fromGameObject;

		[Tooltip("The center of the sphere at the start of the sweep. \nOr use Game Object parameter.")]
		public FsmVector3 fromPosition;
		
		[Tooltip("The radius of the shpere.")]
		public FsmFloat radius;

		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;
		
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;

		
		[ActionSection("Result")] 
		
		[RequiredField]
		[Tooltip("The overlaping GameObject for this iteration")]
		public FsmGameObject overlapingGameObject;
		
		[RequiredField]
		[Tooltip("Event to send to get the next child.")]
		public FsmEvent loopEvent;

		[Tooltip("Event to send if there is no overlap at all")]
		public FsmEvent noOverlapEvent;
		
		[RequiredField]
		[Tooltip("Event to send when there are no more hits to loop.")]
		public FsmEvent finishedEvent;

		public override void Reset()
		{
			
			fromGameObject = null;
			fromPosition = new FsmVector3 { UseVariable = true };
			radius = 1f;
	
			layerMask = new FsmInt[0];
			invertMask = false;
		
			overlapingGameObject = null;
			
			loopEvent = null;
			finishedEvent = null;
			noOverlapEvent = null;
		}
		
		// cache the hits
		private Collider[] overlaps;
		
		// increment a overlap index as we loop through the overlaps
		private int nextOverlapIndex;
		
		
		public override void OnEnter()
		{
			
			if (nextOverlapIndex==0)
			{
				DoOverlap();
			}
			
			if (overlaps.Length==0)
			{
				nextOverlapIndex = 0;
				Fsm.Event(noOverlapEvent);
				Fsm.Event(finishedEvent);
				Finish();
				return;
			}
			
			if (nextOverlapIndex>=overlaps.Length)
			{
				nextOverlapIndex = 0;
				Fsm.Event(finishedEvent);
				Finish();
				return;
			}
			
			Debug.Log("getting index"+nextOverlapIndex );
			
			overlapingGameObject.Value = overlaps[nextOverlapIndex].gameObject;
			
			nextOverlapIndex++;
			
			Fsm.Event(loopEvent);
			
			Finish();
		}
		
		
		void DoOverlap()
		{
			Debug.Log("DoOverlap");
			
			var go = Fsm.GetOwnerDefaultTarget(fromGameObject);
			
			var originPos = go != null ? go.transform.position : fromPosition.Value;
		
			overlaps = Physics.OverlapSphere(originPos,radius.Value, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value));
			
			if (overlaps.Length==0)
			{
				Fsm.Event(noOverlapEvent);
			}
			
		}
		
		
	}
}
