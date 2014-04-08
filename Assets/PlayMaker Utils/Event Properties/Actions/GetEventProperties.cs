// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Gets properties on the last event that caused a state change. Use Set Event Properties to define these values when sending events")]
	public class GetEventProperties : FsmStateAction
	{
	
		[CompoundArray("Event Properties", "Key", "Data")]
		public FsmString[] keys;
		[UIHint(UIHint.Variable)]
		public FsmVar[] datas;

		public override void Reset()
		{
			keys = new FsmString[1];
			datas = new FsmVar[1];
		}

		public override void OnEnter()
		{
		
			try{
				if (SetEventProperties.properties == null)
				{
					throw new System.ArgumentException("no properties");
				}
				
			
				for (int i = 0; i < keys.Length; i++) 
				{
					Debug.Log(keys[i].Value);
					
					if (SetEventProperties.properties.ContainsKey(keys[i].Value))
					{
						Debug.Log("found");
						PlayMakerUtils.ApplyValueToFsmVar(this.Fsm,datas[i],SetEventProperties.properties[keys[i].Value]);
					}else{
						Debug.Log("not found");
					}
				}
				
			}catch(Exception e)
			{
				Debug.Log("no properties found "+e);
			}
			
			Finish();
		}
	}
}