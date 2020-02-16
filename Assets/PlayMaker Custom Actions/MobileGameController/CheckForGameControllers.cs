// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

// JoystickName pattern: [$profile_type,$connection_type] joystick $number by $model
// this action assume that only one game controller can be connected.

using UnityEngine;
using System.Text.RegularExpressions;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.InputDevice)]
	[Tooltip("Sends Events based on the Connection status of Mobile Game Controller. Currently Only works on IOS devices, but potentially works on all future supported devices")]
	public class CheckForGameControllers : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("The connection status.")]
		public FsmBool isConnected;

		[UIHint(UIHint.Variable)]
		[Tooltip("The raw joystick name.")]
		public FsmString rawJoystickName;

		[UIHint(UIHint.Variable)]
		[Tooltip("The joystick vendorName.")]
		public FsmString vendorName;

		[UIHint(UIHint.Variable)]
		[Tooltip("The joystick number.")]
		public FsmInt number;

		[UIHint(UIHint.Variable)]
		[Tooltip("The profile type of the connected game controller. 'Basic' or 'Extended' ")]
		public FsmString profile;

		[UIHint(UIHint.Variable)]
		[Tooltip("True if the profile is Extended, false if Basic")]
		public FsmBool isExtended;

		[UIHint(UIHint.Variable)]
		[Tooltip("The connection type of the connected game controller. 'wired' or 'wireless' ")]
		public FsmString connection;

		[UIHint(UIHint.Variable)]
		[Tooltip("True if the profile is wireless, false if wired")]
		public FsmBool isWireless;
		
		[Tooltip("Event to send if a game controller is detected.")]
		public FsmEvent isConnectedEvent;
		
		[Tooltip("Event to send if the connected game controller is lost.")]
		public FsmEvent isDisconnectedEvent;

		[Tooltip("Repeat every frame while the state is active. If false, will fire the current state, if true, will fire when the status changes based on the status this action started with")]
		public bool everyFrame;

		private string[] _controllers;

		private string regex = @"^\s*\[(?<profile>[a-z]+),(?<connection>[a-z]+)\] joystick (?<number>[0-9]*) by (?<vendorName>[A-Za-z\s]+)";

		bool initialyConnected;
		bool currentlyConnected;

		public override void Reset()
		{
			isConnected = null;
			rawJoystickName = null;
			number = null;
			vendorName = null;
			profile = null;
			isExtended = null;
			connection = null;
			isWireless = null;

			everyFrame = false;
		}

		public override void OnEnter()
		{


			Check();

			if (everyFrame)
			{
				initialyConnected = currentlyConnected;
			}else{

				if (currentlyConnected)
				{
					Fsm.Event(isConnectedEvent);
				}else{
					Fsm.Event(isDisconnectedEvent);
				}

				Finish();
			}

		}

		public override void OnUpdate()
		{
			Check();


			if (initialyConnected!=currentlyConnected)
			{
				if (currentlyConnected)
				{
					Fsm.Event(isConnectedEvent);
				}else{
					Fsm.Event(isDisconnectedEvent);
				}

				initialyConnected = currentlyConnected;
			}

		}

		public void Check()
		{
			_controllers = Input.GetJoystickNames();

			currentlyConnected = _controllers.Length>0;

			if (initialyConnected!=currentlyConnected || !everyFrame)
			{
				isConnected.Value = currentlyConnected;

				if (currentlyConnected)
				{
					rawJoystickName.Value = _controllers[0]; // "[basic,wired] joystick 1 by Logitech PowerShell"; // 

					Regex r = new Regex(regex);
					Match m = r.Match(rawJoystickName.Value);
				
					//Debug.Log(m.Groups["profile"].Value+" "+m.Groups["connection"].Value+" "+m.Groups["number"].Value+" "+m.Groups["vendorName"].Value);
					profile.Value = m.Groups["profile"].Value;
					connection.Value = m.Groups["connection"].Value;
					int _num = 0;
					int.TryParse(m.Groups["number"].Value,out _num);
					number.Value = _num;

					vendorName.Value = m.Groups["vendorName"].Value;

				}

			}

		}

		// only use during dev, to facilitate the regex development.
		private string ComputeRegex()
		{
			string fullregex =  @"\[$profile,$connection\] joystick $number by $model";
			
			string floatregex = @"[0-9]*";
			
			string attributeRegex =  @"[a-z]+";
			string vendorNameRegex =  @"[A-Za-z\s]+";

			fullregex = fullregex.Replace("$profile","(?<profile>" + attributeRegex + ")");
			fullregex = fullregex.Replace("$connection","(?<connection>" + attributeRegex + ")");
			fullregex = fullregex.Replace("$number","(?<number>" + floatregex + ")");
			fullregex = fullregex.Replace("$vendorName","(?<vendorName>" + vendorNameRegex + ")");
			
			fullregex = @"^\s*" + fullregex;

			Debug.Log(fullregex);

			return fullregex;
		}



	}
}
