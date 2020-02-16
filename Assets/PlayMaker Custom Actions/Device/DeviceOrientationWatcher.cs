// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Sends discrete Event based on the Orientation changes of the mobile device.")]
	public class DeviceOrientationWatcher : FsmStateAction
	{
		[Tooltip("send when device entered face up orientation")]
		public FsmEvent FaceUpEvent;
		
		[Tooltip("send when device entered face down orientation")]
		public FsmEvent FaceDownEvent;
		
		[Tooltip("send when device entered llndscape left orientation")]
		public FsmEvent LandscapeLeftEvent;
		
		[Tooltip("send when device entered landscape right orientation")]
		public FsmEvent LandscapeRightEvent;
		
		[Tooltip("send when device entered portrait orientation")]
		public FsmEvent PortraitEvent;
		
		[Tooltip("send when device entered upside down portrait orientation")]
		public FsmEvent PortraitUpsideDownEvent;
		
		[Tooltip("send when device entered unkown orientation")]
		public FsmEvent UnknownEvent;
		
		[Tooltip("if true, do not fire an event on the orientation found when this action starts")]
		public bool ignoreInitialOrientation;
		
		private DeviceOrientation _currentOrientation;
		
		public override void Reset()
		{
			FaceUpEvent = null;
			FaceDownEvent = null;
			LandscapeRightEvent = null;
			LandscapeLeftEvent = null;
			PortraitEvent = null;
			PortraitUpsideDownEvent = null;
			UnknownEvent = null;
			
			ignoreInitialOrientation = true;
		}
		
		public override void OnEnter()
		{
			
			_currentOrientation = Input.deviceOrientation;
			if (!ignoreInitialOrientation)
			{
				FireOrientationEvent();
			}
		}
	
		public override void OnUpdate()
		{
			DoWatchDeviceOrientation();
		}
		
		void DoWatchDeviceOrientation()
		{
			if (Input.deviceOrientation != _currentOrientation)
			{
				_currentOrientation = Input.deviceOrientation;
			 	FireOrientationEvent();
			}
		}
		
		void FireOrientationEvent()
		{
			switch (_currentOrientation)
				{
					case DeviceOrientation.FaceDown:
					  	Fsm.Event(FaceDownEvent);
						break;
					case DeviceOrientation.FaceUp:
					  	Fsm.Event(FaceUpEvent);
						break;
					case DeviceOrientation.LandscapeLeft:
					  	Fsm.Event(LandscapeLeftEvent);
						break;
					case DeviceOrientation.LandscapeRight:
					 	Fsm.Event(LandscapeRightEvent);
						break;
					case DeviceOrientation.Portrait:
					  	Fsm.Event(PortraitEvent);
						break;
					case DeviceOrientation.PortraitUpsideDown:
						Fsm.Event(PortraitUpsideDownEvent);
						break;
					case DeviceOrientation.Unknown:
						Fsm.Event(UnknownEvent);
						break;
				}
		}
		
	}
}
