// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Sprite")]
	[Tooltip("Gets a Sprite from a url and store it in variables.")]
	public class WWWSprite : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Url to download Sprite from.")]
		public FsmString url;

		[Tooltip("Pivot of the Sprite. Default is 0,0.")]
		public FsmVector2 pivot;

		[Tooltip("Pixels to Unit of the Sprite. Default is 1.")]
		public FsmInt pixelsToUnit;

		[ActionSection("Results")]

        [UIHint(UIHint.Variable)]
		[ObjectType(typeof(Sprite))]
		[Tooltip("Gets a Sprite from the url.")]
		public FsmObject storeSprite;

		[UIHint(UIHint.Variable)]
		[Tooltip("Error message if there was an error during the download.")]
		public FsmString errorString;

		[UIHint(UIHint.Variable)] 
		[Tooltip("How far the download progressed (0-1).")]
		public FsmFloat progress;

		[ActionSection("Events")] 
		
		[Tooltip("Event to send when the sprite has finished loading (progress = 1).")]
		public FsmEvent isDone;

		[Tooltip("Event to send if there was an error.")]
		public FsmEvent isError;

		private WWW wwwObject;

		public override void Reset()
		{
			url = null;
			pivot = null;
			pixelsToUnit = 1;
			storeSprite = null;
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

			wwwObject = new WWW(url.Value);
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
			
			
				Texture2D _tex = wwwObject.texture;
				if (_tex!=null)
				{
					Rect rec = new Rect(0, 0, _tex.width, _tex.height);

					storeSprite.Value =	Sprite.Create(_tex,rec,pivot.Value,pixelsToUnit.IsNone?1:pixelsToUnit.Value);
					errorString.Value = wwwObject.error;
				}else{
					errorString.Value = "Data is not a Texture";
				}

				Fsm.Event(string.IsNullOrEmpty(errorString.Value) ? isDone : isError);

				Finish();
			}
		}
	}
}
