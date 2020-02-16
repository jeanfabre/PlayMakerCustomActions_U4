// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// v1.2

using System;
using UnityEngine;
using System.Collections;
using System.IO;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("Saves a Screenshot from the camera. Save as png or jpg.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11308.0")]
	public class TakeCameraScreenshot : FsmStateAction
	{
		[ActionSection("Camera")]
		[RequiredField]
		[CheckForComponent(typeof(Camera))]
		[Tooltip("The GameObject camera to take picture from (Must have a Camera component).")]
		public FsmGameObject gameObject;
		
		[ActionSection("Screen Setup")]
		public FsmInt resWidth;
		public FsmInt resHeight;
		[Tooltip("Automatically get the current resolution - RECOMMENDED")]
		public FsmBool Auto;
		[Tooltip("Use the current resolution - NOT RECOMMENDED")]
		public FsmBool useCurrentRes;
		
		
		[ActionSection("File Path Setup")]
		public FsmString filePath;
		[Tooltip("Use the default MyPictures Folder?")]
		public FsmBool useMypictures;
		[Tooltip("Use the default save Folder?")]
		public FsmBool useDefaultFolder;
		[ActionSection("File Name Setup")]
		public FsmString filename;
		public FsmBool autoNumber;
		
		[ActionSection("Option")]
		public FsmBool useJpeg;
		[Tooltip("Must be 0 or 16 or 24 - The precision of the render texture's depth buffer in bits / When 0 is used, then no Z buffer is created by a render texture")]
		private int Depth;
		public enum depthSelect  {
			
			_24,
			_16,
			_0,
			
		};


		public depthSelect setDepth;

		public FsmBool debugOn;


		[ActionSection("Result")]
		public FsmString StoreFilePath;


		private int screenshotCount;
		private string screenshotPath;
		private string screenshotFullPath;
		
		public override void Reset()
		{
			gameObject = null;
			filename = "";
			autoNumber = false;
			Auto = false;
			useCurrentRes = false;
			resWidth = 2560;
			resHeight = 1440;
			useMypictures = false;
			useDefaultFolder = true;
			useJpeg = false;
			debugOn = false;

			StoreFilePath = null;
		}

		public override void OnPreprocess()
		{
			Fsm.HandleLateUpdate = true;
		}


		public override void OnLateUpdate()
		{
			if (useCurrentRes.Value == true || Auto.Value == true) getResolutions();
			
			switch(setDepth){
			case depthSelect._0:
				Depth = 0;
				break;
				
			case depthSelect._16:
				Depth = 16;
				break;
				
			case depthSelect._24:
				Depth = 24;
				break;
				
			}

			getPicture();

			Finish();
		}


		public void getPicture()
		{
			if (debugOn.Value == true) UnityEngine.Debug.Log("getPicture");

	
			RenderTexture rt = new RenderTexture(resWidth.Value, resHeight.Value, Depth);
			gameObject.Value.GetComponent<Camera>().targetTexture = rt;
			Texture2D screenShot = new Texture2D(resWidth.Value, resHeight.Value, TextureFormat.RGB24, false);
			gameObject.Value.GetComponent<Camera>().Render();
			RenderTexture.active = rt;
			screenShot.ReadPixels(new Rect(0, 0, resWidth.Value, resHeight.Value), 0, 0);
			screenShot.Apply();
			gameObject.Value.GetComponent<Camera>().targetTexture = null;
			RenderTexture.active = null; 
			UnityEngine.Object.Destroy(rt);
			
			
			if (useMypictures.Value == true) 
			{
				screenshotPath = Environment.GetFolderPath (Environment.SpecialFolder.MyPictures) + "/";

				if (!string.IsNullOrEmpty(filePath.Value))
				{
					screenshotPath += filePath.Value+"/";
				}

			} else if (useDefaultFolder.Value) 
			{
				screenshotPath = Application.persistentDataPath;
				if (!string.IsNullOrEmpty(filePath.Value))
				{
					screenshotPath += "/"+filePath.Value+"/";
				}
			}
			else
			{
				screenshotPath = filePath.Value;
			}

			
			if (!useJpeg.Value) {
				screenshotFullPath = screenshotPath + filename.Value + ".png";
			} else {
				screenshotFullPath = screenshotPath + filename.Value + ".jpg";
			}
			
			
			if (autoNumber.Value)
			{
				while (System.IO.File.Exists(screenshotFullPath)) 
				{
					screenshotCount++;
					if (!useJpeg.Value)
						screenshotFullPath = screenshotPath + filename.Value + screenshotCount + ".png";
					else screenshotFullPath = screenshotPath + filename.Value + screenshotCount + ".jpg";
				} 
			}

			if (debugOn.Value == true) UnityEngine.Debug.Log("File path: "+screenshotFullPath+" File Name: "+filename.Value);


			byte[] bytes;
			if (!useJpeg.Value)
				bytes = screenShot.EncodeToPNG();
			else bytes = screenShot.EncodeToJPG();

			Directory.CreateDirectory (screenshotPath);

			File.WriteAllBytes(screenshotFullPath,bytes);

			StoreFilePath.Value = screenshotFullPath;

		}

		public void getResolutions()
		{
			if (useCurrentRes.Value == true){
				resWidth.Value = Screen.currentResolution.width;
				resHeight.Value = Screen.currentResolution.height;
			}
			else {
				resWidth.Value = Screen.width;
				resHeight.Value = Screen.height;
			}
			return;
		}

	}
}
