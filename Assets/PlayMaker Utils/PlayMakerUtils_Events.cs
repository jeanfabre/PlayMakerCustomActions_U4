//	(c) Jean Fabre, 2013 All rights reserved.
//	http://www.fabrejean.net
//  contact: http://www.fabrejean.net/contact.htm
//
// Version Alpha 0.1

// INSTRUCTIONS
// This set of utils is here to help custom action development, and scripts in general that wants to connect and work with PlayMaker API.


using UnityEngine;

using HutongGames.PlayMaker;

public partial class PlayMakerUtils {
	
	public static void SendEventToGameObject(PlayMakerFSM fromFsm,GameObject target,string fsmEvent)
	{
		SendEventToGameObject(fromFsm,target,fsmEvent,null);
	}
	
	public static void SendEventToGameObject(PlayMakerFSM fromFsm,GameObject target,string fsmEvent,FsmEventData eventData)
	{
		if (eventData!=null)
		{
			HutongGames.PlayMaker.Fsm.EventData = eventData;
		}
		
		FsmEventTarget _eventTarget = new FsmEventTarget();
		_eventTarget.excludeSelf = false;
		FsmOwnerDefault owner = new FsmOwnerDefault();
		owner.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
		owner.GameObject = new FsmGameObject();
		owner.GameObject.Value = target;
		_eventTarget.gameObject = owner;
		_eventTarget.target = FsmEventTarget.EventTarget.GameObject;	
			
		_eventTarget.sendToChildren = false;
		
		fromFsm.Fsm.Event(_eventTarget,fsmEvent);
	}
}
