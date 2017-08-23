using UnityEngine;
using System.Collections;

public class DelegateEventReceiver : MonoBehaviour {
	
	public PlayMakerFsmStateWatcher stateWatcher;

	public string previousState;
	public string newState;

	void Awake()
	{
		stateWatcher.onStateChangedEvent += HandleOnStateChangeEvent;
	}
	
	void OnEnable()
	{
		stateWatcher.onStateChangedEvent -= HandleOnStateChangeEvent;
		stateWatcher.onStateChangedEvent += HandleOnStateChangeEvent;
	}
	
	void OnDisable()
	{
		stateWatcher.onStateChangedEvent -= HandleOnStateChangeEvent;
	}

	void HandleOnStateChangeEvent(string arg1, string arg2)
	{
		this.previousState = arg1;
		this.newState = arg2;
	}


}
