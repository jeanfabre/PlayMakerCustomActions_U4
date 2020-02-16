// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Loads a Level using a special technic to free resources and avoid memory leak. This Object WILL set itself to survive level loading.")]
	public class LoadLevelClean : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The name of the level to load. NOTE: Must be in the list of levels defined in File->Build Settings... ")]
		public FsmString levelName;
		
		[RequiredField]
		[Tooltip("The name of the base level to load from. Ideally, this is a totally empty level, with litterally nothing in the hierarchy")]
		public FsmString BaselevelName;
		
		
		[Tooltip("Event to send when the level has loaded. NOTE: This only makes sense if the FSM is still in the scene!")]
		public FsmEvent loadedEvent;
		
		public bool DebugLog;
		

		private AsyncOperation asyncOperation;
		
		private enum loadState {LoadBase,UnloadUnusedResources,LoadLevel}
		
		private loadState state;
		
		public override void Reset()
		{
			levelName = "";
			BaselevelName = "";
			loadedEvent = null;
			DebugLog = true;
		}
		
		private void LogDebug(string message)
		{
			if (DebugLog)
			{
				Debug.Log(message);
			}
		}
		
		public override void OnEnter()
		{
			Object.DontDestroyOnLoad(Owner.transform.root.gameObject);
			
			LogDebug("Loading base level :"+BaselevelName.Value);
			
			Application.LoadLevel(BaselevelName.Value);
			
			Debug.Log("done");
			
			state = loadState.LoadBase;
		//	LogDebug("Loading base level :"+BaselevelName.Value);
			//asyncOperation = Application.LoadLevelAsync(levelName.Value);
		}
		
		
		
		public override void OnUpdate()
		{
		//	Debug.Log(asyncOperation.isDone);
			
			if (asyncOperation ==null || asyncOperation.isDone )
			{
				Debug.Log(state);
				
				if (state == loadState.LoadBase)
				{
					LogDebug("UnloadUnusedAssets");
					state = loadState.UnloadUnusedResources;
					asyncOperation = null;
					asyncOperation =  Resources.UnloadUnusedAssets();
	
				}else if (state == loadState.UnloadUnusedResources)
				{
					LogDebug("loading level :"+levelName.Value);
					state = loadState.LoadLevel;
					asyncOperation = null;
					asyncOperation = Application.LoadLevelAdditiveAsync(levelName.Value);
					
				}else if (state == loadState.LoadLevel)
				{
					LogDebug("loading done");
			
					Fsm.Event(loadedEvent);
					Finish();
				}
			}
			
		}
		
	}
}
