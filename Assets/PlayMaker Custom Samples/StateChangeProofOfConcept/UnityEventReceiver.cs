using UnityEngine;
using System.Collections;

public class UnityEventReceiver : MonoBehaviour {

	public string previousState;
	public string newState;

	public void OnFsmStateChanged(string previousState,string newState)
	{
		this.previousState = previousState;
		this.newState = newState;
	}

}
