// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10009.msg47455#msg47455

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Tests if the value of a another FSM Bool Variable has changed.")]
	public class FsmBoolChange : FsmStateAction
	{
		[ActionSection("Get Fsm Bool")]
		[RequiredField]
		public FsmOwnerDefault gameObject;
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;
		[RequiredField]
		[UIHint(UIHint.FsmBool)]
		public FsmString variableName;

		[ActionSection("Setup")]
		[Tooltip("Check Everyframe")]
		public bool everyFrame;
		[Tooltip("Set the expection that bool should be. Will not work if EveryFrame is on (will set to null)")]
		[UIHint(UIHint.FsmBool)]
		public FsmBool originalValue;

		[ActionSection("Did Fsm Bool change")]
		[Tooltip("Event to send if the variable changes.")]
		public FsmEvent changedEvent;
		[Tooltip("Event to send if the variable did not change. Will not work if EveryFrame is on (will set to null)")]
		public FsmEvent notChangedEvent;

		[ActionSection("Data")]
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Set to True if changed.")]
		[Title("Store Change Result")]
		public FsmBool storeValue;

		[UIHint(UIHint.FsmBool)]
		[Tooltip("Get Fsm Bool Value")]
		[Title("Store Fsm Value")]
		public FsmBool storeFsmValue;


		bool previousValue;
		GameObject goLastFrame;
		PlayMakerFSM fsm;
		
		public override void Reset()
		{
			gameObject = null;
			fsmName = "";
			storeValue = null;
			changedEvent = null;
			notChangedEvent = null;
			originalValue = null;
			storeFsmValue= null;
			everyFrame = true;
		}

		public override void OnEnter()
		{

			if (everyFrame){
				notChangedEvent = null;
				originalValue = null;
			}

			DoGetFsmBool();

			if (!everyFrame){
				previousValue = originalValue.Value;
				
			}

			else{
				previousValue = storeFsmValue.Value;
			}

			BoolChanged();

			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			
			DoGetFsmBool();
			
			BoolChanged();
		}

		void DoGetFsmBool()
		{

			if (storeFsmValue == null) return;

			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;

			if (go != goLastFrame)
			{
				goLastFrame = go;
				fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
			}			
			
			if (fsm == null) return;
			
			FsmBool fsmBool = fsm.FsmVariables.GetFsmBool(variableName.Value);
			
			if (fsmBool == null) return;
			
			storeFsmValue.Value = fsmBool.Value;

			return;

		}

		void BoolChanged()
		{

			storeValue.Value = false;

			if (storeFsmValue.Value != previousValue & changedEvent != null)
			{
				storeValue.Value = true;
				Fsm.Event(changedEvent);
			}

			else if (!everyFrame & notChangedEvent != null){
				storeValue.Value = false;
				Fsm.Event(notChangedEvent);
			}
			return;
		}
	}
}