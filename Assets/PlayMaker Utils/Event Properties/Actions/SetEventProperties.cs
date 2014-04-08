// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;



namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sets Event Data before sending an event. Get the Event Data, Get Event Properties action.")]
	public class SetEventProperties : FsmStateAction
	{
	
		[CompoundArray("Event Properties", "Key", "Data")]
		public FsmString[] keys;
		public FsmVar[] datas;
		
		public static Dictionary<string,object> properties;
		
		
		public override void Reset()
		{
			keys = new FsmString[1];
			datas = new FsmVar[1];
			
		}

		public override void OnEnter()
		{

			properties = new Dictionary<string, object>();
			
			for (int i = 0; i < keys.Length; i++) 
			{
				properties[keys[i].Value] = PlayMakerUtils.GetValueFromFsmVar(this.Fsm,datas[i]);
				//UnityEngine.Debug.Log(keys[i].Value+" "+_props.properties[keys[i].Value]);
			}
			
			Finish();
		}
	}
}