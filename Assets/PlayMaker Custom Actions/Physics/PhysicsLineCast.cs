// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Performs a lineCast hit ")]
	public class PhysicsLineCast : FsmStateAction
	{
		
		[ActionSection("Raycast Settings")] 
		
		[Tooltip("The line start of the sweep. \nOr use FromPosition parameter.")]
		public FsmOwnerDefault fromGameObject;

		[Tooltip("The line start of the sweep. \nOr use FromGameObject parameter.")]
		public FsmVector3 fromPosition;
	
		
		[Tooltip("The end position of the linecast sweep. \nOr use ToPosition parameter.")]
		public FsmGameObject toGameObject;
		
		[Tooltip("The end position of the linecast sweep. \nOr use ToGameObject parameter.")]
		public FsmVector3 toPosition;

		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;
		
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;
		
		[ActionSection("Result")] 
		
		public FsmBool hit;
		
		[Tooltip("Event to send when there are no more hits to loop.")]
		public FsmEvent hitEvent;
		
		[Tooltip("Event to send if there is no hit at all")]
		public FsmEvent noHitEvent;
		
		

		public override void Reset()
		{
			
			fromGameObject = null;
			fromPosition = new FsmVector3 { UseVariable = true };
			toGameObject = null;
			toPosition = new FsmVector3 { UseVariable = true };
			
			
			layerMask = new FsmInt[0];
			invertMask = false;
			
			hitEvent = null;
			noHitEvent = null;
		}
		

		
		
		public override void OnEnter()
		{

			
			bool _hit = DoLineCast();
			
			hit.Value = _hit;
			
			if (_hit)
			{
				Fsm.Event(hitEvent);
				
			}else{
				Fsm.Event(noHitEvent);
			}
		
			Finish();
		}
		
		
		bool DoLineCast()
		{
			var go = Fsm.GetOwnerDefaultTarget(fromGameObject);
			
			Vector3 startPos = go != null ? go.transform.position : fromPosition.Value;
			Vector3 endPos =  toGameObject.Value != null ? toGameObject.Value .transform.position : toPosition.Value;
			
			RaycastHit rhit;
			
			bool _hit = Physics.Linecast(startPos,endPos,out rhit, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value));
			Fsm.RaycastHitInfo = rhit;
			
			return _hit;
		}
		
		
	}
}
