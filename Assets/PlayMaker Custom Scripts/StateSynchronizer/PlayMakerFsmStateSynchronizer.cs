// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*
 inspired by Paul Hayes asset: https://github.com/paulhayes/AnimatorStateMachineUtil

This script will monitor an Fsm Component and will switch the fsm target states to reflect the source current state

*/

using System;

using System.Collections;
using System.Collections.Generic;

using System.Reflection;

using UnityEngine;

using HutongGames.PlayMaker;

using HutongGames.PlayMaker.Ecosystem.Utils;


public class PlayMakerFsmStateSynchronizer : MonoBehaviour
{

	public enum StateMatching {Always,IfNotEqual,IfDoesntContains,IfDoesntStartsWith,IfDoesntEndsWith};

	public PlayMakerFsmTarget fsmSource;
	
	public PlayMakerFsmTarget fsmTarget;

	public StateMatching matchType = StateMatching.IfNotEqual;

	public bool throwSynchErrors = false;

	public bool debug = false;
	
	public bool everyFrame =true;

	Dictionary<string,FsmState> TargetStatesLut;


	string lastSourceActiveState;

	bool _started;

	void Start()
	{
		TargetStatesLut = new Dictionary<string, FsmState>();

		foreach (FsmState _state in fsmTarget.fsmComponent.Fsm.States)
		{
			TargetStatesLut.Add (_state.Name,_state);
				
		}
		Synchronize();
	}

	void Update()
	{
		/*
		if (_started)
		{
			Synchronize();
		}else{
			_started =true;
			return;
		}
*/

		if (everyFrame)
		{
			Synchronize();
		}
	}
	
	public void Synchronize()
	{

		if (fsmSource.fsmComponent==null || fsmTarget.fsmComponent==null)
		{
			Debug.LogError("No Components to synchronize",this);
			return;
		}

		string _sourceActiveStateName = fsmSource.fsmComponent.ActiveStateName;

		if (everyFrame)
		{
			if (lastSourceActiveState == _sourceActiveStateName)
			{
				return;
			}

			lastSourceActiveState = _sourceActiveStateName;
		}

		if (debug) Debug.Log("Synching state "+lastSourceActiveState);



		bool _canSynch=false;

		switch(matchType)
		{
			case StateMatching.Always:
				_canSynch = true;
				break;
			case StateMatching.IfNotEqual:
				_canSynch = fsmTarget.fsmComponent.ActiveStateName != _sourceActiveStateName;
				break;
			case StateMatching.IfDoesntStartsWith:
				_canSynch = ! fsmTarget.fsmComponent.ActiveStateName.StartsWith(_sourceActiveStateName);
				break;
			case StateMatching.IfDoesntEndsWith:
				_canSynch = ! fsmTarget.fsmComponent.ActiveStateName.EndsWith(_sourceActiveStateName);
				break;
			case StateMatching.IfDoesntContains:
				_canSynch = ! fsmTarget.fsmComponent.ActiveStateName.Contains(_sourceActiveStateName);
				break;
		}
		if (_canSynch)
		{
			if (TargetStatesLut.ContainsKey(_sourceActiveStateName))
			{
				if (debug) Debug.Log("Synching successul");
				SwitchState(fsmTarget.fsmComponent.Fsm,TargetStatesLut[_sourceActiveStateName]);
			}else{
				if (throwSynchErrors)
				{
					Debug.LogException(new Exception("Could not Synch source state "+_sourceActiveStateName),this);
				}

				if (debug)  Debug.Log("Could not Synch "+_sourceActiveStateName+", it's not found on the target");
			}
			// Only in 1.8
			//Fsm.Fsm.SwitchState(_fsmState);
		}else{
			if (debug)  Debug.Log("Match Rule prevented synch: source '"+_sourceActiveStateName+ "' "+matchType+" '"+fsmTarget.fsmComponent.ActiveStateName+"'"  );
		}
			
	}
	
	void SwitchState(Fsm fsm, FsmState _state)
	{
		MethodInfo switchState = fsm.GetType().GetMethod("SwitchState", BindingFlags.NonPublic | BindingFlags.Instance);
		if (switchState!=null)
		{
			switchState.Invoke(fsm , new object[] { _state });
		}
	}
}






