// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Adds a value to an fsm Integer Variable.")]
	public class AddToFsmInt : FsmStateAction
	{
		
		[RequiredField]
		 [Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;
		
		[RequiredField]
		[UIHint(UIHint.FsmInt)]
		public FsmString variableName;

		[RequiredField]
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public FsmInt add;
		
		[Tooltip("Optional storage of the result")]
		[UIHint(UIHint.Variable)]
		public FsmInt storeResult;
		
		 [Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;
		
		GameObject goLastFrame;
		PlayMakerFSM fsm;
		
		public override void Reset()
		{
			gameObject = null;
			add = null;
			fsmName = "";
			storeResult = null;
		}

		public override void OnEnter()
		{
			DoAddToFsmInt();

			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoAddToFsmInt();
		}
		
		void DoAddToFsmInt()
		{
			
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;

			// only get the fsm component if go has changed

			if (go != goLastFrame)
			{
				goLastFrame = go;
				fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
			}
			
			if (fsm == null) return;
			
			FsmInt fsmInt = fsm.FsmVariables.GetFsmInt(variableName.Value);
			
			if (fsmInt == null) return;
			
			fsmInt.Value += add.Value;
			
			if (storeResult != null){
				storeResult.Value = fsmInt.Value;
			}
			
		}// DoAddToFsmInt
	}
}