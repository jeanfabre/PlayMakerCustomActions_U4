// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;
using uUI = UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the text value of a UGui Text component.")]
	public class uGuiGetText : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(uUI.Text))]
		[Tooltip("The GameObject with the text ui component.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The text value of the UGui Text component.")]
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
			
			DoGetTextValue();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoGetTextValue();
		}
		
		void DoGetTextValue()
		{
			if (_text!=null)
			{
				text.Value = _text.text;
			}
		}
		
	}
}