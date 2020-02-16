// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Made by nightcorelv

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
[ActionCategory(ActionCategory.StateMachine)]
[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
[Tooltip("Compare the value of a String Variable from another FSM.")]
public class FsmStringCompare : FsmStateAction
{


[RequiredField]
public FsmOwnerDefault gameObject;
[UIHint(UIHint.FsmName)]
[Tooltip("Optional name of FSM on Game Object")]
public FsmString fsmName;

[RequiredField]
[UIHint(UIHint.FsmString)]
public FsmString variableName;

[RequiredField]
public FsmString value;
public FsmEvent Equal;
public FsmEvent NotEqualEvent;

string StoreFsmVariable;
public bool everyFrame;


GameObject goLastFrame;
PlayMakerFSM fsm;




public override void Reset()
		{
			value = "";
			Equal = null;
			NotEqualEvent = null;
			everyFrame = false;
			gameObject = null;
			variableName = "";
		}


public override void OnEnter()
	{
		DoGetFsmString();

		if (!everyFrame)
		Finish();
	}




public override void OnUpdate()
	{
		DoGetFsmString();
	}

void DoGetFsmString()
		{

			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;

			

			if (go != goLastFrame)
			{
				goLastFrame = go;
				fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
			}
			
			if (fsm == null) return;
			
			FsmString fsmString = fsm.FsmVariables.GetFsmString(variableName.Value);
			
			if (fsmString == null) return;

			StoreFsmVariable = fsmString.Value;
			
			DoStringCompare();
			
		}
void DoStringCompare()
		{
			if (StoreFsmVariable == null || value == null) return;
			var equal = StoreFsmVariable == value.Value;

			if (equal && Equal != null)
			{
				Fsm.Event(Equal);
				return;
			}

			if (!equal && NotEqualEvent != null)
			{
				Fsm.Event(NotEqualEvent);

		
		    }
		}
}

}
