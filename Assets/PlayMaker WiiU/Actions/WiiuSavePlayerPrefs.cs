// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
//--- __ECO__ __ACTION__

using UnityEngine;
#if UNITY_WIIU
using UnityEngine.WiiU;
#endif
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("WiiU")]
	[Tooltip("Save Player Prefs")]
	public class WiiuSavePlayerPrefs : FsmStateAction
	{
		
		public FsmEvent NotEnoughFreeSpaceEvent;
		
		public FsmEvent FailureEvent;
		
		public FsmEvent SuccessEvent;
		
		
		public override void Reset()
		{
			NotEnoughFreeSpaceEvent = null;
			
		}
		
		public override void OnEnter()
		{
			
			SaveMyPlayerPrefs();
			
			
			Finish ();
		}
		
		#if UNITY_WIIU
		void SaveMyPlayerPrefs()
		{
			
			WiiUSAVECommand cmd = WiiUSave.SaveCommand(WiiUSave.accountNo);
			
			long freespace = 0;
			// see if we have enough free space for our save file
			WiiUSave.FSStatus savestatus = cmd.GetFreeSpaceSize(out freespace, WiiUSave.FSRetFlag.None);
			var needspace = Mathf.Min(1024 * 1024, WiiUPlayerPrefsHelper.rawData.Length);
			
			if (savestatus == WiiUSave.FSStatus.OK)
			{
				if (freespace < needspace)
				{
					// not enough free space
					Debug.Log("Not enough free space, freespace = " + freespace.ToString() + ", needspace = " + needspace.ToString());
					
					Fsm.Event(NotEnoughFreeSpaceEvent);
					Fsm.Event(FailureEvent);
				}
				else
				{
					// The line below corresponds to step 4. The save file is not committed to NAND yet and will be lost if you quit at this point.
					PlayerPrefs.Save();
					// The line below corresponds to step 5. Barring any errors, the save file is now committed and it is ok to exit your game.
					savestatus = cmd.FlushQuota(WiiUSave.FSRetFlag.None);
					
					if (savestatus != WiiUSave.FSStatus.OK)
					{
						Debug.Log("Save failed: " + savestatus.ToString());
						Fsm.Event(FailureEvent);
					}else{
						Fsm.Event(SuccessEvent);
					}
				}
			}
			else
			{
				// save failed
				Debug.Log("Save failed to get free space: " + savestatus.ToString());
				Fsm.Event(NotEnoughFreeSpaceEvent);
				Fsm.Event(FailureEvent);
			}
		}
		
		#else
		
		void SaveMyPlayerPrefs()
		{
			Debug.Log("WiiuSavePlayerPrefs only works properly on WiiU platform");
			Fsm.Event(SuccessEvent);
		}
		#endif	
	}
}