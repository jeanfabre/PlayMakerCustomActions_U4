// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.

using System;

using System.Collections;
using System.Collections.Generic;

using System.Reflection;

using UnityEngine;

using HutongGames.PlayMaker;

using HutongGames.PlayMaker.Ecosystem.Utils;

using UnityEngine.Events;

/// <summary>
/// Proof of concept for getting notified about state changes in PlayMaker.
/// 
/// I implemented three different types of c# notification, 
/// .net event Delegate, I prefer this because I get intellisense to give very productive presets of code snippets.
/// 
/// Unity Event. good for drag and drop
/// 
/// PlayMaker event. good to have fsm being made aware of other fsm change states.
/// 
/// so all three solution are probably required, one for scripters, one for convenient inspector based scripts + drag and drop, and one for PlayMaker
/// 
/// </summary>
using HutongGames.PlayMaker.Actions;


public class PlayMakerFsmStateWatcher : MonoBehaviour
{
	[Header("Setup")]
	public PlayMakerFsmTarget fsmSource;

	public bool ignoreStart = true;

	public bool debug = false;


	#region c# event delegate
	/// <summary>
	/// delegate for state change event
	/// </summary>
	///  <param name="previousStateName">previous State Name.</param>
	/// <param name="newStateName">new State Name.</param>
	public delegate void onStateChangedDelegate(string previousStateName,string newStateName);

	/// <summary>
	/// Occurs when on state change event action.
	///  <para>&#160;</para>- First parameter is the previous State Name
	///  <para>&#160;</para>- Second parameter is the new State Name
	/// </summary>
	public event onStateChangedDelegate onStateChangedEvent;

	#endregion
	

	#region UnityEvent notifications 

	[System.Serializable]
	public class StateChangeEvent : UnityEvent<string,string> { }
	[Header("Unity Event notification")]
	public StateChangeEvent onStateChanged;

	#endregion

	#region PlayMaker event notifications
	[Header("PlayMaker Event notification")]
	public PlayMakerEventTarget target;

	[EventTargetVariable("target")]
	public PlayMakerEvent onStateChangedFsmEvent;

	#endregion



	string lastSourceActiveState;

	void Update()
	{
		Synchronize();

	}

	string _sourceActiveStateName;

	public void Synchronize()
	{

		if (fsmSource.fsmComponent==null )
		{
			Debug.LogError("No Components to synchronize",this);
			return;
		}

	
		_sourceActiveStateName = fsmSource.fsmComponent.ActiveStateName;

		if (ignoreStart && lastSourceActiveState == null)
		{
			lastSourceActiveState = fsmSource.fsmComponent.ActiveStateName;
			return;
		}

		if (lastSourceActiveState == _sourceActiveStateName )
		{
			return;
		}

		if (debug) Debug.Log("switching from state <"+lastSourceActiveState+"> to <"+_sourceActiveStateName+"> ");

		NotifyStateChange(lastSourceActiveState,_sourceActiveStateName);

		lastSourceActiveState = _sourceActiveStateName;

	}
	
	void NotifyStateChange(string previousState, string newState)
	{
		// delegate way
		if (this.onStateChangedEvent!=null)
		{
			this.onStateChangedEvent(previousState,newState);
		}

		// Unity Event way
		this.onStateChanged.Invoke(previousState,newState);

		// playmaker utils way

		Fsm.EventData = new FsmEventData();
		Fsm.EventData.StringData = newState;

		SetEventProperties.properties = new Dictionary<string, object>();
		SetEventProperties.properties["Previous State"] = previousState;
		SetEventProperties.properties["New State"] = newState;

		this.onStateChangedFsmEvent.SendEvent(null,target);
	}
}






