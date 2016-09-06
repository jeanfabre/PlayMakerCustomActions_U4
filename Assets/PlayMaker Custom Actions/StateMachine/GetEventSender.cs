// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Gets sender info on the last event that caused a state change.")]
	public class GetEventSender : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		public FsmGameObject sentByGameObject;
		
		[UIHint(UIHint.Variable)]
		public FsmString fsmName;

		public override void Reset()
		{
			sentByGameObject = null;
			fsmName = null;
		}

		public override void OnEnter()
		{
			if (Fsm.EventData.SentByFsm != null)
			{
				sentByGameObject.Value = Fsm.EventData.SentByFsm.GameObject;
				fsmName.Value = Fsm.EventData.SentByFsm.Name;
			}
			else
			{
				sentByGameObject.Value = null;
				fsmName.Value = "";
			}

			Finish();
		}
	}
}