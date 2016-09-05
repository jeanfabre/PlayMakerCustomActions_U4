// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

#if !(UNITY_IPHONE || UNITY_ANDROID || UNITY_FLASH || UNITY_PS3 || UNITY_BLACKBERRY || UNITY_WP8)


using System.Linq;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Web Player")]
	[Tooltip("Gets data from a url and store it in variables. See Unity WWW docs for more details.")]
	public class WWWObjectRelative : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Url to download data from.")]
		public FsmString url;

		[Tooltip("If path is relative to Application datapath folder.")]
		public FsmBool pathIsRelative;

		[ActionSection("Results")]

		[UIHint(UIHint.Variable)]
		[Tooltip("Gets text from the url.")]
		public FsmString storeText;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Gets a Texture from the url.")]
		public FsmTexture storeTexture;

		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(AudioClip))]
		[Tooltip("Gets Audio from the url.")]
		public FsmObject storeAudio;

        [UIHint(UIHint.Variable)]
		[ObjectType(typeof(MovieTexture))]
		[Tooltip("Gets a Texture from the url.")]
		public FsmObject storeMovieTexture;

		[UIHint(UIHint.Variable)]
		[Tooltip("Error message if there was an error during the download.")]
		public FsmString errorString;

		[UIHint(UIHint.Variable)] 
		[Tooltip("How far the download progressed (0-1).")]
		public FsmFloat progress;

		[ActionSection("Events")] 
		
		[Tooltip("Event to send when the data has finished loading (progress = 1).")]
		public FsmEvent isDone;
		
		[Tooltip("Event to send if there was an error.")]
		public FsmEvent isError;

		private WWW wwwObject;
		private string urlPrefix = "file://"+Application.dataPath+"/";

		public override void Reset()
		{
			url = null;
			pathIsRelative = false;
			storeText = null;
			storeTexture = null;
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

			if (pathIsRelative.Value == true)
			{
				wwwObject = new WWW(urlPrefix+url.Value);
			}

			else
			{
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

			if (progress.Value.Equals(1f))
			{
				storeText.Value = wwwObject.text;
				storeTexture.Value = wwwObject.texture;

				storeAudio.Value = wwwObject.audioClip;

                storeMovieTexture.Value = wwwObject.movie;

				errorString.Value = wwwObject.error;

				Fsm.Event(string.IsNullOrEmpty(errorString.Value) ? isDone : isError);

				Finish();
			}
		}
	}
}

#endif
