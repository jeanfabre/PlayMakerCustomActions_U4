// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Gets information from the Host when a template is running as a Sub FSM.")]
	public class GetHostInfo : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		public FsmString hostFsmName;

		[UIHint(UIHint.Variable)]
		public FsmString gameObjectName;

		[UIHint(UIHint.Variable)]
		public FsmGameObject GameObject;

		[UIHint(UIHint.Variable)]
		public FsmString currentStateName;

		public bool everyFrame;

		public override void Reset()
		{
			hostFsmName = null;
			gameObjectName = null;
			GameObject = null;
			currentStateName = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			GetInfo();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			GetInfo();
		}

		public void GetInfo()
		{
			if (! hostFsmName.IsNone){
				hostFsmName.Value = Fsm.Host.Name;
			}

			if ( ! GameObject.IsNone)
			{
                GameObject.Value = Fsm.Host.GameObject;
			}

			if (! gameObjectName.IsNone){
				gameObjectName.Value = Fsm.Host.GameObjectName;
			}

			if (! currentStateName.IsNone){
				currentStateName.Value = Fsm.Host.ActiveStateName;
			}
		}
	}
}
