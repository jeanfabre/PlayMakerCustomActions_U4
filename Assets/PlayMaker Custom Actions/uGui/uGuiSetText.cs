// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;
using uUI = UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the text value of a UGui Text component.")]
	public class uGuiSetText : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(uUI.Text))]
		[Tooltip("The GameObject with the text ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The text of the UGui Text component.")]
		public FsmString text;

		public bool everyFrame;

		private uUI.Text _text;

		public override void Reset()
		{
			text = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_text = _go.GetComponent<uUI.Text>();
			}

			DoSetTextValue();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoSetTextValue();
		}
		
		void DoSetTextValue()
		{


			if (_text!=null)
			{
				_text.text = text.Value;
			}
		}
		
	}
}