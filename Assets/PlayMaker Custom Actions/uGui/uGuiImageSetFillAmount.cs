// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __ACTION__ ---//
// Made By : DjayDino
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Set The Fill Amount on a uGui image")]
	public class uGuiImageSetFillAmount : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Image))]
		[Tooltip("The GameObject with the Image ui component.")]
		public FsmOwnerDefault gameObject;

		[HasFloatSlider(0,1)]
		public FsmFloat ImageFillAmount;
		
		public bool everyFrame;

		UnityEngine.UI.Image _image;

		public override void Reset()
		{
			gameObject = null;
			ImageFillAmount = 1;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_image = _go.GetComponent<UnityEngine.UI.Image>();
			}
			
			DoSetFillAmount();
		
			if (!everyFrame)
		    {
		        Finish();
		    }
		}
		
		public override void OnUpdate ()
		{
			DoSetFillAmount();
		}
		
		void DoSetFillAmount()
		{
			_image.fillAmount = ImageFillAmount.Value;
		}

		
	}
}