// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

#if !(UNITY_IPHONE || UNITY_ANDROID || UNITY_FLASH || UNITY_PS3)

using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Web")]
	[Tooltip("Gets data from a url and store it in variables, Accept Post variables and headers setup. See Unity WWW docs for more details.")]
	public class WWWPostHeaders : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Url to download data from.")]
		public FsmString url;

		[ActionSection("Header Data")]
		[CompoundArray("Header", "Key", "Value")]
		public FsmString[] headerKeys;
		public FsmString[] headerValues;

		[ActionSection("POST Data")]
		
		[CompoundArray("POST", "Key", "Value")]
		public FsmString[] postKeys;
		public FsmVar[] postValues;
		
		[ActionSection("Results")]

		[UIHint(UIHint.Variable)]
		[Tooltip("Gets text from the url.")]
		public FsmString storeText;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Gets a Texture from the url.")]
		public FsmTexture storeTexture;

        [UIHint(UIHint.Variable)]
		[ObjectType(typeof(MovieTexture))]
		[Tooltip("Gets a Texture from the url.")]
		public FsmObject storeMovieTexture;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Gets a audio from the url.")]
		[ObjectType(typeof(AudioClip))]
		public FsmObject storeAudio;

		[Tooltip("Audio setting: Is it 3d")]
		public FsmBool audio3d;
		[Tooltip("Audio setting: Is it a stream")]
		public FsmBool audioStream;
		[Tooltip("Audio setting: type")]
		public AudioType audioType;
		
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

		public override void Reset()
		{
			url = null;

			headerKeys = new FsmString[0];
			headerValues = new FsmString[0];

			postKeys = new FsmString[0];
			postValues = new FsmVar[0];
			
			storeText = null;
			storeTexture = null;
			
			storeAudio =null;
			audio3d = false;
			audioStream =false;
			audioType = AudioType.UNKNOWN;
			
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

			Dictionary<string,string> _headers = new Dictionary<string, string> ();


			if (headerKeys.Length > 0) {
				int h =0;
				foreach(FsmString _headerKey in headerKeys)
				{
					string _hkey = _headerKey.Value;

					_headers.Add(_headerKey.Value,headerValues[h].Value);
					h++;
				}
			}
			
			if (postKeys.Length>0)
			{
				WWWForm _wwwForm = new WWWForm();
				int i = 0;
				
				foreach(FsmString _Fsmkey in postKeys)
				{
					string _key = _Fsmkey.Value;
					
					switch (postValues[i].Type)
					{
						case VariableType.Material:
						case VariableType.Unknown:
						case VariableType.Object:
							//not supported;
						break;
					case VariableType.Texture:
						
						Texture2D rt = (Texture2D)postValues[i].textureValue;
						
						_wwwForm.AddBinaryData(_key,rt.EncodeToPNG());
						break;
						default:
						_wwwForm.AddField(_key,postValues[i].ToString());
						break;
					}
					
					
					i++;
				}

				if (_headers.Count>0)
				{
					wwwObject = new WWW(url.Value,null,_headers);
				}else{
					wwwObject = new WWW(url.Value,_wwwForm);
				}
				
			}else{
				if (_headers.Count>0)
				{
					wwwObject = new WWW(url.Value,null,_headers);
				}else{
					wwwObject = new WWW(url.Value);
				}
			}
		}


		public override void OnUpdate()
		{
			if (wwwObject == null)
			{
				errorString.Value = "WWW Object is Null!";
				Finish();
				Fsm.Event(isError);
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
				storeText.Value = wwwObject.text;
				storeTexture.Value = wwwObject.texture;

                storeMovieTexture.Value = wwwObject.movie;
				
				if (!storeAudio.IsNone)
				{
					storeAudio.Value = wwwObject.GetAudioClip(audio3d.Value,audioStream.Value,audioType);
				}
				
				
				errorString.Value = wwwObject.error;

				Fsm.Event(string.IsNullOrEmpty(errorString.Value) ? isDone : isError);

				Finish();
			}
		}
		
		public override string ErrorCheck ()
		{
			foreach(FsmVar _Fsmvar in postValues)
			{
				switch (_Fsmvar.Type)
				{
					case VariableType.Material:
					case VariableType.Unknown:
					case VariableType.Object:
					return _Fsmvar.Type+" not supported";
				}
			}
			return "";
		}

		
	}
}

#endif
