// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
using UnityEngine;
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Subtract the value of a Float Variable in another FSM.")]
	public class SubtractFsmFloat : FsmStateAction
	{
		[RequiredField]
        [Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		[RequiredField]
		[UIHint(UIHint.FsmFloat)]
        [Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

		[RequiredField]
        [Tooltip("Subtract this from the target variable.")]
		public FsmFloat subtractValue;

        [Tooltip("Repeat every frame. Useful if the value is changing.")]
        public bool everyFrame;
		
		[Tooltip("Use with Every Frame only to continue over time")]
		public bool perSecond;

		GameObject goLastFrame;
		PlayMakerFSM fsm;
		
		public override void Reset()
		{
			gameObject = null;
			perSecond = false;
			fsmName = "";
			subtractValue = null;
		}

		public override void OnEnter()
		{
			DoSubtractFsmFloat();
			
			if (!everyFrame)
			{
			    Finish();
			}		
		}

		void DoSubtractFsmFloat()
		{
			if (subtractValue == null)
			{
			    return;
			}

			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
			    return;
			}
			
			if (go != goLastFrame)
			{
				goLastFrame = go;
				
				// only get the fsm component if go has changed
				
				fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
			}			
			
			if (fsm == null)
			{
                LogWarning("Could not find FSM: " + fsmName.Value);
			    return;
			}
			
			var fsmFloat = fsm.FsmVariables.GetFsmFloat(variableName.Value);
			
			if (fsmFloat != null && !perSecond)
			{
			    fsmFloat.Value -= subtractValue.Value;
			}
			if (fsmFloat != null && perSecond)
			{
			    fsmFloat.Value -= subtractValue.Value * Time.deltaTime;
			}
		}

		public override void OnUpdate()
		{
			DoSubtractFsmFloat();
		}

	}
}

