// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// __ECO__ __PLAYMAKER__ __ACTION__

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Logic)]
    [Tooltip("Sends an Event based on the value of an Enum Variable on a given Fsm.")]
    public class FsmEnumSwitch : FsmStateAction
    {
		[RequiredField]
		[Tooltip("The GameObject with the Fsm")]
		public FsmOwnerDefault gameObject;
		[UIHint(UIHint.FsmName)]

		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		[RequiredField]
		[Tooltip("the name of the fsmEnum variable")]
		[UIHint(UIHint.FsmEnum)]
		public FsmString variableName;


        

        [CompoundArray("Enum Switches", "Compare Enum Values", "Send")] 
       // [MatchFieldType("enumVariable")] 
        public FsmEnum[] compareTo;      
        public FsmEvent[] sendEvent;

        public bool everyFrame;


		GameObject goLastFrame;
		PlayMakerFSM fsm;
		FsmEnum enumVariable;

        public override void Reset()
        {
            enumVariable = null;
            compareTo = new FsmEnum[0];
            sendEvent = new FsmEvent[0];
            everyFrame = false;
        }

        public override void OnEnter()
        {
            DoEnumSwitch();

            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            DoEnumSwitch();
        }

        private void DoEnumSwitch()
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
			
			enumVariable = fsm.FsmVariables.GetFsmEnum(variableName.Value);
			
			if (enumVariable == null) return;

            if (enumVariable.IsNone)
                return;

            for (int i = 0; i < compareTo.Length; i++)
            {
                if (Equals(enumVariable.Value, compareTo[i].Value))
                {
                    Fsm.Event(sendEvent[i]);
                    return;
                }
            }
        }
    }
}