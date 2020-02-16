// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("AssetBundle")]
	[Tooltip("Gets Bundle from a url and store it in a FsmObject. See Unity WWW and Bundles docs for more details.")]
	public class WWWAssetBundle : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Url to download bundle from.")]
		public FsmString url;
		
		[ActionSection("Caching")] 
		[Tooltip("will get the cached version if available, else downloads normally")]
		public FsmBool loadFromCacheOrDownload;
		
		[Tooltip("Version of the AssetBundle. The file will only be loaded from the disk cache if it has previously been downloaded with the same version parameter. By incrementing the version number requested by your application, you can force Caching to download a new copy of the AssetBunlde from url.")]
		public FsmInt cacheVersion;
		
		
		[ActionSection("Results")]

		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(AssetBundle))]
		[Tooltip("Gets bundle from the url.")]
		public FsmObject storeBundle;

		[UIHint(UIHint.Variable)]
		[Tooltip("Error message if there was an error during the download.")]
		public FsmString errorString;

		[UIHint(UIHint.Variable)] 
		[Tooltip("How far the download progressed (0-1).")]
		public FsmFloat progress;

		[ActionSection("Events")] 
		
		[Tooltip("Event to send when the bundle has finished loading (progress = 1).")]
		public FsmEvent isDone;
		
		[Tooltip("Event to send if there was an error.")]
		public FsmEvent isError;

		private WWW wwwObject;

		public override void Reset()
		{
			url = null;
			loadFromCacheOrDownload = true;
			cacheVersion = 1;
			storeBundle = null;
			errorString = null;
			progress = null;
			isDone = null;
		}

		public override void OnEnter()
		{
			if (string.IsNullOrEmpty(url.Value))
			{
				Finish();
				return;
			}
			
			if (loadFromCacheOrDownload.Value)
			{
				wwwObject =  WWW.LoadFromCacheOrDownload (url.Value, cacheVersion.Value);
			}else{
				wwwObject = new WWW(url.Value);
			}
			
		}


		public override void OnUpdate()
		{
			if (wwwObject == null)
			{
				errorString.Value = "WWW Object is Null!";
				Finish();
				return;
			}

			errorString.Value = wwwObject.error;

			if (!string.IsNullOrEmpty(wwwObject.error))
			{
				Finish();
				Fsm.Event(isError);
				return;
			}

			progress.Value = wwwObject.progress;

			if (wwwObject.isDone)
			{
				storeBundle.Value = wwwObject.assetBundle;

				errorString.Value = wwwObject.error;	

				Fsm.Event(string.IsNullOrEmpty(errorString.Value) ? isDone : isError);

				Finish();
			}
		}
	}
}
