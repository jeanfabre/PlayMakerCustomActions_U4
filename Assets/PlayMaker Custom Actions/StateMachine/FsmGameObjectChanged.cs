// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Made by nightcorelv

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if the value of a FSM GameObject variable changed. Use this to send an event on change, or store a bool that can be used in other operations.")]
	public class FsmGameObjectChanged : FsmStateAction
	{
	[RequiredField]
		public FsmOwnerDefault gameObject;
		[UIHint(UIHint.FsmName)]
		public FsmString fsmName;
		[RequiredField]
		[UIHint(UIHint.FsmGameObject)]
		public FsmString variableName;
		GameObject storeFsmValue;
		public FsmBool storeResult;
		public FsmEvent changedEvent;
		private GameObject previousValue;
		
		GameObject goLastFrame;
		PlayMakerFSM fsm;
		
		public override void Reset()
		{
            storeResult = false;
            gameObject = null;
            changedEvent = null;
            fsmName = "";
            variableName = "";
            previousValue = null;
        }
        public override void OnEnter()
        {
            Ggop();
            previousValue = storeFsmValue;
            ttop();
        }

        public override void OnUpdate()
        {
            Ggop();
            ttop();
        }



        void Ggop()
        {
            storeResult.Value = false;

			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;

			
			if (go != goLastFrame)
			{
				goLastFrame = go;
				fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
			}
			
			if (fsm == null) return;
			
			FsmGameObject fsmGameObject = fsm.FsmVariables.GetFsmGameObject(variableName.Value);
			
			if (fsmGameObject == null) return;
			
			storeFsmValue = fsmGameObject.Value;
        }
        void ttop()
        {
            if (storeFsmValue != previousValue)
            {
                storeResult.Value = true;
                Fsm.Event(changedEvent);
            }
        }
    }
}

