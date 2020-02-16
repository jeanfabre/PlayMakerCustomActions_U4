// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Returns the Gameobject within an array which have the max float value in its FSM")]
	public class ArrayGetGameobjectMaxFsmFloatIndex : FsmStateAction
	{
	
			[RequiredField]
			[UIHint(UIHint.Variable)]
			[Tooltip("The Array Variable to use.")]
			public FsmArray array;
			
			[UIHint(UIHint.FsmName)]
			[Tooltip("Optional name of FSM on Game Object")]
			public FsmString fsmName;
		
			[RequiredField]
			[UIHint(UIHint.FsmFloat)]
			public FsmString variableName;
			
			public bool everyframe;
			
			[ActionSection("Result")]
		
			[UIHint(UIHint.Variable)]
			public FsmFloat storeMaxValue;
		
			[UIHint(UIHint.Variable)]
			public FsmGameObject maxGameObject;
			
			[UIHint(UIHint.Variable)]
			public FsmInt maxIndex;
		
			GameObject goLastFrame;
			PlayMakerFSM fsm;
			
			
			public override void Reset()
			{
			
				array = null;
				maxGameObject = null;
				maxIndex = null;
				
				everyframe = true;
				fsmName = "";
				storeMaxValue = null;
			}
			
			
			public override void OnEnter()
			{
				DoFindMaxGo();
				
				if (!everyframe)
				{
					Finish();
				}
				
			}
			
			public override void OnUpdate()
			{
				DoFindMaxGo();
			}
	
	
			void DoFindMaxGo()
			{
				float compValue = 0;	
			
				if (storeMaxValue.IsNone) return;
	

			
				int _index = 0;
	
				foreach(GameObject _go in array.objectReferences)
				{
					
					if (_go!=null) 
					{
							
						fsm = ActionHelpers.GetGameObjectFsm(_go, fsmName.Value);
					
						if (fsm == null) return;
				
						FsmFloat fsmFloat = fsm.FsmVariables.GetFsmFloat(variableName.Value);
				
						if (fsmFloat == null) return;
					
						if(fsmFloat.Value > compValue)
						{
						storeMaxValue.Value = fsmFloat.Value;
						compValue = fsmFloat.Value;
						maxGameObject.Value = _go;
						maxIndex.Value = _index;
						}	
					
					}
					_index++;
				}
	
			}
			
	}
	
}

