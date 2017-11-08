using UnityEngine;
using UnityEditor;

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
using HutongGames.PlayMakerEditor;

public class PlayMakerCommsToolWindow : EditorWindow 
{
	
	static PlayMakerCommsTool pmCommsTool = null;

	// So we can compare to PlayMakerCommsTool
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

	// Scroll position
	private Vector2 scrollPos = Vector2.zero;

	//------------------------------------------------------------------------------------------------------
	// Add menu named "Windowed Comm Tool" to the PlayMaker/Tools menu
	//------------------------------------------------------------------------------------------------------
	[MenuItem ("PlayMaker/Tools/Windowed Comm Tool")]
	static void Init ()
	{
		// If Playmaker is not initialised we get errors. Opening the editor solves that
		if (FsmEditorWindow.IsOpen() == false)
		{
			FsmEditorWindow.OpenWindow();
		}
		
		// Get existing open window or if none, make a new one:
		PlayMakerCommsToolWindow window = (PlayMakerCommsToolWindow)EditorWindow.GetWindow (typeof (PlayMakerCommsToolWindow));
		
		if (pmCommsTool == null)
		{
			pmCommsTool = FindObjectOfType(typeof(PlayMakerCommsTool)) as PlayMakerCommsTool;
			if (pmCommsTool == null)
			{
				GameObject gameObject = new GameObject("PlayMakerCommsTool");
				pmCommsTool = gameObject.AddComponent<PlayMakerCommsTool>();
			}
		}
		pmCommsTool.Rebuild(true);
	}

	//------------------------------------------------------------------------------------------------------
	// Do the top part of the window and then make call ShowCommsResults() to get the information shown...
	//------------------------------------------------------------------------------------------------------
	void OnGUI ()
	{
		if (pmCommsTool == null) return;
		
		if ((pmCommsTool.commsList == null) || (pmCommsTool.commsList.Count == 0))
		{
			GUILayout.Label ("Nothing to show? MAKESURE YOU START Playmaker, from the menu: PlayMaker->Playmaker Editor", EditorStyles.boldLabel);
			GUILayout.Label ("Remember; Filters are case sensitive.", EditorStyles.boldLabel);
		}
		
		GUILayout.Label ("Playmaker Comms", EditorStyles.boldLabel);

		//------------------------------------------------------------------------------------------------------
		// Remember what was set before (so we can look for changes)
		ShowSendMessage = pmCommsTool.ShowSendMessage;
		FilterMethodName = pmCommsTool.FilterMethodName;
		ShowSendEvent = pmCommsTool.ShowSendEvent;
		EventMessageTogether = pmCommsTool.EventMessageTogether;
		FilterStateName = pmCommsTool.FilterStateName;
		FilterFsmName = pmCommsTool.FilterFsmName;
		FilterEventName =  pmCommsTool.FilterEventName;
		FilterTargetFsmName = pmCommsTool.FilterTargetFsmName;
		FilterTargetGameObjectName = pmCommsTool.FilterTargetGameObjectName;
		ShowActivateGameObject = pmCommsTool.ShowActivateGameObject;
		
		//------------------------------------------------------------------------------------------------------
		// Filter and other settings that affect the results
		GUILayout.Label ("Filters affecting both SendMessage and SendEvent (optional part or complete case sensitive name)", EditorStyles.label);
		pmCommsTool.FilterFsmName =  EditorGUILayout.TextField ("FSM", pmCommsTool.FilterFsmName);
		pmCommsTool.FilterStateName =  EditorGUILayout.TextField ("State", pmCommsTool.FilterStateName);
		pmCommsTool.FilterTargetGameObjectName =  EditorGUILayout.TextField ("Target GameObject", pmCommsTool.FilterTargetGameObjectName);
		
		pmCommsTool.ShowSendMessage = EditorGUILayout.Toggle ("Show Send Message", pmCommsTool.ShowSendMessage);
		if (pmCommsTool.ShowSendMessage == true)
		{
			GUILayout.Label ("Filter (optional part or complete case sensitive name)", EditorStyles.label);
			pmCommsTool.FilterMethodName =  EditorGUILayout.TextField ("Method", pmCommsTool.FilterMethodName);
		}
		pmCommsTool.ShowSendEvent = EditorGUILayout.Toggle ("Show Send Event", pmCommsTool.ShowSendEvent);
		if (pmCommsTool.ShowSendEvent == true)
		{
			GUILayout.Label ("Filter (optional part or complete case sensitive name)", EditorStyles.label);
			pmCommsTool.FilterEventName =  EditorGUILayout.TextField ("Event", pmCommsTool.FilterEventName);
			pmCommsTool.FilterTargetFsmName =  EditorGUILayout.TextField ("Fsm Target", pmCommsTool.FilterTargetFsmName);
		}
		pmCommsTool.ShowActivateGameObject = EditorGUILayout.Toggle ("Show Activate GameObject", pmCommsTool.ShowActivateGameObject);
		GUILayout.Label ("You can have Events and Message mixed together in the order they're found or separate (ONLY affects console output):", EditorStyles.label);
		pmCommsTool.EventMessageTogether = EditorGUILayout.Toggle ("In Find Order", pmCommsTool.EventMessageTogether);
		
		//------------------------------------------------------------------------------------------------------
		// Did something change?
		bool changed = (
			(ShowSendMessage != pmCommsTool.ShowSendMessage) ||
			(FilterMethodName != pmCommsTool.FilterMethodName) ||
			(ShowSendEvent != pmCommsTool.ShowSendEvent) ||
			(EventMessageTogether != pmCommsTool.EventMessageTogether) ||
			(FilterStateName != pmCommsTool.FilterStateName) ||
			(FilterFsmName != pmCommsTool.FilterFsmName) ||
			(FilterEventName !=  pmCommsTool.FilterEventName) ||
			(FilterTargetFsmName != pmCommsTool.FilterTargetFsmName) ||
			(FilterTargetGameObjectName != pmCommsTool.FilterTargetGameObjectName) ||
			(ShowActivateGameObject != pmCommsTool.ShowActivateGameObject)
			);
		
		//------------------------------------------------------------------------------------------------------
		// To we need to regenerate the results?
		if (changed == true)
		{
			pmCommsTool.Rebuild(false);
			scrollPos = Vector2.zero;	// Otherwise you may not see the new results as the scroll could leave everything out of view.
		}
		else
		{
			if ((pmCommsTool.ShowSendEvent == false) && (pmCommsTool.ShowSendMessage == false) && (pmCommsTool.ShowActivateGameObject == false))
			{
				GUILayout.Label ("Nothing useful to show if you set SendMessage, SendEvent & ActivateGameObject false!", EditorStyles.boldLabel);
			}
			else
			if (GUILayout.Button("Re-generate Playmaker communication data (inc to Console)")){
				pmCommsTool.Rebuild(true);
			}
		}

		//------------------------------------------------------------------------------------------------------
		// The position of the window for results
		Rect windowRect = new Rect(0,0, Screen.width, Screen.height);
		// Set up a scroll view
		scrollPos = GUI.BeginScrollView (
			new Rect(0, 300, position.width, position.height), // NOTE; Magic number for Rect.top in use to position the data, TODO: get the actual position of where there is space below the GUI items all ready put in.
				scrollPos,
			new Rect (0, 0, 1000, 100000),	// TODO: set vertical scroll bar based on size of window it controls!
			true,
			true
			);		
		BeginWindows ();
		windowRect = GUILayout.Window (1, windowRect, ShowCommsResults, pmCommsTool.numFsms + " FSMs enabled in scene");
		EndWindows ();
		// Close the scroll view
		GUI.EndScrollView ();
	}
	
	//------------------------------------------------------------------------------------------------------
	// Show the results
	//------------------------------------------------------------------------------------------------------
	void ShowCommsResults(int n) 
	{
		//------------------------------------------------------------------------------------------------------
		// Any info we want to let the user know to help them understand why things are as shown...
		if ((FilterMethodName.Length > 0) || (FilterStateName.Length > 0) || (FilterFsmName.Length > 0) ||
			(FilterEventName.Length > 0) || (FilterTargetFsmName.Length > 0) || (FilterTargetGameObjectName.Length > 0))
		{
			GUILayout.Label ("Note: CASE SENSITIVE filter(s) are in use.", EditorStyles.boldLabel);
			EditorGUILayout.Space();
		}
			
		if ((pmCommsTool.ShowSendEvent == true) && (pmCommsTool.ShowSendMessage == true) && (pmCommsTool.ShowActivateGameObject == true))
			GUILayout.Label ("FSMs with a SendMessage or SendEvent or ActivateGameObject here:", EditorStyles.boldLabel);
		else
		{
			if ((pmCommsTool.ShowSendEvent == true) || (pmCommsTool.ShowSendMessage == true) || (pmCommsTool.ShowActivateGameObject == true))
			{
				string info = "NOTE: Only FSMs with a ";
				if (pmCommsTool.ShowSendEvent == true) info += "SendEvent ";
				if (pmCommsTool.ShowSendMessage == true) info += "SendMessage ";
				if (pmCommsTool.ShowActivateGameObject == true) info += "ActivateGameObject ";
				
				info += "action are here:";
				GUILayout.Label (info, EditorStyles.boldLabel);
			}
		}
		
		//------------------------------------------------------------------------------------------------------
		// Show the results
		int currentFsmIndex = -1;
		for (int i=0; i<pmCommsTool.commsList.Count; ++i)
		{
			if (pmCommsTool.commsList[i].ourFsmIndex > currentFsmIndex)
			{
				currentFsmIndex = pmCommsTool.commsList[i].ourFsmIndex;
				
				EditorGUILayout.Space();
				GUILayout.Label ("(FSM name)= " + pmCommsTool.commsList[i].fsm.name, EditorStyles.boldLabel);
				
				pmCommsTool.commsList[i].showMyFsm = EditorGUILayout.Toggle (
					"Show this FSM?", 
					pmCommsTool.commsList[i].showMyFsm);
				
				if (pmCommsTool.commsList[i].showMyFsm == true)
				{
					GUILayout.Label ("States with entry:-", EditorStyles.label);
					string state = "";
					bool showState = true;
					for (int ii=i; ii<pmCommsTool.commsList.Count; ++ii)
					{
						if (pmCommsTool.commsList[ii].ourFsmIndex == currentFsmIndex)
						{
							if (state != pmCommsTool.commsList[ii].state.Name)
							{
								state = pmCommsTool.commsList[ii].state.Name;
								
								showState = pmCommsTool.commsList[ii].showMyState = EditorGUILayout.Toggle (
									(pmCommsTool.commsList[ii].state.Name + ":"), 
									pmCommsTool.commsList[ii].showMyState);
							}

							if (showState == true)
							{
								if (pmCommsTool.commsList[ii].action.GetType() == typeof(HutongGames.PlayMaker.Actions.SendEvent))
								{
									HutongGames.PlayMaker.Actions.SendEvent se = (HutongGames.PlayMaker.Actions.SendEvent)(pmCommsTool.commsList[ii].action);
									
									string TargetFsmName = se.eventTarget.fsmName.Value;
									if (TargetFsmName.Length == 0) TargetFsmName = se.eventTarget.fsmName.Name;
									if (TargetFsmName.Length == 0) TargetFsmName = "UNKOWN";
									
									GUILayout.Label (
										"SentEvent:" + se.sendEvent.Name + 
										" to (FSM called) " + TargetFsmName +										
										" on GameObject " + se.eventTarget.gameObject.GameObject.ToString()
										, EditorStyles.label);
								}
								else							
								if (pmCommsTool.commsList[ii].action.GetType() == typeof(HutongGames.PlayMaker.Actions.SendMessage))
								{
									HutongGames.PlayMaker.Actions.SendMessage sm = (HutongGames.PlayMaker.Actions.SendMessage)(pmCommsTool.commsList[ii].action);
									string funcName = sm.functionCall.FunctionName;
									GameObject targetGO = sm.Fsm.GetOwnerDefaultTarget(sm.gameObject);
									string gameObjectName = ((targetGO == null) ? "UNKNOWN NAME" : targetGO.name);
									
									GUILayout.Label (
										"SendMessage to GameObject " + gameObjectName +
										" method=" + funcName
										, EditorStyles.label);
								}
								else
								if (pmCommsTool.commsList[ii].action.GetType() == typeof(HutongGames.PlayMaker.Actions.ActivateGameObject))
								{
									HutongGames.PlayMaker.Actions.ActivateGameObject ago = (HutongGames.PlayMaker.Actions.ActivateGameObject)(pmCommsTool.commsList[ii].action);
									GameObject targetGameObject = ago.Fsm.GetOwnerDefaultTarget(ago.gameObject);
									string targetGOname = ((targetGameObject != null) ? targetGameObject.name : "UNKNOWN");
									
									GUILayout.Label (
										"ActivateGameObject " + 
										" Enable=" + pmCommsTool.FsmBoolToString(ago.activate) +
										" Recursive=" + pmCommsTool.FsmBoolToString(ago.recursive.Value) +
										" on GameObject " + targetGOname
										, EditorStyles.label);
								}
							}
						}
						else
						{
							i = ii-1;
							break;
						}
					}
				}
			}
		}
	}
}