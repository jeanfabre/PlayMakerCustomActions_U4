// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Adds a value to a Float Variable in another FSM.")]
	public class FsmFloatAdd : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;
		[RequiredField]
		[UIHint(UIHint.FsmFloat)]
		public FsmString variableName;
		
		
		[RequiredField]
		public FsmFloat add;
		public bool everyFrame;
		public bool perSecond;
		
		
		GameObject goLastFrame;
		PlayMakerFSM fsm;
		
		public override void Reset()
		{
			gameObject = null;
			fsmName = null;
			variableName = null;
			
			add = null;
			everyFrame = false;
			perSecond = false;
		}

		public override void OnEnter()
		{
			DoSetFsmFloatAdd();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoSetFsmFloatAdd();
		}
		
		void DoSetFsmFloatAdd()
		{

			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			
			if (go != goLastFrame)
			{
				goLastFrame = go;
				
				// only get the fsm component if go has changed
				
				fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
			}			
			
			if (fsm == null) return;
			
			FsmFloat fsmFloat = fsm.FsmVariables.GetFsmFloat(variableName.Value);
			
			if (fsmFloat == null) return;
			
			if (!perSecond)
				fsmFloat.Value += add.Value;
			else
				fsmFloat.Value += add.Value * Time.deltaTime;
			
		}
		
	}
}
