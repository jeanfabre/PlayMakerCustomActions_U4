// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// by Brzozowsky Adam (brzozowsky@gmail.com)
// Forum : http://hutonggames.com/playmakerforum/index.php?topic=3880.msg53819#msg53819


using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("WebCam")]
    [Tooltip("Find webcam and store name as string")]
    public class GetWebcamName : FsmStateAction
    {
        public FsmString[] storeWebcamName;
		public FsmInt storeWebcamCount;
		private FsmString[] camName;
		private WebCamTexture webCamTexture;

        public override void Reset()
        {
			storeWebcamName = null;
			storeWebcamCount = 0;
        }

        public override void OnEnter()
		{
			int numOfCams = WebCamTexture.devices.Length;
			this.camName = new FsmString[numOfCams];
			storeWebcamCount.Value = numOfCams;
			
			for(int i = 0; i < numOfCams; i++)  
			{  
				this.camName[i] = WebCamTexture.devices[i].name;    
				storeWebcamName[i].Value = camName[i].Value;
			} 
		Finish();
		}
	}
}
