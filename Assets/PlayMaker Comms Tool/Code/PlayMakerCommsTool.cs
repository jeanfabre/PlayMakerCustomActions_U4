using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;

//------------------------------------------------------------------------------------------------------
// Show SendEvent and SendMessage actions in FSMs so you can track down what's going on in
// multi-FSM designs and where you call script methods more easily.
// Put the PlayMakerCommsTool.cs script in with your normal code (NOT in the Assets/Editor directory).
// Put the PlayMakerCommsToolEditor and PlayMakerCommsToolWindow in the Assets/Editor directory.
//
// To use:
// Once the code is in the right place you will find a new menu item "Comm Tool Window" under
// the Playmaker -> Tools menu. Click this, you only need to do it once in a scene. 
// You can delete the PlayMakerCommsTool object from your Hierarchy when finished.
// If you get any issue when you try it, close the Comm Tool window and from the
// Playmaker menu select the Playmaker Editor. Now try the Comm Tool window again.
//------------------------------------------------------------------------------------------------------


public class PlayMakerCommsTool : MonoBehaviour {
	
	public bool ShowSendMessage = true;
	public string FilterMethodName = "";
	public bool ShowSendEvent = true;
	public bool EventMessageTogether = false;
	public string FilterStateName = "";
	public string FilterFsmName = "";
	public string FilterEventName =  "";
	public string FilterTargetFsmName = "";
	public string FilterTargetGameObjectName = "";
	
	public bool ShowActivateGameObject = true;
	
	// Results in a crude string form
	private string results;
	
	// Results in a list - if you want to stick them in a nice tree control or something.
	public List<PlaymakerCommsEntry> commsList = null;
	
	[HideInInspector]
	public int numFsms = 0;
		
	//------------------------------------------------------------------------------------------------------
	public void Rebuild(bool showInConsole){
		// An array of all FSMs
		PlayMakerFSM [] fsms = FindObjectsOfType(typeof(PlayMakerFSM)) as PlayMakerFSM[];
		commsList = null;
		numFsms = 0;
		
		if (fsms != null)
		{
			results = "PlayMakerCommsTool: " + fsms.Length + " FSMs found. CLICK THIS in top panel to expand in bottom panel.\n";
			results += "Also, try the windowed results too from the menu Playmaker->Tools->Windowed Comm Tool.\n";
			
			commsList = new List<PlaymakerCommsEntry>();
			
			numFsms = fsms.Length;
			int ourFsmIndex = 0;
			foreach(PlayMakerFSM fsm in fsms)
			{
				if ((FilterFsmName.Length > 0) && (fsm.name.Contains(FilterFsmName) == false)) continue;
				
				if (fsm.FsmStates != null) fsm.FsmStates.Initialize();
				
				for (int s = 0; s<fsm.FsmStates.Length; ++s)
				{
					if ((FilterStateName.Length > 0) && (fsm.FsmStates[s].Name.Contains(FilterStateName) == false)) continue;
					
					if (EventMessageTogether == true)
					{
						// Show SendEvent and SendMessage as we find them
						foreach(FsmStateAction action in fsm.FsmStates[s].Actions)
						{
							if (ShowSendEvent == true) HandleSendEvent(ourFsmIndex, fsm, fsm.FsmStates[s], action);
							if (ShowSendMessage == true) HandleSendMessage(ourFsmIndex, fsm, fsm.FsmStates[s], action);
							if (ShowActivateGameObject == true) HandleActivateGameObject(ourFsmIndex, fsm, fsm.FsmStates[s], action);
							
						}
					}
					else
					{
						// Show the SendEvent and then the SendMessage text
						if (ShowSendEvent == true)
						{
							int numActions = fsm.FsmStates[s].Actions.Length;
							for (int actionNum=0; actionNum < numActions; ++actionNum)
							{
								//foreach(FsmStateAction action in fsm.FsmStates[s].Actions) HandleSendEvent(ourFsmIndex, fsm, fsm.FsmStates[s], action);
								HandleSendEvent(ourFsmIndex, fsm, fsm.FsmStates[s], fsm.FsmStates[s].Actions[actionNum]);
							}
						}
						
						if (ShowSendMessage == true)
						{
							foreach(FsmStateAction action in fsm.FsmStates[s].Actions) HandleSendMessage(ourFsmIndex, fsm, fsm.FsmStates[s], action);
						}
						
						if (ShowActivateGameObject == true)
						{
							foreach(FsmStateAction action in fsm.FsmStates[s].Actions) HandleActivateGameObject(ourFsmIndex, fsm, fsm.FsmStates[s], action);
						}
					}
				}
				++ourFsmIndex;
			}
			
			// Show what filters affected the output
			if (FilterMethodName.Length > 0) results += "Filter Method Name = " + FilterMethodName + "\n";
			if (FilterStateName.Length > 0) results += "Filter State Name = " + FilterStateName + "\n";
			if (FilterFsmName.Length > 0) results += "Filter FSM Name = " + FilterFsmName + "\n";
			if (FilterEventName.Length > 0) results += "Filter Event Name = " + FilterEventName + "\n";
			if (FilterTargetFsmName.Length > 0) results += "Filter Target FSM Name = " + FilterTargetFsmName + "\n";
			if (FilterTargetGameObjectName.Length > 0) results += "Filter Target GameObject Name = " + FilterTargetGameObjectName + "\n";
			
			if ((ShowSendMessage == false) && (ShowSendEvent == false)) results += "Unable to show anything useful with both ShowSendMessage false and ShowSendEvent false!\n";
			if (showInConsole == true)
			{
				Debug.Log(results);
			}
		}
		else
		{
			if (showInConsole == true)
			{
				Debug.Log("PlayMakerCommsTool: No FSMs found");
			}
		}
	}
	
	//------------------------------------------------------------------------------------------------------
	private void HandleSendMessage(int ourFsmIndex, PlayMakerFSM fsm, FsmState state, FsmStateAction action)
	{
		if (action.GetType() == typeof(HutongGames.PlayMaker.Actions.SendMessage))
		{
			HutongGames.PlayMaker.Actions.SendMessage sm = (HutongGames.PlayMaker.Actions.SendMessage)(action);
			string funcName = sm.functionCall.FunctionName;
			GameObject targetGO = sm.Fsm.GetOwnerDefaultTarget(sm.gameObject);
			string gameObjectName = ((targetGO != null) ? targetGO.name : "UNKNOWN");
			
			if ((FilterMethodName.Length > 0) && (funcName.Contains(FilterMethodName) == false)) return;
			if ((FilterTargetGameObjectName.Length > 0) && (gameObjectName.Contains(FilterTargetGameObjectName) == false)) return;
			
			results += "SendMessage " + fsm.ToString() + " State " + state.Name + " calls method " + funcName + " on GameObject " + gameObjectName +"\n";
			
			commsList.Add(new PlaymakerCommsEntry(ourFsmIndex, fsm, state, action));
		}
	}
	
	//------------------------------------------------------------------------------------------------------
	private void HandleSendEvent(int ourFsmIndex, PlayMakerFSM fsm, FsmState state, FsmStateAction action)
	{
		if (action.GetType() == typeof(HutongGames.PlayMaker.Actions.SendEvent))
		{
			HutongGames.PlayMaker.Actions.SendEvent se = (HutongGames.PlayMaker.Actions.SendEvent)(action);
			
			if ((FilterEventName.Length > 0) && (se.sendEvent.Name.Contains(FilterEventName) == false)) return;

			string TargetFsmName = se.eventTarget.fsmName.Value;
			if (TargetFsmName.Length == 0) TargetFsmName = se.eventTarget.fsmName.Name;
			if (TargetFsmName.Length == 0) TargetFsmName = "UNKOWN";
			
			if ((FilterTargetFsmName.Length > 0) && (TargetFsmName.Contains(FilterTargetFsmName) == false)) return;
			
			if ((FilterTargetGameObjectName.Length > 0) && (se.eventTarget.gameObject.GameObject.ToString().Contains(FilterTargetGameObjectName) == false)) return;
			
			results += "SendEvent " + fsm.ToString() + " State " + state.Name + " sends " + se.sendEvent.Name +
				" to (FSM called) " + TargetFsmName +										
				" on GameObject " + se.eventTarget.gameObject.GameObject.ToString() +
				"\n";
			commsList.Add(new PlaymakerCommsEntry(ourFsmIndex, fsm, state, action));
		}
	}
	
	private void HandleActivateGameObject(int ourFsmIndex, PlayMakerFSM fsm, FsmState state, FsmStateAction action)
	{
		if (action.GetType() == typeof(HutongGames.PlayMaker.Actions.ActivateGameObject))
		{
			HutongGames.PlayMaker.Actions.ActivateGameObject ago = (HutongGames.PlayMaker.Actions.ActivateGameObject)(action);
			GameObject targetGameObject = ago.Fsm.GetOwnerDefaultTarget(ago.gameObject);
			string targetGOname = ((targetGameObject != null) ? targetGameObject.name : "UNKNOWN");
			
			if ((FilterTargetGameObjectName.Length > 0) && (targetGOname.Contains(FilterTargetGameObjectName) == false)) return;
			
			results += "ActivateGameObject " + fsm.ToString() + " State " + state.Name + 
				" Enable=" + FsmBoolToString(ago.activate) +
				" Recursive=" + FsmBoolToString(ago.recursive.Value) +
				" on GameObject " + targetGOname +
				"\n";
			commsList.Add(new PlaymakerCommsEntry(ourFsmIndex, fsm, state, action));
		}
	}
	
	public string FsmBoolToString(FsmBool b)
	{
		if (b == null) return "null";
		return (b.Value ? "True" : "False");
	}
}


//------------------------------------------------------------------------------------------------------
public class PlaymakerCommsEntry
{
	public bool showMyFsm = true;
	public bool showMyState = true;
	public bool showMe = true;
	public int ourFsmIndex = 0;
	public PlayMakerFSM fsm;
	public FsmState state;
	public FsmStateAction action;
	
	public PlaymakerCommsEntry(int anFsmIndex, PlayMakerFSM an_fsm, FsmState a_state, FsmStateAction an_action)
	{
		showMe = showMyState = showMyFsm = true;
		ourFsmIndex = anFsmIndex;
		fsm = an_fsm;
		state = a_state;
		action = an_action;
	}
}