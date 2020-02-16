// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Send event to all the GameObjects within an array.")]
	public class ArraySendEventToGameObjects : FsmStateAction
	{

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;

		[RequiredField]
		[Tooltip("The event to send. NOTE: Events must be marked Global to send between FSMs.")]
		public FsmEvent sendEvent;
		
		public FsmBool excludeSelf;
		
		public FsmBool sendToChildren;
	
		FsmEventTarget _eventTarget;
		FsmOwnerDefault owner;

		public override void Reset()
		{
			array = null;
			sendEvent = null;
			excludeSelf = false;
			sendToChildren = false;
		}
		
		
		public override void OnEnter()
		{

			DoSendEvent();

			Finish ();
		}

		void DoSendEvent()
		{
			foreach(GameObject _go in array.objectReferences)
			{
				sendEventToGO(_go);
			}
			
		}
		
		void sendEventToGO(GameObject _go)
		{
			_eventTarget = new FsmEventTarget();
			_eventTarget.excludeSelf = excludeSelf.Value;
			owner = new FsmOwnerDefault();
			owner.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			owner.GameObject = new FsmGameObject();
			owner.GameObject.Value = _go;
			_eventTarget.gameObject = owner;
			_eventTarget.target = FsmEventTarget.EventTarget.GameObject;	
				
			_eventTarget.sendToChildren = sendToChildren.Value;
			
			Fsm.Event(_eventTarget,sendEvent);
		}
		
	}
}