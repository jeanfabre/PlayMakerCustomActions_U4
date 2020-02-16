// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("System")]
	[Tooltip("Get full system informations.")]
	public class GetSystemInformations : FsmStateAction
	{
		public FsmString userName;
		public FsmString systemInfo;
		public FsmString processorInfo;
		public FsmInt processorCount;
		public FsmInt systemMemorySize;
		public FsmInt graphicsMemorySize;
		public FsmString graphicsDeviceName;
		public FsmString graphicsDeviceVendor;
		public FsmInt graphicsDeviceID;
		public FsmInt graphicsDeviceVendorID;
		public FsmString graphicsDeviceVersion;
		public FsmInt graphicsShaderLevel;
		public FsmInt graphicsPixelFillrate;
		public FsmBool supportsShadows;
		public FsmBool supportsRenderTextures;
		public FsmBool supportsImageEffects;
		public FsmBool supports3DTextures;
		public FsmBool supportsComputeShaders;
		public FsmBool supportsInstancing;
		public FsmInt supportedRenderTargetCount;
		public FsmString deviceUniqueIdentifier;
		public FsmString deviceName;
		public FsmString deviceModel;
		public FsmBool supportsAccelerometer;
		public FsmBool supportsGyroscope;
		public FsmBool supportsLocationService;
		public FsmBool supportsVibration;
		
		public override void Reset()
		{
			userName = null;
			systemInfo = null;
			processorInfo = null;
			processorCount = null;
			systemMemorySize = null;			
			graphicsMemorySize = null;			
			graphicsDeviceName = null;
			graphicsDeviceVendor = null;			
			graphicsDeviceID = null;			
			graphicsDeviceVendorID = null;
			graphicsDeviceVersion = null;			
			graphicsShaderLevel = null;			
			graphicsPixelFillrate = null;
			supportsShadows = false;			
			supportsRenderTextures = false;			
			supportsImageEffects = false;
			supports3DTextures = false;			
			supportsComputeShaders = false;			
			supportsInstancing = false;
			supportedRenderTargetCount = null;			
			deviceUniqueIdentifier = null;			
			deviceName = null;
			deviceModel = null;			
			supportsAccelerometer = false;			
			supportsGyroscope = false;
			supportsLocationService = false;
			supportsVibration = false;
		}

		public override void OnEnter()
		{
			userName.Value = System.Environment.UserName;
			systemInfo.Value = SystemInfo.operatingSystem;
			processorInfo.Value = SystemInfo.processorType;
			processorCount.Value = SystemInfo.processorCount;			
			systemMemorySize.Value = SystemInfo.systemMemorySize;			
			graphicsMemorySize.Value = SystemInfo.graphicsMemorySize;
			graphicsDeviceName.Value = SystemInfo.graphicsDeviceName;			
			graphicsDeviceVendor.Value = SystemInfo.graphicsDeviceVendor;	
			graphicsDeviceID.Value = SystemInfo.graphicsDeviceID;			
			graphicsDeviceVendorID.Value = SystemInfo.graphicsDeviceVendorID;	
			graphicsDeviceVersion.Value = SystemInfo.graphicsDeviceVersion;			
			graphicsShaderLevel.Value = SystemInfo.graphicsShaderLevel;	
			graphicsPixelFillrate.Value = SystemInfo.graphicsPixelFillrate;			
			supportsShadows = SystemInfo.supportsShadows;
			supportsRenderTextures = SystemInfo.supportsRenderTextures;	
			supportsImageEffects = SystemInfo.supportsImageEffects;					
			supportedRenderTargetCount.Value = SystemInfo.supportedRenderTargetCount;			
			deviceUniqueIdentifier.Value = SystemInfo.deviceUniqueIdentifier;
			deviceName.Value = SystemInfo.deviceName;			
			deviceModel.Value = SystemInfo.deviceModel;
			supportsAccelerometer = SystemInfo.supportsAccelerometer;			
			supportsGyroscope = SystemInfo.supportsGyroscope;
			supportsLocationService = SystemInfo.supportsLocationService;
			supportsVibration = SystemInfo.supportsVibration;
			
			Finish();
		}
	}
}

