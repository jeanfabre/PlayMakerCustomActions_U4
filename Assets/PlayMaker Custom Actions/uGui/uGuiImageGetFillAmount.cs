// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __ACTION__ ---//
// Made By : DjayDino
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Set The Fill Amount on a uGui image")]
	public class uGuiImageGetFillAmount : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.Image))]
		[Tooltip("The GameObject with the Image ui component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		public FsmFloat ImageFillAmount;
		
		public bool everyFrame;

		UnityEngine.UI.Image _image;

		public override void Reset()
		{
			gameObject = null;
			ImageFillAmount = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_image = _go.GetComponent<UnityEngine.UI.Image>();
			}
			
			DoGetFillAmount();
		
			if (!everyFrame)
		    {
		        Finish();
		    }
		}
		
		public override void OnUpdate ()
		{
			DoGetFillAmount();
		}
		
		void DoGetFillAmount()
		{
			ImageFillAmount.Value =_image.fillAmount;
		}

		
	}
}