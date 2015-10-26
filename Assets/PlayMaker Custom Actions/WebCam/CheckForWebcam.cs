// (c) copyright HutongGames, LLC 2010-2015. All rights reserved.
// by Brzozowsky Adam (brzozowsky@gmail.com)
// Forum : http://hutonggames.com/playmakerforum/index.php?topic=3880.msg53819#msg53819
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("WebCam")]
	[Tooltip("Checks system for a webcam.")]
	public class CheckForWebcam : FsmStateAction
	{
        [Tooltip("Event to send if a webcam was found.")]
		public FsmEvent webcamFound;

        [Tooltip("Event to send if a webcam was not found.")]
		public FsmEvent webcamNotFound;

		public override void Reset()
		{
			webcamFound = null;
			webcamNotFound = null;
		}

		public override void OnEnter()
		{
			int numOfCams = WebCamTexture.devices.Length;
			if (numOfCams > 0)
			{
				Fsm.Event(webcamFound);
			}
			else
			{
				Fsm.Event(webcamNotFound);
			}
		}
	}
}
