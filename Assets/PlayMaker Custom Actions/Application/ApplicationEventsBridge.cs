// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __PROXY__ ---*/

using UnityEngine;
using System.Collections;
using HutongGames;

public class ApplicationEventsBridge : MonoBehaviour {
	
	
	void OnApplicationQuit()
	{
		PlayMakerFSM.BroadcastEvent("APPLICATION QUIT");
	}
	void OnApplicationPause(bool pause)
	{
		
		PlayMakerFSM.BroadcastEvent("APPLICATION PAUSE");
	}
	void OnApplicationFocus(bool focus)
	{
		PlayMakerFSM.BroadcastEvent("APPLICATION FOCUS");
	}
	
}
