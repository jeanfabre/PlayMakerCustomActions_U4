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
    [Tooltip("Stream a webcam video to selected material.")]
    public class SetWebcamMaterial : FsmStateAction
    {
        public FsmString webcamName;
		public FsmMaterial webcamMaterial;
		private WebCamTexture webCamTexture;

        public override void Reset()
        {
			webcamName = null;
			webcamMaterial = null;
        }

        public override void OnEnter()
		{
			webCamTexture = new WebCamTexture();
			webCamTexture.deviceName = webcamName.Value;
			webcamMaterial.Value.SetTexture("_MainTex", webCamTexture);
			webCamTexture.Play();
			Finish();
		}
	}
}
