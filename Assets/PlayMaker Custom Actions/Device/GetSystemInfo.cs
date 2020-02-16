// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Gets the system info on the device. Some are expensive calls so use only as needed.")]
	public class GetSystemInfo : FsmStateAction
	{
		[ActionSection("NOTE: Some are expensive.")]

		[UIHint(UIHint.Variable)]
		public FsmString deviceModel;

		[UIHint(UIHint.Variable)]
		public FsmString deviceName;

		[UIHint(UIHint.Variable)]
		public FsmString deviceType;

		[UIHint(UIHint.Variable)]
		public FsmString deviceUniqueIdentifier;

		[UIHint(UIHint.Variable)]
		public FsmInt graphicsDeviceID;

		[UIHint(UIHint.Variable)]
		public FsmString graphicsDeviceName;

		[UIHint(UIHint.Variable)]
		public FsmString graphicsDeviceVendor;

		[UIHint(UIHint.Variable)]
		public FsmInt graphicsDeviceVendorID;

		[UIHint(UIHint.Variable)]
		public FsmString graphicsDeviceVersion;

		[UIHint(UIHint.Variable)]
		public FsmInt graphicsMemorySize;

		[UIHint(UIHint.Variable)]
		public FsmInt graphicsPixelFillrate;

		[UIHint(UIHint.Variable)]
		public FsmInt graphicsShaderLevel;
		
		#if UNITY_3_5 || UNITY_3_4
		#else
		[UIHint(UIHint.Variable)]
		public FsmString npotSupport;
		#endif
		[UIHint(UIHint.Variable)]
		public FsmString operatingSystem;

		[UIHint(UIHint.Variable)]
		public FsmInt processorCount;

		[UIHint(UIHint.Variable)]
		public FsmString processorType;

		[UIHint(UIHint.Variable)]
		public FsmInt supportedRenderTargetCount;
		
		#if UNITY_3_5 || UNITY_3_4
		#else
		[UIHint(UIHint.Variable)]
		public FsmBool supports3DTextures;
		#endif
		
		[UIHint(UIHint.Variable)]
		public FsmBool supportsAccelerometer;
		
		#if UNITY_3_5 || UNITY_3_4
		#else
		[UIHint(UIHint.Variable)]
		public FsmBool supportsComputeShaders;
		#endif
		
		[UIHint(UIHint.Variable)]
		public FsmBool supportsGyroscope;

		[UIHint(UIHint.Variable)]
		public FsmBool supportsImageEffects;
		
		#if UNITY_3_5 || UNITY_3_4
		#else
		[UIHint(UIHint.Variable)]
		public FsmBool supportsInstancing;
		#endif
		[UIHint(UIHint.Variable)]
		public FsmBool supportsLocationService;

		[UIHint(UIHint.Variable)]
		public FsmBool supportsRenderTextures;

		[UIHint(UIHint.Variable)]
		public FsmBool supportsRenderToCubemap;

		[UIHint(UIHint.Variable)]
		public FsmBool supportsShadows;
		
		#if UNITY_3_5 || UNITY_3_4
		#else
		[UIHint(UIHint.Variable)]
		public FsmBool supportsSparseTextures;

		[UIHint(UIHint.Variable)]
		public FsmInt supportsStencil;
		#endif
		[UIHint(UIHint.Variable)]
		public FsmBool supportsVibration;

		[UIHint(UIHint.Variable)]
		public FsmInt systemMemorySize;

		public override void OnEnter()
		{
			if (deviceModel.Value != null){
				deviceModel.Value = SystemInfo.deviceModel;
			}
			if (deviceName.Value != null){
				deviceName.Value = SystemInfo.deviceName;
			}
			if (deviceType.Value != null){
				if(SystemInfo.deviceType == DeviceType.Handheld){deviceType.Value=("Handheld");}
				if(SystemInfo.deviceType == DeviceType.Desktop){deviceType.Value=("Desktop");}
				if(SystemInfo.deviceType == DeviceType.Console){deviceType.Value=("Console");}
				else{deviceType.Value=("Unknown");}

			}
			if (deviceUniqueIdentifier != null){
				deviceUniqueIdentifier.Value = SystemInfo.deviceUniqueIdentifier;
			}
			if (graphicsDeviceID != null){
				graphicsDeviceID.Value = SystemInfo.graphicsDeviceID;
			}
			if (graphicsDeviceName != null){
				graphicsDeviceName.Value = SystemInfo.graphicsDeviceName;
			}
			if (graphicsDeviceVendor != null){
				graphicsDeviceVendor.Value = SystemInfo.graphicsDeviceVendor;
			    }
			if (graphicsDeviceVendorID != null){
				graphicsDeviceVendorID.Value = SystemInfo.graphicsDeviceVendorID;
			}
			if (graphicsDeviceVersion != null){
				graphicsDeviceVersion.Value = SystemInfo.graphicsDeviceVersion;
			}
			if (graphicsMemorySize != null){
				graphicsMemorySize.Value = SystemInfo.graphicsMemorySize;
			}
			if (graphicsPixelFillrate != null){
				graphicsPixelFillrate.Value = SystemInfo.graphicsPixelFillrate;
			}
			if (graphicsShaderLevel != null){
				graphicsShaderLevel.Value = SystemInfo.graphicsShaderLevel;
			}
			#if UNITY_3_5 || UNITY_3_4
			#else
			if (npotSupport != null){
				if(SystemInfo.npotSupport == NPOTSupport.None){npotSupport.Value=("None");}
				if(SystemInfo.npotSupport == NPOTSupport.Restricted){npotSupport.Value=("Restricted");}
				else{npotSupport.Value=("Full");}
			}
			#endif
			
			if (operatingSystem != null){
				operatingSystem.Value = SystemInfo.operatingSystem;
			}
			if (processorCount != null){
				processorCount.Value = SystemInfo.processorCount;
			}
			if (processorType != null){
				processorType.Value = SystemInfo.processorType;
			}
			if (supportedRenderTargetCount != null){
				supportedRenderTargetCount.Value = SystemInfo.supportedRenderTargetCount;
			}
			#if UNITY_3_5 || UNITY_3_4
			#else
			if (supports3DTextures != null){
				supports3DTextures.Value = SystemInfo.supports3DTextures;
			}
			#endif
			
			if (supportsAccelerometer != null){
				supportsAccelerometer.Value = SystemInfo.supportsAccelerometer;
			}
			#if UNITY_3_5 || UNITY_3_4
			#else
			if (supportsComputeShaders != null){
				supportsComputeShaders.Value = SystemInfo.supportsComputeShaders;
			}
			#endif
			if (supportsGyroscope != null){
				supportsGyroscope.Value = SystemInfo.supportsGyroscope;
			}
			if (supportsImageEffects != null){
				supportsImageEffects.Value = SystemInfo.supportsImageEffects;
			}
			
			#if UNITY_3_5 || UNITY_3_4
			#else
			if (supportsInstancing != null){
				supportsInstancing.Value = SystemInfo.supportsInstancing;
			}
			#endif
			
			if (supportsLocationService != null){
				supportsLocationService.Value = SystemInfo.supportsLocationService;
			}
			if (supportsRenderTextures != null){
				supportsRenderTextures.Value = SystemInfo.supportsRenderTextures;
			}
			#if UNITY_3_5 || UNITY_3_4
			#else
			if (supportsRenderToCubemap != null){
				supportsRenderToCubemap.Value = SystemInfo.supportsRenderToCubemap;
			}
			#endif
			if (supportsShadows != null){
				supportsShadows.Value = SystemInfo.supportsShadows;
			}
			#if UNITY_3_5 || UNITY_3_4
			#else
			if (supportsSparseTextures != null){
				supportsSparseTextures.Value = SystemInfo.supportsSparseTextures;
			}
		
			if (supportsStencil != null){
				supportsStencil.Value = SystemInfo.supportsStencil;
			}
			#endif
			if (supportsVibration != null){
				supportsVibration.Value = SystemInfo.supportsVibration;
			}
			if (systemMemorySize != null){
				systemMemorySize.Value = SystemInfo.systemMemorySize;
			}
			
			Finish();
		}
	}
}

